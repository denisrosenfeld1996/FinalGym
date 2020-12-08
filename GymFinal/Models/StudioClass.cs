using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
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
        public int ID { get; set; }
        public string ClassName { get; set; }
        public string Type { get; set; }
        //GroupBy
        public int TypeID { get; set; }
        //GroupBy
        public int DuringTime { get; set; }
        //Odeya&maya
        public int BurnCalories { get; set; }
        //Odeya&maya
        //Join-14.11
        public Trainers trainers { get; set; }
        ////Join
        public ICollection<Lesson> Lessons { get; set; }
    }
}
