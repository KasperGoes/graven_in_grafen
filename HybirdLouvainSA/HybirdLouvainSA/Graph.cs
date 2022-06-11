using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public class Graph
	{
		public int n; // number of vertices
		public int m; // number of edges

		public float modularity;

		public Vertex[] vertices; // all vertices in the graph
		
		public int[,] AdjacencyMatrix;
	
		public int[] community_per_vertex;

		public Dictionary<int,Community> communities;

		public Graph(int n)
		{
			this.n = n;
			vertices = new Vertex[n];
			AdjacencyMatrix = new int[n, n];

			community_per_vertex = new int[n];
			
			communities = new Dictionary<int, Community>();
		}

		public void set_initial_community_per_node()
        {
			// Create community for ever node
			for (int i = 0; i < this.vertices.Length; i++)
            {
				this.communities.Add(i, new Community(i, this.vertices[i]));

				HashSet<int> neighbours = new HashSet<int>();

				for(int j = 0; j < this.vertices[i].neighbours.Count; j++)
                {
					int neighbouring_v_com = this.vertices[i].neighbours[j];
					int weight = this.AdjacencyMatrix[i, neighbouring_v_com];
					communities[i].neighbouring_communities_sum_out[neighbouring_v_com] = (weight, new HashSet<int> { neighbouring_v_com });
				}
			}
		}
	}
}

