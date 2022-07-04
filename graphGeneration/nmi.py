from sklearn.metrics.cluster import normalized_mutual_info_score
import community as community_louvain
import networkit as nk
from datetime import datetime

def partitionparse(partition, withtime_and_modularity):
    time, modularity = 0, 0
    with open(partition) as f:
        input = f.read().split("\n")

        if input[-1] == "":
            input.pop()

        if withtime_and_modularity:
            modularity = float(input[0])
            time = input[1]
            input = input [2:]

    input = [int(i) for i in input]
    return input, time, modularity

def NMI_calc(partition1, partition2):

    NMI = normalized_mutual_info_score(partition1, partition2)

    return NMI
    
def maincalc(partition1, partition2):
    part1, time, modularity = partitionparse(partition1, 0)
    part2, time, modularity = partitionparse(partition2, 1)
    # part3 = [y for x,y in community_louvain.best_partition(graph).items()]
    # normalized_mutual_info_score(part3, partition2)
    NMI = NMI_calc(part1, part2)

    calculatation_time_minutes = datetime.strptime(time,"%H:%M:%S.%f").minute
    calculatation_time_seconds = datetime.strptime(time,"%H:%M:%S.%f").second
    calculatation_time_miliseconds = datetime.strptime(time,"%H:%M:%S.%f").microsecond
    time = calculatation_time_minutes*60 + calculatation_time_seconds + calculatation_time_miliseconds/1000000
    return NMI, time, modularity


if __name__ == "__main__":

    path1 = "HybirdLouvainSA/HybirdLouvainSA/partitions/1000/0.7/0_sa.txt"
    path2 = "HybirdLouvainSA/HybirdLouvainSA/partitions/1000/0.7/0_louvain.txt"
    # path3 = "HybirdLouvainSA/HybirdLouvainSA/partitions kopie/50000/0.3/0_sa.txt"
    answer = "graphGeneration/graphs/1000/0.7/0_graph_partition.txt"
    NMI  = maincalc(answer,path1)
    print(f"SA: {NMI}")
    NMI  = maincalc(answer,path2)
    print(f"Louvain: {NMI}")
    # NMI  = maincalc(answer,path3)
    # print(NMI)