using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymFinal.Models
{
    //public enum Intensive
    //{
    //    Easy,Medium,Hard
    //}
    public class StudioClass
    {
        [RegularExpression("[0-9]{9}")]
        public int ID { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(30)]
        public string ClassName { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(30)]
        public string Type { get; set; }
        //GroupBy
        public int TypeID { get; set; }
        //GroupBy
        [RegularExpression("[0-9]{2}")]
        public int DuringTime { get; set; }
        //Odeya&maya
        [RegularExpression("[0-9]{4}")]
        public int BurnCalories { get; set; }
        //Odeya&maya
        //Join-14.11
        public Trainers trainers { get; set; }
        ////Join
        public ICollection<Lesson> Lessons { get; set; }


        //denis
        

    }
}
