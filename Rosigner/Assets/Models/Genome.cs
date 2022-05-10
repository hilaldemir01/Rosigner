using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Models
{
    public class Genome
    {
		public List<int> bits;
		public double fitness;
		private static System.Random random;

		public Genome()
		{
			Initialize();
		}
		// we need to create random matrix 
		public Genome(List<Furniture> furnitureList,int coordinate1, int coordinate2, string[][] floorPlan)
		{
			Initialize();
			var canBePlaced = 0;
			int xcoordinate, ycoordinate;
			int howManyCellsX, howManyCellsY;
			int i = 0;
            int startPosX = 0, finishPosX = 0, startPosY = 0, finishPosY = 0;
			while (i < furnitureList.Count)
			{
				// genenating random positions for 
				xcoordinate = random.Next(0, coordinate1);
				ycoordinate = random.Next(0, coordinate2);

				howManyCellsX = (int)furnitureList[i].Xdimension / 10;
				howManyCellsY = (int)furnitureList[i].Ydimension / 10;
					
				// checking whether the furniture can fit into the random generated positions
				if(howManyCellsX + xcoordinate < coordinate1 && howManyCellsY + ycoordinate < coordinate2)
                {
					 startPosX = xcoordinate;
					 finishPosX = xcoordinate + howManyCellsX;

					 startPosY = ycoordinate;
					 finishPosY = ycoordinate + howManyCellsY;

					// now, I will check whether the selected cells are empty or not

					for (int j = startPosX ; j <= finishPosX ; j++)
					{
						for(int k = startPosY ; k <= finishPosY ; k++)
                        {
							// if the position is not empty, then quit the loop
                            if (floorPlan[j][k] != "")
                            {
								canBePlaced = 1;
								break ;
                            }
                        }

						// if the position is not empty, then quit the loop
						if (canBePlaced == 1)
                        {
							break;
                        }
					}
				}
				// if the position is not empty, then the random position generation will happen again
				if(canBePlaced == 0)
                {
					// replace the furniture id into array if positions are empty
					for(int j = startPosX; j < finishPosX; j++)
                    {
						for(int k = startPosY; k < finishPosY; k++)
                        {
							floorPlan[j][k] = ""+furnitureList[i].FurnitureID;
                        }
                    }
					// after the id is written, now we need to define the front part of the object, I will put 'X' value to define the front part
					for(int j = startPosY ; j < finishPosY; j++)
                    {
						floorPlan[finishPosX + 1][j] = "X";
                    }
					// after the X values are written, then "Y" values are going to be replaced to leave an empty space for each furniture
					// for now, every object will have 30cm space in front of them
					for (int j = startPosY; j < finishPosY; j++)
					{
						for(int k = finishPosX + 2; k < 3; k++ )
						floorPlan[k][j] = "Y";
					}
					i++;
				}
			}
		}

		private void Initialize()
		{
			fitness = 0;
			bits = new List<int>();
		}
	}
}
