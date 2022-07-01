using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public class Community
    {
		public int id;
        public HashSet<int> vertices;
        public NeighbouringCommunities neighbouring_communities;

		public int sum_in = 0; // Sum of links between nodes in the community
		public int sum_tot = 0; // Sum of all links incident to nodes in community

		public int weight_selfloops = 0;

        public LinkedList<int> og_vertices; // List to maintain all vertices from original graph that are in the community

        public Community(int id, Vertex v, int sum_in)
		{
			this.id = id;
			this.vertices = new HashSet<int> { id };
			this.sum_tot = v.degree;
			this.sum_in = sum_in;
			this.weight_selfloops = sum_in;
			this.neighbouring_communities = new NeighbouringCommunities(id);
		}

		// Remove a vertex from the community
		public void remove_vertex(Graph graph, Vertex vertex)
        {
			// If this vertex was the only vertex in the community, community will vanish
			if (this.vertices.Count == 1)
            {
                graph.communities.Remove(vertex.community);
				graph.community_list.Remove(vertex.community);
            } 
            else
            {
				this.vertices.Remove(vertex.id);

                int weight_loop_v = graph.AdjacencyMatrix[vertex.id, vertex.id];
                this.weight_selfloops += weight_loop_v;

                int sum_in_old_community = sum_in_community_per_vertex(graph, vertex) - 2 * weight_selfloops; ;
				
                this.sum_in -= sum_in_old_community;
                this.sum_tot -= vertex.degree;
            }
        }

        // Add vertex to the community
		public void add_vertex(Graph graph, Vertex vertex)
        { 
			this.vertices.Add(vertex.id);

			int weight_loop_v = graph.AdjacencyMatrix[vertex.id, vertex.id];
			this.weight_selfloops += weight_loop_v;

			int sum_in_new_com = sum_in_community_per_vertex(graph, vertex) + weight_loop_v;
			this.sum_in += sum_in_new_com;
			this.sum_tot += vertex.degree;
		}

		// Given a vertex, compute the number of edges that connect the community and the vertex 
        public int sum_in_community_per_vertex(Graph graph, Vertex vertex)
		{
			// TO DO: SHOULD THIS INCUDE SELFLOOPS IN SOME WAY?
			int sum_in_com = 0;
			for (int i = 0; i < vertex.neighbours.Count; i++)
				if (this.vertices.Contains(vertex.neighbours[i]))
					sum_in_com += graph.AdjacencyMatrix[vertex.id, vertex.neighbours[i]];

			return sum_in_com;
		}
	}
}

