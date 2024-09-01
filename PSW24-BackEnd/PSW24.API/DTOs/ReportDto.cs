using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.API.DTOs
{
    public class ReportDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Path { get; set; }
    }
}
