# Hybrid approach for detecting communities: combining Louvain with simulated annealing

## Description

This project contains two different programs, one for generating graphs with the LFR benchmark and one for running the hybrid algorithm that combines Louvain with simulated annealing for community detection.

The graphGeneration project already contains a folder with LFR graphs, sorted in folders based on the number of nodes and mu parameter. The parameters for the generation can be set in the main.py. Per generated graph, two files are saved, one with the number of nodes, edges and adjacent nodes per line, and the other contains per line for each vertex to which community it belongs. 

The HybridLouvainSA project runs the Louvain, SA and Hybrid algorithm on the generated graphs from graphGeneration. The project returns the found partitions as a text file with the modularity and runtime for each found partition.

After the partitions are found. result.py calculates the result of the found partitions by comparing it to the ground truth.

## Dependencies

The graphGeneration project uses the NetworKit library.


