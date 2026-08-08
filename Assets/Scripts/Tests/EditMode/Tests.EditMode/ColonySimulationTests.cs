using NUnit.Framework;
using Simulation.Core;

public class ColonySimulationTests {
    [Test]
    public void AdvanceDay_ReduceResourcesByCorrectAmount() {
        
        // Assign values
        var populationConfig = new PopulationConfig { VillagerCount = 10, StartingFood = 100, StartingWater = 80 };
        var consumptionConfig = new ConsumptionConfig { FoodPerVillagerPerDay = 1f, WaterPerVillagerPerDay = 0.8f };
        var simulation = new ColonySimulation(populationConfig, consumptionConfig);
        
        // Advance to day 3
        simulation.AdvanceDay();
        simulation.AdvanceDay();
        simulation.AdvanceDay();

        // Check reserve's value after 3 days
        Assert.AreEqual(70f, simulation.FoodReserve);
        Assert.AreEqual(56f, simulation.WaterReserve);
    }

    [Test]
    public void AdvanceDay_IncreasesCurrentDayByOne() {
        
        // Assign values
        var populationConfig = new PopulationConfig { VillagerCount = 10, StartingFood = 100, StartingWater = 80 };
        var consumptionConfig = new ConsumptionConfig { FoodPerVillagerPerDay = 1f, WaterPerVillagerPerDay = 0.8f };
        var simulation = new ColonySimulation(populationConfig, consumptionConfig);

        // Advance the day
        simulation.AdvanceDay();

        // Check whether the current day incremented or not
        Assert.AreEqual(1, simulation.CurrentDay);
    }
}
