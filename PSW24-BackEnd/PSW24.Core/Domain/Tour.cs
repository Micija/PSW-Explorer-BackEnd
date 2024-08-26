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
        public Difficulty Difficulty { get; private set; }
        public Interest Interest { get; private set; }
        public double Price { get; private set; }
        public TourStatus Status { get; private set; }
        public long AuthorId { get; private set; }
        public User Author { get; private set; }

        public Tour(string name, Difficulty difficulty, Interest interest, double price, TourStatus status, long authorId)
        {
            Name = name;
            Difficulty = difficulty;
            Interest = interest;
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
    }
}
