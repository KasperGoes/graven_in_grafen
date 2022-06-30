using System;
using System.Collections.Generic;
using System.Linq;

namespace HybridLouvainSA
{
	public static class Louvain
	{
		public static Graph louvain(Graph g, int minimum_nodes)
        {
			bool change = true;

			// If no more communties found/graph reduced, we return the graph with the current communities
			while(change)
            {
				Graph graph;

				// Find the communities wihtin the current graph structure
				(graph, change) = phase_one(g);

				// If no new community configuration was found, return the graph
				if (!change)
					return graph;

				// If the number of nodes is smaller that the switch treshold, stop the algorithm
				if (g.communities.Count < minimum_nodes)
					return graph;

				// Reduce the graph given the new communities
				g = phase_two(graph);
			}

			return g;
		}

		private static (Graph, bool) phase_one(Graph g)
        {
			bool change = false;
            bool found_improvement_all_vertices = true;

            g.set_initial_community_per_node();

			// Compute initial modularity
			g.modularity = Modularity.mod2(g);

            // Create random order of vertices
            List<int> order = Enumerable.Range(0, g.vertices.Length).ToList();
            Random random = new Random();
            List<int> random_order = order.OrderBy(x => random.Next()).ToList();

            // TEMPORARY SAME ORDER VERTICES
            //List<int> random_order = Enumerable.Range(0, g.vertices.Length).ToList();

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

			int[] communities_array = old_graph.communities.Keys.ToArray<int>();
			Dictionary<int, int> new_to_old_community_id = new Dictionary<int, int>();

			// Create a vertex for every community
			for (int i = 0; i < number_communities; i++)
            {
				int real_com_id = communities_array[i];
				new_to_old_community_id.Add(real_com_id, i);

				Community community = old_graph.communities[real_com_id];
                Vertex vertex = new Vertex(i, i);
                graph.vertices[i] = vertex;

                int loop_degree = 2 * community.sum_in;

                graph.AdjacencyMatrix[i, i] = loop_degree;
                graph.m += loop_degree;
                vertex.degree += loop_degree * 2;

                LinkedList og_vertices = new LinkedList();
                foreach (int v in community.vertices)
                {
					Vertex v_in_com = old_graph.vertices[v];
					og_vertices.add_linkedlist_end(v_in_com.original_vertices);
                }

				vertex.original_vertices = og_vertices;
			}

			// Set the correct values for the sums, adjacecy matrices based on the neighbouring communities
			for(int i = 0; i < number_communities; i++)
            {
                int real_com_id = communities_array[i];

                Community community = old_graph.communities[real_com_id];
                Vertex vertex = graph.vertices[i];

				RandomList<int> communities = community.neighbouring_communities.communtity_ids;

                for (int j = 0; j < communities.Count; j++)
                {
					int n = communities.list[j];
					int new_nc_id = new_to_old_community_id[n];

					vertex.neighbours.Add(new_nc_id);

                    int weight = community.neighbouring_communities.total_weight[n];
					graph.AdjacencyMatrix[i, new_nc_id] = weight;
					vertex.degree += weight;
                    
                    if (i < new_nc_id)
                        graph.m += weight;
                }
			}
                return graph;
        }
    }
}

