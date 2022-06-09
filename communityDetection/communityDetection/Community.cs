using System;
namespace communityDetection
{
	public class Community
    {
		public int id;
		public int sum_in = 0;
		public int sum_tot = 0;
		public HashSet<int> vertices;
		public HashSet<int> original_vertices;

		public Community(int id, Vertex v)
        {
			this.id = id;
			this.vertices = new HashSet<int>{ id };
			this.original_vertices = new HashSet<int>();
			this.sum_tot = v.degree;
		}

		/// <summary>
		/// Computes the new sum of weights in the community after adding a vertex
		/// </summary>
		/// <param name="community"></param>
		/// <param name="vertex"></param>
		/// <returns></returns>
		public int update_sum_in(Vertex vertex)
		{
			int degree_in_com = 0;
			for (int i = 0; i < vertex.degree; i++)
				if (this.vertices.Contains(vertex.neighbours[i]))
					degree_in_com++;

			return degree_in_com;
		}
	}

}

