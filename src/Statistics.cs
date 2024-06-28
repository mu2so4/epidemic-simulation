namespace monte_carlo_simulation.src {
    class EpidemicStatiscics(int timeUnit, IEnumerable<Person> people) {
        public int TimeUnit {get;} = timeUnit;
        public int SusceptibleCount {get;} = people.Count(p => p.GetHealth() == Health.Susceptible);
        public int InfectedCount {get;} = people.Count(p => p.GetHealth() == Health.Infected);
        public int RecoveredCount {get;} = people.Count(p => p.GetHealth() == Health.Recovered);
        public int DeceasedCount {get;} = people.Count(p => p.GetHealth() == Health.Deceased);
    }
}