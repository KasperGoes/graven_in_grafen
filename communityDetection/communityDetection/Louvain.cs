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
				g.communities.Add(i,new Community(i, g.vertices[i]));

			float modularity = Modularity.modularity(g);

			// Create random order of vertices
			List<int> order = Enumerable.Range(0, g.vertices.Length).ToList();
			Random random = new Random();
			List <int> random_order = order.OrderBy(x => random.Next()).ToList();

			float max_gain = 0;
			bool found_improvement = true;

			// While improvement is possible
			while(found_improvement)
            {
				found_improvement = false;

				// Loop through all vertices in random order
				for (int i = 0; i < g.vertices.Length; i++)
                {
					// Acces random vertex
					Vertex vertex = g.vertices[random_order[i]];
					int best_community = -1;
					
					// Compute best change in community if possible
					foreach (int neighbourcom in g.AdjacenceList[vertex.id])
                    {
						float modularity_diff = Modularity.modularity_difference(g, g.communities[neighbourcom], vertex);
						if(modularity_diff > max_gain)
						{
							max_gain = modularity_diff;
							best_community = neighbourcom;
							found_improvement = true;
						}
                    }

					// If improvement found, update the corresponding community
					if (found_improvement)
                    {
						update_communities(g, g.communities[best_community], vertex);
						modularity += max_gain;
					}
				}
			}

			//PHASE 2 + check to stop when number of vertices is < 1000

			return null;
        }

		/// <summary>
        /// Computes the new sum of weights in the community after adding a vertex
        /// </summary>
        /// <param name="community"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
		private static int update_sum_in(Community community, Vertex vertex)
        {
			int degree_in_com = 0;
			for (int i = 0; i < vertex.degree; i++)
				if (community.vertices.Keys.Contains(vertex.neighbours[i].id))
					degree_in_com++;

			return degree_in_com;
        }

		/// <summary>
        /// Updates the community values after adding a vertex 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="community"></param>
        /// <param name="vertex"></param>
		public static void update_communities(Graph g, Community community, Vertex vertex)
        {
			// Remove from old community
			g.communities[vertex.community].vertices.Remove(vertex.id);

			// If the resulting community is empty, remove community from graph
			if (g.communities[vertex.community].vertices.Count == 0)
				g.communities.Remove(vertex.community);

			// Add to new community
			community.vertices.Add(vertex.id, vertex);
			int degree_in_com = update_sum_in(community, vertex);

			// Update sums
			community.sum_in = community.sum_in + (degree_in_com * 2);
			community.sum_tot = community.sum_tot + vertex.degree;
		}
	}
}

