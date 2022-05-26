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
		string[,] floorPlan; // floor plan of our design that will include all furniture selected

		public List<Genome> genomes;
		public List<Genome> lastGenerationGenomes;
		public Vector2 startPosition;
		public Vector2 endPosition;
		public GeneticAlgorithm geneticAlgorithm;
		public List<int> fittestDirections;
		public List<List<string>> furnitureFronts;
		// each gene have 4 direction
		public int populationSize = 10;
		public double crossoverRate = 0.7f;
		public double mutationRate = 0.001f;
		public int chromosomeLength = 5;
		public int geneLength = 2;
		public int fittestGenome;
		public int genomCount;
		public int generation;
		public bool busy;
		public int momFitnessScore = 0;
		public int dadFitnessScore = 0;
		public int xcoordinate = 0;
		public int ycoordinate = 0;
		public int count = 0; //-1
		public int babyCounter = 0; 
		List<FurnitureGeneticLocation> momFurnitureGeneticLocations;
		List<FurnitureGeneticLocation> dadFurnitureGeneticLocations;
		List<FurnitureGeneticLocation> baby1FurnitureGeneticLocations;
		List<FurnitureGeneticLocation> baby2FurnitureGeneticLocations;
		List<FurnitureGeneticLocation> populationFurnitureGeneticLocations;
		List<FurnitureGeneticLocation> allPopulationFurnitureGeneticLocations;
		public List<FurnitureGeneticLocation> locationList;
		private Random random = new Random();
		public string ClassWallName;
		public static int wallCounter;
		public static int callCount;
		public int ClassDegree = 0 ;
		Genome baby1; // chromosomeLength = 5
		Genome baby2; // chromosomeLength = 5
		private string RoomStructureLetter = "D";
		public double totalFitnessScore = 0;
		public static List<FurnitureGeneticLocation> returnedDesign;
		public GeneticAlgorithm()
		{
			busy = false;
			//startMatrix();
			//initializng all lists that we are going to use
			baby1 = new Genome();
			baby2 = new Genome();
			genomes = new List<Genome>();
			wallList = new List<Wall>();
			furnitureList = new List<Furniture>();
			furnitureFronts = new List<List<string>>();
			lastGenerationGenomes = new List<Genome>();
			roomStructuresList = new List<RoomStructure>();
			locationList = new List<FurnitureGeneticLocation>();
			returnedDesign = new List<FurnitureGeneticLocation>();
			momFurnitureGeneticLocations = new List<FurnitureGeneticLocation>();
			dadFurnitureGeneticLocations = new List<FurnitureGeneticLocation>();
			baby1FurnitureGeneticLocations = new List<FurnitureGeneticLocation>();
			baby2FurnitureGeneticLocations = new List<FurnitureGeneticLocation>();
			populationFurnitureGeneticLocations = new List<FurnitureGeneticLocation>();
			allPopulationFurnitureGeneticLocations = new List<FurnitureGeneticLocation>();
		}
		// creating a matrix for the purpose of creating designs on it
		/*
		public void startMatrix()
		{
			//coordinate1 = (int)wallList[0].WallLength;
			//coordinate2 = (int)wallList[1].WallLength;
			//floorPlan = new string[(int)wallList[0].WallLength * 100, (int)wallList[1].WallLength * 100];
		}
		*/
		//public void Run(int coordinate1, int coordinate2, string[,] floorPlan, List<RoomStructure> roomStructureList, List<Furniture> furnitureList, List<Wall> wallList)
		//{
		//	CreateStartPopulation(coordinate1,coordinate2,floorPlan,roomStructureList,furnitureList,wallList);
		//	busy = true;
		//}

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
		// step number 1
		public List<FurnitureGeneticLocation> CreateStartPopulation(int coordinate1, int coordinate2, string[,] floorPlan, List<RoomStructure> roomStructureList, List<Furniture> furnitureList, List<Wall> wallList)
		{
			for (int k = 0; k < coordinate1; k++)
			{
				for (int j = 0; j < coordinate2; j++)
				{
					floorPlan[k, j] = "T";
				}
			}

			//creating a new empty design

			floorPlan = returnStructurePlan((int)wallList[1].WallLength * 100, (int)wallList[0].WallLength * 100, floorPlan, roomStructureList, wallList);

			genomes.Clear(); // clear out any genomes at first

			for (int m = 0; m < populationSize; m++)
			{
				//we created 10 design(population)
				Genome population = new Genome();
				populationFurnitureGeneticLocations = population.GenomeInit(coordinate1, coordinate2, floorPlan, roomStructuresList, furnitureList, wallList); //populationFurnitureGeneticLocations is just one design																																
				genomes.Add(population); //to add population designs in genomes
				double value = CalculateTotalFitnessScores(populationFurnitureGeneticLocations, furnitureList, coordinate1, coordinate2);//to calculate fitness score of each design
				genomes[m].populationFitnessScore = value; //value is total fitness score
				
				allPopulationFurnitureGeneticLocations.AddRange(populationFurnitureGeneticLocations); //allPopulationFurnitureGeneticLocations is merge of populationFurnitureGeneticLocations
			}
			for(int x = 0; x<allPopulationFurnitureGeneticLocations.Count; x++){
				Debug.Log("wallName "+allPopulationFurnitureGeneticLocations[x].WallName);
			}
			// StartCoroutine(db.TempFurnitureLocation(dadFurnitureGeneticLocations));

			int selectedGenomeIndex;
			selectedGenomeIndex = RouletteWheelSelection();//to get the randomly selected population index for mom
			Genome momGenome = genomes[selectedGenomeIndex];
			
			for (int n = furnitureList.Count * selectedGenomeIndex; n < ((selectedGenomeIndex * furnitureList.Count) + furnitureList.Count); n++)
			{
				momFurnitureGeneticLocations.Add(allPopulationFurnitureGeneticLocations[n]);
			}
			var canBePlaced = 0;
			int xcoordinate, ycoordinate;
			int howManyCellsX, howManyCellsY;
			int i = 0;
			int startPosX = 0, finishPosX = 0, startPosY = 0, finishPosY = 0;


			
			
			// the default capacity of a list is fixed at 4, so if you get error about size, please consider it
			while (i < (int)furnitureList.Capacity)
			{

				startPosX = momFurnitureGeneticLocations[i].StartX;
				finishPosX = momFurnitureGeneticLocations[i].FinishX;
				startPosY = momFurnitureGeneticLocations[i].StartY;
				finishPosY = momFurnitureGeneticLocations[i].FinishY;
				canBePlaced = 0;


				// checking whether the furniture can fit into the random generated positions
				if (finishPosX + 7 < coordinate1 && finishPosY < coordinate2)
				{

					// now, I will check whether the selected cells are empty or not

					for (int j = startPosX; j <= finishPosX; j++)
					{
						for (int k = startPosY; k <= finishPosY; k++)
						{
							// if the position is not empty, then quit the loop
							if (floorPlan[j, k] != "T")
							{
								if (floorPlan[j, k] == "D")
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
						i++;
					}
				}

			}

			string bastir = "";
			for (int k = 0; k < coordinate1; k++)
			{
				for (int j = 0; j < coordinate2; j++)
				{
					bastir += floorPlan[k, j];

				}
				bastir += "\n";
			}
			string fileName1 = @"D:\eskii.txt";

			try
			{
				// Check if file already exists. If yes, delete it.     
				if (File.Exists(fileName1))
				{
					File.Delete(fileName1);
				}

				// Create a new file     
				using (FileStream fs = File.Create(fileName1))
				{
					// Add some text to file    
					Byte[] title = new UTF8Encoding(true).GetBytes(bastir);
					fs.Write(title, 0, title.Length);
				}

				// Open the stream and read it back.    
				using (StreamReader sr = File.OpenText(fileName1))
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
			selectedGenomeIndex = RouletteWheelSelection(); //to get the randomly selected population index for dad
			Genome dadGenome = genomes[selectedGenomeIndex];
			

			for (int m = furnitureList.Count * selectedGenomeIndex; m < ((selectedGenomeIndex * furnitureList.Count) + furnitureList.Count); m++)
			{
				dadFurnitureGeneticLocations.Add(allPopulationFurnitureGeneticLocations[m]);
			}

			Crossover(momGenome, dadGenome, baby1, baby2, furnitureList);
			Debug.Log("baby1 pop fitnessscore" + baby1.populationFitnessScore);
			int forsize = momGenome.furnitureListGenom.Capacity; //??
			//MoveRandomFurniture(coordinate1, coordinate2, furnitureList); //index of furniture in baby to be moved			
			//baby1FurnitureGeneticLocations = rotateRandomFurniture(baby1FurnitureGeneticLocations, floorPlan, coordinate1, coordinate2);
			//totalFitness = CalculateTotalFitnessScores(baby1FurnitureGeneticLocations, furnitureList, coordinate1, coordinate2); //to calculate the baby's new fitness score whose position has been randomized in moverandomfurniture function
			//baby1.populationFitnessScore = totalFitness;
			
			//iteration part for baby
			//while loop will be run until the population fitness score of baby will be bigger than 0.95
			//In while loop, baby1 will be new mom and dad will be randomly re-selected from the population
			while (baby1.populationFitnessScore < 0.80)
			{		
				momFurnitureGeneticLocations.Clear();
				dadFurnitureGeneticLocations.Clear();
				double totalFitness = 0;

				for (int t = 0; t < forsize; t++)
				{
					momGenome.furnitureListGenom.Add(new Furniture() { FurnitureID = baby1.newOne[t].FurnitureID, Xdimension = baby1.newOne[t].Xdimension, Zdimension = baby1.newOne[t].Zdimension });
					momFurnitureGeneticLocations.Add(baby1FurnitureGeneticLocations[t]);
				}

				selectedGenomeIndex = RouletteWheelSelection();
				dadGenome = genomes[selectedGenomeIndex];

				for (int m = furnitureList.Count * selectedGenomeIndex; m < ((selectedGenomeIndex * furnitureList.Count) + furnitureList.Count); m++)
				{
					dadFurnitureGeneticLocations.Add(allPopulationFurnitureGeneticLocations[m]);
				}

				baby1.newOne.Clear();
				baby2.newOne.Clear();

				baby1.xcoordinatebaby.Clear();
				baby1.ycoordinatebaby.Clear();
				baby2.xcoordinatebaby.Clear();
				baby2.ycoordinatebaby.Clear();

				baby1FurnitureGeneticLocations.Clear();
				baby2FurnitureGeneticLocations.Clear();

				Crossover(momGenome, dadGenome, baby1, baby2, furnitureList);
		//		MoveRandomFurniture(coordinate1, coordinate2, furnitureList); //index of furniture in baby to be moved			
		//		baby1FurnitureGeneticLocations = rotateRandomFurniture(baby1FurnitureGeneticLocations, floorPlan, coordinate1, coordinate2);
			//	totalFitness = CalculateTotalFitnessScores(baby1FurnitureGeneticLocations, furnitureList, coordinate1, coordinate2); //to calculate the baby's new fitness score whose position has been randomized in moverandomfurniture function
			//	baby1.populationFitnessScore = totalFitness;

				Debug.Log("babyCounter"+babyCounter);
				Debug.Log("baby1 pop fitnessscore" + baby1.populationFitnessScore);
				babyCounter++;
				////oluşturduktan sonra oluşanlar gerekirse burda da db ye ekleme için yazılır temizliyoruz her defasında çünkü
			}
			if (baby1.populationFitnessScore >= 0.80)
			{
				Debug.Log("Found"); // TOTAL FITNESS SCORE BABY1

			}

			Debug.Log("baby1 pop fitnessscore" + baby1.populationFitnessScore);

			//genomes.Add(baby1);
			//genomes.Add(baby2);

			// StartCoroutine(db.TempFurnitureLocation(baby1FurnitureGeneticLocations));
			// StartCoroutine(db.TempFurnitureLocation(baby2FurnitureGeneticLocations));

			for (int k = 0; k < coordinate1; k++)
			{
				for (int j = 0; j < coordinate2; j++)
				{
					floorPlan[k, j] = "T";
				}
			}
			int capacityminusone = (int)furnitureList.Capacity; // -1 durumu?
		    i = 0;
			floorPlan = returnStructurePlan((int)wallList[1].WallLength * 100, (int)wallList[0].WallLength * 100, floorPlan, roomStructureList, wallList);
			string wallName;
			while (i < capacityminusone)
			{
				//int startPosX = 0;
				//int finishPosX = 0;
				//int startPosY = 0;
				//int finishPosY = 0;
				//var canBePlaced = 0;

				xcoordinate = baby1.xcoordinatebaby[i];
				ycoordinate = baby1.ycoordinatebaby[i];
				//int howManyCellsX = (int)baby1.newOne[i].Xdimension;
				//int howManyCellsY = (int)baby1.newOne[i].Zdimension;
				howManyCellsX = (int)baby1.newOne[i].Xdimension;
				howManyCellsY = (int)baby1.newOne[i].Zdimension;
				//string wallName = wallNameUpdate(startPosX, startPosY, finishPosX, finishPosY, coordinate1, coordinate2);
				wallName = baby1FurnitureGeneticLocations[i].WallName;
				int degree = rotateRandomFurniture(baby1FurnitureGeneticLocations, floorPlan, coordinate1, coordinate2, i);

				baby1FurnitureGeneticLocations[i].Degree = degree;
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
						locationList.Add(new FurnitureGeneticLocation()
						{
							FurnitureID = baby1.newOne[i].FurnitureID,
							StartX = startPosX,
							FinishX = finishPosX,
							CenterX = (startPosX + finishPosX) / 2,
							StartY = startPosY,
							CenterY = (startPosY + finishPosY) / 2,
							FinishY = finishPosY,
							XPositionStartX = finishPosX,
							XPositionFinishX = finishPosX + 1,
							XPositionFinishY = finishPosY,
							XPositionStartY = startPosY,
							YPositionStartX = finishPosX + 1,
							YPositionFinishX = finishPosX + 6,
							YPositionStartY = startPosY,
							YPositionFinishY = finishPosY,
							WallName = wallName,
							FitnessScore = baby1FurnitureGeneticLocations[i].FitnessScore,
							Degree = degree
						});
						i++;
						

					}

				}
			}


			string show = "";
			for (int k = 0; k < coordinate1; k++)//düzelmesi lazım
			{
				for (int j = 0; j < coordinate2; j++)//düzelmesi lazım
				{
					show += floorPlan[k, j];


				}
				show += "\n";
			}
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
			return locationList;

		}

		//In a roulette wheel selection, the circular wheel is divided. The region of the wheel which comes in front of the fixed point is chosen as the parent. 
		//to randomly select parent according to the probabilities of the population fitness scores of the populations.
		public int RouletteWheelSelection()
		{
			double total = 0;
			double random;
			int selectedGenome = 0;
			double prob = 0;

			List<double> probabiltyofPopulation = new List<double>();
			for (int i = 0; i < populationSize; i++)
			{
				total = total + genomes[i].populationFitnessScore;
			}

			//calculate probabilty of each population design
			for (int i = 0; i < populationSize; i++)
			{
				prob = genomes[i].populationFitnessScore / total;
				probabiltyofPopulation.Add(prob);
				Debug.Log(probabiltyofPopulation[i]);
			}
			random = UnityEngine.Random.value; // (0,1]

			for (int i = 0; i < populationSize; i++)
			{
				if (random <= probabiltyofPopulation[i])
				{
					selectedGenome = i;
					break;
				}
				else if (i < populationSize - 1)
				{
					probabiltyofPopulation[i + 1] = probabiltyofPopulation[i + 1] + probabiltyofPopulation[i];
				}
			}
			return selectedGenome;
		}

		//Crossing function of mom and dad's furniture to create babies according to their fitness scores.
		//baby1 will have the furniture with the highest score.
		//baby2 will have the furniture with the lowest score.
		public void Crossover(Genome mom, Genome dad, Genome baby1, Genome baby2, List<Furniture> furnitureList)
		{
			int size = furnitureList.Count;

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if (mom.furnitureListGenom[i].FurnitureID == dad.furnitureListGenom[j].FurnitureID)
					{
						if (momFurnitureGeneticLocations[i].FitnessScore >= dadFurnitureGeneticLocations[j].FitnessScore) //We will assign the location of the furniture to the baby, whichever parent has the greatest fitness score.
						{
							baby1.newOne.Add(new Furniture() { FurnitureID = mom.furnitureListGenom[i].FurnitureID, Xdimension = mom.furnitureListGenom[i].Xdimension, Zdimension = mom.furnitureListGenom[i].Zdimension });
						

							//these 2 lines for babies' Genome type
							baby1.xcoordinatebaby.Add(momFurnitureGeneticLocations[i].StartX);
							baby1.ycoordinatebaby.Add(momFurnitureGeneticLocations[i].StartY);

							//these 2 lines for babies' FurnitureGenetic Location type

							baby1FurnitureGeneticLocations.Add(new FurnitureGeneticLocation()
							{
								FurnitureID = momFurnitureGeneticLocations[i].FurnitureID,
								StartX = momFurnitureGeneticLocations[i].StartX,
								StartY = momFurnitureGeneticLocations[i].StartY,
								FinishX = momFurnitureGeneticLocations[i].FinishX,
								FinishY = momFurnitureGeneticLocations[i].FinishY,
								CenterX = momFurnitureGeneticLocations[i].CenterX,
								CenterY = momFurnitureGeneticLocations[i].CenterY,
								FitnessScore = momFurnitureGeneticLocations[i].FitnessScore,
								WallName = momFurnitureGeneticLocations[i].WallName	

							});


							baby1.populationFitnessScore = baby1.populationFitnessScore + baby1FurnitureGeneticLocations[i].FitnessScore;


							baby2.xcoordinatebaby.Add(dadFurnitureGeneticLocations[j].StartX);
							baby2.ycoordinatebaby.Add(dadFurnitureGeneticLocations[j].StartY);
							
							baby2FurnitureGeneticLocations.Add(new FurnitureGeneticLocation()
							{
								FurnitureID = dadFurnitureGeneticLocations[j].FurnitureID,
								StartX = dadFurnitureGeneticLocations[j].StartX,
								StartY = dadFurnitureGeneticLocations[j].StartY,
								FinishX = dadFurnitureGeneticLocations[j].FinishX,
								FinishY = dadFurnitureGeneticLocations[j].FinishY,
								CenterX = dadFurnitureGeneticLocations[j].CenterX,
								CenterY = dadFurnitureGeneticLocations[j].CenterY,
								FitnessScore = dadFurnitureGeneticLocations[j].FitnessScore,
								WallName = dadFurnitureGeneticLocations[j].WallName	

							});

						}
						else
						{
							baby1.newOne.Add(new Furniture() { FurnitureID = dad.furnitureListGenom[j].FurnitureID, Xdimension = dad.furnitureListGenom[j].Xdimension, Zdimension = dad.furnitureListGenom[j].Zdimension });
							baby1FurnitureGeneticLocations.Add(new FurnitureGeneticLocation()
							{
								FurnitureID = dadFurnitureGeneticLocations[j].FurnitureID,
								StartX = dadFurnitureGeneticLocations[j].StartX,
								StartY = dadFurnitureGeneticLocations[j].StartY,
								FinishX = dadFurnitureGeneticLocations[j].FinishX,
								FinishY = dadFurnitureGeneticLocations[j].FinishY,
								CenterX = dadFurnitureGeneticLocations[j].CenterX,
								CenterY = dadFurnitureGeneticLocations[j].CenterY,
								FitnessScore = dadFurnitureGeneticLocations[j].FitnessScore,
								WallName = dadFurnitureGeneticLocations[j].WallName	
							});

							baby1.xcoordinatebaby.Add(dadFurnitureGeneticLocations[j].StartX);
							baby1.ycoordinatebaby.Add(dadFurnitureGeneticLocations[j].StartY);

							baby1.populationFitnessScore = baby1.populationFitnessScore + baby1FurnitureGeneticLocations[i].FitnessScore;

							baby2.xcoordinatebaby.Add(momFurnitureGeneticLocations[i].StartX);
							baby2.ycoordinatebaby.Add(momFurnitureGeneticLocations[i].StartY);

							baby2FurnitureGeneticLocations.Add(new FurnitureGeneticLocation()
							{
								FurnitureID = momFurnitureGeneticLocations[i].FurnitureID,
								StartX = momFurnitureGeneticLocations[i].StartX,
								StartY = momFurnitureGeneticLocations[i].StartY,
								FinishX = momFurnitureGeneticLocations[i].FinishX,
								FinishY = momFurnitureGeneticLocations[i].FinishY,
								CenterX = momFurnitureGeneticLocations[i].CenterX,
								CenterY = momFurnitureGeneticLocations[i].CenterY,
								FitnessScore = momFurnitureGeneticLocations[i].FitnessScore,
								WallName = momFurnitureGeneticLocations[i].WallName	
							});

						}

						break;
					}
				}
			}
		}


		// since we already assigned the position of the furniture when we created the design, 
		// we want to empty it before we want to change it's rotation.
		public void emptyPreviousLocationForXY(FurnitureGeneticLocation furnitureGeneticLocation, string[,] floorPlan)
		{
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
		public int rotateRandomFurniture(List<FurnitureGeneticLocation> furnitureGeneticLocations, string[,] floorPlan, int coordinate1, int coordinate2, int indexVal)
		{
			int furnitureID = indexVal; //= random.Next(0, furnitureGeneticLocations.Count - 1);
			Debug.Log("First Degree: " + furnitureGeneticLocations[furnitureID].Degree);

			int canBeRotated = 0;
			if (furnitureGeneticLocations[furnitureID].Degree == 0 || furnitureGeneticLocations[furnitureID].Degree == 1)
			{
				if (furnitureGeneticLocations[furnitureID].WallName == "W1") // wall on the bottom
				{
					Debug.Log("Wall1 rotation");

				//	emptyPreviousLocationForXY(furnitureGeneticLocations[furnitureID], floorPlan);
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
					if (canBeRotated == 0)
					{
						for (int j = furnitureGeneticLocations[furnitureID].StartY; j < furnitureGeneticLocations[furnitureID].FinishY; j++)
						{
							floorPlan[furnitureGeneticLocations[furnitureID].StartX - 1, j] = "X";
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
					furnitureGeneticLocations[furnitureID].XPositionStartX = furnitureGeneticLocations[furnitureID].StartX - 1;
					furnitureGeneticLocations[furnitureID].XPositionFinishX = furnitureGeneticLocations[furnitureID].StartX;
					furnitureGeneticLocations[furnitureID].XPositionStartY = furnitureGeneticLocations[furnitureID].StartY;
					furnitureGeneticLocations[furnitureID].XPositionFinishY = furnitureGeneticLocations[furnitureID].FinishY;
					furnitureGeneticLocations[furnitureID].YPositionStartX = furnitureGeneticLocations[furnitureID].StartX - 6;
					furnitureGeneticLocations[furnitureID].YPositionFinishX = furnitureGeneticLocations[furnitureID].StartX - 1;
					furnitureGeneticLocations[furnitureID].YPositionStartY = furnitureGeneticLocations[furnitureID].StartY - 6;
					furnitureGeneticLocations[furnitureID].YPositionFinishY = furnitureGeneticLocations[furnitureID].FinishY;
					furnitureGeneticLocations[furnitureID].Degree = 180;
					Debug.Log("Wall1 rotation " + furnitureGeneticLocations[furnitureID].Degree);


				}
				else if (furnitureGeneticLocations[furnitureID].WallName == "W2") // right-side wall
				{
					// Start positions of poth x and y will stay the same, later their finish positions will change according to their width
					Debug.Log("Wall2 rotation");

					int XValue = furnitureGeneticLocations[furnitureID].StartX + (furnitureGeneticLocations[furnitureID].FinishY - furnitureGeneticLocations[furnitureID].StartY);
					int YValue = furnitureGeneticLocations[furnitureID].StartY + (furnitureGeneticLocations[furnitureID].FinishX - furnitureGeneticLocations[furnitureID].StartX);

					if (XValue < coordinate1 && YValue < coordinate2)
					{
						// first empty the area that it currently allocates
					//	emptyAllLocations(furnitureGeneticLocations[furnitureID], floorPlan);

						int newFinishX, newFinishY;

						newFinishX = XValue;
						newFinishY = YValue;

						furnitureGeneticLocations[indexVal].FinishX = newFinishX;
						furnitureGeneticLocations[indexVal].FinishY = newFinishY;

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
						furnitureGeneticLocations[furnitureID].Degree = 90;

						Debug.Log("Wall2 rotation " + furnitureGeneticLocations[furnitureID].Degree);

					}
				}
				else if (furnitureGeneticLocations[furnitureID].WallName == "W3") // wall on the top
				{
					Debug.Log("Wall3 rotation");

					furnitureGeneticLocations[furnitureID].Degree = 0;

					// we don't want to turn it around since it is already aligned
				}
				else if (furnitureGeneticLocations[furnitureID].WallName == "W4") // wall on the left side
				{
					Debug.Log("Wall4 rotation");

					// Start positions of poth x and y will stay the same, later their finish positions will change according to their width

					int XValue = furnitureGeneticLocations[furnitureID].StartX + (furnitureGeneticLocations[furnitureID].FinishY - furnitureGeneticLocations[furnitureID].StartY);
					int YValue = furnitureGeneticLocations[furnitureID].StartY + (furnitureGeneticLocations[furnitureID].FinishX - furnitureGeneticLocations[furnitureID].StartX);

					if (XValue < coordinate1 && YValue < coordinate2)
					{
						// first empty the area that it currently allocates
				//		emptyAllLocations(furnitureGeneticLocations[furnitureID], floorPlan);

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
						furnitureGeneticLocations[furnitureID].Degree = 270;
						Debug.Log("Wall4 rotation " + furnitureGeneticLocations[furnitureID].Degree);



					}
				}
			}
			return furnitureGeneticLocations[furnitureID].Degree;
		}

		public double distanceFromWalls(int startX, int startY, int finishX, int finishY, int coordinate1, int coordinate2)
		{
			double score = 0;
			int value1 = 0, value2 = 0, value3 = 0, value4 = 0;
			int roomCenterX = coordinate1 / 2, roomCenterY = coordinate2 / 2;
			int centerX = finishX + startX / 2;
			int centerY = finishY + startY / 2;

			value1 = (int)(coordinate2 - finishY); //wall1 
			Debug.Log("Value1" + value1);

			// checking the distance of the furniture to x axis
			value2 = (int)(coordinate1 - finishX); //wall2
			Debug.Log("Value2" + value2);

			// checking the distance of the furniture to x-y axis (y is in the upper part)
			value3 = (int)(startY); //wall3
			Debug.Log("Value3" + value3);

			// checking the distance of the furniture to x-y axis (x is in the upper part)
			value4 = (int)(startX); //wall4
			Debug.Log("Value4" + value4);

			int selectedFormula = 90000;
			int formulaNum = 0;

			int[] myNum = { value1, value2, value3, value4 };

			for (int i = 0; i < myNum.Length; i++)
			{
				if (selectedFormula > myNum[i])
				{
					selectedFormula = myNum[i];
					formulaNum = i + 1;
				}
			}

			score = FindFitnessScoreWallDistance(startX, startY, finishX, finishY, coordinate1, coordinate2, formulaNum);

			Debug.Log("score in dist " + score);
			if (formulaNum == 1)
			{
				ClassWallName = "W2";
			}
			else if (formulaNum == 2)
			{
				ClassWallName = "W1";
			}
			else if (formulaNum == 3)
			{
				ClassWallName = "W4";
			}
			else
			{
				ClassWallName = "W3";
			}
			populationFurnitureGeneticLocations[wallCounter].WallName = ClassWallName;
			wallCounter++;
			return score;
		}
	/*	public string ReturnWallName(){
			string wall;
			wall = baby1FurnitureGeneticLocations[callCount].WallName;
			callCount++;
			return wall;
		}
	*/
		// this code is used to evaluate the value returned after formulas used to create a cost value upto 1
		public double FindFitnessScoreWallDistance(int startX, int startY, int finishX, int finishY, int coordinate1, int coordinate2, int formulaNum)
		{
			int fitnessScore;
			double rate = 0.0; 
			if (formulaNum == 1) // w1
			{
				//fitnessScore = (int)(roomCenterX / Math.Sqrt(2));
				rate = ((double) finishY/coordinate1);
			}
			else if (formulaNum == 2)
			{
			//	fitnessScore = (int)(centerX / Math.Sqrt(2));
				rate = (double)(finishX/coordinate2);
			}
			else if (formulaNum == 3)
			{
				//	fitnessScore = (int)((centerX + 2 * centerY) / Math.Sqrt(2));
				rate = ((double) Math.Abs(startY - coordinate1) / coordinate1);
			}
			else
			{
				//fitnessScore = (int)((centerY + 2 * centerX) / Math.Sqrt(2));
				rate = ((double)Math.Abs(startX - coordinate2) / coordinate2); //starty deydi hilalde
			}

			Debug.Log( " Rate = " + rate.ToString());
			rate = rate / 10;
			return rate;
		}

		//to calculate fitness score of each furniture in one population and also total fitness score of one population
		public double CalculateTotalFitnessScores(List<FurnitureGeneticLocation> populationFurnitureGeneticLocations, List<Furniture> furnitureList, int coordinate1, int coordinate2)
		{
			//total fitness score of all furniture in a design
			
			int startX, finishX, startY, finishY;
			totalFitnessScore = 0;

			for (int i = 0; i < furnitureList.Count; i++)
			{
				startX = populationFurnitureGeneticLocations[i].StartX;
				finishX = populationFurnitureGeneticLocations[i].FinishX;
				startY = populationFurnitureGeneticLocations[i].StartY;
				finishY = populationFurnitureGeneticLocations[i].FinishY;
				populationFurnitureGeneticLocations[i].FitnessScore = distanceFromWalls(startX, startY, finishX, finishY, coordinate1, coordinate2); //bir desingdeki bir furnitureın fitness scoreu

				Debug.Log("population fitness score" + populationFurnitureGeneticLocations[i].FitnessScore);
				totalFitnessScore = totalFitnessScore + populationFurnitureGeneticLocations[i].FitnessScore;

			}
			wallCounter = 0; //to reset wall counter 
			return (totalFitnessScore/( furnitureList.Count*10));

		}

		//to randomly move one furniture of one population(design)
		public void MoveRandomFurniture(int coordinate1, int coordinate2, List<Furniture> furnitureList)
		{
			int startPosX = 0;
			int finishPosX = 0;
			int startPosY = 0;
			int finishPosY = 0;

			int length = baby1.newOne.Count; // ????? RANDOMLUKTA SONUNCU DAHİL DEĞİL
			int index = random.Next(0, length);


			int i = 0;
			int xcoordinatetoBaby = random.Next(0, coordinate1);
			int ycoordinatetoBaby = random.Next(0, coordinate2);

			int howManyCellsX = (int)furnitureList[i].Xdimension;
			int howManyCellsY = (int)furnitureList[i].Zdimension;

			startPosX = xcoordinatetoBaby;
			finishPosX = xcoordinatetoBaby + howManyCellsX;
			startPosY = ycoordinatetoBaby;
			finishPosY = ycoordinatetoBaby + howManyCellsY;
			int centerX = (startPosX + finishPosX) / 2;
			int centerY = (startPosY + finishPosY) / 2;

			int capacityminusone = (int)furnitureList.Capacity; //BAKILMALI

			baby1.xcoordinatebaby[index] = xcoordinatetoBaby;
			baby1.ycoordinatebaby[index] = ycoordinatetoBaby;

			Debug.Log("index" + index);

			baby1FurnitureGeneticLocations.RemoveAt(index); //to clear index from the list
			baby1FurnitureGeneticLocations.Insert(index, new FurnitureGeneticLocation() { FurnitureID = baby1.newOne[index].FurnitureID, StartX = startPosX, FinishX = finishPosX, CenterX = centerX, StartY = startPosY, CenterY = centerY, FinishY = finishPosY });

		}
		/*
		public string wallNameUpdate(int startX, int startY, int finishX, int finishY, int coordinate1, int coordinate2)
		{
			int value1 = 0, value2 = 0, value3 = 0, value4 = 0;
			int roomCenterX = coordinate1 / 2, roomCenterY = coordinate2 / 2;
			int centerX = finishX + startX / 2;
			int centerY = finishY + startY / 2;
			Debug.Log("coordinate1: " + coordinate1 + " coordinate2: " + coordinate2);
			// checking the distance of the furniture to y axis // 
			value1 = (int)(coordinate1-finishY); //wall1 
			Debug.Log("Value1" + value1);

			// checking the distance of the furniture to x axis
			value2 = (int)(coordinate2-finishX); //wall2
			Debug.Log("Value2" + value2);

			// checking the distance of the furniture to x-y axis (y is in the upper part)
			value3 = (int)(startY); //wall3
			Debug.Log("Value3" + value3);

			// checking the distance of the furniture to x-y axis (x is in the upper part)
			value4 = (int)(startX); //wall4
			Debug.Log("Value4" + value4);

			int selectedFormula = 90000;
			int formulaNum = 0;

			int[] myNum = { value1, value2, value3, value4 };

			for (int i = 0; i < myNum.Length; i++)
			{
				if (selectedFormula > myNum[i])
				{
					selectedFormula = myNum[i];
					formulaNum = i + 1;
				}
			}

			if (formulaNum == 1)
			{
				ClassWallName = "W1";
			}
			else if (formulaNum == 2)
			{
				ClassWallName = "W2";
			}
			else if (formulaNum == 3)
			{
				ClassWallName = "W3";
			}
			else
			{
				ClassWallName = "W4";
			}
			return ClassWallName;
		}*/
	}
}