using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public class NeighbouringCommunities
	{
		public HashSet<int> communtity_ids;
		public Dictionary<int, HashSet<int>> connecting_vertices;
		public Dictionary<int, int> total_weight;

		public NeighbouringCommunities()
		{
			this.communtity_ids = new HashSet<int>();
			this.connecting_vertices = new Dictionary<int, HashSet<int>>();
			this.total_weight = new Dictionary<int, int>();
		}

		public void add_update_neighbouring_community(Graph graph, int nc, int vertex, int neighbour_vertex)
		{
			if(!this.communtity_ids.Contains(nc))
            {
				this.communtity_ids.Add(nc);
				this.connecting_vertices[nc] = new HashSet<int>();
				this.total_weight[nc] = 0;
			}

			connecting_vertices[nc].Add(vertex);
			total_weight[nc] += graph.AdjacencyMatrix[vertex, neighbour_vertex];
		}

		public void remove_update_neighbouring_community(Graph graph, int nc, int vertex, int neighbour_vertex)
		{
			// If the vertex is the only vertex that connects the neighbouring community, remove the entire neighbouring community
			if (this.connecting_vertices[nc].Count == 1)
			{
				communtity_ids.Remove(nc);
				// CHECK IF IT REMOVES BOTH KEY AND VALUE
				connecting_vertices.Remove(nc);
				total_weight.Remove(nc);
			}
			else
			{
				connecting_vertices[nc].Remove(vertex);
				total_weight[nc] -= graph.AdjacencyMatrix[vertex, neighbour_vertex];
			}
		}
	}
}

