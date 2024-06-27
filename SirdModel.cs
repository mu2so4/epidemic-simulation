

namespace monte_carlo_simulation {
    class SirdSimulator {
        readonly HashSet<Person> susceptibleIndividuals = [];
        readonly HashSet<Person> infectedIndividuals = [];
        readonly HashSet<Person> recoveredIndividuals = [];
        readonly HashSet<Person> deceasedIndividuals = [];
        readonly Disease disease;
        uint stepNumber = 0;

        public SirdSimulator(int individualCount, int initInfectedCount, Disease disease) {
            //TODO
            this.disease = disease;
        }

        public void Next() {
            int infectedCount = infectedIndividuals.Count;
            int susceptibleCount = susceptibleIndividuals.Count;
            if(infectedCount == 0) {
                return;
            }

            Random random = new();

            
            HashSet<Person> formerInfected = [];
            foreach(Person person in infectedIndividuals) {
                var value = random.NextDouble();
                bool recovered = value < disease.GetRecoveryChance(person);
                bool deceased = (1 - value) > disease.GetDeathRisk(person);
                if(recovered) {
                    recoveredIndividuals.Add(person);
                    formerInfected.Add(person);
                }
                if(deceased) {
                    deceasedIndividuals.Add(person);
                    formerInfected.Add(person);
                }
            }
            infectedIndividuals.RemoveWhere(formerInfected.Contains);
            

            HashSet<Person> formerSusceptible = [];
            foreach(Person person in susceptibleIndividuals) {
                var value = random.NextDouble();
                bool infected = value < disease.GetSickRisk(person);
                if(infected) {
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
    }
}