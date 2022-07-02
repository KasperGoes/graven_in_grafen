from nmi import maincalc
import os
import statistics
import scipy.stats
import numpy as np
import pandas as pd
import csv

def get_location(path):

    Answers = []
    for subdir, dirs, files in os.walk(path):

        mu_list = []

        for file in files:
            if "partition" in file:
                mu_list.append(os.path.join(subdir, file))

        Answers.append(mu_list)
    
    return Answers[1:]

def load_files(path):
    """
    Loads the files in the given path.
    :param path: The path to the files.
    :return: A list of the files.
    """
    Hybrid = []
    Louvain = []
    SA = []
    for subdir, dirs, files in os.walk(path):

        mu_list_hybrid = []
        mu_list_louvain = []
        mu_list_sa = []

        for file in files:
            if "hybrid" in file:
                mu_list_hybrid.append(os.path.join(subdir, file))
            if "louvain" in file:
                mu_list_louvain.append(os.path.join(subdir, file))
            if "sa" in file:
                mu_list_sa.append(os.path.join(subdir, file))

        Hybrid.append(mu_list_hybrid)
        Louvain.append(mu_list_louvain)
        SA.append(mu_list_sa)

    return Hybrid[1:], Louvain[1:], SA[1:]


def calculateNMI(Experiment, Answer):

    Result_Experiment = {}

    mulist = ["0.1", "0.2", "0.3", "0.4", "0.5", "0.6", "0.7", "0.8"]

    for mu_exp, mu_ans,  mu in zip(Experiment, Answer, mulist):

        Results_Hybrid_time_per_mu = []
        Results_Hybrid_NMI_per_mu = []

        for graph_ans, graph_exp in zip(mu_ans, mu_exp):

            NMI, calculation_time = maincalc(graph_ans, graph_exp)

            Results_Hybrid_NMI_per_mu.append(NMI)
            Results_Hybrid_time_per_mu.append(calculation_time)

        Result_Experiment[mu] = confidence_interval_and_average_time(Results_Hybrid_NMI_per_mu, Results_Hybrid_time_per_mu)


    return Result_Experiment

def confidence_interval_and_average_time(Results_Hybrid_NMI_per_mu, Results_Hybrid_time_per_mu):

    averagetime = statistics.mean(Results_Hybrid_time_per_mu)
    mean, lowconf, highconf = mean_confidence_interval(Results_Hybrid_NMI_per_mu)
    Answer = {"mean_NMI": round(mean,2), "Lowerbound": round(lowconf,2), "Upperbound": round(highconf,2), "average_time": averagetime}

    return Answer


def mean_confidence_interval(data, confidence=0.95):
    
    a = 1.0 * np.array(data)
    n = len(a)
    m, se = np.mean(a), scipy.stats.sem(a)
    h = se * scipy.stats.t.ppf((1 + confidence) / 2., n-1)
    return m, m-h, m+h

def writing_csv(name, data):
    with open(f'Results/{name}.csv', 'w') as f:
        w = csv.DictWriter(f, data.keys())
        w.writeheader()
        w.writerow(data)
    


def main():


    #graph sizes used in the experiment
    graphsizes = ["10000","20000","50000"]

    path1000 = "HybirdLouvainSA/HybirdLouvainSA/partitions/10000"
    path10000 = "HybirdLouvainSA/HybirdLouvainSA/partitions/20000"
    path50000 = "HybirdLouvainSA/HybirdLouvainSA/partitions/50000"

    Hybrid, Louvain, SA = load_files(path1000)

    path1000answer = "graphGeneration/graphs/10000"
    path10000answer = "graphGeneration/graphs/20000"
    path50000answer = "graphGeneration/graphs/50000"
    Answers = get_location(path1000answer)
    

    Results_Hybrid = calculateNMI(Hybrid, Answers)
    Results_Louvain = calculateNMI(Louvain, Answers)

    writing_csv("Hybrid", Results_Hybrid)
    writing_csv("Louvain", Results_Louvain)
    
    print(Results_Hybrid)






if __name__ == "__main__":
    main()