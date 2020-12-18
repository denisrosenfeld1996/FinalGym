
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
        [Required]
        public int LessonID { get; set; }

        [Required]
        public int TrainerID { get; set; }

        [Required]
        public int StudioClassID { get; set; }

        public int MemberID { get; set; }
        [Required]

        public int Capacity { get; set; }

        [Display(Name = "Lesson Day")]
        [RegularExpression(@"[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(10)]
        public string LessonDay { get; set; }

        [Display(Name = "Lesson Time")]
        [Required]
        [DataType(DataType.Time)]
        public DateTime LessonTime { get; set; }

        public Trainers Trainer { get; set; }
        public StudioClass StudioClass { get; set; }
        public Members Members { get; set; }

    }
}
