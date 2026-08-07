using Newtonsoft.Json;

namespace Simulation.Core {

    public static class ConfigLoader {

        public static PopulationConfig LoadPopulationConfig(string json) {
        
            return JsonConvert.DeserializeObject<PopulationConfig>(json);
        }

        public static ConsumptionConfig LoadConsumptionConfig(string json) {

            return JsonConvert.DeserializeObject<ConsumptionConfig>(json);
        }
    }
}