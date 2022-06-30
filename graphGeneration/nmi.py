from sklearn.metrics.cluster import normalized_mutual_info_score

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
    NMI = NMI_calc(part1, part2)

    return NMI


if __name__ == "__main__":
    NMI  = main("extra voorbeeld partition.txt", "voorbeeldantwoord.txt")
    print(NMI)