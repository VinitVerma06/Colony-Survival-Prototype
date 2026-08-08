using NUnit.Framework;
using Simulation.Core;

public class ColonySimulationTests {

    private ColonySimulation simulation;

    [SetUp] 
    public void Setup() {

        // Assign values
        PopulationConfig populationConfig = new PopulationConfig { 
            VillagerCount = 10, 
            StartingFood = 100, 
            StartingWater = 80 
        };
        
        ConsumptionConfig consumptionConfig = new ConsumptionConfig { 
            FoodPerVillagerPerDay = 1f, 
            WaterPerVillagerPerDay = 0.8f 
        };
        
        simulation = new ColonySimulation(
            populationConfig, 
            consumptionConfig
        );
    }

    [Test]
    public void AdvanceDay_ReduceResourcesByCorrectAmount() {
        
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

        // Advance the day
        simulation.AdvanceDay();

        // Check whether the current day incremented or not
        Assert.AreEqual(1, simulation.CurrentDay);
    }
}
