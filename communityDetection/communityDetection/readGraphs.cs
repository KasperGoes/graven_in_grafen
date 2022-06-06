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

			Graph graph = new Graph(vertices);

			fill_adjacency_matrix(graph, reader);

			fill_communities(graph, new StreamReader(file_partition));
		}

		private static void fill_adjacency_matrix(Graph graph, StreamReader reader)
        {
			for (int i = 0; i < graph.vertices; i++)
			{
				string[] adjacencylist = reader.ReadLine().Split(" ");

				for (int j = 0; j < adjacencylist.Length; j++)
				{
					if (adjacencylist[j] != "")
					{
						int neighbour = int.Parse(adjacencylist[j]);
						graph.AdjacencyMatrix[i, neighbour - 1] = 1;
					}
				}
			}
		}

		private static void fill_communities(Graph graph, StreamReader reader)
        {
			for (int i = 0; i < graph.vertices; i++)
			{
				int community = int.Parse(reader.ReadLine());
				if (!graph.communities.ContainsKey(community))
					graph.communities[community] = new List<int>();
				graph.communities[community].Add(i);
			}
		}
	}
}

