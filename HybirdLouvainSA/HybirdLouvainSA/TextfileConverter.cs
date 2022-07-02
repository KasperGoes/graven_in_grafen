using System;
using System.IO;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public static class TextfileConverter
	{
		// Creates a graph based on a textfile
		public static Graph create_graph(string graphpath)
		{
			StreamReader readergraph = new StreamReader(graphpath);
            
            string[] input = readergraph.ReadLine().Split(" ");

			int n = int.Parse(input[0]);
			int m = int.Parse(input[1]);

			Graph graph = new Graph(n);
			graph.m = m;
			Vertex[] vertices = graph.vertices;

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
			}

			return graph;
		}

		// Write partition to textfile
		public static void write_result(string filepath, Dictionary<int,int> partition, string elapsed_time)
		{
			using (StreamWriter stream = new StreamWriter(filepath, false))
            {
                stream.WriteLine(elapsed_time);
                for (int i = 0; i < partition.Count; i++)
				{
					if(i == partition.Count - 1)
                        stream.Write(partition[i]);
                    else
						stream.WriteLine(partition[i]);
				}
			}
		}
	}
}

