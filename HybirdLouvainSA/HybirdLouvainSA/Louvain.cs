using System;
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
				
				graph = phase_two(g);
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
			//List<int> order = Enumerable.Range(0, g.vertices.Length).ToList();
			//Random random = new Random();
			//List<int> random_order = order.OrderBy(x => random.Next()).ToList();

			// TEMPORARY SAME ORDER VERTICES
			List<int> random_order = Enumerable.Range(0, g.vertices.Length).ToList();

			bool found_improvement_all_vertices = true;

			// While improvement is possible
			// TO DO: use threshold for modularity improvement 
			while (found_improvement_all_vertices)
			{
				found_improvement_all_vertices = false;

				// Loop through all vertices in random order
				for (int i = 0; i < g.vertices.Length; i++)
				{
					float max_gain = 0;
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
					if (found_improvement)
					{
						v.switch_to_new_community(g, best_neighbour);
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

			for(int i = 0; i < number_communities; i++)
            {
				Vertex vertex = new Vertex(i, i);
				
				graph.vertices[i] = vertex;

				graph.AdjacencyMatrix[i, i] = 2 * old_graph.communities[i].sum_in;

				foreach(int n in old_graph.communities[i].neighbouring_communities.communtity_ids)
                {
					vertex.neighbours.Add(n);
					graph.AdjacencyMatrix[i, n] = old_graph.communities[i].neighbouring_communities.total_weight[n];
					vertex.degree++;
					vertex.sum_degrees += old_graph.communities[i].neighbouring_communities.total_weight[n];
				}
			}

			return graph;
        }
	}
}

