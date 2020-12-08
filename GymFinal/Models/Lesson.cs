using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymFinal.Models
{
    public class Lesson
    {
        public int LessonID { get; set; }
        public int TrainerID { get; set; }
        public int StudioClassID { get; set; }
        //Join
        public int MemberID { get; set; }
        //Join
        public int Capacity { get; set; }
        public string LessonDay { get; set; }
        [DataType(DataType.Time)]
        public DateTime LessonTime { get; set; }
        public Trainers Trainer { get; set; }
        public StudioClass StudioClass { get; set; }
        //Join
        public Members Members { get; set; }
    }
}
