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
            string filename1 = "../../../graphs/smallgraph_graph_edges.txt";
            string filename2 = "../../../graphs/smallgraph_graph_partition.txt";

            //string filename1 = "../../../graphs/kleine graaf edges.txt";
            //string filename2 = "../../../graphs/kleine graaf partition.txt";

            StreamReader readeredges = new StreamReader(filename1);
            StreamReader readerpartition = new StreamReader(filename2);

            (Graph graph, Dictionary<int,int> real_partition) = readGraphs.create_graph(readeredges, readerpartition);

            graph = Louvain.louvain(graph);
            graph.set_partition();

            //test_modularity(graph);
            //float Result = SA.simulatedAnnealing(graph);
            Console.WriteLine("finished");
        }
    }
}
