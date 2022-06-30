using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
    public class Graph
    {
        public int n; // number of vertices
        public int m; // number of edges

        public float modularity;

        public Vertex[] vertices; 

        public int[,] AdjacencyMatrix;

        public Dictionary<int, Community> communities; // Stores the community objects with their corresponding id's
        public RandomList<int> community_list; // Stores the communities in random list for fast random element acces

        public Dictionary<int, int> partition; // Partition: the key denotes the vertex, value is its community

        public Graph(int n)
        {
            this.n = n;
            vertices = new Vertex[n];
            AdjacencyMatrix = new int[n, n];
            communities = new Dictionary<int, Community>();
            partition = new Dictionary<int, int>();
            community_list = new RandomList<int>();
        }

        // Initialize the graph such that every node has its own community
        public void set_initial_community_per_node()
        {
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

            initialize_community_list();
        }

        // Assign vertex to another community
        public void switch_to_community(Vertex vertex, Vertex v_in_com)
        {
            Community old_commmunity = this.communities[vertex.community];
            Community new_community = this.communities[v_in_com.community];

            update_all_neighbouring_communities(vertex, v_in_com);

            old_commmunity.remove_vertex(this, vertex);
            new_community.add_vertex(this, vertex);
            vertex.switch_community(v_in_com.community);
        }

        // Update neighbouring communities given a vertex community swap
        private void update_all_neighbouring_communities(Vertex vertex_swap, Vertex vertex_in_com)
        {
            Community old_commmunity = this.communities[vertex_swap.community];
            Community new_community = this.communities[vertex_in_com.community];

            for (int i = 0; i < vertex_swap.neighbours.Count; i++)
            {
                Vertex u = this.vertices[vertex_swap.neighbours[i]];
                Community u_communtiy = this.communities[u.community];

                if (u.community != new_community.id && u.community != old_commmunity.id)
                {
                    u_communtiy.neighbouring_communities.add_update_neighbouring_community(this, new_community.id, u.id, vertex_swap.id);
                    u_communtiy.neighbouring_communities.remove_update_neighbouring_community(this, old_commmunity.id, u.id, vertex_swap.id);

                    new_community.neighbouring_communities.add_update_neighbouring_community(this, u.community, vertex_swap.id, u.id);
                    old_commmunity.neighbouring_communities.remove_update_neighbouring_community(this, u.community, vertex_swap.id, u.id);
                }

                if(u.community == new_community.id)
                {
                    u_communtiy.neighbouring_communities.remove_update_neighbouring_community(this, old_commmunity.id, u.id, vertex_swap.id);
                    old_commmunity.neighbouring_communities.remove_update_neighbouring_community(this, u.community, vertex_swap.id, u.id);
                }

                if(u.community == old_commmunity.id)
                {
                    u_communtiy.neighbouring_communities.add_update_neighbouring_community(this, new_community.id, u.id, vertex_swap.id);
                    new_community.neighbouring_communities.add_update_neighbouring_community(this, u.community, vertex_swap.id, u.id);
                }
            }
        }

        // Combines the original vertices for each community to determine partition
        public void compute_final_communities()
        {
           for (int i = 0; i < vertices.Length; i++)
            {
                Vertex vertex = vertices[i];
                vertex.original_vertices.add_to_partition(vertex.community, partition);
            }
        }

        public void initialize_community_list()
        {
            foreach(int community in communities.Keys)
            {
                community_list.Add(community);
            }
        }
    }
}

