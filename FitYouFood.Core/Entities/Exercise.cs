using FitYouFood.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitYouFood.Core.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Target Target { get; set; }
        public string Description { get; set; }
        public string VisualisationUrl { get; set; }
        //TODO: change rating to double (it is the final rating of the exercise)
        public int Rating { get; set; }
        public bool IsOfficial { get; set; }
        public bool IsDeleted { get; set; }
    }
}
