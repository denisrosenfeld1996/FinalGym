using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading.Tasks;


namespace GymFinal.Models
{
    public class Trainers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TrainersID { get; set; }
        public string TrainerName { get; set; }
        public int Seniority { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Lesson> Lessons { get; set; }


    }
}
