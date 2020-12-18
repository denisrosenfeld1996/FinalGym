
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymFinal.Models
{

    public class StudioClass
    {
        [Display(Name = "Studio Class Id")]
        [Required]
        public int ID { get; set; }

        [Display(Name = "Class Name")]
        [RegularExpression(@"[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(30)]
        public string ClassName { get; set; }

        [RegularExpression(@"[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(30)]
        public string Type { get; set; }

        [Display(Name = "During Time")]
        [Required]
        public int DuringTime { get; set; }

        [Display(Name = "Burn Calories")]
        [Required]
        public int BurnCalories { get; set; }

        public Trainers trainers { get; set; }
        public ICollection<Lesson> Lessons { get; set; }

    }
}
