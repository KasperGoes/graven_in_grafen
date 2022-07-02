import time
import generatingGraphs as gg
import networkit as nk
import community as community_louvain
import os
import networkx as nx
import matplotlib.pyplot as plt

# Initialize parameters
number_graphs = 20
sizes = [10000, 20000, 50000]
mus = [0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8]
avg_degree = 20
max_degree = 50
node_degree_exponent = -2
min_com_size = 20
max_com_size = 100
com_size_exponent = -1

# Generate graphs
all_graphs = gg.generate_all_graphs(number_graphs, sizes, mus, avg_degree, max_degree, node_degree_exponent,
                                    min_com_size, max_com_size, com_size_exponent)



def write_graphs(sizes, mus, number_graphs, all_graphs):
    """
    Writes the graphs to files.
    Format for the edge list: first line is the number of vertices and the number of edges, then adjacency list.
    Format for the partition: each line is the community id of a vertex.
    """
    for i in range(len(sizes)):
        n = sizes[i]
        for j in range(len(mus)):
            mu = mus[j]
            mu = mus[j]
            if not os.path.isdir('./graphs/' + str(n) + '/' + str(mu)):
                os.makedirs('./graphs/' + str(n) + '/' + str(mu))
            for k in range(number_graphs):
                graph = all_graphs[i][j][k][0]
                partition = all_graphs[i][j][k][1]
                nk.writeGraph(graph, './graphs/' + str(n) + '/' + str(mu) + '/' + str(k) + "_graph_edges.txt",
                              nk.Format.METIS)
                nk.community.writeCommunities(partition,
                                              './graphs/' + str(n) + '/' + str(mu) + '/' + str(k) + "_graph_partition.txt")


def write_one_graph(graph, partition, filename):
    nk.writeGraph(graph, filename + "_graph_edges.txt", nk.Format.METIS)
    nk.community.writeCommunities(partition, filename + "_graph_partition.txt")

write_graphs(sizes, mus, number_graphs, all_graphs)

def read_graph(filename):
    graph = nk.readGraph(filename, nk.Format.METIS)
    graph = nk.nxadapter.nk2nx(graph)
    return graph

def time_convert(sec):
  mins = sec // 60
  sec = sec % 60
  hours = mins // 60
  mins = mins % 60
  print("Time Lapsed = {0}:{1}:{2}".format(int(hours),int(mins),sec))

def louvain(graph):
    start_time = time.time()
    partition = community_louvain.best_partition(graph)
    end_time = time.time()
    elapsed = start_time - end_time
    time_convert(elapsed)
    print(community_louvain.modularity(partition, graph))
    # print(partition)

# print('yuh')
# graph = read_graph("./graphs/50000/0.8/0_graph_edges.txt")
# louvain(graph)
# print("finished")