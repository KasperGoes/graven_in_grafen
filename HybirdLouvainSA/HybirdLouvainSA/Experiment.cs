using System;
using System.Collections.Generic;

namespace HybridLouvainSA
{
	public static class Experiment
	{
		/// <summary>
        /// Performs experiment based on the given parameters
        /// </summary>
        /// <param name="exp"></param> Denotes the experiment, can be hybrid, louvain or simulated annealing
        /// <param name="graph"></param>
        /// <param name="switch_treshold"></param> Denotes the number of nodes threshold to transition from louvain to sa
        /// <param name="og_graph"></param>
        /// <returns></returns> Returns the modularity and the corresponding partition
		public static (float, Dictionary<int,int>) perform_experiment(experiment exp, Graph graph, int switch_treshold, Graph og_graph)
        {
			switch(exp)
            {
				case experiment.hybrid:
                    graph = Louvain.louvain(graph, switch_treshold);
                    graph = SA.simulatedAnnealing(graph, true);
                    break;

				case experiment.sa:
                    graph = SA.simulatedAnnealing(graph, false);
                    break;

				default:
                    graph = Louvain.louvain(graph, 0);
                    break;
            }

			graph.compute_final_communities();

			// TO DO: copy required of the orignal graph to compute the final modularity
            // Computes the final modularity
            // TO DO: Final modularity should not differ from the modularity saved in the graph, so mod diff is still not correct
			float final_mod = Modularity.modularity_given_partition(og_graph, graph.partition);

			return (final_mod, graph.partition);
        }
	}
}

