using System;
using System.IO;
using System.Collections.Generic;

namespace HybridLouvainSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filename1;
            string filename2;

            // Choose the desired example graph
            // TO DO: Create loop in experiment class to automatically loop through all generated graphs
            files file = files.extra;

            switch(file)
            {
                case files.small:
                    filename1 = "../../../graphs/smallgraph_graph_edges.txt";
                    filename2 = "../../../graphs/smallgraph_graph_partition.txt";
                    break;

                case files.extra:
                    filename1 = "../../../graphs/extra voorbeeld edges.txt";
                    filename2 = "../../../graphs/extra voorbeeld partition.txt";
                    break;

                default:
                    filename1 = "../../../graphs/graphs/1000/0.2/0_graph_edges.txt";
                    filename2 = "../../../graphs/graphs/1000/0.2/0_graph_partition.txt";
                    break;
            }

            // TO DO: Now read file two times, for storing the original value of the graph
            StreamReader readeredges = new StreamReader(filename1);
            StreamReader readerpartition = new StreamReader(filename2);

            (Graph graph, Dictionary<int,int> real_partition) = TextfileConverter.create_graph(readeredges, readerpartition);

            readeredges = new StreamReader(filename1);
            readerpartition = new StreamReader(filename2);
            (Graph og_graph, Dictionary<int, int> og_real_partition) = TextfileConverter.create_graph(readeredges, readerpartition);

            // Declare threshold that defines the stopping criterium for hybrid louvain-sa
            int threshold = 100;
            float alpha = 0.90F;
            float temp = graph.m; // TO DO: GEEN IDEE WAT HIER GOEDE WAARDES VOOR ZIJN
            float epsilon = 0.03F;

            // Run the experiment
            (float mod, Dictionary<int,int> partition) = Experiment.perform_experiment(experiment.hybrid, graph, threshold, og_graph, temp, alpha, epsilon);
            Console.WriteLine(mod);

            // Write partition to file
            TextfileConverter.write_partition("../../../partition/extra voorbeeld edges.txt", graph);

            Console.WriteLine("finished");
        }
    }

    enum files
    {
        small,
        extra,
        thousend
    }

    public enum experiment
    {
        hybrid,
        sa,
        louvain
    }
}
