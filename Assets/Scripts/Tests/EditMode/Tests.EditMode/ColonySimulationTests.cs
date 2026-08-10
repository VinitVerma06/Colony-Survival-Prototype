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

    [Test]
    public void AdvanceDay_DoesNotAllowFoodBelowZero() {

        // Advance the simulation enough times for the food reserve to deplete and attempt to go below zero
        for (int i = 0; i < 11; i++) {
            simulation.AdvanceDay();
        }

        // Verify that the food reserve is clamped at zero instead becoming negative
        Assert.AreEqual(0f, simulation.FoodReserve);
    }

    [Test]
    public void AdvanceDay_DoesNotAllowWaterBelowZero() {

        // Advance the simulation enough times for the water reserve to deplete and attempt to go below zero
        for (int i = 0; i < 11; i++) {
            simulation.AdvanceDay();
        }

        // Verify that the water reserve is clamped at zero instead becoming negative
        Assert.AreEqual(0f, simulation.WaterReserve);
    }

    [Test]
    public void IsStarving_ReturnsFalseWhenResourcesAvailable() {

        // Check the starvation state while both resources are available
        bool isStarving = simulation.IsStarving;

        // The colony should not be starving when it has food and water
        Assert.IsFalse(isStarving);
    }
    
    [Test]
    public void IsStarving_ReturnsTrueWhenFoodReachesZero() {

        // Advance the simulation until the food reserve reaches zero
        for (int i = 0; i < 11; i++) {
            simulation.AdvanceDay();
        }

        // The colony should be considered starving when food reaches zero
        Assert.IsTrue(simulation.IsStarving);
    }
}
