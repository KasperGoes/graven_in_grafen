using System;
using System.Collections;

namespace communityDetection
{
	public static class Louvain
	{
		public static Graph louvain(Graph g)
        {
			// Create community for ever node
			for (int i = 0; i < g.vertices.Length; i++)
				g.communities.Add(i, new Community(i, g.vertices[i]));

			float modularity = Modularity.modularity(g);

			// Create random order of vertices
			List<int> order = Enumerable.Range(0, g.vertices.Length).ToList();
			Random random = new Random();
			List <int> random_order = order.OrderBy(x => random.Next()).ToList();

			bool found_improvement_all_vertices = true;

			// While improvement is possible
			// TO DO: use threshold for modularity improvement 
			while(found_improvement_all_vertices)
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
					foreach(int neighbour in v.neighbours)
                    {
						Vertex u = g.vertices[neighbour];

						if (u.community == v.community)
							continue;

						float modularity_diff = Modularity.modularity_difference(g, g.communities[u.community], v);

						if(modularity_diff > max_gain)
						{
							max_gain = modularity_diff;
							best_community = u.community;
							found_improvement = true;
						}
                    }

					// If improvement found, update the corresponding community
					if (found_improvement)
                    {
						update_graph(g, g.communities[best_community], v);
						modularity += max_gain;
						found_improvement_all_vertices = true;
					}
				}
			}

			//PHASE 2 + check to stop when number of vertices is < 1000

			return null;
        }

		/// <summary>
        /// Updates the graph for the one community after adding a vertex 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="community"></param>
        /// <param name="vertex"></param>
		public static void update_graph(Graph g, Community community, Vertex vertex)
        {
			float modularitydiff = Modularity.modularity_difference(g, community, vertex);
			// Remove from old community
			g.communities[vertex.community].vertices.Remove(vertex.id);

            //If the resulting community is empty, remove community from graph
			if (g.communities[vertex.community].vertices.Count == 0)
                g.communities.Remove(vertex.community);

            // Add to new community
            community.vertices.Add(vertex.id);
			vertex.community = community.id;

			int degree_in_community = update_sum_in(community, vertex);

			// Update sums
			community.sum_in += degree_in_community;
			community.sum_tot = community.sum_tot + vertex.degree;
		}

		/// <summary>
		/// Computes the new sum of weights in the community after adding a vertex
		/// </summary>
		/// <param name="community"></param>
		/// <param name="vertex"></param>
		/// <returns></returns>
		public static int update_sum_in(Community community, Vertex vertex)
		{
			int degree_in_com = 0;
			for (int i = 0; i < vertex.degree; i++)
				if (community.vertices.Contains(vertex.neighbours[i]))
					degree_in_com++;

			return degree_in_com;
		}
	}
}

