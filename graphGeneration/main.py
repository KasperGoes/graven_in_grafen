import community

import generatingGraphs as gg
import networkit as nk
import community as community_louvain

# Initialize parameters
number_graphs = 1
sizes = [10000, 10000, 100000, 1000000]
mus = [0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8]
avg_degree = 8
max_degree = 50
node_degree_exponent = -2
min_com_size = 10
max_com_size = 50
com_size_exponent = -1

# Generate graphs
all_graphs = gg.generate_all_graphs(number_graphs, sizes, mus, avg_degree, max_degree, node_degree_exponent,
                                    min_com_size, max_com_size, com_size_exponent)
graph = all_graphs[0][0]
print(nk.overview(graph))

graphx = nk.nxadapter.nk2nx(graph)
partition = community_louvain.best_partition(graphx)

print(community.modularity(partition, graphx))
