import networkx as nx


def first_example():
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

    mod_1 = nx.community.modularity(graph, [{0}, {1}, {2}, {3}, {4}])
    # mod_2 = nx.community.modularity(graph,[{0,1},{2},{3},{4}])
    mod_3 = nx.community.modularity(graph, [{0, 1}, {2}, {3}, {4}])
    mod_4 = nx.community.modularity(graph, [{0, 1}, {2, 3}, {4}])

    mod_5 = nx.community.modularity(graph, [{0, 1}, {2}, {3, 4}])

    print(mod_3)
    print(mod_5)

    print(mod_5 - mod_4)

def example_2():
    graph = nx.Graph()
    graph.add_node(0)
    graph.add_node(1)

    graph.add_edge(0, 0, weight=2)
    graph.add_edge(0, 1, weight=1)
    graph.add_edge(1, 1, weight=6)

    before = nx.community.modularity(graph, [{0}, {1}])
    after = nx.community.modularity(graph, [{0, 1}])
    print(after)
    print("final: ", after - before)

def example_3():
    graph = nx.Graph()
    graph.add_node(0)
    graph.add_node(1)
    graph.add_node(2)

    graph.add_edge(0, 0, weight=6)
    graph.add_edge(0, 1, weight=1)
    graph.add_edge(0, 2, weight=2)

    graph.add_edge(1, 1, weight=4)

    graph.add_edge(2, 2, weight=2)

    print(nx.community.modularity(graph, [{0,1}, {2}]) -nx.community.modularity(graph, [{0},{1}, {2}]))

# example_3()

def extra_example():
    graph = nx.Graph()
    graph.add_node(0)
    graph.add_node(1)
    graph.add_node(2)
    graph.add_node(3)
    graph.add_node(4)
    graph.add_node(5)
    graph.add_node(6)
    graph.add_node(7)

    graph.add_edge(0, 1)
    graph.add_edge(0, 2)
    graph.add_edge(0, 3)
    graph.add_edge(1, 2)
    graph.add_edge(1, 7)
    graph.add_edge(2, 7)
    graph.add_edge(3, 4)
    graph.add_edge(3, 5)
    graph.add_edge(6, 7)

    init = nx.community.modularity(graph, [{0,1,2},{3}, {4}, {5}, {6}, {7}])
    first = nx.community.modularity(graph, [{0,1,2,3},{4}, {5}, {6}, {7}])

    print("EUUU", init)
    print(first - init)

    # print("YEYYE: ", nx.community.modularity(graph, [{1, 2, 3}, {4, 5, 6}, {7, 8}]))
    # print("YEYYE: ", nx.community.modularity(graph, [{1, 2, 3, 8, 7}, {4, 5, 6}]))

extra_example()

def read_graph(filename):
    graph = nk.readGraph("smallgraph_graph_edges.txt", nk.Format.METIS)
    nk.nxadapter.nk2nx(graph)
    return graph

def loops_example():
    graph = nx.Graph()
    graph.add_node(0)
    graph.add_node(1)
    graph.add_node(2)
    graph.add_node(3)

    graph.add_edge(0, 0, weight=14)
    graph.add_edge(0, 1, weight=8)
    graph.add_edge(0, 2, weight=1)
    graph.add_edge(0, 3, weight=1)

    graph.add_edge(1, 1, weight=4)
    graph.add_edge(1, 3, weight=1)

    graph.add_edge(2, 2, weight=16)
    graph.add_edge(2, 3, weight=3)

    graph.add_edge(3, 3, weight=2)

    # init = nx.community.modularity(graph, [{0,1}, {2}, {3}])
    # print(init)
    # first_swap = nx.community.modularity(graph, [{0,1}, {2,3}])
    # print(first_swap)
    # print(first_swap - init)


    init = nx.community.modularity(graph, [{0,1}, {2}, {3}])
    print(init)
    first_swap = nx.community.modularity(graph, [{0,1},{3,2}])
    print("YuH", first_swap)
    print(first_swap - init)
    #
    #
    # init = nx.community.modularity(graph, [{0}, {1}, {2}, {3}])
    # print(init)
    # first_swap = nx.community.modularity(graph, [{2}, {1}, {0,3}])
    # print(first_swap)
    # print(first_swap - init)




def loops_example_2():
    graph = nx.Graph()
    graph.add_node(0)
    graph.add_node(1)
    graph.add_node(2)

    graph.add_edge(0, 0, weight=34)
    graph.add_edge(0, 1, weight=1)
    graph.add_edge(0, 2, weight=2)

    graph.add_edge(1, 1, weight=16)
    graph.add_edge(1, 2, weight=3)

    graph.add_edge(2, 2, weight=2)


    init = nx.community.modularity(graph, [{0}, {1}, {2}])
    print(init)
    first =  nx.community.modularity(graph, [{0,2}, {1}])
    print(first)
    print(first - init)

def loops_example_3():
    graph = nx.Graph()

    graph.add_node(0)
    graph.add_node(1)

    graph.add_edge(0, 0, weight=34)
    graph.add_edge(0, 1, weight=3)
    graph.add_edge(1, 1, weight=24)

    init = nx.community.modularity(graph, [{0}, {1}])
    print(init)
    first = nx.community.modularity(graph, [{0,1}])
    print(first)
    print(first - init)

###### LOOPS EXAMPLE ######
loops_example()
# loops_example_2()