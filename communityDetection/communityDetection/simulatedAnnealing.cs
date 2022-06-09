using System;
namespace communityDetection
{
	public  class myClass
    {
        public Graph a;
        public object Clone()
        {
            return this.MemberwiseClone();
        }
	}
	public class simulatedAnnealing
	{
		public float simulatedAnnealing(Graph g)
		{

			int iteration =-1;
			//the probability
			double proba;
			double alpha =0.999;

			//vm veel te hoge temp met onze delta
			double temperature = 400.0;
			double epsilon = 0.001;
			float delta;

			while(temperature > epsilon)
			{
				iteration++;
        		//get the next random permutation of distances 

				candadite, community, vertex = compute_candadite(g);
				delta = modularity_difference(candadite, community, vertex)
				
				//if we improve we accept the change
				if(delta>0)
				{
					//update graph
					g = candadite;
				}
				// if we dont improve we accept the change with a chance
				else
				{
					double acceptProb = Math.Exp(delta/temperature);
					proba = rnd.NextDouble();
					if (proba < Math.Exp(delta/temperature))
					{
						//update graph
						g = candadite;
					}
				}
				// we cool every iteration
				temperature = temperature*alpha;	

				//print modularity every 400 iterations
				if (iteration%400==0)
				Console.WriteLine(modularity(g));
			}
			return modularity(g);
		}

		// generate the next iteration
		public static void compute_candadite(Graph g)
		{	
			Graph candadite = g.Clone();
			while(true)
			{
				// we pick a random vertex
				int number = random.Next(candadite.vertices.Count);
				Vertex vertex = candadite.vertices[number];

				//look at the neightbours of vertex and see if they have a different community
				foreach(int neighbour in vertex.neighbours)
				{
					Vertex neighbour = candadite.vertices[neighbour];
					if(neighbour.community != vertex.community)
					{
						//we have a change
						candadite.communities[vertex.community].vertices.remove(vertex);

						//we update the community of the vertex
						vertex.community = neighbour.community;

						//we update the community of the vertex
						candadite.communities[vertex.community].vertices.Add(vertex);

						return candadite, neighbour.community, vertex 
					}
				}
			}
		}	
	}
}

