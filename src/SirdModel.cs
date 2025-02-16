
namespace monte_carlo_simulation.src {
    using disease;
    
    class SirdSimulator(HashSet<Person> people, IDisease disease) {
        readonly HashSet<Person> allIndividuals = new(people);
        readonly HashSet<Person> susceptibleIndividuals =
            new(people.Where(p => p.GetHealth() == Health.Susceptible));
        
        readonly HashSet<Person> infectedIndividuals =
            new(people.Where(p => p.GetHealth() == Health.Infected));
        
        readonly HashSet<Person> recoveredIndividuals = [];
        readonly HashSet<Person> deceasedIndividuals = [];
        readonly IDisease disease = disease;
        readonly Random random = new();

        public void Next() {
            int infectedCount = infectedIndividuals.Count;
            if(infectedCount == 0) {
                return;
            }

            
            HashSet<Person> formerInfected = [];
            foreach(Person person in infectedIndividuals) {
                var value = random.NextDouble();
                bool recovered = value < disease.GetRecoveryCoefficient(person);
                bool deceased = (1 - value) < disease.GetDeceaseCoefficient(person);
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
                bool infected = value < disease.GetInfectionCoefficient(person) * infectedCount / allIndividuals.Count;
                if(infected) {
                    person.SetHealth(Health.Infected);
                    infectedIndividuals.Add(person);
                    formerSusceptible.Add(person);
                }
            }
            susceptibleIndividuals.RemoveWhere(formerSusceptible.Contains);
        }

        public HashSet<Person> GetIndividuals() {
            return allIndividuals;
        }
    }
}