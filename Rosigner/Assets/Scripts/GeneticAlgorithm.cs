using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class GeneticAlgorithm
    {
        List<Wall> wallList = new List<Wall>();
        List<RoomStructure> roomStructuresList = new List<RoomStructure>();
        RoomStructureLocation roomStructureLocation = new RoomStructureLocation();
        int[,] floorPlan;
        public void startMatrix()
        {
            int coordinate1 = (int)wallList[0].WallLength;
            int coordinate2 = (int)wallList[1].WallLength;
            floorPlan = new int[coordinate1,coordinate2];
        }
    }
}
