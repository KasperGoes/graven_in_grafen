using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
    public class Graph
    {
        public int n; // number of vertices
        public int m; // number of edges

        public float modularity;

        public Vertex[] vertices; // all vertices in the graph

        public int[,] AdjacencyMatrix;

        public Dictionary<int, Community> communities;

        public Graph(int n)
        {
            this.n = n;
            vertices = new Vertex[n];
            AdjacencyMatrix = new int[n, n];
            communities = new Dictionary<int, Community>();
        }

        public void set_initial_community_per_node()
        {
            // Create community for ever node
            for (int i = 0; i < this.vertices.Length; i++)
            {
                Vertex vertex = this.vertices[i];
                this.communities.Add(i, new Community(i, this.vertices[i], AdjacencyMatrix[i,i]));

                for (int j = 0; j < this.vertices[i].neighbours.Count; j++)
                {
                    int vertex_in_nc = this.vertices[i].neighbours[j];
                    
                    communities[i].neighbouring_communities.add_update_neighbouring_community(this, vertex_in_nc, vertex.id, vertex_in_nc);
                }
            }
        }

        public void switch_to_community(Vertex vertex, Vertex v_in_com)
        {
            Community old_commmunity = this.communities[vertex.community];
            Community new_community = this.communities[v_in_com.community];

            old_commmunity.remove_vertex(this, vertex);
            new_community.add_vertex(this, vertex);
            vertex.switch_community(v_in_com.community);
        }

        //Vertex_swap = relocated vertex
        //Vertex_in_com = neighbour vertex of vertex_swap which was already in communtiy
        public void update_all_neighbouring_communities(Vertex vertex_swap, Vertex vertex_in_com)
        {
            Community old_commmunity = this.communities[vertex_swap.community];
            Community new_community = this.communities[vertex_in_com.community];

            for (int i = 0; i < vertex_swap.neighbours.Count; i++)
            {
                Vertex u = this.vertices[vertex_swap.neighbours[i]];
                Community u_communtiy = this.communities[u.community];

                if (u.community != new_community.id && u.community != old_commmunity.id)
                {
                    //Update neighbouring communities for the community of u
                    u_communtiy.neighbouring_communities.add_update_neighbouring_community(this, new_community.id, u.id, vertex_swap.id);
                    u_communtiy.neighbouring_communities.remove_update_neighbouring_community(this, old_commmunity.id, u.id, vertex_swap.id);

                    //Update the old and new communtie of the reassigned vertex
                    new_community.neighbouring_communities.add_update_neighbouring_community(this, u.community, vertex_swap.id, u.id);
                    old_commmunity.neighbouring_communities.remove_update_neighbouring_community(this, u.community, vertex_swap.id, u.id);
                }

                if(u.community == new_community.id)
                {
                    //Update neighbouring communities for the community of u
                    u_communtiy.neighbouring_communities.remove_update_neighbouring_community(this, old_commmunity.id, u.id, vertex_swap.id);
                    old_commmunity.neighbouring_communities.remove_update_neighbouring_community(this, u.community, vertex_swap.id, u.id);
                }

                if(u.community == old_commmunity.id)
                {
                    //Update neighbouring communities for the community of u
                    u_communtiy.neighbouring_communities.add_update_neighbouring_community(this, new_community.id, u.id, vertex_swap.id);
                    new_community.neighbouring_communities.add_update_neighbouring_community(this, u.community, vertex_swap.id, u.id);
                }
            }
        }
    }
}

