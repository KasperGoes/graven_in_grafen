from sklearn.metrics.cluster import normalized_mutual_info_score
import community as community_louvain
import networkit as nk
from datetime import datetime

def partitionparse(partition, withtime):
    time = 0
    with open(partition) as f:
        input = f.read().split("\n")

        if input[-1] == "":
            input.pop()

        if withtime:
            time = input[0]
            input = input [1:]

    input = [int(i) for i in input]
    return input, time

def NMI_calc(partition1, partition2):

    NMI = normalized_mutual_info_score(partition1, partition2)

    return NMI
    
def maincalc(partition1, partition2):
    part1, time = partitionparse(partition1, 0)
    part2, time = partitionparse(partition2, 1)
    # part3 = [y for x,y in community_louvain.best_partition(graph).items()]
    # normalized_mutual_info_score(part3, partition2)
    NMI = NMI_calc(part1, part2)

    calculatation_time = datetime.strptime(time,"%H:%M:%S.%f").second
    return NMI, calculatation_time


if __name__ == "__main__":
    graph = nk.readGraph("10000 test.txt", nk.Format.METIS)
    graph = nk.nxadapter.nk2nx(graph)
    NMI  = maincalc("sa 10000.txt", "10000 03 partition.txt")
    print(NMI)