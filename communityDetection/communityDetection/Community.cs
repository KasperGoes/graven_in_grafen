using System;
namespace communityDetection
{
	public class Community
    {
		public int id;
		public int sum_in = 0;
		public int sum_tot = 0;
		public HashSet<int> vertices;

		public Community(int id, Vertex v)
        {
			this.id = id;
			this.vertices = new HashSet<int> { id };
			this.sum_tot = v.degree;
		}

		public int update_sum_in(Vertex vertex)
		{
			int degree_in_com = 0;
			for (int i = 0; i < vertex.degree; i++)
				if (this.vertices.Contains(vertex.neighbours[i]))
					degree_in_com++; // TO DO: sum over the weights instead of just count

			return degree_in_com;
		}
	}
}

