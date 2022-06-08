using System;
using System.IO;

namespace communityDetection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            readGraphs.read_graph();
            test_modularity();
        }

        /// <summary>
        /// Tests the modularity for the example graph with 5 nodes and 2 communities
        /// </summary>
        static void test_modularity()
        {
            string filename1 = "../../../graphs/kleine graaf edges.txt";
            string filename2 = "../../../graphs/kleine graaf partition.txt";
            StreamReader readeredges = new StreamReader(filename1);
            StreamReader readerpartition = new StreamReader(filename2);

            Graph graph = readGraphs.create_graph(readeredges, readerpartition);
            float modularity = Modularity.modularity(graph);

            for (int i = 0; i < graph.vertices.Length; i++)
                graph.communities.Add(i, new Community(i, graph.vertices[i]));

            Louvain.update_graph(graph, graph.communities[2], graph.vertices[0]);
        }
    }
}
