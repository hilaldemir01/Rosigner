using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using Random=UnityEngine.Random;


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
		public int coordinate1;
		public int coordinate2;
		 public int momFitnessScore = 0;
        public int dadFitnessScore = 0;
        public int xcoordinate = 0;
        public int ycoordinate = 0;
		public int count = 0; //-1
		List<FurnitureGeneticLocation> momFurnitureGeneticLocations;
		List<FurnitureGeneticLocation> dadFurnitureGeneticLocations;
		List<FurnitureGeneticLocation> baby1FurnitureGeneticLocations;
		List<FurnitureGeneticLocation> baby2FurnitureGeneticLocations;
		List<FurnitureGeneticLocation> populationFurnitureGeneticLocations;
		List<FurnitureGeneticLocation> allPopulationFurnitureGeneticLocations;
		public int totalFitnessScore = 0;
		List<int> totalFitnessScoreList;
		public GeneticAlgorithm()
		{
			busy = false;
			startMatrix();
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
			populationFurnitureGeneticLocations = new List<FurnitureGeneticLocation>();
			allPopulationFurnitureGeneticLocations = new List<FurnitureGeneticLocation>();
			totalFitnessScoreList = new List<int>();
		}
		// creating a matrix for the purpose of creating designs on it
		public void startMatrix()
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
		// this function will be used to define the front part of furniture 
		public void defineFrontPart()
        {
			for(int i = 0; i < furnitureList.Count; i++)
            {
				// first, we are going to divide the measurements of a furniture 
            }
        }
		// step number 1
		public void CreateStartPopulation(int coordinate1,int coordinate2,string[,] floorPlan, List<RoomStructure> roomStructureList, List<Furniture> furnitureList)
		{
			genomes.Clear(); // clear out any genomes at first
			int a = 0;
			
			for(int m = 0; m < populationSize; m++ ){
				 //we created 10 design
				Genome population = new Genome();
				populationFurnitureGeneticLocations = population.GenomeInit(coordinate1, coordinate2, floorPlan, roomStructuresList, furnitureList); //populationFurnitureGeneticLocations is just one design
				allPopulationFurnitureGeneticLocations.AddRange(populationFurnitureGeneticLocations); //allPopulationFurnitureGeneticLocations is merge of populationFurnitureGeneticLocations
				genomes.Add(population); //?? //we will put population designs in genomes
				int value = CalculateTotalFitnessScores(furnitureList);
				totalFitnessScoreList.Add(value); // total fitness scores of all designs (for ex 10 total fitness score)
				Debug.Log("say"+a);
				Debug.Log("genomes count"+genomes.Count);
				a++;	
			}
		
			/* Genome momGenome = new Genome();
			momFurnitureGeneticLocations = momGenome.GenomeInit(coordinate1, coordinate2, floorPlan, roomStructuresList, furnitureList);
			UpdateFitnessScores(furnitureList);//to calculate mom's furniture's fitness scores
			*/
			// StartCoroutine(db.TempFurnitureLocation(momFurnitureGeneticLocations));
			
			/*Genome dadGenome = new Genome();
			dadFurnitureGeneticLocations = dadGenome.GenomeInit(coordinate1, coordinate2, floorPlan, roomStructuresList, furnitureList);
			UpdateFitnessScores(furnitureList); //to calculate dad's furniture's fitness scores
			*/
			// StartCoroutine(db.TempFurnitureLocation(dadFurnitureGeneticLocations));

			int selectedGenomeIndex;
		
			selectedGenomeIndex = RouletteWheelSelection();
			Debug.Log("selectedGenomeIndex"+selectedGenomeIndex);
			Genome momGenome = genomes[selectedGenomeIndex];
			Debug.Log("mom txt num"+selectedGenomeIndex);
			Debug.Log("pop"+populationFurnitureGeneticLocations.Count);
			for(int n =furnitureList.Count * selectedGenomeIndex; n < ((selectedGenomeIndex * furnitureList.Count) +furnitureList.Count) ; n++){
				momFurnitureGeneticLocations.Add(allPopulationFurnitureGeneticLocations[n]);
				Debug.Log("burası n"+n);
				Debug.Log("burası furniturecount"+n);
			}
			
			Debug.Log("hello");
			selectedGenomeIndex = RouletteWheelSelection();
			Genome dadGenome = genomes[selectedGenomeIndex];
			Debug.Log("dad txt num"+selectedGenomeIndex);
			for(int m =furnitureList.Count * selectedGenomeIndex; m < ((selectedGenomeIndex * furnitureList.Count) +furnitureList.Count) ; m++){
				dadFurnitureGeneticLocations.Add(allPopulationFurnitureGeneticLocations[m]);
			}
			Debug.Log("gelebildik mi");
			//burası fora göre değişir
			Genome baby1 = new Genome(); // chromosomeLength = 5
			Genome baby2 = new Genome(); // chromosomeLength = 5
			Crossover(momGenome, dadGenome, baby1, baby2,furnitureList);

			//genomes.Add(baby1);
			//genomes.Add(baby2);
			
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
			int i = 0;
			while (i < capacityminusone)
			{
				int startPosX = 0;
				int finishPosX = 0;
				int startPosY = 0;
				int finishPosY = 0;
				var canBePlaced = 0;
				
				xcoordinate = baby1.xcoordinatebaby[i];
				ycoordinate = baby1.ycoordinatebaby[i];
				Debug.Log("deneme coordinate xco"+xcoordinate +"deneme coordinate yco"+ ycoordinate);
				int howManyCellsX = (int)baby1.newOne[i].Xdimension/10;
				int howManyCellsY = (int)baby1.newOne[i].Zdimension/10;
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
			for (int k = 0; k < 100; k++)//düzelmesi lazım
			{
				for (int j = 0; j < 100; j++)//düzelmesi lazım
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
		public void Crossover(Genome mom, Genome dad, Genome baby1, Genome baby2,List<Furniture> furnitureList) {
			Debug.Log("mom.furnitureList genom size"+ mom.furnitureListGenom.Capacity);
            for (int i = 0; i <mom.furnitureListGenom.Capacity-1; i++)
                {
                    for(int j = 0; j < mom.furnitureListGenom.Capacity-1; j++)
                    {
                        if(mom.furnitureListGenom[i].FurnitureID == dad.furnitureListGenom[j].FurnitureID)
                        {
                            if (momFurnitureGeneticLocations[i].FitnessScore >= dadFurnitureGeneticLocations[j].FitnessScore) //We will assign the location of the furniture to the baby, whichever parent has the greatest fitness score.
                            {
                                baby1.newOne.Add(new Furniture() { FurnitureID = mom.furnitureListGenom[i].FurnitureID,Xdimension = mom.furnitureListGenom[i].Xdimension, Zdimension = mom.furnitureListGenom[i].Zdimension });
                            //baby1 will have the furniture with the highest score.
                            //baby2 will have the furniture with the lowest score.
                                
								//these 2 lines for babies' Genome type
								baby1.xcoordinatebaby.Add(momFurnitureGeneticLocations[i].StartX);
								baby1.ycoordinatebaby.Add(momFurnitureGeneticLocations[i].StartY);
								//these 2 lines for babies' FurnitureGenetic Location type
								
								
								Debug.Log("FITMom"+momFurnitureGeneticLocations[i].FitnessScore );
								baby1FurnitureGeneticLocations.Add(new FurnitureGeneticLocation(){FurnitureID = momFurnitureGeneticLocations[i].FurnitureID,StartX=momFurnitureGeneticLocations[i].StartX,
									StartY = momFurnitureGeneticLocations[i].StartY, FinishX= momFurnitureGeneticLocations[i].FinishX,FinishY =momFurnitureGeneticLocations[i].FinishY,
								 	CenterX =momFurnitureGeneticLocations[i].CenterX, CenterY = momFurnitureGeneticLocations[i].CenterY, FitnessScore =  momFurnitureGeneticLocations[i].FitnessScore});
                               	Debug.Log("furID"+baby1FurnitureGeneticLocations[i].FurnitureID);
								Debug.Log("deneme coordinate mom startx "+momFurnitureGeneticLocations[i].StartX+"starty"+momFurnitureGeneticLocations[i].StartY);
								
								Debug.Log("deneme coordinate baby startx "+baby1FurnitureGeneticLocations[i].StartX+"starty"+baby1FurnitureGeneticLocations[i].StartY);

                                baby2.xcoordinatebaby.Add(dadFurnitureGeneticLocations[j].StartX);
                                baby2.ycoordinatebaby.Add(dadFurnitureGeneticLocations[j].StartY);

								baby2FurnitureGeneticLocations.Add(new FurnitureGeneticLocation(){FurnitureID = dadFurnitureGeneticLocations[j].FurnitureID,StartX = dadFurnitureGeneticLocations[j].StartX,
								StartY = dadFurnitureGeneticLocations[j].StartY, FinishX = dadFurnitureGeneticLocations[j].FinishX,FinishY= dadFurnitureGeneticLocations[j].FinishY, CenterX = dadFurnitureGeneticLocations[j].CenterX,
								CenterY=dadFurnitureGeneticLocations[j].CenterY,FitnessScore = dadFurnitureGeneticLocations[j].FitnessScore });


                            }
                            else
                            {
								Debug.Log("FITDad"+dadFurnitureGeneticLocations[i].FitnessScore );
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

		public int RouletteWheelSelection()
		{
			int total = 0;
			double random;
			int selectedGenome = 0;
			double prob = 0;

			List<double> probabiltyofPopulation = new List<double>();
			for(int i = 0; i< populationSize ;i++){
				total += totalFitnessScoreList[i];
			}

			//calculate probabilty of each population design
			for(int i = 0; i<populationSize;i++){
				Debug.Log("total"+total);
				prob = (double)totalFitnessScoreList[i] / total;
				probabiltyofPopulation.Add(prob);
				Debug.Log(probabiltyofPopulation[i]);
			}
			random = UnityEngine.Random.value; // (0,1]

			Debug.Log("random"+random);
			for(int i = 0; i<populationSize;i++){
				if(random <= probabiltyofPopulation[i]){
					Debug.Log("probabiltyofPopulation"+ probabiltyofPopulation[i]);
					selectedGenome = i;
					Debug.Log("selectedGenome"+selectedGenome);
					break;
				}else if(i < populationSize-1){
					probabiltyofPopulation[i+1] = probabiltyofPopulation[i+1] +probabiltyofPopulation[i];	
				}else{
					selectedGenome = populationSize - 1;
					Debug.Log("else e girdi rezillik");
				}
			}
			return selectedGenome;
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
          
            score = FindFitnessScoreWallDistance(centerX, centerY, selectedFormula, formulaNum, roomCenterX, roomCenterY);
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
		

 		public int CalculateTotalFitnessScores(List<Furniture> furnitureList){
			 //total fitness score of all furniture in a design
			int startX, finishX ,startY,finishY;
			totalFitnessScore = 0;
			
			for(int i = 0; i < furnitureList.Count; i++){
				startX =populationFurnitureGeneticLocations[i].StartX;
				finishX =  populationFurnitureGeneticLocations[i].FinishX;
				startY = populationFurnitureGeneticLocations[i].StartY;
				finishY = populationFurnitureGeneticLocations[i].FinishY;
				populationFurnitureGeneticLocations[i].FitnessScore =  distanceFromWalls(startX, startY, finishX, finishY); //bir desingdeki bir furnitureın fitness scoreu
				
				totalFitnessScore = totalFitnessScore + populationFurnitureGeneticLocations[i].FitnessScore; // to get total fitness score of 1 design
				Debug.Log("totalfitnessScore"+totalFitnessScore);
			}
			genomCount++;

			return totalFitnessScore;
		
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
		/*public void Epoch()
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
		}*/
	}
}