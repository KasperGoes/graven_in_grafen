using System;
using System.IO;
using System.Collections.Generic;

namespace HybridLouvainSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //readGraphs.read_graph();
            //string filename1 = "../../../graphs/smallgraph_graph_edges.txt";
            //string filename2 = "../../../graphs/smallgraph_graph_partition.txt";

            //string filename1 = "../../../graphs/graphs/1000/0.2/0_graph_edges.txt";
            //string filename2 = "../../../graphs/graphs/1000/0.2/0_graph_partition.txt";

            string filename1 = "../../../graphs/extra voorbeeld edges.txt";
            string filename2 = "../../../graphs/extra voorbeeld partition.txt";

            StreamReader readeredges = new StreamReader(filename1);
            StreamReader readerpartition = new StreamReader(filename2);

            (Graph graph, Dictionary<int,int> real_partition) = readGraphs.create_graph(readeredges, readerpartition);

            readeredges = new StreamReader(filename1);
            readerpartition = new StreamReader(filename2);
            (Graph og_graph, Dictionary<int, int> og_real_partition) = readGraphs.create_graph(readeredges, readerpartition);


            int threshold = 100;
            (float mod, Dictionary<int,int> partition) = Experiment.perform_experiment(experiment.hybrid, graph, threshold, og_graph);
            Console.WriteLine(mod);

            Console.WriteLine("finished");
        }
    }
}
