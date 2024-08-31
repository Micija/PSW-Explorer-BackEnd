using FluentResults;
using PSW24.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.API.Public
{
    public interface ITourService
    {
        Result<List<TourDto>> GetAll();
        Result<TourDto> Create(TourDto dto);
        Result<List<TourDto>> GetForUser(long loggedUserId);
        Result<TourDto> Publish(long tourId);
        Result<List<TourDto>> GetPublish();
        Result<List<TourDto>> GetAuthor(long authorId);
        Result<TourDto> Archive(long tourId);
        Result<List<TourDto>> GetCartTour(long customerId);

        TourDto GetTourById(long id);

        Result<List<TourDto>> GetRecommendations(long userId, string difficulty);

        
    }
}
