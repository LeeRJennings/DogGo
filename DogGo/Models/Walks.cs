using System;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walks
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int WalkerId { get; set; }
        public int DogId { get; set; }
        public Dog Dog { get; set; }
        public Owner Owner { get; set; }
        public string TotalWalkTime
        {
            get
            {
                int numMin = Duration / 60;
                int numHours = numMin / 60;
                int remainingMin = numMin % 60;
                return $"{numHours} hr {remainingMin} min";
            }
        }
    }
}
