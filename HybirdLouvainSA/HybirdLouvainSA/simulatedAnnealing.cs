using System;

namespace HybridLouvainSA
{
    public static class SA
    {
        static Random random = new Random();

        public static Graph simulatedAnnealing(Graph g, bool hybrid, float alpha, float temperature, float epsilon)
        {
            // If the experiment is not hybrid, sest the each node as its own community
            if (!hybrid)
                g.set_initial_community_per_node();

            g.modularity = Modularity.mod2(g);

            int iteration = 0;

            // While stopping criterium is not met
            while (temperature > epsilon)
            {
                dynamic found_candidate = compute_candadite(g);

                if (found_candidate is bool)
                {
                    Console.WriteLine("No more candidates: " + g.modularity);
                    break;
                }

                int n = found_candidate.Item1;
                int vertex = found_candidate.Item2;

                Vertex v_in_com = g.vertices[n];
                Vertex v_to_move = g.vertices[vertex];

                int new_community = v_in_com.community;

                float mod_difference = Modularity.modularity_difference(g, g.communities[new_community], v_to_move);

                if (mod_difference > 0)
                {
                    g.switch_to_community(v_to_move, v_in_com);
                    g.modularity += mod_difference;
                }
                else
                {
                    double mod_differencechance = mod_difference;
                    double acceptProb = Math.Exp(mod_differencechance / temperature);
                    double random_probability = random.NextDouble();

                    if (random_probability < acceptProb && mod_difference != 0)
                    {
                        g.switch_to_community(v_to_move, v_in_com);
                        g.modularity += mod_difference;
                    }
                }

                temperature = temperature * alpha;

                iteration++;
            }

            return g;
        }

        // Finds candidate community swap
        private static dynamic compute_candadite(Graph g)
        {
            // Get random community
            dynamic random_com = g.community_list.get_random_element();
            if (random_com is not int)
                return false;

            Community community = g.communities[random_com];

            // If possible, get random neighbouring community
            dynamic random_nc = community.neighbouring_communities.communtity_ids.get_random_element();

            if (random_nc is not int)
                return false;

            dynamic edge_tuple = community.neighbouring_communities.connecting_edges[random_nc].get_random_element();

            return edge_tuple;
        }
    }
}

