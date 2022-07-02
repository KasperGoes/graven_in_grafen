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
            int threshold = 100;
            float alpha = 0.999999F;
            float temp = 5; // TO DO: GEEN IDEE WAT HIER GOEDE WAARDES VOOR ZIJN
            float epsilon = 0.03F;

            Experiment.run_all_experiments(threshold, temp, alpha, epsilon);

            files file_exp = files.extra;
            experiment experiment = experiment.sa;
            Experiment.run_one_experiment(file_exp, experiment, threshold, temp, alpha, epsilon);
        }
    }

    public enum files
    {
        small,
        extra,
        thousand,
        tenthousand,
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
