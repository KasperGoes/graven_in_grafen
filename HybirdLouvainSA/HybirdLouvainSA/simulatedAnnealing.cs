using System;

namespace HybridLouvainSA
{
	public static class SA
	{
		static Random random = new Random();

		public static float simulatedAnnealing(Graph g)
		{
            Console.WriteLine(Modularity.modularity(g));
			int iteration =-1;

			float alpha =0.90F;

			double temperature = 1.00;
			double epsilon = 0.4;
			
			while(temperature > epsilon)
			{
				(int n, int vertex) = compute_candadite(g);

				Vertex v_in_com = g.vertices[n];
				Vertex v_to_move = g.vertices[vertex];

				int new_community = v_in_com.community;

				float delta = Modularity.modularity_difference(g, g.communities[new_community], v_to_move);
				
				if(delta > 0)
				{
					v_to_move.switch_community(new_community);
				}
				else
				{
					delta = delta * 2* g.m;
					double acceptProb = Math.Exp(delta/temperature);
					double random_probability = random.NextDouble();

					if (random_probability < Math.Exp(delta/temperature))
					{ 
						v_to_move.switch_community(new_community);
					}
				}

				temperature = temperature*alpha;

				if (iteration % 400 == 0)
				{
					Console.WriteLine(Modularity.modularity(g));
				}

				iteration++;
			}

            Console.WriteLine(Modularity.modularity(g));
			return Modularity.modularity(g);
		}

		private static (int, int) compute_candadite(Graph g)
		{
			int random_com = g.community_list.get_random_element();

			Community community = g.communities[random_com];

			int random_nc = community.neighbouring_communities.communtity_ids.get_random_element();

			(int vertex_in_com, int vertex_in_nc) = community.neighbouring_communities.connecting_edges[random_nc].get_random_element();

			return (vertex_in_com, vertex_in_nc);
		}
	}
}

