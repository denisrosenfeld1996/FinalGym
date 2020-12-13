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
        [RegularExpression("[0-9]{9}")]
        public int LessonID { get; set; }
        [RegularExpression("[0-9]{9}")]
        public int TrainerID { get; set; }
        [RegularExpression("[0-9]{9}")]
        public int StudioClassID { get; set; }
        //Join
        public int MemberID { get; set; }
        //Join
        [RegularExpression("[0-9]{2}")]
        public int Capacity { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(10)]
        public string LessonDay { get; set; }
        [DataType(DataType.Time)]
        public DateTime LessonTime { get; set; }
        public Trainers Trainer { get; set; }
        public StudioClass StudioClass { get; set; }
        //Join
        public Members Members { get; set; }
    }
}
