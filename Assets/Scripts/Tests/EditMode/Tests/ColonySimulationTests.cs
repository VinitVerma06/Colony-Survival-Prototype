using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Simulation.Core;

public class ColonySimulationTests {
    // A Test behaves as an ordinary method
    [Test]
    public void AdvanceDay_ReduceFoodBySpecCorrectAmount() {
        var populationConfig = new PopulationConfig { VillagerCount = 10, StartingFood = 100, StartingWater = 80 };
        var consumptionConfig = new ConsumptionConfig { FoodPerVillagerPerDay = 1f, WaterPerVillagerPerDay = 0.8f };
        var simulation = new ColonySimulation(populationConfig, consumptionConfig);

        simulation.AdvanceDay();
        simulation.AdvanceDay();
        simulation.AdvanceDay();

        Assert.AreEqual(70f, simulation.FoodReserve);
        Assert.AreEqual(56f, simulation.WaterReserve);
    }

}