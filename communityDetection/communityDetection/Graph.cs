using System;

namespace communityDetection
{
	public class Graph
	{
		public int n; // number of vertices
		public int m; // number of edges

		public Vertex[] vertices; // all vertices in the graph
		
		public int[,] AdjacencyMatrix;
		//public List<int>[] AdjacenceList;

		public int[] community_per_vertex;
		public Dictionary<int, Community> benchmark_communities;

		public Dictionary<int,Community> communities;

		public Graph(int n, int m)
		{
			this.n = n;
			this.m = m;
			vertices = new Vertex[n];
			AdjacencyMatrix = new int[n, n];

			community_per_vertex = new int[n];
			benchmark_communities = new Dictionary<int, Community>();
			communities = new Dictionary<int, Community>();
		}

		/// <summary>
		/// Updates the graph for the one community after adding a vertex 
		/// </summary>
		/// <param name="g"></param>
		/// <param name="community"></param>
		/// <param name="vertex"></param>
		public void update_graph(Community community, Vertex vertex)
		{
			float modularitydiff = Modularity.modularity_difference(this, community, vertex);
			// Remove from old community
			this.communities[vertex.community].vertices.Remove(vertex.id);

			//If the resulting community is empty, remove community from graph
			if (this.communities[vertex.community].vertices.Count == 0)
				this.communities.Remove(vertex.community);

			// Add to new community
			community.vertices.Add(vertex.id);
			vertex.community = community.id;

			int degree_in_community = community.update_sum_in(vertex);

			// Update sums
			community.sum_in += degree_in_community;
			community.sum_tot = community.sum_tot + vertex.degree;
		}
	}

	public class Vertex
	{
		public int id;
		public int degree;
		public int community;
		public List<int> neighbours;

		public Vertex(int id, int community, int degree)
		{
			this.id = id;
			this.community = community;
			this.degree = degree;
			neighbours = new List<int>();
		}
	}
}

