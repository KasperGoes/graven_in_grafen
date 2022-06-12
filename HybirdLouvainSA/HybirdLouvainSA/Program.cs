using System;
using System.IO;

namespace HybridLouvainSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //readGraphs.read_graph();
            string filename1 = "../../../graphs/kleine graaf edges.txt";
            string filename2 = "../../../graphs/kleine graaf partition.txt";
            StreamReader readeredges = new StreamReader(filename1);
            StreamReader readerpartition = new StreamReader(filename2);

            (Graph graph, int[] benchmark_communities) = readGraphs.create_graph(readeredges, readerpartition);
            //set_initial_communities(graph);

            Louvain.louvain(graph);
            //test_modularity(graph);
            //float Result = SA.simulatedAnnealing(graph);
            Console.WriteLine("finished");
        }
    }
}
