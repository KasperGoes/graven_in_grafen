using System;
namespace communityDetection
{
	public static class readGraphs
	{
		static string file_edges = "../../../graphs/smallgraph_graph_edges.txt";
		static string file_partition = "../../../graphs/smallgraph_graph_partition.txt";

		public static void read_graph()
        {
			Graph graph = create_graph(new StreamReader(file_edges), new StreamReader(file_partition));
		}

		public static Graph create_graph(StreamReader readergraph, StreamReader readerpartition)
        {
			string[] input = readergraph.ReadLine().Split(" ");

			int n = int.Parse(input[0]);
			int m = int.Parse(input[1]);

			Graph graph = new Graph(n, m);
			Vertex[] vertices = graph.vertices; 

			for (int i = 0; i < n; i++)
				vertices[i] = new Vertex(i, i, 0);

			for (int i = 0; i < graph.vertices.Length; i++)
			{
				string[] adjacencylist = readergraph.ReadLine().Split(" ");

				Vertex vertex = vertices[i];
				
				for (int j = 0; j < adjacencylist.Length; j++)
				{
					if (adjacencylist[j] != "")
					{
						int neighbour = int.Parse(adjacencylist[j]) - 1;
						graph.AdjacencyMatrix[i, neighbour] = 1;

						//graph.AdjacenceList[i].Add(neighbour);

						// TO DO: Add neighbours to vertex class
						vertex.neighbours.Add(vertices[neighbour]);
						vertex.degree++;
					}
				}

				int community = int.Parse(readerpartition.ReadLine());

				vertex.community = community;


				// Add benchmark communities NOG CONTROLEREN!!!!!!
				if (!graph.benchmark_communities.ContainsKey(community))
					graph.benchmark_communities[community] = new Community(community, vertex);
				else
					graph.benchmark_communities[community].vertices.Add(vertex.id, vertex);
			}

			return graph;
		}
	}
}

