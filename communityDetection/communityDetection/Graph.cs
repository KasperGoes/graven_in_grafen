using System;
namespace communityDetection
{
	public class Graph
	{
		public int vertices;
		public int[,] AdjacencyMatrix;
		public int[] community_per_vertex;
		public Dictionary<int, List<int>> communities;

		public Graph(int n)
		{
			vertices = n;
			AdjacencyMatrix = new int[n, n];
			community_per_vertex = new int[n];
			communities = new Dictionary<int, List<int>>();
		}

	}
}

