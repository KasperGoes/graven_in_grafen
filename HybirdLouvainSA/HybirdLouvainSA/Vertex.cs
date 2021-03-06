using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public class Vertex
	{
		public int id;
		public int degree;

		public int community;

		public List<int> neighbours;

		public LinkedList original_vertices; // Stores the id's from the vertices in original graphs in reduces Louvain graph

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
}

