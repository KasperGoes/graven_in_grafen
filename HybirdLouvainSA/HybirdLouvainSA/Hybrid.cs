using System;

namespace HybridLouvainSA
{
	public static class Hybrid
	{
		public static Graph hybrid_algorithm(Graph g, int min_nodes, bool hybrid, float alpha, float temperature, float epsilon)
        {
			g = Louvain.louvain(g, min_nodes);
			g = SA.simulatedAnnealing(g, hybrid, alpha, temperature, epsilon);
			return g;
        }
	}
}

