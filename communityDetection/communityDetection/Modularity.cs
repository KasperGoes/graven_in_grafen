using System;
namespace communityDetection
{
	public static class Modularity
	{
		// TO DO: FILL IN
		public static float modularity(Graph g)
        {
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
			// TO DO: Check interpretations of sum_in and sum_out

			int W = g.W;
			int degree = vertex.degree;

			int degree_in_com = Louvain.update_sum_in(community, vertex);

			int new_sum_in = community.sum_in + (degree_in_com * 2);
			int new_sum_tot = community.sum_tot + degree;

			float first = ((new_sum_in + 2 * degree_in_com) / W) - (float)Math.Pow(((new_sum_tot + degree) / W), 2);
			float second = new_sum_in / (2 * W) - (float)Math.Pow((new_sum_tot / (2 * W)), 2) - (float)Math.Pow((degree / (2 * W)), 2);

			float modularity_difference = first - second;

			return modularity_difference;
		}
	}
}

