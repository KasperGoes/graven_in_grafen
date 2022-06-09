using System;
namespace communityDetection
{
	//public  class myClass
 //   {
 //       public Graph a;
 //       public object Clone()
 //       {
 //           return this.MemberwiseClone();
 //       }
	//}

	public static class SA
	{
		static Random random = new Random();

		public static Graph simulatedAnnealing(Graph g)
		{
			int iteration =-1;

			float alpha =0.999F;

			//vm veel te hoge temp met onze delta
			double temperature = 400.0;
			double epsilon = 0.001;
			
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
				if (iteration%400==0)
<<<<<<< HEAD
				Console.WriteLine(modularity(g));
			}
			return modularity(g);
=======
				Console.WriteLine(Modularity.modularity(g));
			}

			return g;
>>>>>>> 752c02feb01c430bcb86d0c932fa229aa897c238
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
				int index = 0;
				int neighbour = v.neighbours[index];
				u = g.vertices[neighbour];
				bool found_candidate_neighbour = true;


				// While neighbour u has the same community as v, try a new neighbour 
				while (u.community == v.community)
				{
					index++;

					if (index >= v.neighbours.Count)
                    {
						found_candidate_neighbour = false;
						break;
                    }

					neighbour = v.neighbours[index];
					u = g.vertices[neighbour];
				}

				if(found_candidate_neighbour)
					return (g.communities[u.community], v);
			}

			return (null, null);
		}
	}
}

