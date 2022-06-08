import networkx as nx

graph = nx.Graph()
graph.add_node(1)
graph.add_node(2)
graph.add_node(3)
graph.add_node(4)
graph.add_node(5)

graph.add_edge(1, 2)
graph.add_edge(1, 3)
graph.add_edge(3, 4)
graph.add_edge(3, 5)
graph.add_edge(4, 5)

print(nx.community.modularity(graph,[{1,3},{2},{4},{5}]))
