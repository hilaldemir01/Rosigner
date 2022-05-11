using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Assets.Models
{
    public class Genome
    {
		public List<int> bits;
		public double fitness;
		private Random random = new Random();

		public Genome()
		{
			Initialize();
		}


		// we need to create random matrix 
		//	public Genome(List<Furniture> furnitureList,int coordinate1, int coordinate2, string[][] floorPlan)
		public void  GenomeInit(int coordinate1, int coordinate2, string[,] floorPlan)
		{
			//	Initialize();
			List<Furniture> newOne = new List<Furniture>();
			newOne.Capacity = 0;
			newOne.Add(new Furniture() { Xdimension = 50.0f, Ydimension = 40.0f, FurnitureID = 5 });
			newOne.Add(new Furniture() { Xdimension = 20.0f, Ydimension = 30.0f, FurnitureID = 7 });
			newOne.Add(new Furniture() { Xdimension = 10.0f, Ydimension = 30.0f, FurnitureID = 8 });

			var canBePlaced = 0;
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

			// the default capacity of a list is fixed at 4, so if you get error about size, please consider it
			while (i < newOne.Capacity - 1)
			{
				// genenating random positions for 
				xcoordinate = random.Next(0, coordinate1);
				ycoordinate = random.Next(0, coordinate2);

				Debug.Log("random X Y:" + xcoordinate + " , " + ycoordinate);

				howManyCellsX = (int)newOne[i].Xdimension / 10;
				howManyCellsY = (int)newOne[i].Ydimension / 10;

				Debug.Log("howManyCells X Y:" + howManyCellsX + " , "+ howManyCellsY);

				// checking whether the furniture can fit into the random generated positions
				if (howManyCellsX + xcoordinate < coordinate1 && howManyCellsY + ycoordinate < coordinate2)
				{
					Debug.Log("İlk if");
					startPosX = xcoordinate;
					finishPosX = xcoordinate + howManyCellsX;

					startPosY = ycoordinate;
					finishPosY = ycoordinate + howManyCellsY;

					Debug.Log("positions: x s"+ startPosX + " x f"+ finishPosX + " y s" + startPosY + "y f:" + finishPosY);
					// now, I will check whether the selected cells are empty or not

					for (int j = startPosX; j <= finishPosX; j++)
					{
						for (int k = startPosY; k <= finishPosY; k++)
						{
							// if the position is not empty, then quit the loop
							if (floorPlan[j,k] != "")
							{
								canBePlaced = 1;
								Console.WriteLine("Dolu mu");
								break;
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
				if (canBePlaced == 0)
				{
					Debug.Log("canBePlaced if ine girdi mi");
					// replace the furniture id into array if positions are empty
					for (int j = startPosX; j < finishPosX; j++)
					{
						for (int k = startPosY; k < finishPosY; k++)
						{
							floorPlan[j, k] = "" + newOne[i].FurnitureID.ToString();
						}
					}
					// after the id is written, now we need to define the front part of the object, I will put 'X' value to define the front part
					for (int j = startPosY; j < finishPosY; j++)
					{
						floorPlan[finishPosX,j] = "X";
					}
					// after the X values are written, then "Y" values are going to be replaced to leave an empty space for each furniture
					// for now, every object will have 30cm space in front of them
					for (int k = finishPosX + 1; k < finishPosX + 4; k++)
					{
						for (int j = startPosY; j < finishPosY; j++)
							floorPlan[k,j] = "Y";
					}
					i++;
				}
			}
			string bastir = "";
			for (int k = 0; k < coordinate1; k++)
			{
				for(int j = 0; j < coordinate2; j++)
                {
					bastir += floorPlan[k,j];

				}
				bastir += "\n";
			}
			Debug.Log(bastir);

		}

		private void Initialize()
		{
			fitness = 0;
			bits = new List<int>();
		}
	}
}
