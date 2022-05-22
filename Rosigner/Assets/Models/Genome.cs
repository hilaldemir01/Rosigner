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
		public List<int> bits;
		public double fitness;
		private Random random = new Random();
		public List<FurnitureGeneticLocation> locationList = new List<FurnitureGeneticLocation>();
		RosignerContext db = new RosignerContext();
		public List<Furniture> newOne;
		public int xcoordinate, ycoordinate;
		public List<Furniture> babyFurniture;

		public List<int> xcoordinatebaby;
		public List<int> ycoordinatebaby;
		public List<Furniture> furnitureListGenom;
		public int x,y ;
		public string[,] floorSecond;
		public static int counter;
		public string fileName = "D:\\design";
		public Genome()
		{
			Initialize();
			xcoordinatebaby = new List<int>();
			ycoordinatebaby = new List<int>();
			newOne = new List<Furniture>();
			furnitureListGenom=new List<Furniture>();

		}


		// we need to create random matrix 
		public List<FurnitureGeneticLocation> GenomeInit(int coordinate1, int coordinate2, string[,] floorPlan, List<RoomStructure> roomStructureList,List<Furniture> furnitureList)
		{
			x = coordinate1;
			y = coordinate2;
			floorSecond= floorPlan;
			//	Initialize();
			// this part is to test:
			//List<Furniture> newOne = new List<Furniture>();
			//newOne.Capacity = 0;
			//newOne.Add(new Furniture() { Xdimension = 50.0f, Ydimension = 40.0f, FurnitureID = 5 });
			//newOne.Add(new Furniture() { Xdimension = 20.0f, Ydimension = 30.0f, FurnitureID = 7 });
			//newOne.Add(new Furniture() { Xdimension = 10.0f, Ydimension = 30.0f, FurnitureID = 8 });
			//newOne.AddRange(furnitureList);
			Debug.Log("count:"+furnitureList.Count);
			furnitureListGenom.AddRange(furnitureList);
			Debug.Log("counget:"+furnitureListGenom.Count);

			Debug.Log("NewOne:"+newOne);
			
			var canBePlaced=0;
			int xcoordinate, ycoordinate;
			int howManyCellsX, howManyCellsY;
			int i = 0;
			int startPosX , finishPosX , startPosY, finishPosY ;

			//creating a new empty design

			for (int k = 0; k < coordinate1; k++)
			{
				for (int j = 0; j < coordinate2; j++)
				{
					floorPlan[k, j] = "T";
				}
			}
			Debug.Log("CAPACITY"+furnitureList.Capacity);
			int capacityminusone = (int)furnitureList.Capacity -1;
			// the default capacity of a list is fixed at 4, so if you get error about size, please consider it
			while (i < capacityminusone)
			{
				
				startPosX = 0;
				finishPosX = 0;
				startPosY = 0;
				finishPosY = 0;
				canBePlaced = 0;
				Debug.Log("Furniture IDs : " + furnitureList[i].FurnitureID);
				// genenating random positions for 
				xcoordinate = random.Next(0, coordinate1);
				ycoordinate = random.Next(0, coordinate2);

				Debug.Log("random X Y:" + xcoordinate + " , " + ycoordinate);

				howManyCellsX = (int)furnitureList[i].Xdimension /10;
				howManyCellsY = (int)furnitureList[i].Zdimension/10 ;

				Debug.Log("howManyCells X Y:" + howManyCellsX + " , "+ howManyCellsY);

				// checking whether the furniture can fit into the random generated positions
				if (howManyCellsX + xcoordinate + 7 < coordinate1 && howManyCellsY + ycoordinate < coordinate2)
				{
					Debug.Log("İlk if");
					startPosX = xcoordinate;
					finishPosX = xcoordinate + howManyCellsX;
					startPosY = ycoordinate;
					finishPosY = ycoordinate + howManyCellsY;
					Debug.Log("Gerçek startposX"+startPosX+ "Gerçek startposY"+ startPosY);

					Debug.Log("PARENT fınısh X Y:"+finishPosX + ","+finishPosY);

					Debug.Log("positions: x s"+ startPosX + " x f"+ finishPosX + " y s" + startPosY + "y f:" + finishPosY);
					// now, I will check whether the selected cells are empty or not

					for (int j = startPosX; j <= finishPosX; j++)
					{
						for (int k = startPosY; k <= finishPosY; k++)
						{
							// if the position is not empty, then quit the loop
							if (floorPlan[j,k] != "T")
							{
								canBePlaced = 1; // cannot be placed
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
					// if the position is not empty, then the random position generation will happen again
					if (canBePlaced == 0)
					{
						Debug.Log("canBePlaced if ine girdi mi");
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
						locationList.Add(new FurnitureGeneticLocation() { FurnitureID = furnitureList[i].FurnitureID, StartX = startPosX, FinishX= finishPosX, CenterX = centerX, StartY = startPosY, CenterY = centerY, FinishY = finishPosY});
					
						Debug.Log("id"+locationList);
						//Debug.Log("StartX"+locationList[1]);
						//Debug.Log("FinishX"+locationList[2]);
						/*Debug.Log("CenterX"+locationList[3]);
						Debug.Log("StartY"+locationList[4]);
						Debug.Log("FinishY"+locationList[5]);
						Debug.Log("CenterY"+locationList[6]);
						*/
						i++;
					}
				}
				
			}
			Debug.Log("COUNT"+ locationList.Count);
			Debug.Log("StartX"+locationList[0].StartX);
						//Debug.Log("FinishX"+locationList[2]);
						/*Debug.Log("CenterX"+locationList[3]);
						Debug.Log("StartY"+locationList[4]);
						Debug.Log("FinishY"+locationList[5]);
						Debug.Log("CenterY"+locationList[6]);
						*/
			
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
			
			//string fileName = @"D:\mom.txt";
		 
			try
			{
				// Check if file already exists. If yes, delete it.     

				fileName = fileName+ "(" + counter.ToString() + ").txt";
				Debug.Log("filenew"+fileName);
				counter++;	
				Debug.Log("counter"+counter);

				// Create a new file     
				using (FileStream fs = File.Create(fileName))
				{
					// Add some text to file    
					Byte[] title = new UTF8Encoding(true).GetBytes(bastir);
					fs.Write(title, 0, title.Length);
				}

				// Open the stream and read it back.    
				using (StreamReader sr = File.OpenText(fileName))
				{
					string s = "";
					while ((s = sr.ReadLine()) != null)
					{
						Console.WriteLine(s);
					}
				}
			}
			catch (Exception Ex)
			{
				Console.WriteLine(Ex.ToString());
			}
			
			return locationList;
	
		}

		
		private void Initialize()
		{
			fitness = 0;
			bits = new List<int>();
		}
	}
}
