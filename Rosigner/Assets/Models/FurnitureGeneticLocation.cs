using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class FurnitureGeneticLocation
    {
        public int FurnitureID { get; set; }
        public int StartX { get; set; }
        public int FinishX { get; set; }
        public int CenterX { get; set; }
        public int StartY { get; set; }
        public int FinishY { get; set; }
        public int CenterY { get; set; }
        public int Fitness { get; set; }
    }
}
