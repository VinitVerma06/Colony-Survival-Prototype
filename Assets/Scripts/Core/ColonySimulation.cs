namespace Simulation.Core {

    public class ColonySimulation {

        public int VillagerCount { get; }
        public float FoodPerVillagerPerDay { get; }
        public float WaterPerVillagerPerDay { get; }

    
        public int CurrentDay { get; private set; }
        public float FoodReserve { get; private set; }
        public float WaterReserve { get; private set; }


        public ColonySimulation(PopulationConfig population, ConsumptionConfig consumption) {
        
            // Reads the values from population config
            VillagerCount = population.VillagerCount;
            FoodReserve = population.StartingFood;
            WaterReserve = population.StartingWater;

            // Reads the values from consumption config
            FoodPerVillagerPerDay = consumption.FoodPerVillagerPerDay;
            WaterPerVillagerPerDay = consumption.FoodPerVillagerPerDay;

            CurrentDay = 0;
        }

        // Returns food consumed by the village in a day
        public float DailyFoodConsumption() {
            return VillagerCount * FoodPerVillagerPerDay;
        }

        // Returns water consumed by the village in a day
        public float DailyWaterConsumption() {
            return VillagerCount * WaterPerVillagerPerDay;
        }

        public float DaysRemaining(float reserve, float dailyConsumption) {
        
            // Avoid dividing by zero
            if (dailyConsumption <= 0f) return float.PositiveInfinity;

            return reserve / dailyConsumption;
        }

        public void AdvanceDay() {
            // Deplete the reserves
            FoodReserve -= DailyFoodConsumption();
            WaterReserve -= DailyWaterConsumption();

            // Clamps the reserves at zero to avoid negative values
            if (FoodReserve <= 0f) FoodReserve = 0f;
            if (WaterReserve <= 0f) WaterReserve = 0f;

            // Advance to next day
            CurrentDay++;
        }

        public bool IsStarving => FoodReserve <= 0f || WaterReserve <= 0f;
    }
}