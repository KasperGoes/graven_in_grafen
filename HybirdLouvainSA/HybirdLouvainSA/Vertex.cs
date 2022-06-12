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

		public HashSet<int> original_vertices;

		public NeighbouringCommunities neighbouring_communities;

		public Vertex(int id, int community)
		{
			this.id = id;
			this.community = community;
			this.neighbours = new List<int>();
			this.original_vertices = new HashSet<int>();
			this.neighbouring_communities = new NeighbouringCommunities();
		}

		public void switch_to_new_community(Graph graph, Vertex neighbour)
		{
			Community old_commmunity = graph.communities[this.community];
			Community new_community = graph.communities[neighbour.community];

			foreach(int nc in neighbouring_communities.communtity_ids)
            {
				if (nc == new_community.id)
					graph.communities[nc].neighbouring_communities.add_update_neighbouring_community(graph, new_community.id, this.id, neighbour.id);
				graph.communities[nc].neighbouring_communities.remove_update_neighbouring_community(graph, old_commmunity.id, this.id, neighbour.id);
			}

			foreach(int nv in neighbours)
            {
				if (graph.vertices[nv].community != new_community.id)
					graph.vertices[nv].neighbouring_communities.add_update_neighbouring_community(graph, new_community.id, this.id, neighbour.id);

				graph.vertices[nv].neighbouring_communities.remove_update_neighbouring_community(graph, old_commmunity.id, this.id, neighbour.id);
			}

			old_commmunity.update_old_community(graph, this, neighbour);
			new_community.update_new_community(graph, this, neighbour);

			this.community = new_community.id;
		}
	}
}

