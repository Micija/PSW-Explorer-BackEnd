using PSW24.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain
{
    public class KeyPoint : Entity
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string Image { get; private set; }
        public long TourId { get; private set; }
        public Tour Tour { get; private set; }

        public KeyPoint(double latitude, double longitude, string name, string? description, string image, long tourId)
        {
            Latitude = latitude;
            Longitude = longitude;
            Name = name;
            Description = description;
            Image = image;
            TourId = tourId;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid name");
            if (Latitude is > 90 or < -90) throw new ArgumentException("Invalid latitude");
            if (Longitude is > 180 or < -180) throw new ArgumentException("Invalid longitude");
        }
    }
}
