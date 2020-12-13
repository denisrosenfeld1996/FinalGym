//Graph
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymFinal.Models
{
    public class StatsViewModel
    {
        public int id { get; set; }
        public int UsersCount { get; set; }
        public int TrainersCount { get; set; }
        public ICollection<StudioClass> Classes { get; set; }
        public int types { get; set; }
        public int ClassesCount { get; set; }
    }
}

//denis
