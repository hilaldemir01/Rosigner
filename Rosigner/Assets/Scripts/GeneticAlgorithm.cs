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
		public int babyCounter = 0;
		List<FurnitureGeneticLocation> momFurnitureGeneticLocations;
		List<FurnitureGeneticLocation> dadFurnitureGeneticLocations;
		List<FurnitureGeneticLocation> baby1FurnitureGeneticLocations;
		List<FurnitureGeneticLocation> baby2FurnitureGeneticLocations;
		List<FurnitureGeneticLocation> populationFurnitureGeneticLocations;
		List<FurnitureGeneticLocation> allPopulationFurnitureGeneticLocations;
		public double totalFitnessScore = 0;
		List<double> totalFitnessScoreList;
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
			populationFurnitureGeneticLocations = new List<FurnitureGeneticLocation>(); //it is for all furniture of population
			allPopulationFurnitureGeneticLocations = new List<FurnitureGeneticLocation>(); //it is for all furniture of all population
			totalFitnessScoreList = new List<double>();
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
				//allPopulationFurnitureGeneticLocations is merge of populationFurnitureGeneticLocations
				genomes.Add(population); //?? //we will put population designs in genomes
				double value = CalculateTotalFitnessScores(furnitureList);
				//totalFitnessScoreList.Add(value); // total fitness scores of all designs (for ex 10 total fitness score)
				genomes[m].populationFitnessScore = value; //total fitness score
				allPopulationFurnitureGeneticLocations.AddRange(populationFurnitureGeneticLocations); 
				
				
				Debug.Log("genom populationfitnessscore"+genomes[m].populationFitnessScore );
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
			Debug.Log("populationFurnitureGeneticLocations count"+ populationFurnitureGeneticLocations.Count);
			Debug.Log("allPopulationFurnitureGeneticLocations count"+ allPopulationFurnitureGeneticLocations.Count);

			

			selectedGenomeIndex = RouletteWheelSelection();
			Genome momGenome = genomes[selectedGenomeIndex];
			Debug.Log("mom txt num"+selectedGenomeIndex);
			for(int n =furnitureList.Count * selectedGenomeIndex; n < ((selectedGenomeIndex * furnitureList.Count) +furnitureList.Count) ; n++){
				momFurnitureGeneticLocations.Add(allPopulationFurnitureGeneticLocations[n]);
			}

			selectedGenomeIndex = RouletteWheelSelection();
			Genome dadGenome = genomes[selectedGenomeIndex];
			Debug.Log("dad txt num"+selectedGenomeIndex);

			for(int m =furnitureList.Count * selectedGenomeIndex; m < ((selectedGenomeIndex * furnitureList.Count) +furnitureList.Count) ; m++){
				dadFurnitureGeneticLocations.Add(allPopulationFurnitureGeneticLocations[m]);
			}


			//burası fora göre değişir
			Genome baby1 = new Genome(); // chromosomeLength = 5
			Genome baby2 = new Genome(); // chromosomeLength = 5
			Crossover(momGenome, dadGenome, baby1, baby2,furnitureList);
			Debug.Log( "baby1 pop fitnessscore"+ baby1.populationFitnessScore);
			int forsize = momGenome.furnitureListGenom.Capacity-1 ;
			//clearlamadan önce database'e yazma işleme yaparız
			while(baby1.populationFitnessScore < 0.90){
				Debug.Log("burası"+momGenome.furnitureListGenom[0].FurnitureID);
				Debug.Log("burası"+momGenome.furnitureListGenom[1].FurnitureID);
				Debug.Log("burası"+momGenome.furnitureListGenom[2].FurnitureID);

				//momGenome.furnitureListGenom.Clear();
				//dadGenome.furnitureListGenom.Clear();
				
				Debug.Log("burası"+momGenome.furnitureListGenom[0].FurnitureID);
				Debug.Log("burası"+momGenome.furnitureListGenom[1].FurnitureID);
				Debug.Log("burası"+momGenome.furnitureListGenom[2].FurnitureID);
				
				momFurnitureGeneticLocations.Clear();
				dadFurnitureGeneticLocations.Clear();

				for(int t = 0; t < forsize ; t++){
					momGenome.furnitureListGenom.Add(new Furniture() { FurnitureID = baby1.newOne[t].FurnitureID,Xdimension = baby1.newOne[t].Xdimension, Zdimension = baby1.newOne[t].Zdimension });
					momFurnitureGeneticLocations.Add(baby1FurnitureGeneticLocations[t]);
				}
				Debug.Log("burası"+momGenome.furnitureListGenom[0].FurnitureID);
				Debug.Log("burası"+momGenome.furnitureListGenom[1].FurnitureID);
				Debug.Log("burası"+momGenome.furnitureListGenom[2].FurnitureID);
				
				selectedGenomeIndex = RouletteWheelSelection();
				dadGenome = genomes[selectedGenomeIndex];
				Debug.Log("fora selectedgenome"+selectedGenomeIndex);
				int beyza=0;
				for(int m =furnitureList.Count * selectedGenomeIndex; m < ((selectedGenomeIndex * furnitureList.Count) +furnitureList.Count) ; m++){
					Debug.Log("fora girdik mi");
					dadFurnitureGeneticLocations.Add(allPopulationFurnitureGeneticLocations[m]);
					Debug.Log("dadFurnitureGeneticLocations ALL fitness"+allPopulationFurnitureGeneticLocations[m].FitnessScore);
				}
				int dadSize= dadFurnitureGeneticLocations.Count;
				for(int m =dadSize-furnitureList.Count; m <dadSize; m++ ){ //getting last added
					Debug.Log("dadFurnitureGeneticLocations fitnessScore"+dadFurnitureGeneticLocations[m].FitnessScore);
				}

				Debug.Log("fordan çıktık mı");
				baby1.newOne.Clear();
				baby2.newOne.Clear();
				
				baby1.xcoordinatebaby.Clear();
				baby1.ycoordinatebaby.Clear();
				baby2.xcoordinatebaby.Clear();
				baby2.ycoordinatebaby.Clear();
				baby1FurnitureGeneticLocations.Clear();
				baby2FurnitureGeneticLocations.Clear();

				Crossover(momGenome, dadGenome, baby1, baby2,furnitureList);
				Debug.Log("geldik mi");
				babyCounter++;
				Debug.Log("babyCounter"+ babyCounter);
				Debug.Log( "baby1 pop fitnessscore while"+ baby1.populationFitnessScore);

				////oluşturduktan sonra oluşanlar gerekirse burda da db ye ekleme için yazılır temizliyoruz her defasında çünkü
			}
			if(baby1.populationFitnessScore >= 0.90){
				Debug.Log("Bitttiğğğğğğ");
			}
		

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
				int howManyCellsX = (int)baby1.newOne[i].Xdimension/10;
				int howManyCellsY = (int)baby1.newOne[i].Zdimension/10;

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
			int size = mom.furnitureListGenom.Capacity-1;
			Debug.Log("crossover");
			Debug.Log("SIZE"+size);
			
            for (int i = 0; i <3; i++)
                {
                    for(int j = 0; j < 3; j++)
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

								//baby1FurnitureGeneticLocations[i].populationFitnessScore = baby1FurnitureGeneticLocations[i].populationFitnessScore + baby1FurnitureGeneticLocations[i].FitnessScore;
                              	baby1.populationFitnessScore = baby1.populationFitnessScore + baby1FurnitureGeneticLocations[i].FitnessScore;
 
							   	Debug.Log("baby1 fitnessScore mom"+	baby1FurnitureGeneticLocations[i].FitnessScore);


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
								baby1FurnitureGeneticLocations.Add(new FurnitureGeneticLocation(){FurnitureID = dadFurnitureGeneticLocations[j].FurnitureID,StartX = dadFurnitureGeneticLocations[j].StartX,
								StartY = dadFurnitureGeneticLocations[j].StartY, FinishX = dadFurnitureGeneticLocations[j].FinishX,FinishY= dadFurnitureGeneticLocations[j].FinishY, CenterX = dadFurnitureGeneticLocations[j].CenterX,
								CenterY=dadFurnitureGeneticLocations[j].CenterY,FitnessScore = dadFurnitureGeneticLocations[j].FitnessScore });
		
                                baby1.xcoordinatebaby.Add(dadFurnitureGeneticLocations[j].StartX);
                                baby1.ycoordinatebaby.Add(dadFurnitureGeneticLocations[j].StartY);
                            
								baby1.populationFitnessScore = baby1.populationFitnessScore + baby1FurnitureGeneticLocations[i].FitnessScore;
								Debug.Log("baby1 fitnessScore dad"+	baby1FurnitureGeneticLocations[i].FitnessScore);
						
                                baby2.xcoordinatebaby.Add(momFurnitureGeneticLocations[i].StartX);
                                baby2.ycoordinatebaby.Add(momFurnitureGeneticLocations[i].StartY);
						
								baby2FurnitureGeneticLocations.Add(new FurnitureGeneticLocation(){FurnitureID = momFurnitureGeneticLocations[i].FurnitureID,StartX = momFurnitureGeneticLocations[i].StartX, StartY = momFurnitureGeneticLocations[i].StartY,
								FinishX = momFurnitureGeneticLocations[i].FinishX, FinishY = momFurnitureGeneticLocations[i].FinishY, CenterX= momFurnitureGeneticLocations[i].CenterX,CenterY=momFurnitureGeneticLocations[i].CenterY,
								FitnessScore = momFurnitureGeneticLocations[i].FitnessScore});
					
                        	}
							
							break;
                    	}
                    }
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
		//1 genome un 1 population score u var.
		public int RouletteWheelSelection()
		{
			double total = 0;
			double random;
			int selectedGenome = 0;
			double prob = 0;
			Debug.Log("girdi rw");
			Debug.Log("POP Co"+populationFurnitureGeneticLocations.Count);
			Debug.Log("ALL POP Co"+allPopulationFurnitureGeneticLocations.Count);
			Debug.Log("ALL fitnes"+allPopulationFurnitureGeneticLocations[0].FitnessScore );
			Debug.Log("ALL fitnes"+allPopulationFurnitureGeneticLocations[1].FitnessScore );
			List<double> probabiltyofPopulation = new List<double>();
			for(int i = 0; i< populationSize ;i++){
				//total += populationFurnitureGeneticLocations[i].populationFitnessScore;
				total = total+ genomes[i].populationFitnessScore;
				Debug.Log("girdi rw il for");
			}

			//calculate probabilty of each population design
			for(int i = 0; i<populationSize;i++){
				Debug.Log("total"+total);
				prob = genomes[i].populationFitnessScore / total;
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
		 public double distanceFromWalls(int startX, int startY, int finishX, int finishY)
        {
            double score =0;
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
		  public double FindFitnessScoreWallDistance(int centerX, int centerY, int selectedFormula, int formulaNum, int roomCenterX, int roomCenterY)
        {
            int fitnessScore;
            double rate = 0.0;
            if(formulaNum == 1)
            {
                fitnessScore = (int)(roomCenterX / Math.Sqrt(2));
                rate = ((double)fitnessScore * selectedFormula) / 1000000;
            }
            else if (formulaNum == 2)
            {
                fitnessScore = (int)(centerX / Math.Sqrt(2));
                rate = ((double)fitnessScore * selectedFormula) / 1000000;

            }
            else if (formulaNum == 3)
            {
                fitnessScore = (int)((centerX + 2 * centerY) / Math.Sqrt(2));
                rate = ((double)fitnessScore * selectedFormula) / 1000000;
            }
            else
            {
                fitnessScore = (int)((centerY + 2 * centerX) / Math.Sqrt(2));
                rate = ((double)fitnessScore * selectedFormula) / 1000000;
            }
            return rate;
        }
		
		/*public void UpdateFitnessScores(List<Furniture> furnitureList)
        {
            int score = 0;
			count++;
			Debug.Log("count"+count);
			int startX, finishX ,startY,finishY;
			Debug.Log("furnitureList count:"+furnitureList.Count);

			for(int i = 0; i <furnitureList.Count; i++){
				Debug.Log("kaç defa dönüyor");
				if(count % 2 == 0){
					Debug.Log("MOM");
					Debug.Log("startx"+ momFurnitureGeneticLocations[0].StartX);
					startX = momFurnitureGeneticLocations[i].StartX;
					finishX =  momFurnitureGeneticLocations[i].FinishX;
					startY = momFurnitureGeneticLocations[i].StartY;
					finishY =  momFurnitureGeneticLocations[i].FinishY;

					momFurnitureGeneticLocations[i].FitnessScore = distanceFromWalls(startX, startY, finishX, finishY);
					Debug.Log("mom fitness score"+momFurnitureGeneticLocations[i].FitnessScore);
					Debug.Log("finishX"+finishX);
					Debug.Log("startY"+startY);
					Debug.Log("finishY"+finishY);


				}else{
					Debug.Log("DAD");
					startX = dadFurnitureGeneticLocations[i].StartX;
					Debug.Log("dad fitness score"+dadFurnitureGeneticLocations[i].FitnessScore);
					finishX =  dadFurnitureGeneticLocations[i].FinishX;
					startY = dadFurnitureGeneticLocations[i].StartY;
					finishY =  dadFurnitureGeneticLocations[i].FinishY;
					dadFurnitureGeneticLocations[i].FitnessScore = distanceFromWalls(startX, startY, finishX, finishY);
					Debug.Log("dad fitness score"+dadFurnitureGeneticLocations[i].FitnessScore);

				}
			}
		}*/
 		public double CalculateTotalFitnessScores(List<Furniture> furnitureList){
			 //total fitness score of all furniture in a design
			int startX, finishX ,startY,finishY;
			totalFitnessScore = 0;
	

			for(int i = 0; i < furnitureList.Count; i++){
				startX =populationFurnitureGeneticLocations[i].StartX;
				finishX =  populationFurnitureGeneticLocations[i].FinishX;
				startY = populationFurnitureGeneticLocations[i].StartY;
				finishY = populationFurnitureGeneticLocations[i].FinishY;
				populationFurnitureGeneticLocations[i].FitnessScore =  distanceFromWalls(startX, startY, finishX, finishY); //bir desingdeki bir furnitureın fitness scoreu
				
				//totalFitnessScore = totalFitnessScore + populationFurnitureGeneticLocations[i].FitnessScore; // to get total fitness score of 1 design
				Debug.Log("population fitness score" + populationFurnitureGeneticLocations[i].FitnessScore);
				totalFitnessScore = totalFitnessScore + populationFurnitureGeneticLocations[i].FitnessScore;
			
			}
			genomCount++;
			Debug.Log("totalfitnessScore"+totalFitnessScore);
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



	}
}