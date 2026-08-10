# ColonySurvivalPrototype
A colony simulation prototype: simulates a colony's food and water reserves depleting over time, with values loaded from JSON config files.


## How to Run
1. Clone this repository.
2. Open the project in Unity 6000.3.13f1.
3. Open the `SampleScene`.
4. Press Play. The simulation starts automatically and runs at 1 real second = 1 game day.


## How to Run the Unit Tests
1. In Unity, open **Window → General → Test Runner**.
2. Click the **EditMode** tab at the top of the Test Runner window.
3. You should see `ColonySimulationTests` listed, containing the following tests:
   - `AdvanceDay_ReduceResourcesByCorrectAmount`
   - `AdvanceDay_IncreasesCurrentDayByOne`
   - `AdvanceDay_DoesNotAllowFoodBelowZero`
   - `AdvanceDay_DoesNotAllowWaterBelowZero`
   - `IsStarving_ReturnsFalseWhenResourcesAvailable`
   - `IsStarving_ReturnsTrueWhenFoodReachesZero`
   - `IsStarving_ReturnsTrueWhenOnlyOneResourceDepletes`
4. Click **Run All** to execute all tests, or select an individual test and click **Run Selected**.
5. Passed tests show a green checkmark; failed tests show a red icon with details on what didn't match.


## AI Tools Used
I used Claude throughout this project as a guided collaborator, not a code generator I pasted blindly.

**Pure C# core (`ColonySimulation`, `ConfigLoader`, config classes):** built with Claude explaining each architectural decision (data flow, JSON deserialization approach, why Newtonsoft over JsonUtility) before I wrote the code myself.

**EditMode unit tests:** this was my first time writing Unity tests, so AI assistance was heaviest here — Claude explained the Assembly Definition setup, the Arrange/Act/Assert pattern, and `[SetUp]` usage, which I then applied and extended with my own additional test cases.

**MonoBehaviours (`SimulationRunner`, `ColonyUI`):** written mostly independently. I planned the event-driven structure (`Action<ColonySimulation>` for day-advance updates, a separate one-shot event for the starving state) myself, and used AI mainly to sanity-check the design and debug two runtime issues (a StreamingAssets path bug and an assembly reference error preventing test discovery) rather than to write the logic itself.


## Decisions & Trade-offs
- **JSON parsing library:** Used Newtonsoft.Json (`com.unity.nuget.newtonsoft-json`) instead of Unity's built-in `JsonUtility`, since `JsonUtility` lives in the `UnityEngine` namespace — using it would have meant `ConfigLoader` technically referencing UnityEngine, which conflicts with the "pure C#, no engine references" architecture rule.

- **Simulation stops on starvation, rather than continuing indefinitely:** Once either reserve hits zero, the day-advancing coroutine breaks out of its loop instead of continuing to tick. This was a deliberate choice: since reserves are clamped at zero, letting the simulation keep running afterward would just repeatedly recompute "0 minus consumption, clamped back to 0" forever with no meaningful change in state — stopping is more honest to what "the colony is starving" actually means.

- **Starvation triggers on either resource independently**, not only when both are depleted. `IsStarving` checks `FoodReserve <= 0 || WaterReserve <= 0`, so if one resource runs out well before the other, the colony is still considered starving and the simulation stops immediately — it doesn't wait for both to reach zero.

- **No negative resource values** `FoodReserve` and `WaterReserve` are clamped to a minimum of 0 after each `AdvanceDay()` call, rather than allowed to go negative. This keeps "days remaining" and the starving check meaningful and avoids displaying nonsensical negative reserve numbers in the UI.

- **No population loss:** Villager count stays fixed for the full run — the brief's scope explicitly excludes population dynamics, so `VillagerCount` is read once from JSON and never modified after that, even once the colony is starving.

- **Assembly Definitions added for both `Core` and `Tests` folders**, not just the required test assembly. Giving the pure-logic `Core` folder its own `.asmdef` with no UnityEngine reference means the "no engine references" rule is enforced by the compiler itself, not just manual review — if `UnityEngine` were ever accidentally imported into that folder, the project simply wouldn't compile.

## Demo Video
[Watch demo video](https://drive.google.com/file/d/1u_-T4i8_ZvhQWW4mA_LtI6TDSWwKC74O/view?usp=sharing)