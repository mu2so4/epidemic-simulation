namespace monte_carlo_simulation.src {
    interface IIndividualGenerator {
        HashSet<Person> GenerateIndividuals(int susceptibleCount, int infectedCount);
    }

    class SimpleIndividualGenerator(double maleProbability) : IIndividualGenerator {
        readonly double maleProbability = maleProbability;
        readonly Random random = new();
        
        public HashSet<Person> GenerateIndividuals(int susceptibleCount, int infectedCount) {
            if(susceptibleCount < 0) {
                throw new ArgumentException(nameof(susceptibleCount) + " must be >= 0");
            }
            if(infectedCount <= 0) {
                throw new ArgumentException(nameof(infectedCount) + " must be > 0");
            }
            HashSet<Person> people = [];
            for(int index = 0; index < susceptibleCount; index++) {
                people.Add(GeneratePerson(Health.Susceptible));
            }
            for(int index = 0; index < infectedCount; index++) {
                people.Add(GeneratePerson(Health.Infected));
            }
            return people;
        }

        Person GeneratePerson(Health health) {
            Gender gender = random.NextDouble() < maleProbability ? Gender.Male : Gender.Female;
            return new(gender, 25, health);
        }
    }
}