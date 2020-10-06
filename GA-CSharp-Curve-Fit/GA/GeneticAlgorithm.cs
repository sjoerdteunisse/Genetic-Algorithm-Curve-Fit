using System;
using System.Collections.Generic;
using System.Linq;

namespace GA_CSharp_Curve_Fit.GA
{
    public class GeneticAlgorithm
    {
        private int _epochs;
        private float _elitism;
        private float _mutationRate;
        private int _populationSize;
        private Organism _bestSnapShot;
        private int _currentIndex = 0;
        private Random rnd = new Random();

        private List<List<Organism>> Generations_organisms;

        public GeneticAlgorithm(int populationSize, float mutationRate, int epochs, float elitism)
        {
            _epochs = epochs;
            _elitism = elitism;
            _mutationRate = mutationRate;
            _populationSize = populationSize;
            Generations_organisms = new List<List<Organism>>();
        }

        private void AddGeneration() => Generations_organisms.Add(new List<Organism>());

        public void InitializePopulation(int geneSize)
        {
            AddGeneration();

            for (int i = 0; i < _populationSize; i++)
            {
                var org = new Organism(geneSize, 20, 10);
                org.SetInitialValues();
                Generations_organisms[0].Add(org);
            }
        }

        public void Start(double[] x, double[] y) {
            InitializePopulation(6);
            
            for (int i = 0; i < _epochs; i++) {
                Console.WriteLine($"Epoch {i}");
                Fitness(x, y);

                var bestOrganism = Generations_organisms[_currentIndex].First();
                Console.WriteLine($"best {bestOrganism.FitLevel}");
                Console.WriteLine($"Values for current best organism values= {bestOrganism.Genomes[0].Value} {bestOrganism.Genomes[1].Value} {bestOrganism.Genomes[2].Value} {bestOrganism.Genomes[3].Value} {bestOrganism.Genomes[4].Value} {bestOrganism.Genomes[5].Value}");

                if(_bestSnapShot == null || bestOrganism.FitLevel < _bestSnapShot.FitLevel )
                    _bestSnapShot = bestOrganism;

                Console.WriteLine($"Values for best organism at fit ={ _bestSnapShot.FitLevel } values= {_bestSnapShot.Genomes[0].Value} {_bestSnapShot.Genomes[1].Value} {_bestSnapShot.Genomes[2].Value} {_bestSnapShot.Genomes[3].Value} {_bestSnapShot.Genomes[4].Value} {_bestSnapShot.Genomes[5].Value}");

                Rebreed();
            }


        }


        public void Rebreed()
        {
            AddGeneration();
            _currentIndex++;

            int lastIndex = _currentIndex - 1;
            
            //Calculate elitism count based on percentages
            int elitism = (int) _elitism * 100;
            //Add elitism; Best N generations with best genomes stay alive in the next generation. Best between 5-10%
            Generations_organisms[_currentIndex].AddRange(Generations_organisms[lastIndex].Take(elitism));

            for (int i = 0; i < _populationSize - elitism; i++) 
            {
                if (lastIndex >= 0 && Generations_organisms[_currentIndex] != null) {

                    var bestOrganisms = Generations_organisms[lastIndex].Take(10).ToArray();

                    var organism = new Organism(6, 20, 10);

                    var pos = rnd.Next(0, 3);
                    var pos2 = rnd.Next(0, 3);

                    organism.SetInitialValues();

                    for (int j = 0; j < 3; j++) {
                        organism.Genomes[j].Value = bestOrganisms[pos].Genomes[j].Value;
                    }

                    for (int j = 3; j < 6; j++)
                    {
                        organism.Genomes[j].Value = bestOrganisms[pos2].Genomes[j].Value;
                    }

                    organism.Mutate(0.02f);

                    Generations_organisms[_currentIndex].Add(organism);
                }
            }
        }

        public void Fitness(double[] x, double[] y)
        {
            for (int i = 0; i < Generations_organisms[_currentIndex].Count; i++)
            {
                var predictions = new double[y.Length];

                for (int d = 0; d < x.Length; d++)
                {
                    var currentOrganism = Generations_organisms[_currentIndex][i];

                    var g1 = currentOrganism.Genomes[0];
                    var g2 = currentOrganism.Genomes[1];
                    var g3 = currentOrganism.Genomes[2];
                    var g4 = currentOrganism.Genomes[3];
                    var g5 = currentOrganism.Genomes[4];
                    var g6 = currentOrganism.Genomes[5];

                    predictions[d] = g1.Value * Math.Pow(x[d], 5) + g2.Value * Math.Pow(x[d], 4) +
                                g3.Value * Math.Pow(x[d], 3) + g4.Value * Math.Pow(x[d], 2) +
                                g5.Value * x[d] + g6.Value;
                }

                double sqError = 0.0;
                
                for (int j = 0; j < predictions.Length; j++) {
                    sqError += (predictions[j] - y[j]) * (predictions[j] - y[j]);
                }

                Generations_organisms[_currentIndex][i].FitLevel = sqError;
            }

            Generations_organisms[_currentIndex].Sort();
        }

        public void Mutate(int index)
        {
            for (int i = 0; i < Generations_organisms[index].Count; i++)
            {
                Generations_organisms[index][i].Mutate(_mutationRate);
            }
        }
    }
}
