using System;
using System.IO;
using UnityEngine;
using Simulation.Core;
using System.Collections;

public class SimulationRunner : MonoBehaviour {

    public static Action<ColonySimulation> OnDayAdvanced;
    public static Action OnColonyStarving;

    private ColonySimulation colonySimulation;

    private void Start() {
        // Build the path to configuration file 
        string populationPath = Path.Combine(Application.streamingAssetsPath, "population.json");
        string consumptionPath = Path.Combine(Application.streamingAssetsPath, "consumption.json");

        // Read the json data from the configuration file
        string populationJson = File.ReadAllText(populationPath);
        string consumptionJson = File.ReadAllText(consumptionPath);

        // Convert the json string into configuration objects
        PopulationConfig populationConfig = ConfigLoader.LoadPopulationConfig(populationJson);
        ConsumptionConfig consumptionConfig = ConfigLoader.LoadConsumptionConfig(consumptionJson);

        // Create the ColonySimulation using the loaded data 
        colonySimulation = new ColonySimulation(populationConfig, consumptionConfig);

        // Start the coroutine for advancing the simulation
        StartCoroutine(RunColonySimulation());
    }

    private IEnumerator RunColonySimulation() {

        while (true) {

            // Wait for a second before advancing to the next day
            yield return new WaitForSeconds(1f);

            // Advances simulation by one day
            colonySimulation.AdvanceDay();

            // Notify the subscribers that a new day has been completed
            OnDayAdvanced?.Invoke(colonySimulation);

            // Check whether the colony has run out of any of the resources
            if (colonySimulation.IsStarving) {

                // Notify subscribers that the colony has entered starvation state
                OnColonyStarving?.Invoke();
                break;      // Stops the simulation
            }
        }
    }
}