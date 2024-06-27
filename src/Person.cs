namespace monte_carlo_simulation.src {
    enum Health {
        Susceptible,
        Infected,
        Recovered,
        Deceased
    }

    enum Gender {
        Female,
        Male
    }

    class Person(Gender gender, uint age, Health health)
    {
        readonly Gender gender = gender;
        Health health = health;
        readonly uint age = age;

        public Health GetHealth() {
            return health;
        }

        public Gender GetGender() {
            return gender;
        }

        public uint GetAge() {
            return age;
        }

        public void SetHealth(Health health) {
            this.health = health;
        }
    }
}