using System;
using System.Collections.Generic;

namespace Assets.Models
{
    public partial class Furniture
    {
        public Furniture()
        {
            Design = new HashSet<Design>();
        }

        public int FurnitureId { get; set; }
        public int FurnitureTypeId { get; set; }
        public double FurnitureXdimension { get; set; }
        public double FurnitureYdimension { get; set; }
        public double FurnitureZdimension { get; set; }
        public double FurnitureXlocation { get; set; }
        public double FurnitureYlocation { get; set; }
        public double FurnitureZlocation { get; set; }

        public virtual FurnitureType FurnitureType { get; set; }
        public virtual ICollection<Design> Design { get; set; }
    }
}
