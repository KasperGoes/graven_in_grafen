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

		// TO DO: Add modularity

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
			community.sum_in += degree_in_community; // TO DO: instead of vertex degree sum over weights
			community.sum_tot = community.sum_tot + vertex.degree; // TO DO: instead of vertex degree sum over weights

			//Update oude community values 
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

