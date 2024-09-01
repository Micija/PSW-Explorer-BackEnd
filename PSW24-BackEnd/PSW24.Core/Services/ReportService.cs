using Microsoft.Extensions.Logging;
using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using Quartz;
//---
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using FluentResults;
using PSW24.BuildingBlocks.Core.UseCases;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;


namespace PSW24.Core.Services
{

    public class ReportService : IJob
    {
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<ReportService> _logger;
    

        public ReportService(ILogger<ReportService> logger, ICartRepository cartRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            if (DateTime.UtcNow.Day != 1) return Task.CompletedTask;
            _logger.LogInformation("{UtcNow}", DateTime.UtcNow);
            FindBestSeller();
            MakeReport();
            SendEmail();

            return Task.CompletedTask;
        }

        private bool CheckLast3Month(Tour tour, List<Cart> carts)
        {
            int currentMonth = DateTime.UtcNow.Month;

            return carts.FirstOrDefault(c => c.TourId == tour.Id && c.Date?.Month == currentMonth) != null || carts.FirstOrDefault(c => c.TourId == tour.Id && c.Date?.Month == (currentMonth - 1)) == null || carts.FirstOrDefault(c => c.TourId == tour.Id && c.Date?.Month == (currentMonth - 2)) == null;
        }

        private void SendEmail()
        {
            string tours = "";
            foreach (User user in _userRepository.GetAllAuthor())
            {
                var carts = _cartRepository.GetSoldTour(user.Id).FindAll(c => DateTime.UtcNow.Month == c.Date?.Month);
                foreach(Tour tour in user.Tours)
                {
                    if (CheckLast3Month(tour, carts)) tours += tour.Name + ",";
                }


                    SmtpClient client = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("markoandjelicpsw@gmail.com", "yasg svva qiep ddfo"),
                        EnableSsl = true,
                    };

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress("markoandjelicpsw@gmail.com"),
                        To = { user.Email },
                        Subject = "Preporuka za brisanje tura",
                        Body = "Ove ture se nisu prodale poslednja 3 meseca: " + tours,
                        IsBodyHtml = true
                    };

                    try
                    {
                        client.Send(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Failed to send email: {ex.Message}");
                    }
                    finally
                    {
                        mailMessage.Dispose();
                        client.Dispose();
                    }
            }

           
        }

        private void FindBestSeller()
        {
            User maxUser = null;
            int max = 0;
            foreach(User user in _userRepository.GetAllAuthor())
            {
                var tours = _cartRepository.GetSoldTour(user.Id).FindAll(c => DateTime.UtcNow.Month == c.Date?.Month).Count;
                if(max < tours)
                {
                    maxUser = user;
                    max = tours;
                }
            }
            if (maxUser != null)
            {
                maxUser.IncPoints();
                _userRepository.Save();
            }
            
        }

        private double CountEarning(List<Cart> carts)
        {
            double earn = 0;
            foreach(Cart cart in carts)
            {
                earn += cart.Tour.Price;
            }
            return earn; 
        }

        private Tour MostSold(List<Cart> carts)
        {
            Tour mostSold = null;
            int count = 0;

            foreach(Cart cart in carts)
            {
                int tmp = carts.FindAll(c => c.TourId == cart.TourId).Count;             
                if(tmp > count)
                {
                    count = tmp;
                    mostSold = cart.Tour;
                }
            }

            return mostSold;
        }

        private List<Tour> NotSold(User user, List<Cart> carts)
        {
            List<Tour> tours = new();

            foreach(Tour tour in user.Tours)
            {
                if(carts.FirstOrDefault(c => c.TourId == tour.Id) == null) tours.Add(tour);
            }

            return tours;
        }

        private void MakeReport()
        {
            foreach (User user in _userRepository.GetAllAuthor())
            {

                var tours = _cartRepository.GetSoldTour(user.Id).FindAll(c => DateTime.UtcNow.Month == c.Date?.Month);
                int soldNumber = tours.Count();
                double earned = CountEarning(tours);


                int lastMonth = DateTime.UtcNow.Month - 1;
                var lastMonthSolds = _cartRepository.GetSoldTour(user.Id).FindAll(c => lastMonth == c.Date?.Month).Count;

                double percentage = lastMonthSolds / soldNumber * 100;
                Tour mostSold = MostSold(tours);

                List<Tour> notSold = NotSold(user, tours);
                try
                {
                    // Definišemo putanju i proveravamo da li direktorijum postoji

                    string filePath = $"..\\\\PSW24-BackEnd\\\\Resources\\\\Pdfs\\\\MonthlyReport_{user.Username}_{DateTime.UtcNow:yyyyMMdd}.pdf";

                    using (var document = new PdfDocument())
                    {
                        // Kreiranje stranice
                        var page = document.AddPage();
                        var gfx = XGraphics.FromPdfPage(page);
                        var font = new XFont("Verdana", 12, XFontStyle.Regular);

                        // Dodavanje naslova
                        gfx.DrawString("Monthly Sales Report", new XFont("Verdana", 18, XFontStyle.Bold), XBrushes.Black, new XRect(0, 40, page.Width, page.Height), XStringFormats.Center);

                        // Dodavanje separatora
                        gfx.DrawString(new string('-', 21), font, XBrushes.Black, new XRect(0, 80, page.Width, page.Height), XStringFormats.Center);

                        // Dodavanje informacija o prodaji
                        gfx.DrawString($"Number of tours sold this month: {soldNumber}", font, XBrushes.Black, new XRect(50, 120, page.Width, page.Height), XStringFormats.TopLeft);
                        gfx.DrawString($"Total earnings this month: {earned:C}", font, XBrushes.Black, new XRect(50, 140, page.Width, page.Height), XStringFormats.TopLeft);
                        gfx.DrawString($"Percentage change in sales compared to last month: {percentage:F2}%", font, XBrushes.Black, new XRect(50, 160, page.Width, page.Height), XStringFormats.TopLeft);

                        // Dodavanje najprodavanije ture
                        if (mostSold != null)
                        {
                            gfx.DrawString($"Most sold tour this month: {mostSold.Name}", font, XBrushes.Black, new XRect(50, 200, page.Width, page.Height), XStringFormats.TopLeft);
                        }
                        else
                        {
                            gfx.DrawString("No tours were sold this month.", font, XBrushes.Black, new XRect(50, 200, page.Width, page.Height), XStringFormats.TopLeft);
                        }

                        // Dodavanje tura koje nisu prodate
                        if (notSold.Any())
                        {
                            gfx.DrawString("Tours not sold this month:", font, XBrushes.Black, new XRect(50, 240, page.Width, page.Height), XStringFormats.TopLeft);
                            int yPosition = 260;
                            foreach (var tour in notSold)
                            {
                                gfx.DrawString($"- {tour.Name}", font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
                                yPosition += 20;
                            }
                        }
                        else
                        {
                            gfx.DrawString("All tours were sold this month.", font, XBrushes.Black, new XRect(50, 240, page.Width, page.Height), XStringFormats.TopLeft);
                        }

                        // Spremanje dokumenta
                        document.Save(filePath);
                    }

                    Console.WriteLine("PDF report generated successfully.");

                }
                catch (Exception ex)
                {
                            Console.WriteLine($"An error occurred while generating the PDF: {ex.Message}");
                }
            }
        }

    }
}
