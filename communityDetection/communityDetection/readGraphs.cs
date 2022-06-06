using System;
namespace communityDetection
{
	public static class readGraphs
	{
		static string file_edges = "../../../graphs/smallgraph_graph_edges.txt";
		static string file_partition = "../../../graphs/smallgraph_graph_partition.txt";

		public static void read_graph()
        {
			StreamReader reader = new StreamReader(file_edges);
			string[] input = reader.ReadLine().Split(" ");

			int vertices = int.Parse(input[0]);
			int edges = int.Parse(input[1]);

			Graph graph = create_graph(vertices, reader, new StreamReader(file_partition));
		}

		private static Graph create_graph(int n, StreamReader readergraph, StreamReader readerpartition)
        {
			Graph graph = new Graph(n);
			for (int i = 0; i < graph.vertices.Length; i++)
			{
				string[] adjacencylist = readergraph.ReadLine().Split(" ");

				int degree = 0;

				for (int j = 0; j < adjacencylist.Length; j++)
				{
					if (adjacencylist[j] != "")
					{
						int neighbour = int.Parse(adjacencylist[j]);
						graph.AdjacencyMatrix[i, neighbour - 1] = 1;

						graph.AdjacenceList[i].Add(neighbour - 1);
						degree++;
					}
				}

				int community = int.Parse(readerpartition.ReadLine());

				graph.vertices[i].degree = degree;
				graph.vertices[i] = new Vertex(i, community, degree);

				// Add benchmark communities
				if (!graph.benchmark_communities.ContainsKey(community))
					graph.benchmark_communities[community] = new List<int>();
				graph.benchmark_communities[community].Add(i);
			}

			return graph;
		}
	}
}

