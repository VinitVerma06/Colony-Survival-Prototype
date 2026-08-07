
public class ColonySimulation {

    public int villagerCount { get; }
    public float foodPerVillagerPerDay { get; }
    public float waterPerVillagerPerDay { get; }

    
    public int currentDay { get; private set; }
    public float foodReserve { get; private set; }
    public float waterReserve { get; private set; }


    public ColonySimulation(PopulationConfig population, ConsumptionConfig consumption) {
        
        // Reads the values from population config
        villagerCount = population.villagerCount;
        foodReserve = population.startingFood;
        waterReserve = population.startingWater;

        // Reads the values from consumption config
        foodPerVillagerPerDay = consumption.foodPerVillagerPerDay;
        waterPerVillagerPerDay = consumption.foodPerVillagerPerDay;

        currentDay = 0;
    }

    // Returns food consumed by the village in a day
    public float DailyFoodConsumption() {
        return villagerCount * foodPerVillagerPerDay;
    }

    // Returns water consumed by the village in a day
    public float DailyWaterConsumption() {
        return villagerCount * waterPerVillagerPerDay;
    }

    public float DaysRemaining(float reserve, float dailyConsumption) {
        
        // Avoid dividing by zero
        if (dailyConsumption <= 0f) return float.PositiveInfinity;

        return reserve / dailyConsumption;
    }

    public void AdvanceDay() {
        // Deplete the reserves
        foodReserve -= DailyFoodConsumption();
        waterReserve -= DailyWaterConsumption();

        // Clamps the reserves at zero to avoid negative values
        if (foodReserve <= 0f) foodReserve = 0f;
        if (waterReserve <= 0f) waterReserve = 0f;

        // Advance to next day
        currentDay++;
    }

    public bool IsStarving => foodReserve <= 0f || waterReserve <= 0f;
}