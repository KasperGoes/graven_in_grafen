using System;

namespace HybridLouvainSA
{
	public static class SA
	{
		static Random random = new Random();

		public static float simulatedAnnealing(Graph g)
		{
            Console.WriteLine(Modularity.modularity(g));

			// NEEDS TO BE REMOVED ONCE EXPERIMENT CODE IS FINSISHED
			g.set_initial_community_per_node();
			int iteration =-1;

			float alpha =0.90F;

			double temperature = 1.00;
			double epsilon = 0.4;

			while(iteration < 10000000)
			{
				dynamic found_candidate = compute_candadite(g);

				if (found_candidate is bool)
                {
					Console.WriteLine("No more candidates: " + g.modularity);
					break;
                }
					
				int n = found_candidate.Item1;
                int vertex = found_candidate.Item2;

                Vertex v_in_com = g.vertices[n];
				Vertex v_to_move = g.vertices[vertex];

				int new_community = v_in_com.community;

				float delta = Modularity.modularity_difference(g, g.communities[new_community], v_to_move);
				
				if(delta > 0)
				{
                    g.update_all_neighbouring_communities(v_to_move, v_in_com);
                    g.switch_to_community(v_to_move, v_in_com);
					g.modularity += delta;
                }
                else
                {
                    delta = delta * 2 * g.m;
                    double acceptProb = Math.Exp(delta / temperature);
                    double random_probability = random.NextDouble();

                    if (random_probability < acceptProb)
                    {
                        g.update_all_neighbouring_communities(v_to_move, v_in_com);
                        g.switch_to_community(v_to_move, v_in_com);
                        g.modularity += delta;
                    }
                }

                temperature = temperature*alpha;

				if (iteration % 1000 == 0)
				{
					Console.WriteLine(Modularity.mod2(g));
				}

				iteration++;
			}

            Console.WriteLine(Modularity.modularity(g));
			return g.modularity;
		}

		private static dynamic compute_candadite(Graph g)
		{
			int random_com = g.community_list.get_random_element();

			Community community = g.communities[random_com];

			dynamic random_nc = community.neighbouring_communities.communtity_ids.get_random_element();

			if (random_nc is not int)
				return false;

			dynamic edge_tuple = community.neighbouring_communities.connecting_edges[random_nc].get_random_element();

			return edge_tuple;
		}
	}
}

