

namespace monte_carlo_simulation.src {
    class SirdSimulator {
        readonly HashSet<Person> susceptibleIndividuals = [];
        readonly HashSet<Person> infectedIndividuals = [];
        readonly HashSet<Person> recoveredIndividuals = [];
        readonly HashSet<Person> deceasedIndividuals = [];
        readonly IDisease disease;
        uint stepNumber = 0;
        readonly Random random = new();

        public SirdSimulator(int individualCount, int initInfectedCount, IDisease disease) {
            if(initInfectedCount > individualCount) {
                throw new ArgumentException("initial infected count cannot be greater than common individual count");
            }
            this.disease = disease ?? throw new ArgumentNullException(nameof(disease));

            for(int index = 0; index < individualCount - initInfectedCount; index++) {
                susceptibleIndividuals.Add(GeneratePerson(Health.Susceptible));
            }
            for(int index = 0; index < initInfectedCount; index++) {
                infectedIndividuals.Add(GeneratePerson(Health.Infected));
            }
        }

        public void Next() {
            int infectedCount = infectedIndividuals.Count;
            int susceptibleCount = susceptibleIndividuals.Count;
            if(infectedCount == 0) {
                return;
            }

            
            HashSet<Person> formerInfected = [];
            foreach(Person person in infectedIndividuals) {
                var value = random.NextDouble();
                bool recovered = value < disease.GetRecoveryCoefficient(person);
                bool deceased = (1 - value) > disease.GetDeceaseCoefficient(person);
                if(recovered) {
                    person.SetHealth(Health.Recovered);
                    recoveredIndividuals.Add(person);
                    formerInfected.Add(person);
                }
                if(deceased) {
                    person.SetHealth(Health.Deceased);
                    deceasedIndividuals.Add(person);
                    formerInfected.Add(person);
                }
            }
            infectedIndividuals.RemoveWhere(formerInfected.Contains);
            

            HashSet<Person> formerSusceptible = [];
            foreach(Person person in susceptibleIndividuals) {
                var value = random.NextDouble();
                bool infected = value < disease.GetInfectionCoefficient(person);
                if(infected) {
                    person.SetHealth(Health.Infected);
                    infectedIndividuals.Add(person);
                    formerSusceptible.Add(person);
                }
            }
            susceptibleIndividuals.RemoveWhere(formerSusceptible.Contains);
            stepNumber++;
        }

        public int GetSusceptibleCount() {
            return susceptibleIndividuals.Count;
        }

        public int GetInfectedCount() {
            return infectedIndividuals.Count;
        }

        public int GetRecoveredCount() {
            return recoveredIndividuals.Count;
        }

        public int GetDeceasedCount() {
            return deceasedIndividuals.Count;
        }

        Person GeneratePerson(Health health) {
            Gender gender = random.NextDouble() < 0.5 ? Gender.Male : Gender.Female;
            return new(gender, 25, health);
        }
    }
}