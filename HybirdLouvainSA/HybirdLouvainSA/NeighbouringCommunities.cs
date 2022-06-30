using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public class NeighbouringCommunities
	{
		int com;
		public RandomList<int> communtity_ids; // ID's from all neighbouring communities

		public Dictionary<int, RandomList<(int,int)>> connecting_edges; // Edges in format (vertex in com, vertex neighbouring com)
		public Dictionary<int, int> total_weight; // Total weight of all connecting edges for each neighbouring community

		public NeighbouringCommunities(int com)
		{
			this.com = com;
			this.communtity_ids = new RandomList<int>();
			this.connecting_edges = new Dictionary<int, RandomList<(int,int)>>();
			this.total_weight = new Dictionary<int, int>();
		}

        // Update a neighbouring community, given that a vertex is added from a community
		public void add_update_neighbouring_community(Graph graph, int nc, int vertex, int vertex_in_nc)
		{
			if(!this.connecting_edges.ContainsKey(nc))
            {
				this.communtity_ids.Add(nc);
				this.connecting_edges[nc] = new RandomList<(int, int)>();
				this.total_weight[nc] = 0;
			}

			connecting_edges[nc].Add((vertex,vertex_in_nc));
			total_weight[nc] += graph.AdjacencyMatrix[vertex, vertex_in_nc]; //this is not correct, 
		}

		// Update a neighbouring community, given that a vertex is removed from a community
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

