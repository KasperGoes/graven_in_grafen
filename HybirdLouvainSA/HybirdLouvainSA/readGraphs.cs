﻿using System;
using System.IO;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public static class readGraphs
	{
		static string file_edges = "../../../graphs/smallgraph_graph_edges.txt";
		static string file_partition = "../../../graphs/smallgraph_graph_partition.txt";

		public static void read_graph()
        {
			(Graph graph, int[] benchmark_communities) = create_graph(new StreamReader(file_edges), new StreamReader(file_partition));
		}

		public static (Graph, int[]) create_graph(StreamReader readergraph, StreamReader readerpartition)
        {
			string[] input = readergraph.ReadLine().Split(" ");

			int n = int.Parse(input[0]);
			int m = int.Parse(input[1]);

			Graph graph = new Graph(n);
			graph.m = m;
			Vertex[] vertices = graph.vertices;

			int[] benchmark_communities = new int[n];

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

						// TO DO: Add neighbours to vertex class
						vertex.neighbours.Add(vertices[neighbour].id);
						vertex.degree++;

						// Initial degree of sums
						vertex.sum_degrees++;
					}
				}

				int community = int.Parse(readerpartition.ReadLine());

				vertex.community = community;
				benchmark_communities[i] = community;
			}

			return (graph, benchmark_communities);
		}
	}
}
