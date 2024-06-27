namespace monte_carlo_simulation {
    enum Health {
        SUSCEPTIBLE,
        INFECTED,
        RECOVERED,
        DECEASED
    }

    enum Gender {
        FEMALE,
        MALE
    }

    class Person {
        Health health;
        Gender gender;
        uint age;

        public Person(Gender gender, uint age, Health health = Health.SUSCEPTIBLE) {
            this.health = health;
            this.gender = gender;
            this.age = age;
        }
    }
}