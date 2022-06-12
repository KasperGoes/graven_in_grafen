# Hybrid approach for detecting communities: combining Louvain with simulated annealing

## Description

This project contains two different programs, one for generating graphs with the LFR benchmark and one for running the hybrid algorithm that combines Louvain with simulated annealing for community detection.

The graphGeneration project already contains a folder with LFR graphs, sorted in folders based on the number of nodes and mu parameter. The parameters for the generation can be set in the main.py. Per generated graph, two files are saved, one with the number of nodes, edges and adjacent nodes per line, and the other contains per line for each vertex to which community it belongs. 

The HybridLouvainSA project runs the proposed algorithm on a small example graph. Unfortunately, due to some last-minute changes to the representation of the neighbouring communities, it is not possible to run the algorithm without run errors. Furthermore, code for the experimental setup still needs to be added such the algorithm can be applied to all generated graphs generated by the graphGenartation project automatically.

## Dependencies

The graphGeneration project uses the NetworKit library.


