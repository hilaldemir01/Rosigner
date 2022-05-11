using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
		public double bestFitnessScore;
		public double totalFitnessScore;
		public int generation;
		public bool busy;
		public int coordinate1;
		public int coordinate2;
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
		}
		// creating a matrix for the purpose of creating designs on it
		public void startMatrix()
		{
			coordinate1 = (int)wallList[0].WallLength;
			coordinate2 = (int)wallList[1].WallLength;
			floorPlan = new string[coordinate1, coordinate2];

		}
		public void Run()
		{
			CreateStartPopulation();
			busy = true;
		}
		// this function will be used to define the front part of furniture 
		public void defineFrontPart()
        {
			for(int i = 0; i < furnitureList.Count; i++)
            {
				// first, we are going to divide the measurements of a furniture 
            }
        }
		// step number 1
		public void CreateStartPopulation()
		{
			genomes.Clear(); // clear out any genomes at first

			for (int i = 0; i < populationSize; i++)
			{ // iterate through 10
				//Genome baby = new Genome(furnitureList,coordinate1,coordinate2, floorPlan); // chromosomeLength = 5
				//genomes.Add(baby);
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
		// step number 2
		// rank fitness
		public void UpdateFitnessScores()
		{
			fittestGenome = 0;
			bestFitnessScore = 0;
			totalFitnessScore = 0;

			for (int i = 0; i < populationSize; i++)
			{
				List<int> directions = Decode(genomes[i].bits);
				// using the method from the mazeController
				genomes[i].fitness = TestRoute(directions);

				totalFitnessScore += genomes[i].fitness;

				if (genomes[i].fitness > bestFitnessScore)
				{
					bestFitnessScore = genomes[i].fitness;
					fittestGenome = i;
					// Has chromosome found the exit?
					if (genomes[i].fitness == 1)
					{
						busy = false; // stop the run
						return;
					}
				}
			}
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
			if (!busy) return;
			UpdateFitnessScores();

			if (!busy)
			{
				lastGenerationGenomes.Clear();
				lastGenerationGenomes.AddRange(genomes);
				return;
			}

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
