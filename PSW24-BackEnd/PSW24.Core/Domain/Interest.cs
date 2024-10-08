﻿using PSW24.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain
{
    public class Interest : Entity
    {
        public string Type { get; private set; }    
        public List<UserInterest> Users { get; private set; }
        public List<Tour> Tours { get; private set; }
        public Interest(string type) {
            Type = type;
            Users = new();
            Tours = new();
        }

        public void AddTour(Tour tour)
        {
            this.Tours.Add(tour);
        }
    }
}
