using System;
using System.IO;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public static class readGraphs
	{
		static string file_edges = "../../../graphs/smallgraph_graph_edges.txt";
		static string file_partition = "../../../graphs/smallgraph_graph_partition.txt";

		public static (Graph, Dictionary<int,int>) create_graph(StreamReader readergraph, StreamReader readerpartition)
        {
			string[] input = readergraph.ReadLine().Split(" ");

			int n = int.Parse(input[0]);
			int m = int.Parse(input[1]);

			Graph graph = new Graph(n);
			graph.m = m;
			Vertex[] vertices = graph.vertices;

			Dictionary<int, int> benchmark_partition = new Dictionary<int, int>();

			for (int i = 0; i < n; i++)
				vertices[i] = new Vertex(i, i);

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

						vertex.neighbours.Add(vertices[neighbour].id);
						vertex.degree++;
					}
				}

				int community = int.Parse(readerpartition.ReadLine());

				benchmark_partition[i] = community;
			}

			return (graph, benchmark_partition);
		}
	}
}

