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

print(mod_3)
print(mod_5)

print(mod_5 - mod_4)

graph = nx.Graph()
graph.add_node(0)
graph.add_node(1)

graph.add_edge(0, 0, weight = 2)
graph.add_edge(0, 1, weight = 1)
graph.add_edge(1, 1, weight = 6)

before = nx.community.modularity(graph,[{0},{1}])
after = nx.community.modularity(graph,[{0,1}])
print(after)
print("final: ", after - before)


#
graph = nx.Graph()
graph.add_node(0)
graph.add_node(1)

graph.add_edge(0, 0, weight = 12)
graph.add_edge(0, 1, weight = 1)
graph.add_edge(1, 1, weight = 4)

print(nx.community.modularity(graph,[{0},{1}]))

graph = nx.Graph()
graph.add_node(1)
graph.add_node(2)
graph.add_node(3)
graph.add_node(4)
graph.add_node(5)
graph.add_node(6)
graph.add_node(7)
graph.add_node(8)

graph.add_edge(1, 2)
graph.add_edge(1, 3)
graph.add_edge(1, 4)
graph.add_edge(2, 3)
graph.add_edge(2, 8)
graph.add_edge(3, 8)
graph.add_edge(4, 5)
graph.add_edge(4, 6)
graph.add_edge(7, 8)

print("YEYYE: ", nx.community.modularity(graph,[{1,2,3,8,7},{4,5,6}]))