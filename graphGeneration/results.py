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

        Results_Experiment_time_per_mu = []
        Results_Experiment_NMI_per_mu = []
        Results_Experiment_modularity_per_mu = []

        for graph_ans, graph_exp in zip(mu_ans, mu_exp):

            NMI, calculation_time, modularity = maincalc(graph_ans, graph_exp)

            Results_Experiment_NMI_per_mu.append(NMI)
            Results_Experiment_time_per_mu.append(calculation_time)
            Results_Experiment_modularity_per_mu.append(modularity)

        Result_Experiment[mu] = confidence_interval_and_average_time(Results_Experiment_NMI_per_mu, Results_Experiment_time_per_mu, Results_Experiment_modularity_per_mu)

    return Result_Experiment

def confidence_interval_and_average_time(Results_Hybrid_NMI_per_mu, Results_Experiment_time_per_mu, Results_Experiment_modularity_per_mu):

    averagetime = statistics.mean(Results_Experiment_time_per_mu)
    averagemod = statistics.mean(Results_Experiment_modularity_per_mu)
    mean, lowconf, highconf = mean_confidence_interval(Results_Hybrid_NMI_per_mu)

    Answer = {"mean_NMI": round(mean,3), "Confidence": (round(lowconf,3),round(highconf,3)), "mean Modularity": round(averagemod,3), "average_time": round(averagetime,3)}

    return Answer


def mean_confidence_interval(data, confidence=0.95):
    
    a = 1.0 * np.array(data)
    n = len(a)
    m, se = np.mean(a), scipy.stats.sem(a)
    h = se * scipy.stats.t.ppf((1 + confidence) / 2., n-1)
    return m, m-h, m+h

def printing_results(name, Resultstotal):

    for dictionary in Resultstotal:
        for key, value in dictionary.items():

            print(f"{dictionary}:")
            print()

            print(key, value)
            print("------------------------------------")

def writing_txt(data):

    for dictionary in data:

        with open("myfile.txt", 'w') as f: 
            for key, value in dictionary.items(): 
                f.write('%s:%s\n' % (key, value))

    # for result in data:
        
    #     with open(f'Results/{result}.csv', 'w') as f:
    #         w = csv.DictWriter(f, data.keys())
    #         w.writeheader()
    #         w.writerow(data)
    

def main():


    #graph sizes used in the experiment
    graphsizes = ["10000","20000","50000"]

    path10000 = "HybirdLouvainSA/HybirdLouvainSA/partitions/10000"
    path20000 = "HybirdLouvainSA/HybirdLouvainSA/partitions/20000"
    path50000 = "HybirdLouvainSA/HybirdLouvainSA/partitions/50000"

    Hybrid10000, Louvain10000, SA10000 = load_files(path10000)
    Hybrid20000, Louvain20000, SA20000 = load_files(path20000)
    Hybrid50000, Louvain50000, SA50000 = load_files(path50000)

    path10000answer = "graphGeneration/graphs/10000"
    path20000answer = "graphGeneration/graphs/20000"
    path50000answer = "graphGeneration/graphs/50000"

    Answers10000 = get_location(path10000answer)
    Answers20000 = get_location(path20000answer)
    Answers50000 = get_location(path50000answer)

    # extra test
    Results_Hybrid10000 = calculateNMI(SA10000, Answers10000)

    Results_Hybrid10000 = calculateNMI(Hybrid10000, Answers10000)
    Results_Louvain10000 = calculateNMI(Louvain10000, Answers10000)
    Results_Hybrid20000 = calculateNMI(Hybrid20000, Answers20000)
    Results_Louvain20000 = calculateNMI(Louvain20000, Answers20000)
    Results_Hybrid50000 = calculateNMI(Hybrid50000, Answers50000)
    Results_Louvain50000 = calculateNMI(Louvain50000, Answers50000)

    Resultstotal = [Results_Hybrid10000, Results_Louvain10000, Results_Hybrid20000, Results_Louvain20000, Results_Hybrid50000, Results_Louvain50000]

    printing_results(Resultstotal)
    writing_txt(Resultstotal)



if __name__ == "__main__":
    main()