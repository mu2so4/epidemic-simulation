namespace monte_carlo_simulation.src.disease {
    class DiseaseFactory {
        readonly SortedDictionary<string, IDiseaseProducer> producers = [];

        public IDisease NewInstance(string name) {
            IDiseaseProducer producer = producers[name];
            return producer.Produce();
        }

        public void Add(string name, IDiseaseProducer producer) {
            producers[name] = producer;
        }
    }
}