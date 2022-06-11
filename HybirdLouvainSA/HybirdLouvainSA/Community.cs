using System;
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

		// Dictionary: for each neighbouring community, store the sum of weights of connecting edges
		public Dictionary<int, (int, HashSet<int>)> neighbouring_communities_sum_out;

		public Community(int id, Vertex v)
		{
			this.id = id;
			this.vertices = new HashSet<int> { id };
			this.sum_tot = v.degree;
			this.neighbouring_communities_sum_out = new Dictionary<int, (int, HashSet<int>)>();
		}

		public void update_old_community(Graph graph, Vertex vertex)
		{
			this.vertices.Remove(vertex.id);

			// If the old community is empty, remove community from graph
			// Else, update the old community parameters
			if (this.vertices.Count == 0)
				graph.communities.Remove(vertex.community);
			else
			{
				int sum_in_old_community = sum_in_community_per_vertex(graph, vertex);

				// TO DO: NOT CORRECT YET
				foreach (int n in neighbouring_communities_sum_out.Keys)
                {
					if (neighbouring_communities_sum_out[n].Item2.Contains(vertex.id))
                    {
                        if (neighbouring_communities_sum_out[n].Item2.Count > 1)
                        {
							(int weight, HashSet<int> neigbours) = neighbouring_communities_sum_out[n];
							weight = weight - graph.AdjacencyMatrix[n, vertex.id];
							neighbouring_communities_sum_out[n].Item2.Remove(n);
							neighbouring_communities_sum_out[n] = (weight, neigbours);
                        }
                        else
                            neighbouring_communities_sum_out.Remove(n);
                    }
                }

				this.sum_in += sum_in_old_community;
				this.sum_tot -= vertex.degree;
			}
		}

		public void update_new_community(Graph graph, Vertex vertex)
		{
			this.vertices.Add(vertex.id);
			vertex.community = this.id;

			int sum_in_new_community = sum_in_community_per_vertex(graph, vertex);

			this.sum_in += sum_in_new_community;
			this.sum_tot += +vertex.sum_degrees;

			// TO DO: NOT CORRECT YET
			foreach (int n in vertex.neighbours)
			{
				Vertex u = graph.vertices[n];

				if(neighbouring_communities_sum_out[n].Item2.Contains(vertex.id))
				{
					if (neighbouring_communities_sum_out[n].Item2.Count != 1)
					{
						(int weight, HashSet<int> neigbours) = neighbouring_communities_sum_out[n];
						weight = weight - graph.AdjacencyMatrix[n, vertex.id];
						neighbouring_communities_sum_out[n].Item2.Remove(n);
						neighbouring_communities_sum_out[n] = (weight, neigbours);
					}
					else
						neighbouring_communities_sum_out.Remove(n);
				}
			}
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

