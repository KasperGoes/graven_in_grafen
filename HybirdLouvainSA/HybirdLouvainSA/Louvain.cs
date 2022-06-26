﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HybridLouvainSA
{
	public static class Louvain
	{
		public static Graph louvain(Graph g)
        {
			// Track if we found a solution, so no new changes to the graph were made during first phase
			bool change = true;

			// We will stop the louvain algorithm when 
			while(change && g.vertices.Length < 1000)
            {
				Graph graph;
				(graph, change) = phase_one(g);
				if (!change)
					return graph;

				g = phase_two(graph);
			}

			return g;
		}

		private static (Graph, bool) phase_one(Graph g)
        {
			// Boolean variable to check if the graph is changend during any of the iterations
			bool change = false;

			g.set_initial_community_per_node();

			g.modularity = Modularity.modularity(g);

            // Create random order of vertices
            List<int> order = Enumerable.Range(0, g.vertices.Length).ToList();
            Random random = new Random();
            List<int> random_order = order.OrderBy(x => random.Next()).ToList();

            // TEMPORARY SAME ORDER VERTICES
            //List<int> random_order = Enumerable.Range(0, g.vertices.Length).ToList();

            bool found_improvement_all_vertices = true;

			// While improvement is possible
			while (found_improvement_all_vertices)
			{
				found_improvement_all_vertices = false;

				// Loop through all vertices in random order
				for (int i = 0; i < g.vertices.Length; i++)
				{
					// Set gain to a treshold
					float max_gain = 0.000001F;
					bool found_improvement = false;

					// Acces random vertex
					Vertex v = g.vertices[random_order[i]];
					Vertex best_neighbour = null;

					// Compute best change in community if possible
					foreach (int neighbour in v.neighbours)
					{
						Vertex u = g.vertices[neighbour];

						if (u.community == v.community)
							continue;

						float modularity_diff = Modularity.modularity_difference(g, g.communities[u.community], v);

						if (modularity_diff > max_gain)
						{
							max_gain = modularity_diff;
							best_neighbour = u;
							found_improvement = true;
						}
					}

					// If improvement found, update the corresponding community
					if(found_improvement)
					{
						g.update_all_neighbouring_communities(v, best_neighbour);
						g.switch_to_community(v, best_neighbour);
                        g.modularity += max_gain;
						found_improvement_all_vertices = true;
						change = true;
					}
				}
			}

			return (g, change);
		}

        private static Graph phase_two(Graph old_graph)
        {
            int number_communities = old_graph.communities.Count;

            Graph graph = new Graph(number_communities);

			//UGLY FIX
			int[] communities_array = old_graph.communities.Keys.ToArray<int>();
			Dictionary<int, int> new_to_old_community_id = new Dictionary<int, int>();

            for (int i = 0; i < number_communities; i++)
            {
				// TO DO: Adjust the id's of the communities and the new vertex id's
				int real_com_id = communities_array[i];
				new_to_old_community_id.Add(real_com_id, i);

                Vertex vertex = new Vertex(i, i);

                graph.vertices[i] = vertex;

				int loop_degree = 2 * old_graph.communities[real_com_id].sum_in;

				graph.AdjacencyMatrix[i, i] = loop_degree;
				graph.m += loop_degree;
			}

			for(int i = 0; i < number_communities; i++)
            {
                int real_com_id = communities_array[i];
				Vertex vertex = graph.vertices[i];

                foreach (int n in old_graph.communities[real_com_id].neighbouring_communities.communtity_ids)
                {
					int new_nc_id = new_to_old_community_id[n];

					
                    vertex.neighbours.Add(new_nc_id);

                    int weight = old_graph.communities[real_com_id].neighbouring_communities.total_weight[n];
					graph.AdjacencyMatrix[i, new_nc_id] = weight;
                    vertex.degree++;
                    vertex.sum_degrees += old_graph.communities[real_com_id].neighbouring_communities.total_weight[n];

					if (i < new_nc_id)
						graph.m += weight;
                }
            }

            return graph;
        }
    }
}
