import networkx as nx
import matplotlib.pyplot as plt
import scipy as sp
import networkit as nk


def graph_generator(n, avg_degree, max_degree, node_degree_exponent, min_com_size, max_com_size, com_size_exponent):
    """
    Returns a LFR graph generator.
    n is the number of vertices
    """
    lfr = nk.generators.LFRGenerator(n)
    lfr.generatePowerlawDegreeSequence(avg_degree, max_degree, node_degree_exponent)
    lfr.generatePowerlawCommunitySizeSequence(min_com_size, max_com_size, com_size_exponent)
    return lfr


def generate_graphs(lfr, number_graphs):
    """
    Generates a graph from the LFR generator.
    """
    graphs = []
    for i in range(number_graphs):
        graph = lfr.generate()
        partition = lfr.getPartition()
        graphs.append((graph, partition))

    return graphs


def generate_all_graphs(number_graphs, sizes, mus, avg_degree, max_degree, node_degree, min_com_size, max_com_size, com_size_exponent):
    """
    Generates all graphs with the given parameters.
    """
    all_graphs = []
    for n in sizes:
        graphs = []
        for mu in mus:
            lfr = graph_generator(n, avg_degree, max_degree, node_degree, min_com_size, max_com_size, com_size_exponent)
            lfr.setMu(mu)
            graphs.append(generate_graphs(lfr, number_graphs))
        all_graphs.append(graphs)
    return all_graphs

def one_graph():
    """
    Generates a small graph.
    """
    n = 10000
    avg_degree = 20
    max_degree = 50
    node_degree_exponent = -2
    min_com_size = 10
    max_com_size = 50
    com_size_exponent = -1

    lfr = nk.generators.LFRGenerator(n)
    lfr.generatePowerlawDegreeSequence(avg_degree, max_degree, node_degree_exponent)
    lfr.generatePowerlawCommunitySizeSequence(min_com_size, max_com_size, com_size_exponent)
    lfr.setMu(0.4)

    graph = lfr.generate()
    partition = lfr.getPartition()
    return graph, partition



