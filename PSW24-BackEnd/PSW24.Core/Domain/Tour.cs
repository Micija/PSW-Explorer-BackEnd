using PSW24.BuildingBlocks.Core.Domain;
using PSW24.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain
{
    public class Tour : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public long InterestId { get; private set;}
        public Interest Interest { get; private set; }
        public double Price { get; private set; }
        public TourStatus Status { get; private set; }
        public long AuthorId { get; private set; }
        public User Author { get; private set; }
        
        public ICollection<KeyPoint> KeyPoints { get; } = [];
        public Tour(string name, Difficulty difficulty, long interestId, double price, TourStatus status, long authorId)
        {
            Name = name;
            Difficulty = difficulty;
            InterestId = interestId;
            Price = price;
            Status = status;
            Validate();
            AuthorId = authorId;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Username");
            if (Price <= 0) throw new ArgumentException("Invalid Price");
        }

        public void Draft()
        {
            Status = TourStatus.DRAFT;
        }

        public void SetInterest(Interest interest)
        {
            Interest = interest;
        }

        public void Publish()
        {
            if(KeyPoints.Count < 2) throw new ArgumentException("Insufficient key points");
            Status = TourStatus.PUBLISHED;
        }

        public void Archive()
        {
            Status = TourStatus.ARCHIVED;    
        }
    }
}
