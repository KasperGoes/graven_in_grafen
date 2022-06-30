using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public static class Modularity
	{
		//public static float modularity(Graph g)
  //      {
		//	float modularity = 0;

		//	for(int i = 0; i < g.n; i ++)
  //          {
		//		Vertex v = g.vertices[i];

		//		for(int j = 0; j < g.n; j++)
  //              {
  //                  int factor = 1;

  //                  if (i == j)
  //                      factor = 2;

  //                  Vertex u = g.vertices[j];

		//			modularity += (factor * g.AdjacencyMatrix[v.id, u.id] - ((float)v.degree * (float)u.degree / (float)(2 * g.m))) * delta(v, u) ;
  //              }
  //          }

		//	modularity = modularity / (2 * (float)g.m);

		//	return modularity;
  //      }

        public static float modularity_given_partition(Graph g, Dictionary<int,int> partition)
        {
            float modularity = 0;

            for (int i = 0; i < g.n; i++)
            {
                Vertex v = g.vertices[i];

                for (int j = 0; j < g.n; j++)
                {
                    int factor = 1;

                    if (i == j)
                        factor = 2;

                    Vertex u = g.vertices[j];

                    modularity += (factor * g.AdjacencyMatrix[v.id, u.id] - ((float)v.degree * (float)u.degree / (float)(2 * g.m))) * delta(partition, v, u);
                }
            }

            modularity = modularity / (2 * (float)g.m);

            return modularity;
        }

        public static int delta(Dictionary<int, int> partition, Vertex v, Vertex u)
        {
            if (partition[v.id] == partition[u.id])
                return 1;
            else
                return 0;
        }


        public static float mod2(Graph g)
        {
			float modularity = 0;

			foreach(Community com in g.communities.Values)
            {
                // TO DO: nu halveer je de som van de community
                float lc = com.sum_in;
                float m = g.m;
				float kc = com.sum_tot;

				modularity += (lc / m) - (float)Math.Pow(kc / (2 * m),2);
            }

			return modularity;
        }

		//public static int delta(Vertex v, Vertex u)
		//{
		//	if (v.community == u.community)
		//		return 1;
		//	else
		//		return 0;
		//}

		public static float modularity_difference(Graph g, Community community, Vertex vertex)
        {
			float add = modularity_difference_add(g, community, vertex);
			float remove = modularity_difference_remove(g, g.communities[vertex.community], vertex);

			return add - remove;
        }


        private static float modularity_difference_add(Graph g, Community community, Vertex vertex)
		{
			int degree = vertex.degree;
			int degree_in_com = community.sum_in_community_per_vertex(g, vertex);
			float sum_in = community.sum_in;
			float sum_tot = community.sum_tot;

			float modularity_difference = mod_diff(g.m, degree, degree_in_com, sum_in, sum_tot);

            return modularity_difference;
		}

		private static float modularity_difference_remove(Graph g, Community community, Vertex vertex)
        {
            int degree = vertex.degree;
            int degree_in_com = community.sum_in_community_per_vertex(g, vertex);
            float sum_in = (float)community.sum_in - degree_in_com;
            float sum_tot = community.sum_tot - degree;

            float modularity_difference = mod_diff(g.m, degree, degree_in_com, sum_in, sum_tot) ;

			return modularity_difference;
        }

		private static float mod_diff(float m, float degree, float degree_in_com, float sum_in, float sum_tot)
        {
            float first = (sum_in + (2 * (float)degree_in_com)) / (2 * m);
            float second = (float)Math.Pow((sum_tot + (float)degree) / (2 * m), 2);
            float third = sum_in / (2 * m);
            float fourth = (float)Math.Pow(sum_tot / (2 * m), 2);
            float fifth = (float)Math.Pow(degree / (2 * m), 2);

            float modularity_difference = (first - second) - (third - fourth - fifth);

            return modularity_difference;
        }
	}
}

