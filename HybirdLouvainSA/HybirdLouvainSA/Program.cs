using System;
using System.IO;
using System.Collections.Generic;

namespace HybridLouvainSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Declare threshold that defines the stopping criterium for hybrid louvain-sa
            int threshold = 5000;
            float alpha = 0.999999F;
            float temp = 5; 
            float epsilon = 0.03F;

            Experiment.run_all_experiments(threshold, temp, alpha, epsilon);

            //files file_exp = files.twentyhousand;
            //experiment experiment = experiment.sa;
            //Experiment.run_one_experiment(file_exp, experiment, threshold, temp, alpha, epsilon);
        }
    }

    public enum files
    {
        small,
        extra,
        thousand,
        twentyhousand,
        loops,
        louvain
    }

    public enum experiment
    {
        hybrid,
        sa,
        louvain
    }
}
