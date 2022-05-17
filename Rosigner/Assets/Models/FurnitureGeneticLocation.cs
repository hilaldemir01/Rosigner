using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class FurnitureGeneticLocation
    {
        public int GeneticLocationID { get; set; }
        public int FurnitureID { get; set; }
        public int StartX { get; set; }
        public int FinishX { get; set; }
        public int CenterX { get; set; }
        public int StartY { get; set; }
        public int FinishY { get; set; }
        public int CenterY { get; set; }
        public int FitnessScore { get; set; }
        public string WallName { get; set; }
        public string XPositionStart { get; set; }
        public string XPositionFinish { get; set; }
        public string YPositionStart { get; set; }
        public string YPositionFinish { get; set; }
    }
}
