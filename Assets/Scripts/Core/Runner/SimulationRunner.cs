using System.IO;
using UnityEngine;
using Simulation.Core;
using System.Collections;

public class SimulationRunner : MonoBehaviour {

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

        // Initialize the ColonySimulation 
        colonySimulation = new ColonySimulation(populationConfig, consumptionConfig);

        // Start the simulation
        StartCoroutine(RunColonySimulation());
    }

    private IEnumerator RunColonySimulation() {

        while (true) {

            yield return new WaitForSeconds(1f);

            colonySimulation.AdvanceDay();

            Debug.Log("Day " + colonySimulation.CurrentDay);

            if (colonySimulation.IsStarving) {
                Debug.Log("Colony is starving!");
            }
        }
    }
}