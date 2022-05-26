using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Random = System.Random;

namespace Assets.Models
{
    public class Genome
    {
		public double fitness;
		private Random random = new Random();
		public List<FurnitureGeneticLocation> locationList = new List<FurnitureGeneticLocation>();
		public List<Furniture> newOne;
		public int xcoordinate, ycoordinate;
		public List<Furniture> babyFurniture;
		private string RoomStructureLetter = "D";

		public List<int> xcoordinatebaby;
		public List<int> ycoordinatebaby;
		public List<Furniture> furnitureListGenom;
		public int x,y ;
		public string[,] floorSecond;
		public static int counter;
		public string fileName = "D:\\design";
		public double populationFitnessScore;//its not for just one furniture, it is for all furniture of population

		public Genome()
		{
			xcoordinatebaby = new List<int>();
			ycoordinatebaby = new List<int>();
			newOne = new List<Furniture>();
			furnitureListGenom=new List<Furniture>();
		}

		public string[,] returnStructurePlan(int coordinate1, int coordinate2, string[,] floorPlan, List<RoomStructure> roomStructureList, List<Wall> wallList)
		{
			int redDotDistance;
			int roomStructureWidth;

			for (int j = 0; j < roomStructureList.Count; j++)
			{
				for (int k = 0; k < wallList.Count; k++)
				{
					if (roomStructureList[j].WallID == wallList[k].WallID)
					{
						if (roomStructureList[j].FurnitureTypeID == 44) //Door
						{
							RoomStructureLetter = "D";
						}
						else if (roomStructureList[j].FurnitureTypeID == 45) //Window
						{
							RoomStructureLetter = "W";
						}

						//Setting Room strucutr letters on matrix.
						if (wallList[k].WallName == "W1")
						{
							redDotDistance = (int)(roomStructureList[j].RedDotDistance * 100);
							roomStructureWidth = (int)(roomStructureList[j].StrructureWidth * 100);


							for (int a = redDotDistance; a < redDotDistance + roomStructureWidth; a++)
							{

								for (int b = coordinate1 - 1; b > coordinate1 - 61; b--)
								{
									floorPlan[b, a] = RoomStructureLetter;
								}

							}

						}
						else if (wallList[k].WallName == "W2")
						{
							redDotDistance = (int)(roomStructureList[j].RedDotDistance * 100);
							roomStructureWidth = (int)(roomStructureList[j].StrructureWidth * 100);
							for (int a = coordinate1 - redDotDistance - roomStructureWidth; a < coordinate1 - redDotDistance; a++)
							{
								for (int b = coordinate2 - 1; b > coordinate2 - 61; b--)
								{
									floorPlan[a, b] = RoomStructureLetter;
								}

							}

						}
						else if (wallList[k].WallName == "W3")
						{
							redDotDistance = (int)(roomStructureList[j].RedDotDistance * 100);
							roomStructureWidth = (int)(roomStructureList[j].StrructureWidth * 100);
							for (int a = coordinate2 - redDotDistance - roomStructureWidth; a < coordinate2 - redDotDistance; a++)
							{
								for (int b = 0; b < 60; b++)
								{
									floorPlan[b, a] = RoomStructureLetter;
								}

							}
						}
						else if (wallList[k].WallName == "W4")
						{
							redDotDistance = (int)(roomStructureList[j].RedDotDistance * 100);
							roomStructureWidth = (int)(roomStructureList[j].StrructureWidth * 100);
							for (int a = redDotDistance; a < redDotDistance + roomStructureWidth; a++)
							{
								for (int b = 0; b < 60; b++)
								{
									floorPlan[a, b] = RoomStructureLetter;
								}

							}
						}

					}
				}
			}
			return floorPlan;

		}
		// we need to create random matrix 
		public List<FurnitureGeneticLocation> GenomeInit(int coordinate1, int coordinate2, string[,] floorPlan, List<RoomStructure> roomStructureList, List<Furniture> furnitureList, List<Wall> wallList)
		{
			floorPlan = returnStructurePlan(coordinate1, coordinate2, floorPlan, roomStructureList, wallList);
			x = coordinate1;
			y = coordinate2;
			floorSecond= floorPlan;

			furnitureListGenom.AddRange(furnitureList);

			var canBePlaced=0;
			int xcoordinate, ycoordinate;
			int howManyCellsX, howManyCellsY;
			int i = 0;
			int startPosX = 0, finishPosX = 0, startPosY = 0, finishPosY = 0;

			//creating a new empty design

			for (int k = 0; k < coordinate1; k++)
			{
				for (int j = 0; j < coordinate2; j++)
				{
					floorPlan[k, j] = "T";
				}
			}

			int capacityminusone = (int)furnitureList.Capacity;
			// the default capacity of a list is fixed at 4, so if you get error about size, please consider it
			while (i < capacityminusone)
			{
				
				startPosX = 0;
				finishPosX = 0;
				startPosY = 0;
				finishPosY = 0;
				canBePlaced = 0;
				// genenating random positions for 
				xcoordinate = random.Next(0, coordinate1);
				ycoordinate = random.Next(0, coordinate2);


				howManyCellsX = (int)furnitureList[i].Xdimension;
				howManyCellsY = (int)furnitureList[i].Zdimension;


				// checking whether the furniture can fit into the random generated positions
				if (howManyCellsX + xcoordinate + 7 < coordinate1 && howManyCellsY + ycoordinate < coordinate2)
				{
					startPosX = xcoordinate;
					finishPosX = xcoordinate + howManyCellsX;
					startPosY = ycoordinate;
					finishPosY = ycoordinate + howManyCellsY;

					// now, I will check whether the selected cells are empty or not

					for (int j = startPosX; j <= finishPosX; j++)
					{
						for (int k = startPosY; k <= finishPosY; k++)
						{
							// if the position is not empty, then quit the loop
							if (floorPlan[j,k] != "T")
							{
								if(floorPlan[j,k] == "D")
                                {
									canBePlaced = 1; // cannot be placed
									break;
								}
							}
						}

						// if the position is not empty, then quit the loop
						if (canBePlaced == 1)
						{
							break;
						}
					}
					// if the position is not empty, then the random position generation will happen again
					if (canBePlaced == 0)
					{
						// replace the furniture id into array if positions are empty
						for (int j = startPosX; j < finishPosX; j++)
						{
							for (int k = startPosY; k < finishPosY; k++)
							{
								floorPlan[j, k] = "" + furnitureList[i].FurnitureID.ToString();
							}
						}
						// after the id is written, now we need to define the front part of the object, I will put 'X' value to define the front part
						for (int j = startPosY; j < finishPosY; j++)
						{
							floorPlan[finishPosX, j] = "X";
						}
						// after the X values are written, then "Y" values are going to be replaced to leave an empty space for each furniture
						// for now, every object will have 30cm space in front of them
						for (int k = finishPosX + 1; k < finishPosX + 6; k++)
						{
							for (int j = startPosY; j < finishPosY; j++)
								floorPlan[k, j] = "Y";
						}
						int centerX = (startPosX + finishPosX) / 2;
						int centerY = (startPosY + finishPosY) / 2;
						GeneticAlgorithm genetic = new GeneticAlgorithm();
						//double score = genetic.distanceFromWalls(startPosX, startPosY, finishPosX, finishPosY, coordinate1, coordinate2);
						
						//string wallName = genetic.wallNameUpdate(startPosX, startPosY, finishPosX, finishPosY, coordinate1, coordinate2);
						/*locationList.Add(new FurnitureGeneticLocation() { FurnitureID = furnitureList[i].FurnitureID, StartX = startPosX, FinishX = finishPosX,
							CenterX = centerX, StartY = startPosY, CenterY = centerY, FinishY = finishPosY ,
							FitnessScore = score, Degree = 0, WallName = wallName
						});*/

						locationList.Add(new FurnitureGeneticLocation() { FurnitureID = furnitureList[i].FurnitureID, StartX = startPosX, FinishX = finishPosX,
							CenterX = centerX, StartY = startPosY, CenterY = centerY, FinishY = finishPosY ,
							Degree = 0
						});
						i++;
					}
				}
				
			}
			
			string bastir = "";
            for (int k = 0; k < x; k++)
            {
                for (int j = 0; j < y; j++)
                {
                    bastir += floorPlan[k, j];

                }
                bastir += "\n";
            }
            //Debug.Log(bastir);

            Debug.Log("Inserted");
			
			return locationList;
	
		}
	}
}
