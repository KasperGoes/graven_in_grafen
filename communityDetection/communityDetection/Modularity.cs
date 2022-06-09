using System;
namespace communityDetection
{
	public static class Modularity
	{
		/// <summary>
        /// Computes the modularity of the enitre graph
        /// </summary>
        /// <param name="g"></param> Graph
        /// <returns></returns>
		public static float modularity(Graph g)
        {
			float modularity = 0;

			for(int i = 0; i < g.n; i ++)
            {
				Vertex v = g.vertices[i];

				for(int j = 0; j < g.n; j++)
                {
					Vertex u = g.vertices[j];
					modularity += (g.AdjacencyMatrix[v.id, u.id] - ((float)v.degree * (float)u.degree / (float)(2 * g.m))) * delta(v, u);
				}
            }

			modularity = modularity / (2 * (float)g.m);

			return modularity;
        }

		public static int delta(Vertex v, Vertex u)
		{
			if (v.community == u.community)
				return 1;
			else
				return 0;
		}

		/// <summary>
		/// Computes the modularity difference after adding a vertex
		/// </summary>
		/// <param name="g"></param>
		/// <param name="community"></param>
		/// <param name="vertex"></param>
		/// <returns></returns>
		public static float modularity_difference(Graph g, Community community, Vertex vertex)
		{
			int degree = vertex.degree;

			int degree_in_com = community.update_sum_in(vertex);

			float first = ((float)community.sum_in + (2 * (float)degree_in_com)) / (2 * (float)g.m);
			float second =  (float)Math.Pow(((( (float) community.sum_tot + (float)degree) / (2 * (float)g.m))), 2);
			float third = (float) community.sum_in / (2 * (float)g.m);
			float fourth = (float) Math.Pow(((float)community.sum_tot / (2 * (float)g.m)), 2);
			float fifth = (float) Math.Pow((degree / (float)(2 * (float)g.m)), 2);

			float modularity_difference = (first - second) - (third - fourth - fifth);

			return modularity_difference;
		}
	}
}

