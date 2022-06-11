using System;
using System.Collections.Generic;

namespace communityDetection
{
	public class Graph
	{
		public int n; // number of vertices
		public int m; // number of edges

		public float modularity;

		public Vertex[] vertices; // all vertices in the graph
		
		public int[,] AdjacencyMatrix;
	
		public int[] community_per_vertex;

		public Dictionary<int, Community> benchmark_communities;

		public Dictionary<int,Community> communities;

		public Graph(int n)
		{
			this.n = n;
			vertices = new Vertex[n];
			AdjacencyMatrix = new int[n, n];

			community_per_vertex = new int[n];
			benchmark_communities = new Dictionary<int, Community>();
			communities = new Dictionary<int, Community>();
		}

		public void update_graph(Community community, Vertex vertex)
		{
			// Remove from old community
			Community old_commmunity = this.communities[vertex.community];
			old_commmunity.vertices.Remove(vertex.id);


			//If the resulting community is empty, remove community from graph
			if (this.communities[vertex.community].vertices.Count == 0)
				this.communities.Remove(vertex.community);
            else
            {
				int edges_in_old_community = old_commmunity.update_sum_in(vertex);
				old_commmunity.sum_in += edges_in_old_community;
				old_commmunity.sum_tot -= vertex.degree;
            }

			// Add to new community
			community.vertices.Add(vertex.id);
			vertex.community = community.id;

			int degree_in_community = community.update_sum_in(vertex);

			// Update sums
			community.sum_in += degree_in_community; // TO DO: instead of vertex degree sum over weights
			community.sum_tot += + vertex.degree; // TO DO: instead of vertex degree sum over weights
		}
	}

	public class Vertex
	{
		public int id;
		public int degree;
		public int community;
		public List<int> neighbours;

		public HashSet<int> original_vertices;

		// TO DO: Add variable to compute the neighbouring communities and by which vertices those communtities are connected (Dict <int,list<int>)

		public Vertex(int id, int community)
		{
			this.id = id;
			this.community = community;
			neighbours = new List<int>();
			original_vertices = new HashSet<int>();
		}
	}
}

