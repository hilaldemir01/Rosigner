using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using Random = System.Random;


namespace Assets.Models
{
	public class GeneticAlgorithm
	{
		List<Wall> wallList;
		List<RoomStructure> roomStructuresList;
		List<Furniture> furnitureList;
		//string[,] floorPlan; // floor plan of our design that will include all furniture selected

		public List<Genome> genomes;
		public List<Genome> lastGenerationGenomes;
		public Vector2 startPosition;
		public Vector2 endPosition;
		public GeneticAlgorithm geneticAlgorithm;
		public List<int> fittestDirections;
		public List<List<string>> furnitureFronts;
		// each gene have 4 direction
		public int populationSize = 2;
		public double crossoverRate = 0.7f;
		public double mutationRate = 0.001f;
		public int chromosomeLength = 5;
		public int geneLength = 2;
		public int fittestGenome;
		public double bestFitnessScore;
		public double totalFitnessScore;
		public int generation;
		public bool busy;
		public int coordinate1;
		public int coordinate2;
		 public int momFitnessScore = 0;
        public int dadFitnessScore = 0;
        public int xcoordinate = 0;
        public int ycoordinate = 0;
		public int count = -1;
		List<FurnitureGeneticLocation> momFurnitureGeneticLocations;
		List<FurnitureGeneticLocation> dadFurnitureGeneticLocations;
		List<FurnitureGeneticLocation> baby1FurnitureGeneticLocations;
		List<FurnitureGeneticLocation> baby2FurnitureGeneticLocations;
		private Random random = new Random();

		public GeneticAlgorithm()
		{
			busy = false;
			//startMatrix();
			//initializng all lists that we are going to use
			genomes = new List<Genome>();
			wallList = new List<Wall>();
			roomStructuresList = new List<RoomStructure>();
			furnitureList = new List<Furniture>();
			furnitureFronts = new List<List<string>>();
			lastGenerationGenomes = new List<Genome>();
			momFurnitureGeneticLocations = new List<FurnitureGeneticLocation>();
			dadFurnitureGeneticLocations = new List<FurnitureGeneticLocation>();
			baby1FurnitureGeneticLocations = new List<FurnitureGeneticLocation>();
			baby2FurnitureGeneticLocations = new List<FurnitureGeneticLocation>();
		}
		// creating a matrix for the purpose of creating designs on it
		/*public void startMatrix()
		{
			//coordinate1 = (int)wallList[0].WallLength;
			//coordinate2 = (int)wallList[1].WallLength;
			//floorPlan = new string[(int)wallList[0].WallLength * 100, (int)wallList[1].WallLength * 100];
		}
	/*	public void Run()
		{
			CreateStartPopulation();
			busy = true;
		}
		*/

		// step number 1
		public void CreateStartPopulation(int coordinate1,int coordinate2,string[,] floorPlan, List<RoomStructure> roomStructureList, List<Furniture> furnitureList, List<Wall> wallList)
		{
			genomes.Clear(); // clear out any genomes at first
			
			for (int i = 0; i < populationSize / 2; i++)
            { // iterate through 10, for now it is 1
              //Genome baby = new Genome(furnitureList,coordinate1,coordinate2, floorPlan); // chromosomeLength = 5
              //genomes.Add(baby);
                Genome momGenome = new Genome();
//public List<FurnitureGeneticLocation> GenomeInit(int coordinate1, int coordinate2, string[,] floorPlan, List<RoomStructure> roomStructureList, List<Furniture> furnitureList, List<Wall> wallList)

				momFurnitureGeneticLocations = momGenome.GenomeInit(coordinate1, coordinate2, floorPlan, roomStructuresList, furnitureList, wallList);
				UpdateFitnessScores(furnitureList);//to calculate mom's furniture's fitness scores
 				// StartCoroutine(db.TempFurnitureLocation(momFurnitureGeneticLocations));
                
				Genome dadGenome = new Genome();
				dadFurnitureGeneticLocations = dadGenome.GenomeInit(coordinate1, coordinate2, floorPlan, roomStructuresList, furnitureList, wallList);
				UpdateFitnessScores(furnitureList); //to calculate dad's furniture's fitness scores
                // StartCoroutine(db.TempFurnitureLocation(dadFurnitureGeneticLocations));

				

                //  Genome mom = RouletteWheelSelection ();
                //  Genome dad = RouletteWheelSelection();
                //burası fora göre değişir
                Genome baby1 = new Genome(); // chromosomeLength = 5
                Genome baby2 = new Genome(); // chromosomeLength = 5
                Crossover(momGenome, dadGenome, baby1, baby2,furnitureList);
                genomes.Add(baby1);
                genomes.Add(baby2);

				// StartCoroutine(db.TempFurnitureLocation(baby1FurnitureGeneticLocations));
				// StartCoroutine(db.TempFurnitureLocation(baby2FurnitureGeneticLocations));

                for (int k = 0; k < 100; k++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        floorPlan[k, j] = "T";
                    }
                }
				int capacityminusone =(int) furnitureList.Capacity -1;
                while (i < capacityminusone)
                {
                    int startPosX = 0;
                    int finishPosX = 0;
                    int startPosY = 0;
                    int finishPosY = 0;
                    var canBePlaced = 0;
                 
                    xcoordinate = baby1.xcoordinatebaby[i];
                    ycoordinate = baby1.ycoordinatebaby[i];
                    int howManyCellsX = (int)baby1.newOne[i].Xdimension;
                    int howManyCellsY = (int)baby1.newOne[i].Zdimension;
					Debug.Log("baby cellx"+howManyCellsX);
					Debug.Log("baby celly"+howManyCellsY);
                    if (howManyCellsX + xcoordinate +7 < coordinate1 && howManyCellsY + ycoordinate < coordinate2)
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
                                if (floorPlan[j, k] != "T")
                                {
                                    canBePlaced = 1;
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
                            // replace the furniture id into array if positions are empty
                            for (int j = startPosX; j < finishPosX; j++)
                            {
                                for (int k = startPosY; k < finishPosY; k++)
                                {
                                    floorPlan[j, k] = "" + baby1.newOne[i].FurnitureID.ToString();
                                    
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
                                {
                                    floorPlan[k, j] = "Y";

                                }
                            }
                            i++;

                        }

                    }
                    

                    // Genome baby = new Genome();
                    //baby.GenomeInit(coordinate1,coordinate2,floorPlan);
                    //genomes.Add(baby);
                }
                
                string show = "";
                for (int k = 0; k < 100; k++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        show += floorPlan[k, j];
                       

                    }
                    show += "\n";
                }
                Console.WriteLine(show);
				string fileName = @"D:\deneme.txt";

				try
				{
					// Check if file already exists. If yes, delete it.     
					if (File.Exists(fileName))
					{
						File.Delete(fileName);
					}

					// Create a new file     
					using (FileStream fs = File.Create(fileName))
					{
						// Add some text to file    
						Byte[] title = new UTF8Encoding(true).GetBytes(show);
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
                
            }
		}
		public void Crossover(Genome mom, Genome dad, Genome baby1, Genome baby2,List<Furniture> furnitureList) {
			Debug.Log("mom.furnitureList genom size"+ mom.furnitureListGenom.Capacity);
            for (int i = 0; i <mom.furnitureListGenom.Capacity-1; i++)
                {
                    for(int j = 0; j < mom.furnitureListGenom.Capacity-1; j++)
                    {
                        if(mom.furnitureListGenom[i].FurnitureID == dad.furnitureListGenom[j].FurnitureID)
                        {
							Debug.Log("mom.furnitureListGenom[i].XD " + mom.furnitureListGenom[i].Xdimension);
							Debug.Log("mom.furnitureListGenom[i].YD" + mom.furnitureListGenom[i].Zdimension);

                            Debug.Log("dadFurnitureGeneticLocations[j]"+dadFurnitureGeneticLocations[j].FitnessScore);
                            if (momFurnitureGeneticLocations[i].FitnessScore >= dadFurnitureGeneticLocations[j].FitnessScore) //We will assign the location of the furniture to the baby, whichever parent has the greatest fitness score.
                            {
                                baby1.newOne.Add(new Furniture() { FurnitureID = mom.furnitureListGenom[i].FurnitureID,Xdimension = mom.furnitureListGenom[i].Xdimension, Zdimension = mom.furnitureListGenom[i].Zdimension });
                            //baby1 will have the furniture with the highest score.
                            //baby2 will have the furniture with the lowest score.
                                
								//these 2 lines for babies' Genome type
								baby1.xcoordinatebaby.Add(momFurnitureGeneticLocations[i].StartX);
								baby1.ycoordinatebaby.Add(momFurnitureGeneticLocations[i].StartY);
								//these 2 lines for babies' FurnitureGenetic Location type
								
								
							
								baby1FurnitureGeneticLocations.Add(new FurnitureGeneticLocation(){FurnitureID = momFurnitureGeneticLocations[i].FurnitureID,StartX=momFurnitureGeneticLocations[i].StartX,
									StartY = momFurnitureGeneticLocations[i].StartY, FinishX= momFurnitureGeneticLocations[i].FinishX,FinishY =momFurnitureGeneticLocations[i].FinishY,
								 	CenterX =momFurnitureGeneticLocations[i].CenterX, CenterY = momFurnitureGeneticLocations[i].CenterY, FitnessScore =  momFurnitureGeneticLocations[i].FitnessScore});
                               	Debug.Log("furID"+baby1FurnitureGeneticLocations[i].FurnitureID);
								Debug.Log("deneme coordinate mom startx "+momFurnitureGeneticLocations[i].StartX+"starty"+momFurnitureGeneticLocations[i].StartY);
								
                                baby2.xcoordinatebaby.Add(dadFurnitureGeneticLocations[j].StartX);
                                baby2.ycoordinatebaby.Add(dadFurnitureGeneticLocations[j].StartY);

								baby2FurnitureGeneticLocations.Add(new FurnitureGeneticLocation(){FurnitureID = dadFurnitureGeneticLocations[j].FurnitureID,StartX = dadFurnitureGeneticLocations[j].StartX,
								StartY = dadFurnitureGeneticLocations[j].StartY, FinishX = dadFurnitureGeneticLocations[j].FinishX,FinishY= dadFurnitureGeneticLocations[j].FinishY, CenterX = dadFurnitureGeneticLocations[j].CenterX,
								CenterY=dadFurnitureGeneticLocations[j].CenterY,FitnessScore = dadFurnitureGeneticLocations[j].FitnessScore });


                            }
                            else
                            {
                                baby1.newOne.Add(new Furniture() {FurnitureID = dad.furnitureListGenom[j].FurnitureID, Xdimension = dad.furnitureListGenom[j].Xdimension, Zdimension = dad.furnitureListGenom[j].Zdimension });

                                baby2.xcoordinatebaby.Add(momFurnitureGeneticLocations[i].StartX);
                                baby2.ycoordinatebaby.Add(momFurnitureGeneticLocations[i].StartY);
								
								baby2FurnitureGeneticLocations.Add(new FurnitureGeneticLocation(){FurnitureID = momFurnitureGeneticLocations[i].FurnitureID,StartX = momFurnitureGeneticLocations[i].StartX, StartY = momFurnitureGeneticLocations[i].StartY,
								FinishX = momFurnitureGeneticLocations[i].FinishX, FinishY = momFurnitureGeneticLocations[i].FinishY, CenterX= momFurnitureGeneticLocations[i].CenterX,CenterY=momFurnitureGeneticLocations[i].CenterY,
								FitnessScore = momFurnitureGeneticLocations[i].FitnessScore});
							
								
								baby1FurnitureGeneticLocations.Add(new FurnitureGeneticLocation(){FurnitureID = dadFurnitureGeneticLocations[j].FurnitureID,StartX = dadFurnitureGeneticLocations[j].StartX,
								StartY = dadFurnitureGeneticLocations[j].StartY, FinishX = dadFurnitureGeneticLocations[j].FinishX,FinishY= dadFurnitureGeneticLocations[j].FinishY, CenterX = dadFurnitureGeneticLocations[j].CenterX,
								CenterY=dadFurnitureGeneticLocations[j].CenterY,FitnessScore = dadFurnitureGeneticLocations[j].FitnessScore });

                                baby1.xcoordinatebaby.Add(dadFurnitureGeneticLocations[j].StartX);
                                baby1.ycoordinatebaby.Add(dadFurnitureGeneticLocations[j].StartY);
								Debug.Log("DAD deneme coordinate startx"+dadFurnitureGeneticLocations[j].StartX+"DAD startY"+dadFurnitureGeneticLocations[j].StartY);

                        	}
						
                    	}
                    }
                }
				
		}
		// 4 directions:
		public Vector2 Move(Vector2 position, int direction)
		{/*
			switch (direction)
			{
				case 0: // North
					if (position.y - 1 < 0 || floorPlan[(int)(position.y - 1), (int)position.x] == 1)
					{
						break;
					}
					else
					{
						position.y -= 1;
					}
					break;
				case 1: // South
					if (position.y + 1 >= floorPlan.GetLength(0) || floorPlan[(int)(position.y + 1), (int)position.x] == 1)
					{
						break;
					}
					else
					{
						position.y += 1;
					}
					break;
				case 2: // East
					if (position.x + 1 >= floorPlan.GetLength(1) || floorPlan[(int)position.y, (int)(position.x + 1)] == 1)
					{
						break;
					}
					else
					{
						position.x += 1;
					}
					break;
				case 3: // West
					if (position.x - 1 < 0 || floorPlan[(int)position.y, (int)(position.x - 1)] == 1)
					{
						break;
					}
					else
					{
						position.x -= 1;
					}
					break;
			} */
			return position; 
		}
		// it basically tests how far away we are from the exit by the end of the snake

		// score with 1= perfect score, fitness = 1, stop simulation
		public double TestRoute(List<int> directions)
		{
			Vector2 position = startPosition;

			for (int directionIndex = 0; directionIndex < directions.Count; directionIndex++)
			{
				int nextDirection = directions[directionIndex];
				position = Move(position, nextDirection);
			}

			Vector2 deltaPosition = new Vector2(
				Math.Abs(position.x - endPosition.x),
				Math.Abs(position.y - endPosition.y));
			double result = 1 / (double)(deltaPosition.x + deltaPosition.y + 1);
			if (result == 1)
				Debug.Log("TestRoute result=" + result + ",(" + position.x + "," + position.y + ")");
			return result;
		}


		// since we already assigned the position of the furniture when we created the design, 
		// we want to empty it before we want to change it's rotation.
		public void emptyPreviousLocationForXY(FurnitureGeneticLocation furnitureGeneticLocation, string[,] floorPlan)
        {
			for(int i = furnitureGeneticLocation.XPositionStartX; i < furnitureGeneticLocation.XPositionFinishX; i++)
            {
				for(int j = furnitureGeneticLocation.XPositionStartY; j < furnitureGeneticLocation.XPositionFinishY; j++)
                {
					floorPlan[i, j] = "T";
                }
            }
			for (int i = furnitureGeneticLocation.YPositionStartX; i < furnitureGeneticLocation.YPositionFinishX; i++)
			{
				for (int j = furnitureGeneticLocation.YPositionStartY; j < furnitureGeneticLocation.YPositionFinishY; j++)
				{
					floorPlan[i, j] = "T";
				}
			}
		}
		public void emptyAllLocations(FurnitureGeneticLocation furnitureGeneticLocation, string[,] floorPlan)
        {
			for (int i = furnitureGeneticLocation.StartX; i < furnitureGeneticLocation.FinishX; i++)
			{
				for (int j = furnitureGeneticLocation.StartY; j < furnitureGeneticLocation.FinishY; j++)
				{
					floorPlan[i, j] = "T";
				}
			}
			for (int i = furnitureGeneticLocation.XPositionStartX; i < furnitureGeneticLocation.XPositionFinishX; i++)
			{
				for (int j = furnitureGeneticLocation.XPositionStartY; j < furnitureGeneticLocation.XPositionFinishY; j++)
				{
					floorPlan[i, j] = "T";
				}
			}
			for (int i = furnitureGeneticLocation.YPositionStartX; i < furnitureGeneticLocation.YPositionFinishX; i++)
			{
				for (int j = furnitureGeneticLocation.YPositionStartY; j < furnitureGeneticLocation.YPositionFinishY; j++)
				{
					floorPlan[i, j] = "T";
				}
			}
		}
		public void rotateRandomFurniture(List<FurnitureGeneticLocation> furnitureGeneticLocations, string[,] floorPlan, int coordinate1, int coordinate2)
        {
			int furnitureID  = random.Next(0, furnitureGeneticLocations.Count - 1);
			int canBeRotated = 0;
			furnitureGeneticLocations[furnitureID].WallName = "W4";
			if (furnitureGeneticLocations[furnitureID].WallName == "W1") // wall on the bottom
            {
				emptyPreviousLocationForXY(furnitureGeneticLocations[furnitureID], floorPlan);
				// the front part of the furniture will be on the top, so we don't want it to exceed 0 
				if (furnitureGeneticLocations[furnitureID].StartX - 6 > 0)
				{
					// we need to check whether the empty space in front of the furniture conflicts with another one
					for (int j = furnitureGeneticLocations[furnitureID].StartX - 6; j < furnitureGeneticLocations[furnitureID].StartX; j++)
					{
						for (int k = furnitureGeneticLocations[furnitureID].StartY; k < furnitureGeneticLocations[furnitureID].FinishY; k++)
						{
							if (floorPlan[j, k] != "T")
							{
								canBeRotated = 1;
								break;
							}
						}
						if (canBeRotated == 1)
						{
							Debug.Log("Cannot be rotated");
							break;
						}
					}
				}
				// we want to turn the furniture 180 degrees
				// we don't have to change the positions of the furniture ID's
				// we just need to change the location of X and Y's

				// after the id is written, now we need to define the front part of the object, I will put 'X' value to define the front part
				if(canBeRotated == 0)
                {
					for (int j = furnitureGeneticLocations[furnitureID].StartY; j < furnitureGeneticLocations[furnitureID].FinishY; j++)
					{
						floorPlan[furnitureGeneticLocations[furnitureID].StartX-1, j] = "X";
					}
					// after the X values are written, then "Y" values are going to be replaced to leave an empty space for each furniture
					// for now, every object will have 30cm space in front of them
					for (int k = furnitureGeneticLocations[furnitureID].StartX - 6; k < furnitureGeneticLocations[furnitureID].StartX - 1; k++)
					{
						for (int j = furnitureGeneticLocations[furnitureID].StartY; j < furnitureGeneticLocations[furnitureID].FinishY; j++)
						{
							floorPlan[k, j] = "Y";
						}
					}
				}
			}
			else if(furnitureGeneticLocations[furnitureID].WallName == "W2") // right-side wall
            {
				// Start positions of poth x and y will stay the same, later their finish positions will change according to their width

				int XValue = furnitureGeneticLocations[furnitureID].StartX + (furnitureGeneticLocations[furnitureID].FinishY - furnitureGeneticLocations[furnitureID].StartY);
				int YValue = furnitureGeneticLocations[furnitureID].StartY + (furnitureGeneticLocations[furnitureID].FinishX - furnitureGeneticLocations[furnitureID].StartX);
				
				if (XValue < coordinate1 && YValue < coordinate2)
                {
					// first empty the area that it currently allocates
					emptyAllLocations(furnitureGeneticLocations[furnitureID], floorPlan);

					int newFinishX, newFinishY;

					newFinishX = XValue;
					newFinishY = YValue;

					furnitureGeneticLocations[furnitureID].FinishX = newFinishX;
					furnitureGeneticLocations[furnitureID].FinishY = newFinishY;

					for (int i = furnitureGeneticLocations[furnitureID].StartX; i < furnitureGeneticLocations[furnitureID].FinishX; i++)
					{
						for (int j = furnitureGeneticLocations[furnitureID].StartY; j < furnitureGeneticLocations[furnitureID].FinishY; j++)
						{
							floorPlan[i,j] = "" + furnitureGeneticLocations[furnitureID].FurnitureID.ToString();
						}
					}
					// now we are going to place x and y values 
					// after the id is written, now we need to define the front part of the object, I will put 'X' value to define the front part
					for (int j = furnitureGeneticLocations[furnitureID].StartX; j < furnitureGeneticLocations[furnitureID].FinishX; j++)
					{
						floorPlan[j, furnitureGeneticLocations[furnitureID].StartY - 1] = "X";
					}
					// after the X values are written, then "Y" values are going to be replaced to leave an empty space for each furniture
					// for now, every object will have 30cm space in front of them
					for (int k = furnitureGeneticLocations[furnitureID].StartX; k < furnitureGeneticLocations[furnitureID].FinishX; k++)
					{
						for (int j = furnitureGeneticLocations[furnitureID].StartY - 6; j < furnitureGeneticLocations[furnitureID].StartY - 1; j++)
						{
							floorPlan[k, j] = "Y";
						}
					}
					furnitureGeneticLocations[furnitureID].XPositionStartX = furnitureGeneticLocations[furnitureID].StartX;
					furnitureGeneticLocations[furnitureID].XPositionFinishX = furnitureGeneticLocations[furnitureID].FinishX;
					furnitureGeneticLocations[furnitureID].XPositionStartY = furnitureGeneticLocations[furnitureID].StartY - 1;
					furnitureGeneticLocations[furnitureID].XPositionFinishY = furnitureGeneticLocations[furnitureID].StartY;
					furnitureGeneticLocations[furnitureID].YPositionStartX = furnitureGeneticLocations[furnitureID].StartX;
					furnitureGeneticLocations[furnitureID].YPositionFinishX = furnitureGeneticLocations[furnitureID].FinishX;
					furnitureGeneticLocations[furnitureID].YPositionStartY = furnitureGeneticLocations[furnitureID].StartY - 6;
					furnitureGeneticLocations[furnitureID].YPositionFinishY = furnitureGeneticLocations[furnitureID].StartY - 1;

				}
			}
			else if(furnitureGeneticLocations[furnitureID].WallName == "W3") // wall on the top
            {
				// we don't want to turn it around since it is already aligned
			}
			else if(furnitureGeneticLocations[furnitureID].WallName == "W4") // wall on the left side
            {
				// Start positions of poth x and y will stay the same, later their finish positions will change according to their width

				int XValue = furnitureGeneticLocations[furnitureID].StartX + (furnitureGeneticLocations[furnitureID].FinishY - furnitureGeneticLocations[furnitureID].StartY);
				int YValue = furnitureGeneticLocations[furnitureID].StartY + (furnitureGeneticLocations[furnitureID].FinishX - furnitureGeneticLocations[furnitureID].StartX);

				if (XValue < coordinate1 && YValue < coordinate2)
				{
					// first empty the area that it currently allocates
					emptyAllLocations(furnitureGeneticLocations[furnitureID], floorPlan);

					int newFinishX, newFinishY;

					newFinishX = XValue;
					newFinishY = YValue;

					furnitureGeneticLocations[furnitureID].FinishX = newFinishX;
					furnitureGeneticLocations[furnitureID].FinishY = newFinishY;

					for (int i = furnitureGeneticLocations[furnitureID].StartX; i < furnitureGeneticLocations[furnitureID].FinishX; i++)
					{
						for (int j = furnitureGeneticLocations[furnitureID].StartY; j < furnitureGeneticLocations[furnitureID].FinishY; j++)
						{
							floorPlan[i, j] = "" + furnitureGeneticLocations[furnitureID].FurnitureID.ToString();
						}
					}
					// now we are going to place x and y values 
					// after the id is written, now we need to define the front part of the object, I will put 'X' value to define the front part
					for (int j = furnitureGeneticLocations[furnitureID].StartX; j < furnitureGeneticLocations[furnitureID].FinishX; j++)
					{
						floorPlan[j, furnitureGeneticLocations[furnitureID].FinishY] = "X";
					}
					// after the X values are written, then "Y" values are going to be replaced to leave an empty space for each furniture
					// for now, every object will have 30cm space in front of them
					for (int k = furnitureGeneticLocations[furnitureID].StartX; k < furnitureGeneticLocations[furnitureID].FinishX; k++)
					{
						for (int j = furnitureGeneticLocations[furnitureID].FinishY + 1; j < furnitureGeneticLocations[furnitureID].FinishY + 6; j++)
						{
							floorPlan[k, j] = "Y";
						}
					}
					furnitureGeneticLocations[furnitureID].XPositionStartX = furnitureGeneticLocations[furnitureID].StartX;
					furnitureGeneticLocations[furnitureID].XPositionFinishX = furnitureGeneticLocations[furnitureID].FinishX;
					furnitureGeneticLocations[furnitureID].XPositionStartY = furnitureGeneticLocations[furnitureID].StartY - 1;
					furnitureGeneticLocations[furnitureID].XPositionFinishY = furnitureGeneticLocations[furnitureID].StartY;
					furnitureGeneticLocations[furnitureID].YPositionStartX = furnitureGeneticLocations[furnitureID].StartX;
					furnitureGeneticLocations[furnitureID].YPositionFinishX = furnitureGeneticLocations[furnitureID].FinishX;
					furnitureGeneticLocations[furnitureID].YPositionStartY = furnitureGeneticLocations[furnitureID].StartY - 6;
					furnitureGeneticLocations[furnitureID].YPositionFinishY = furnitureGeneticLocations[furnitureID].StartY - 1;

				}
			}
			
			string fileName = @"D:\matrix2.txt";
			string bastir = "";
			for (int k = 0; k < coordinate1; k++)
			{
				for (int j = 0; j < coordinate2; j++)
				{
					bastir += floorPlan[k, j];

				}
				bastir += "\n";
			}
			try
			{
				// Check if file already exists. If yes, delete it.     
				if (File.Exists(fileName))
				{
					File.Delete(fileName);
				}

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
		}
		// step number 5
		// creates babies slightly different from their mom and dad
		public void Mutate(List<int> bits)
		{
			// walks through the genes of the chromosome
			for (int i = 0; i < bits.Count; i++)
			{
				// flip this bit?
				if (UnityEngine.Random.value < mutationRate)
				{
					// flip the bit
					bits[i] = bits[i] == 0 ? 1 : 0;
				}
			}
		}
		// chose two genomes from the population, select the one with the highest fitness value
		// choose partials of mom and dad randomly
		// step number 4
		public void Crossover(List<int> mom, List<int> dad, List<int> baby1, List<int> baby2)
		{
			// if the value is more than the crossoverrate just copy mom and dad
			if (UnityEngine.Random.value > crossoverRate || mom == dad)
			{
				baby1.AddRange(mom);
				baby2.AddRange(dad);

				return;
			}
			// if it is not more than the crossoverrate, pick a random placement value 
			System.Random rnd = new System.Random();

			int crossoverPoint = rnd.Next(0, chromosomeLength - 1);
			// mom = 1,2,3,4,5,6,7,8
			// dad = 9,10,11,12,13,14,15,16,17
			// let's say crossoverPoint = 5
			// then baby1 = 1,2,3,4
			// baby2 = 9,10,11,12
			for (int i = 0; i < crossoverPoint; i++)
			{
				baby1.Add(mom[i]);
				baby2.Add(dad[i]);
			}
			// baby1 = 1,2,3,4,13,14,15,16,17
			// baby2 = 9,10,11,12,5,6,7,8
			for (int i = crossoverPoint; i < mom.Count; i++)
			{
				baby1.Add(dad[i]);
				baby2.Add(mom[i]);
			}
		}

		public Genome RouletteWheelSelection()
		{
			double slice = UnityEngine.Random.value * totalFitnessScore;
			double total = 0;
			int selectedGenome = 0;

			for (int i = 0; i < populationSize; i++)
			{
				total += genomes[i].fitness;

				if (total > slice)
				{
					selectedGenome = i;
					break;
				}
			}
			return genomes[selectedGenome];
		}
		//	// step number 2
		//	// rank fitness
		 public int distanceFromWalls(int startX, int startY, int finishX, int finishY)
        {
            int score =0;
            int value1 = 0, value2 = 0, value3 = 0, value4 = 0;
            int roomCenterX = 50, roomCenterY = 50;
            int centerX = finishX + startX / 2;
            int centerY = finishY + startY / 2;

            // checking the distance of the furniture to y axis
            value1 = (int)(centerY / Math.Sqrt(2));

            // checking the distance of the furniture to x axis
            value2 = (int)(centerX / Math.Sqrt(2));

            // checking the distance of the furniture to x-y axis (y is in the upper part)
            value3 = (int)((centerX + 2 * centerY) / Math.Sqrt(2));

            // checking the distance of the furniture to x-y axis (x is in the upper part)
            value4 = (int)((centerY + 2 * centerX) / Math.Sqrt(2));

            int selectedFormula = -900;
            int formulaNum = 0;

            int[] myNum = { value1, value2, value3, value4 };

            for(int i = 0; i < myNum.Length; i++)
            {
                if(selectedFormula < myNum[i])
                {
                    selectedFormula = myNum[i];
                    formulaNum = i+1;
                }
            }
            Console.WriteLine("Value1: " + value1 + " Value2: " + value2 + " Value3: "+value3+ " Value4 :" +value4);
            Console.WriteLine("SelectedFormula: " + selectedFormula + " formulaNum: " + formulaNum);
            score = FindFitnessScoreWallDistance(centerX, centerY, selectedFormula, formulaNum, roomCenterX, roomCenterY);
            Console.WriteLine("score in dist "+score);
            return score;
        }


		// this code is used to evaluate the value returned after formulas used to create a cost value upto 1
		  public int FindFitnessScoreWallDistance(int centerX, int centerY, int selectedFormula, int formulaNum, int roomCenterX, int roomCenterY)
        {
            int fitnessScore;
            double rate = 0.0;
            if(formulaNum == 1)
            {
                fitnessScore = (int)(roomCenterX / Math.Sqrt(2));
                rate = ((double)fitnessScore * selectedFormula) / 10000;
            }
            else if (formulaNum == 2)
            {
                fitnessScore = (int)(centerX / Math.Sqrt(2));
                rate = ((double)fitnessScore * selectedFormula) / 10000;

            }
            else if (formulaNum == 3)
            {
                fitnessScore = (int)((centerX + 2 * centerY) / Math.Sqrt(2));
                rate = ((double)fitnessScore * selectedFormula) / 10000;
            }
            else
            {
                fitnessScore = (int)((centerY + 2 * centerX) / Math.Sqrt(2));
                rate = ((double)fitnessScore * selectedFormula) / 10000;
            }
            Console.WriteLine("FitnessScore: " + fitnessScore + " Rate = " + rate.ToString());
            return fitnessScore;
        }
		public void UpdateFitnessScores(List<Furniture> furnitureList)
        {
            int score = 0;
			count++;
			Debug.Log("count"+count);
			int startX, finishX ,startY,finishY;
			Debug.Log("furnitureList count:"+furnitureList.Count);

			for(int i = 0; i <furnitureList.Count; i++){
				if(count % 2 == 0){
					Debug.Log("startx"+ momFurnitureGeneticLocations[0].StartX);
					startX = momFurnitureGeneticLocations[i].StartX;
					finishX =  momFurnitureGeneticLocations[i].FinishX;
					startY = momFurnitureGeneticLocations[i].StartY;
					finishY =  momFurnitureGeneticLocations[i].FinishY;

					momFurnitureGeneticLocations[i].FitnessScore = distanceFromWalls(startX, startY, finishX, finishY);
					Debug.Log("mom fitness score"+momFurnitureGeneticLocations[i].FitnessScore);
				}else{
					startX = dadFurnitureGeneticLocations[i].StartX;
					finishX =  dadFurnitureGeneticLocations[i].FinishX;
					startY = dadFurnitureGeneticLocations[i].StartY;
					finishY =  dadFurnitureGeneticLocations[i].FinishY;
					dadFurnitureGeneticLocations[i].FitnessScore = distanceFromWalls(startX, startY, finishX, finishY);
					Debug.Log("dad fitness score"+dadFurnitureGeneticLocations[i].FitnessScore);

				}
			}
			
            // what I want to do in this function is that, I want to check the distance of 
            fittestGenome = 0;
            bestFitnessScore = 0;
            totalFitnessScore = 0;

            // getting start and finish positions of the furniture that the cost value will be updated
            //int startX = 20;
            //int finishX = 30;
            //int startY = 20;
            //int finishY = 30;
            //Random random = new Random();
          /*  int num = Random.Range(0, 100);
            int startX = momFurnitureGeneticLocations[i].StartX;

            num = Random.Range(0, 100);
            int finishX = num;

            num = Random.Range(0, 100);
            int startY = num;

            num = Random.Range(0, 100);
            int finishY = num;
			*/
            // score = distanceFromWalls(startX, startY, finishX, finishY);
            

			//for (int i = 0; i < populationSize; i++)
			//{
			////    List<int> directions = Decode(genomes[i].bits);
			//    // using the method from the mazeController
			////    genomes[i].fitness = TestRoute(directions);

			//    totalFitnessScore += genomes[i].fitness;

			//    if (genomes[i].fitness > bestFitnessScore)
			//    {
			//        bestFitnessScore = genomes[i].fitness;
			//        fittestGenome = i;
			//        // Has chromosome found the exit?
			//        if (genomes[i].fitness == 1)
			//        {
			//            busy = false; // stop the run
			//            return;
			//        }
			//    }
			//}

		}

		//---------------------------Decode-------------------------------------
		//
		//	decodes a List of bits into a List of directions (ints)
		//
		//	0=North, 1=South, 2=East, 3=West
		//-----------------------------------------------------------------------
		public List<int> Decode(List<int> bits)
		{
			List<int> directions = new List<int>();

			for (int geneIndex = 0; geneIndex < bits.Count; geneIndex += geneLength)
			{
				List<int> gene = new List<int>();

				for (int bitIndex = 0; bitIndex < geneLength; bitIndex++)
				{
					gene.Add(bits[geneIndex + bitIndex]);
				}

				directions.Add(GeneToInt(gene));
			}
			return directions;
		}

		//-------------------------------GeneToInt-------------------------------
		//	converts a List of bits into an integer
		//----------------------------------------------------------------------
		public int GeneToInt(List<int> gene)
		{
			int value = 0;
			int multiplier = 1;

			for (int i = gene.Count; i > 0; i--)
			{
				value += gene[i - 1] * multiplier;
				multiplier *= 2;
			}
			return value;
		}



		// choose mom and dad
		// step number 3
		public void Epoch()
		{
			//if (!busy) return;
			// evolve the fitness function of all population
			//UpdateFitnessScores();

			//if (!busy)
			//{
			//	lastGenerationGenomes.Clear();
			//	lastGenerationGenomes.AddRange(genomes);
			//	return;
			//}

			int numberOfNewBabies = 0;

			List<Genome> babies = new List<Genome>();
			while (numberOfNewBabies < populationSize)
			{
				// select 2 parents
				Genome mom = RouletteWheelSelection();
				Genome dad = RouletteWheelSelection();
				Genome baby1 = new Genome();
				Genome baby2 = new Genome();
				Crossover(mom.bits, dad.bits, baby1.bits, baby2.bits);
				Mutate(baby1.bits);
				Mutate(baby2.bits);

				// empty babies
				babies.Add(baby1);
				babies.Add(baby2);

				numberOfNewBabies += 2;
			}

			// save last generation for display purposes
			lastGenerationGenomes.Clear();
			lastGenerationGenomes.AddRange(genomes);
			// overwrite population with babies
			genomes = babies;

			// increment the generation counter
			generation++;
		}
	}
}