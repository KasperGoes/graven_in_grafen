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

            //files file_exp = files.ten;
            //experiment experiment = experiment.hybrid;

            //(string edges, string partition) = get_files(file_exp);

            //Experiment.run_one_experiment(edges, partition, experiment, threshold, temp, alpha, epsilon);
        }

        public static (string, string) get_files(files file)
        {
            string filename1;
            string filename2;

            switch (file)
            {
                case files.small:
                    filename1 = "../../../voorbeelden/smallgraph_graph_edges.txt";
                    filename2 = "../../../voorbeelden/smallgraph_graph_partition.txt";
                    break;

                case files.extra:
                    filename1 = "../../../voorbeelden/extra voorbeeld edges.txt";
                    filename2 = "../../../voorbeelden/extra voorbeeld partition.txt";
                    break;

                case files.louvain:
                    filename1 = "../../../voorbeelden/louvain voorbeeld edges.txt";
                    filename2 = "../../../voorbeelden/louvain voorbeeld partition.txt";
                    break;

                case files.thousand:
                    filename1 = "../../../graphs/5000/0.7/0_graph_edges.txt";
                    filename2 = "../../../graphs/5000/0.7/0_graph_partition.txt";
                    break;

                case files.loops:
                    filename1 = "../../../voorbeelden/loops edges.txt";
                    filename2 = "../../../voorbeelden/loops partition.txt";
                    break;

                case files.ten:
                    filename1 = "../../../graphs/10000/0.7/0_graph_edges.txt";
                    filename2 = "../../../graphs/10000/0.7/0_graph_partition.txt";
                    break;

                default:
                    filename1 = "../../../graphs/50000/0.2/0_graph_edges.txt";
                    filename2 = "../../../graphs/50000/0.2/0_graph_partition.txt";
                    break;
            }

            return (filename1, filename2);
        }
    }

    public enum files
    {
        small,
        extra,
        thousand,
        ten,
        fiftyhousand,
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
