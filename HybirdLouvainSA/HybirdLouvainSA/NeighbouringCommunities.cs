using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public class NeighbouringCommunities
	{
		int com;
		public RandomList<int> communtity_ids;

		public Dictionary<int, HashSet<(int,int)>> connecting_edges;
		public Dictionary<int, int> total_weight;

		public NeighbouringCommunities(int com)
		{
			this.com = com;
			this.communtity_ids = new RandomList<int>();
			this.connecting_edges = new Dictionary<int, HashSet<(int,int)>>();
			this.total_weight = new Dictionary<int, int>();
		}

		public void add_update_neighbouring_community(Graph graph, int nc, int vertex, int vertex_in_nc)
		{
			if(!this.connecting_edges.ContainsKey(nc))
            {
				this.communtity_ids.Add(nc);
				this.connecting_edges[nc] = new HashSet<(int, int)>();
				this.total_weight[nc] = 0;
			}

			connecting_edges[nc].Add((vertex,vertex_in_nc));
			total_weight[nc] += graph.AdjacencyMatrix[vertex, vertex_in_nc]; //this is not correct, 
		}

		public void remove_update_neighbouring_community(Graph graph, int nc, int vertex, int vertex_in_nc)
		{
			// If the vertex is the only vertex that connects the neighbouring community, remove the entire neighbouring community
			if (this.connecting_edges[nc].Count == 1)
			{
				communtity_ids.Remove(nc);
                connecting_edges.Remove(nc);
				total_weight.Remove(nc);
			}
			else
			{
                connecting_edges[nc].Remove((vertex, vertex_in_nc));
				total_weight[nc] -= graph.AdjacencyMatrix[vertex, vertex_in_nc];
			}
		}
	}
}

