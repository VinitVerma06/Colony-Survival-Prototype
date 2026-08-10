using UnityEngine;

public class ColonyStarvingUI : MonoBehaviour {

    private void Start() {

        // Hide the Starvation UI when the game starts
        Hide();
        SimulationRunner.OnColonyStarving += SimulationRunner_OnColonyStarving;
    }

    private void SimulationRunner_OnColonyStarving() {
        // Show the starvation warning when the colony is starving
        Show();
    }

    private void Show() {
        // Make the starvation UI visible
        gameObject.SetActive(true);
    }
    
    private void Hide() {
        // Hide the starvation UI
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        SimulationRunner.OnColonyStarving -= SimulationRunner_OnColonyStarving;
    }
}
