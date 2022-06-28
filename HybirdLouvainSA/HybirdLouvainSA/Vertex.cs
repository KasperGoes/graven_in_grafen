using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public class Vertex
	{
		public int id;
		public int degree;

		public int sum_degrees;

		public int community;

		public List<int> neighbours;

		public LinkedList original_vertices;

		public Vertex(int id, int community)
		{
			this.id = id;
			this.community = community;
			this.neighbours = new List<int>();
			this.original_vertices = new LinkedList();
			original_vertices.Add(id);
		}

		public void switch_community(int new_community)
		{
			this.community = new_community;
		}
    }

	public struct com_edge
    {
		public int in_com;
		public int out_com;

		public com_edge(int inc, int outc)
        {
			this.in_com = inc;
			this.out_com = outc;
        }
    }
}

