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
        [RegularExpression("[0-9]{9}")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TrainersID { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(30)]
        public string TrainerName { get; set; }
        [RegularExpression("[0-9]{2}")]
        public int Seniority { get; set; }
        [RegularExpression("[0-9]{10}")]
        public int PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Lesson> Lessons { get; set; }


    }
}
