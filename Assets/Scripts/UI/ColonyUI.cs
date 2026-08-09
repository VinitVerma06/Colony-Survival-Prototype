using TMPro;
using UnityEngine;
using Simulation.Core;

public class ColonyUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI dayCounterText;
    [SerializeField] private TextMeshProUGUI foodReserveText;
    [SerializeField] private TextMeshProUGUI waterReserveText;
    [SerializeField] private TextMeshProUGUI foodDaysRemainingText;
    [SerializeField] private TextMeshProUGUI waterDaysRemainingText;

    private string dayCounterString = "Current Day : ";
    private string foodReserveString = "Food Reserves : ";
    private string waterReserveString = "Water Reserves : ";
    private string foodDaysRemainingString = "Days Remaining for food : ";
    private string waterDaysRemainingString = "Days Remaining for water : ";

    private void Start() {
        SimulationRunner.OnDayAdvanced += SimulationRunner_OnDayAdvanced;
    }

    private void SimulationRunner_OnDayAdvanced(ColonySimulation simulation) {
        UpdateVisual(simulation);
    }

    private void UpdateVisual(ColonySimulation simulation) {
        
        dayCounterText.text = dayCounterString + simulation.CurrentDay;
        
        foodReserveText.text = foodReserveString + simulation.FoodReserve;
        
        waterReserveText.text = waterReserveString + simulation.WaterReserve;
        
        foodDaysRemainingText.text = foodDaysRemainingString + 
            simulation.DaysRemaining(simulation.FoodReserve, simulation.DailyFoodConsumption());
        
        waterDaysRemainingText.text = waterDaysRemainingString + 
            simulation.DaysRemaining(simulation.WaterReserve, simulation.DailyWaterConsumption());
    }


    private void OnDestroy() {
        SimulationRunner.OnDayAdvanced -= SimulationRunner_OnDayAdvanced;
    }
}
