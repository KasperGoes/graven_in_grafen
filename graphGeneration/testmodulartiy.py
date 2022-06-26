import networkx as nx

graph = nx.Graph()
graph.add_node(0)
graph.add_node(1)
graph.add_node(2)
graph.add_node(3)
graph.add_node(4)

graph.add_edge(0, 1)
graph.add_edge(0, 2)
graph.add_edge(2, 3)
graph.add_edge(2, 4)
graph.add_edge(3, 4)

mod_1 = nx.community.modularity(graph,[{0},{1},{2},{3},{4}])
# mod_2 = nx.community.modularity(graph,[{0,1},{2},{3},{4}])
mod_3 = nx.community.modularity(graph,[{0,1},{2},{3},{4}])
mod_4 = nx.community.modularity(graph,[{0,1},{2,3},{4}])
mod_5 = nx.community.modularity(graph,[{0,1},{2},{3,4}])

print(mod_4)
print(mod_5)

print(mod_5 - mod_4)

# graph = nx.Graph()
# graph.add_node(0)
# graph.add_node(1)
# graph.add_node(2)
#
# graph.add_edge(0, 0, weight = 2)
# graph.add_edge(0, 1, weight = 1)
# graph.add_edge(1, 2, weight = 2)
# graph.add_edge(2, 2, weight = 2)
#
# print(nx.community.modularity(graph,[{0},{1},{2}]))
#
# before = nx.community.modularity(graph,[{0},{1},{2}])
# after = nx.community.modularity(graph,[{0,1},{2}])
# print(after - before)

# print((mod_before - mod_after))

#
# graph = nx.Graph()
# graph.add_node(0)
# graph.add_node(1)
# graph.add_node(2)
# graph.add_node(3)
#
# graph.add_edge(0, 1)
# graph.add_edge(0, 2)
# graph.add_edge(1, 2)
# graph.add_edge(2, 3)
#
# print(nx.community.modularity(graph,[{0,1,2},{3}]))