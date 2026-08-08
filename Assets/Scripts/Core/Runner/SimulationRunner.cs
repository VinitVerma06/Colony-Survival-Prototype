using System.IO;
using UnityEngine;
using Simulation.Core;
using System.Collections;

public class SimulationRunner : MonoBehaviour {
    
    private void Start() {
        string populationPath = Path.Combine(Application.streamingAssetsPath, "population.json");
        string populationJson = File.ReadAllText(populationPath);

        string consumptionPath = Path.Combine(Application.streamingAssetsPath, "consumption.json");
        string consumptionJson = File.ReadAllText(consumptionPath);

        PopulationConfig populationConfig = ConfigLoader.LoadPopulationConfig(populationJson);
        ConsumptionConfig consumptionConfig = ConfigLoader.LoadConsumptionConfig(consumptionJson);

        // Initialize the ColonySimulation
        ColonySimulation colonySimulation = new ColonySimulation(populationConfig, consumptionConfig);

        StartCoroutine(RunColonySimulation(colonySimulation));
    }

    private IEnumerator RunColonySimulation(ColonySimulation colonySimulation) {
        
        while (true) {
            yield return new WaitForSeconds(1f);
            colonySimulation.AdvanceDay();
            Debug.Log(colonySimulation.CurrentDay);
        }
    }
}