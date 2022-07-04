using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace HybridLouvainSA
{
	public static class Experiment
	{
        public static void run_all_experiments(int switch_treshold, float temp, float alpha, float epsilon)
        {
            int graphs = 20;
            string filename = "../../../graphs/";

            for (int i = 0; i < graphs; i++)
            {
                foreach (string graphsize_string in Directory.GetDirectories(filename))
                {
                    foreach (string mu_string in Directory.GetDirectories(graphsize_string))
                    {
                        string graph_path = mu_string + "/" + i.ToString() + "_graph_edges.txt";

                        string size_mu = mu_string.Substring(16);

                        Graph og_graph = TextfileConverter.create_graph(graph_path);

                        // Run the experiment
                        foreach (experiment exp in Enum.GetValues(typeof(experiment)))
                        {
                            if(!(og_graph.vertices.Length >= 10000 && exp == experiment.sa))
                            {
                                Graph graph = TextfileConverter.create_graph(graph_path);

                                Stopwatch stopWatch = new Stopwatch();
                                stopWatch.Start();

                                Graph final_graph = Experiment.perform_experiment(exp, graph, switch_treshold, og_graph, temp, alpha, epsilon);

                                stopWatch.Stop();

                                // Get the elapsed time as a TimeSpan value.
                                TimeSpan ts = stopWatch.Elapsed;

                                // Format and display the TimeSpan value.
                                string elapsed_time = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

                                // Write partition to file
                                TextfileConverter.write_result("../../../partitions/" + size_mu + "/" + i.ToString() + "_" + exp.ToString() + ".txt", final_graph.partition, elapsed_time, graph.modularity);
                            }
                            
                        }
                            Console.WriteLine("finished");
                    }
                }

                filename = "../../../graphs/";

                if (i == 9)
                    Console.WriteLine("eerste 10 gedaa");
            } 
        }

		/// <summary>
        /// Performs experiment based on the given parameters
        /// </summary>
        /// <param name="exp"></param> Denotes the experiment, can be hybrid, louvain or simulated annealing
        /// <param name="graph"></param>
        /// <param name="switch_treshold"></param> Denotes the number of nodes threshold to transition from louvain to sa
        /// <param name="og_graph"></param>
        /// <returns></returns> Returns the modularity and the corresponding partition
		public static Graph perform_experiment(experiment exp, Graph graph, int switch_treshold, Graph og_graph, float temp, float alpha, float epsilon)
        {
            switch(exp)
            {
				case experiment.hybrid:
                    graph = Hybrid.hybrid_algorithm(graph, switch_treshold, true, alpha, temp, epsilon);
                    break;

				case experiment.sa:
                    graph = SA.simulatedAnnealing(graph, false, alpha, temp, epsilon);
                    break;

				default:
                    graph = Louvain.louvain(graph, 0);
                    break;
            }

			graph.compute_final_communities();

			return graph;
        }

        public static void run_one_experiment(string filename1, string filename2, experiment experiment, int switch_treshold, float temp, float alpha, float epsilon)
        {
            // Choose the desired example graph
            Graph graph = TextfileConverter.create_graph(filename1);

            
            Graph og_graph = TextfileConverter.create_graph(filename1);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Graph final_graph = Experiment.perform_experiment(experiment, graph, switch_treshold, og_graph, temp, alpha, epsilon);

            stopWatch.Stop();

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsed_time = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            // Write partition to file
            TextfileConverter.write_result("voorbeeld part" + experiment.ToString() + " " + graph.vertices.Length.ToString() + ".txt", final_graph.partition, elapsed_time, graph.modularity);

            Console.WriteLine("finished");
        }
    }
}

