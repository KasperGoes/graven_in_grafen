from sklearn.metrics.cluster import normalized_mutual_info_score
import community as community_louvain
import networkit as nk

def partitionparse(partition):
    with open(partition) as f:
        input = f.read().split("\n")
    input = [int(i) for i in input]
    return input

def NMI_calc(partition1, partition2):

    NMI = normalized_mutual_info_score(partition1, partition2)

    return NMI
    
def main(partition1, partition2):
    part1 = partitionparse(partition1)
    part2 = partitionparse(partition2)
    # part3 = [y for x,y in community_louvain.best_partition(graph).items()]
    # normalized_mutual_info_score(part3, partition2)
    NMI = NMI_calc(part1, part2)

    return NMI


if __name__ == "__main__":
    graph = nk.readGraph("10000 test.txt", nk.Format.METIS)
    graph = nk.nxadapter.nk2nx(graph)
    NMI  = main("sa 10000.txt", "10000 03 partition.txt")
    print(NMI)