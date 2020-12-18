


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading.Tasks;


namespace GymFinal.Models
{
    public class Trainers
    {
        [Display(Name = "Trainer Id")]
        [RegularExpression("[0-9]{9}")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int TrainersID { get; set; }

        [Display(Name = "Trainer Name")]
        [RegularExpression(@"[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(30)]
        public string TrainerName { get; set; }

        [Required]
        public int Seniority { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(10, MinimumLength = 10)]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Lesson> Lessons { get; set; }


    }
}
