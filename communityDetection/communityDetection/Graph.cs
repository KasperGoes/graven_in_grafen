﻿using System;
namespace communityDetection
{
	public class Graph
	{
		public int n; // number of vertices
		public int m; // number of edges

		public Vertex[] vertices; // all vertices in the graph
		
		public int[,] AdjacencyMatrix;
		//public List<int>[] AdjacenceList;

		public int[] community_per_vertex;
		public Dictionary<int, Community> benchmark_communities;

		public Dictionary<int,Community> communities;

		public Graph(int n, int m)
		{
			this.n = n;
			this.m = m;
			vertices = new Vertex[n];
			AdjacencyMatrix = new int[n, n];

			//AdjacenceList = new List<int>[n];
			//for (int i = 0; i < n; i++)
			//	AdjacenceList[i] = new List<int>();

			community_per_vertex = new int[n];
			benchmark_communities = new Dictionary<int, Community>();
			communities = new Dictionary<int, Community>();
		}
	}

	public class Community
    {
		public int id;
		public int sum_in = 0;
		public int sum_tot = 0;
		public Dictionary<int, Vertex> vertices;

		public Community(int id, Vertex v)
        {
			this.id = id;
			this.vertices = new Dictionary<int, Vertex>{ { id,v } };
			this.sum_tot = v.degree;
        }
    }

	public class Vertex
	{
		public int id;
		public int degree;
		public int community;
		public List<Vertex> neighbours;

		public Vertex(int id, int community, int degree)
		{
			this.id = id;
			this.community = community;
			this.degree = degree;
			neighbours = new List<Vertex>();
		}
	}
}

