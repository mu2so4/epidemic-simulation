namespace monte_carlo_simulation.src.disease {
    interface IDiseaseProducer {
        IDisease Produce();
    }

    class SimpleDiseaseProducer : IDiseaseProducer {
        public IDisease Produce() {
            double infectionCoef = Convert.ToDouble(Console.ReadLine());
            double recoverCoef = Convert.ToDouble(Console.ReadLine());
            double deceaseCoef = Convert.ToDouble(Console.ReadLine());
            return new SimpleDisease(infectionCoef, recoverCoef, deceaseCoef);
        }
    }

    class GenderDependentProducer : IDiseaseProducer {
        public IDisease Produce() {
            List<double> infectonCoefs = Console.ReadLine()!.Split().Select(double.Parse).ToList();
            var recoverCoefs = Console.ReadLine()!.Split().Select(double.Parse).ToList();
            var deceaseCoefs = Console.ReadLine()!.Split().Select(double.Parse).ToList();
            return new GenderDependentDisease(infectonCoefs[0], recoverCoefs[0], deceaseCoefs[0],
                infectonCoefs[1], recoverCoefs[1], deceaseCoefs[1]);
        }
    }

}