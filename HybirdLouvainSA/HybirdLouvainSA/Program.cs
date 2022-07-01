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
            files file = files.tenthousand;
            experiment experiment = experiment.sa;

            switch(file)
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
                    filename1 = "../../../graphs/graphs/1000/0.7/0_graph_edges.txt";
                    filename2 = "../../../graphs/graphs/1000/0.7/0_graph_partition.txt";
                    break;

                case files.loops:
                    filename1 = "../../../voorbeelden/loops edges.txt";
                    filename2 = "../../../voorbeelden/loops partition.txt";
                    break;

                default:
                    filename1 = "../../../graphs/graphs/10000/0.3/0_graph_edges.txt";
                    filename2 = "../../../graphs/graphs/10000/0.3/0_graph_partition.txt";
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
            float alpha = 0.999999F;
            float temp = 5; // TO DO: GEEN IDEE WAT HIER GOEDE WAARDES VOOR ZIJN
            float epsilon = 0.03F;

            // Run the experiment
            (float mod, Graph final_graph) = Experiment.perform_experiment(experiment, graph, threshold, og_graph, temp, alpha, epsilon);

            Console.WriteLine(mod);

            // Write partition to file
            TextfileConverter.write_partition("../../../partition/" + experiment.ToString() +" " + graph.vertices.Length.ToString() + ".txt", final_graph.partition);

            Console.WriteLine("finished");
        }
    }

    enum files
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
