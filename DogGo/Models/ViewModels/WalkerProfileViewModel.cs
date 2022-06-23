using System;
using System.Collections.Generic;
using System.Linq;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }
        public List<Walks> Walks { get; set; }
        public string TotalWalkTIme
        {
            get
            {
                int totalMinutes = Walks.Select(w => w.Duration).Sum() / 60;
                int totalHours = totalMinutes / 60;
                int remainingMin = totalMinutes % 60;
                return $"{totalHours} hrs {remainingMin} min";
            }
        }

    }
}
