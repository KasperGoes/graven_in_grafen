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
				(Vertex n, Vertex vertex) = compute_candadite(g);
				float delta = Modularity.modularity_difference(g, g.communities[n.community], vertex);
				
				if(delta > 0)
				{
					vertex.switch_community(n.community);
				}
				else
				{
					delta = delta * 2*g.m;
					double acceptProb = Math.Exp(delta/temperature);
					double random_probability = random.NextDouble();

					if (random_probability < Math.Exp(delta/temperature))
					{ 
						vertex.switch_community(n.community);
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

		public static (Vertex, Vertex) compute_candadite(Graph g)
		{
			bool found_candidate = false;
			Vertex v, u;

			// TO DO: Use the communities and their neighbouring communities to swap vertices instead of picking random vertices
			while(!found_candidate)
            {
				// We pick a random vertex
				int number = random.Next(g.vertices.Length);
				v = g.vertices[number];

				// Look at the neightbours of vertex and see if they have a different community
				// TO DO: Change neighbour solution to picking random community and a random neighbouring community with vertex
				int random_neighbour_index = random.Next(v.neighbours.Count);
				u = g.vertices[v.neighbours[random_neighbour_index]];

				if (u.community != v.community)
				{ return (u, v); }
			}

			return (null, null);
		}
	}
}

