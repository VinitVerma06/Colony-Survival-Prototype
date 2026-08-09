using UnityEngine;

public class ColonyStarvingUI : MonoBehaviour {

    private void Start() {
        Hide();
        SimulationRunner.OnColonyStarving += SimulationRunner_OnColonyStarving;
    }

    private void SimulationRunner_OnColonyStarving() {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    
    private void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        SimulationRunner.OnColonyStarving -= SimulationRunner_OnColonyStarving;
    }
}
