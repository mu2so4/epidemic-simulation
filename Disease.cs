namespace monte_carlo_simulation {
    interface Disease {
        double GetSickRisk(Person person);
        double GetRecoveryChance(Person person);
        double GetDeathRisk(Person person);
    }
}