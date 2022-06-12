﻿using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public class Community
    {
		public int id;
		public int sum_in = 0;
		public int sum_tot = 0;
		public HashSet<int> vertices;

		// Set to collect all original vertex id's 
		public HashSet<int> og_vertices;

		public NeighbouringCommunities neighbouring_communities;

		public Community(int id, Vertex v)
		{
			this.id = id;
			this.vertices = new HashSet<int> { id };
			this.sum_tot = v.degree;
			this.neighbouring_communities = new NeighbouringCommunities();
		}

		// Update community where a vertex is removed from a community
		public void update_old_community(Graph graph, Vertex vertex, Vertex neighbour)
        {
			if (this.vertices.Count == 1)
                graph.communities.Remove(vertex.community);
            else
            {
				foreach (int nc in this.neighbouring_communities.communtity_ids)
					neighbouring_communities.remove_update_neighbouring_community(graph, nc, vertex.id, neighbour.id);

				this.vertices.Remove(vertex.id);
				int sum_in_old_community = sum_in_community_per_vertex(graph, vertex);
                this.sum_in -= sum_in_old_community;
                this.sum_tot -= vertex.degree;
            }
        }

        // Update community where a vertex is added to a community
		public void update_new_community(Graph graph, Vertex vertex, Vertex neighbour)
        { 
            // All neighbouring communities from the vertex are added/updated for the neighbouring communities of the community
            // Skip current community + old community 
            foreach (int nc in vertex.neighbouring_communities.communtity_ids)
    		    neighbouring_communities.add_update_neighbouring_community(graph, nc, vertex.id, neighbour.id);

			this.vertices.Add(vertex.id);

			int sum_in_new_com = sum_in_community_per_vertex(graph, vertex);
			this.sum_in += sum_in_new_com;
			this.sum_tot += vertex.degree;
		}

        public int sum_in_community_per_vertex(Graph graph, Vertex vertex)
		{
			int sum_in_com = 0;
			for (int i = 0; i < vertex.degree; i++)
				if (this.vertices.Contains(vertex.neighbours[i]))
					sum_in_com += graph.AdjacencyMatrix[vertex.id, vertex.neighbours[i]];

			return sum_in_com;
		}
	}
}

