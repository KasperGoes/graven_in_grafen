using System;
namespace communityDetection
{
	public static class SA
	{
		static Random random = new Random();

		public static float simulatedAnnealing(Graph g)
		{
            Console.WriteLine(Modularity.modularity(g));
			int iteration =-1;

			float alpha =0.90F;

			//vm veel te hoge temp met onze delta
			double temperature = 1.00;
			double epsilon = 0.1;
			
			while(temperature > epsilon)
			{
				iteration++;
        		//get the next random permutation of distances 

				(Community community, Vertex vertex) = compute_candadite(g);
				float delta = Modularity.modularity_difference(g, community, vertex);
				
				//if we improve we accept the change
				if(delta > 0)
				{
					//update graph
					g.update_graph(community, vertex);
				}
				// if we dont improve we accept the change with a chance
				else
				{
					delta = delta * 2*g.m;
					double acceptProb = Math.Exp(delta/temperature);
					double random_probability = random.NextDouble();
					if (random_probability < Math.Exp(delta/temperature))
					{
						//update graph
						g.update_graph(community, vertex);
					}
				}

				// we cool every iteration
				temperature = temperature*alpha;

				//print modularity every 400 iterations
				if (iteration % 400 == 0)
				{
					Console.WriteLine(Modularity.modularity(g));
				}
			}
            Console.WriteLine(Modularity.modularity(g));
			return Modularity.modularity(g);
		}

		// generate the next iteration
		public static (Community, Vertex) compute_candadite(Graph g)
		{
			//Graph candadite = g.Clone();
			bool found_candidate = false;
			Vertex v, u;

			while(!found_candidate)
            {
				// We pick a random vertex
				int number = random.Next(g.vertices.Length);
				v = g.vertices[number];

				// Look at the neightbours of vertex and see if they have a different community
				// TO DO: Random neighbour instead of first neighbour in the list
				

				
				int random_neighbour_index = random.Next(v.neighbours.Count);
				u = g.vertices[v.neighbours[random_neighbour_index]];
				//int index = 0;
				//int neighbour = v.neighbours[index];
				//u = g.vertices[neighbour];
				//bool found_candidate_neighbour = true;

				if (u.community != v.community)
				{ return (g.communities[u.community], v); }
             
				// While neighbour u has the same community as v, try a new neighbour 
				//while (u.community == v.community)
				//{
				//	index++;

				//	if (index >= v.neighbours.count)
    //                {
				//		found_candidate_neighbour = false;
				//		break;
    //                }

				//	neighbour = v.neighbours[index];
				//	u = g.vertices[neighbour];
				//}

				//if(found_candidate_neighbour)

				//	return (g.communities[u.community], v);
			}

			return (null, null);
		}
	}
}

