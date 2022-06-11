using System;
using System.Collections.Generic;
using System.Linq;

namespace communityDetection
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
				(Graph graph, bool test) = phase_one(g);

				// NOG AANPASSEN VANWEGE NIEUWE VS
				change = test;
				//Graph new_graph = phase_two(g);
				//g = new_graph;
			}

			return g;
		}

		private static (Graph, bool) phase_one(Graph g)
        {
			// Boolean variable to check if the graph is changend during any of the iterations
			bool change = false;

			// Create community for ever node
			for (int i = 0; i < g.vertices.Length; i++)
				g.communities.Add(i, new Community(i, g.vertices[i]));

			float modularity = Modularity.modularity(g);

			// Create random order of vertices
			List<int> order = Enumerable.Range(0, g.vertices.Length).ToList();
			Random random = new Random();
			List<int> random_order = order.OrderBy(x => random.Next()).ToList();

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
					int best_community = -1;

					// Compute best change in community if possible
					// TO DO: check if already added to neighbouring community, create list of communities to check
					foreach (int neighbour in v.neighbours)
					{
						Vertex u = g.vertices[neighbour];

						if (u.community == v.community)
							continue;

						float modularity_diff = Modularity.modularity_difference(g, g.communities[u.community], v);

						if (modularity_diff > max_gain)
						{
							max_gain = modularity_diff;
							best_community = u.community;
							found_improvement = true;
						}
					}

					// If improvement found, update the corresponding community
					if (found_improvement)
					{
						g.update_graph(g.communities[best_community], v);
						modularity += max_gain;
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

				// Add original vertices from old graph to new vertex
				// degree of vertex v
				
            }

			return null;
        }
	}
}

