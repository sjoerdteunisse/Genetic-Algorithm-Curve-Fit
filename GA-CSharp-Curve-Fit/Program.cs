using GA_CSharp_Curve_Fit.GA;
using System;
using System.Diagnostics;

namespace GA_CSharp_Curve_Fit
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(1000, 0.1f, 0.1f, 10, 100);
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            geneticAlgorithm.InitializePopulation(6);

            double[] xData = new double[] { -5.0, -4.8, -4.6, -4.4, -4.2, -4.0, -3.8, -3.6, -3.4, -3.2, -3.0, -2.8, -2.6, -2.4, -2.2, -2.0, -1.8, -1.6, -1.4, -1.2, -1.0, -0.8, -0.6, -0.4, -0.2, 0.0, 0.2, 0.4, 0.6, 0.8, 1.0, 1.2, 1.4, 1.6, 1.8, 2.0, 2.2, 2.4, 2.6, 2.8, 3.0, 3.2, 3.4, 3.6, 3.8, 4.0, 4.2, 4.4, 4.6, 4.8, 5.0 };
            double[] yData = new double[] { 3732.5, 3158.3, 2657.7, 2223.1, 1847.8, 1525.4, 1250.0, 1016.2, 819.0, 653.9, 516.7, 403.6, 311.3, 236.7, 177.0, 129.8, 93.0, 64.8, 43.4, 27.6, 16.1, 8.0, 2.4, -1.3, -3.6, -5.0, -5.7, -6.0, -6.1, -6.1, -6.1, -6.3, -6.7, -7.6, -9.2, -11.8, -15.8, -21.8, -30.4, -42.3, -58.7, -80.6, -109.5, -147.0, -194.9, -255.4, -330.9, -424.1, -538.1, -676.3, -842.5 };

            //geneticAlgorithm.Mutate(0);


            //geneticAlgorithm.Fitness(xData, yData);
            geneticAlgorithm.Start(xData, yData);
            stopwatch.Stop();

            Console.WriteLine($"Elapsed {stopwatch.ElapsedMilliseconds}ms");

        }
    }
}
