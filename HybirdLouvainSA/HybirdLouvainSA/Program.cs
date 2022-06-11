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
           
            Graph graph = readGraphs.create_graph(readeredges, readerpartition);
            set_initial_communities(graph);

            //Louvain.louvain(graph);
            //test_modularity(graph);
            float Result = SA.simulatedAnnealing(graph);
            Console.WriteLine("yeet");
        }

        static void set_initial_communities(Graph graph)
        {
            //graph.communities.Add(0, new Community(0, graph.vertices[0]));
            //graph.communities[0].vertices.Add(0);

            //graph.communities.Add(1, new Community(1, graph.vertices[1]));
            //graph.communities[1].vertices.Add(1);

            //graph.communities.Add(2, new Community(2, graph.vertices[2]));
            //graph.communities[2].sum_in = 3;
            //graph.communities[2].sum_tot = 7;

            //graph.communities[2].vertices.Add(2);
            //graph.communities[2].vertices.Add(3);
            //graph.communities[2].vertices.Add(4);

            // Create community for ever node
            for (int i = 0; i < graph.vertices.Length; i++)
                graph.communities.Add(i, new Community(i, graph.vertices[i]));
        }

        /// <summary>
        /// Tests the modularity for the example graph with 5 nodes and 2 communities
        /// </summary>
        static void test_modularity(Graph graph)
        {
            float modularity = Modularity.modularity(graph);

            graph.update_graph(graph.communities[2], graph.vertices[0]);
        }
    }
}
