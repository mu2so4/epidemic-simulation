namespace monte_carlo_simulation.src.disease {
    interface IDisease {
        double GetInfectionCoefficient(Person person);
        double GetRecoveryCoefficient(Person person);
        double GetDeceaseCoefficient(Person person);
    }

    class SimpleDisease(double infectionCoefficient,
                        double recoveryCoefficient,
                        double deceaseCoefficient) : IDisease {
        readonly double infectionCoefficient = infectionCoefficient;
        readonly double recoveryCoefficient = recoveryCoefficient;
        readonly double deceaseCoefficient = deceaseCoefficient;

        public double GetDeceaseCoefficient(Person person) {
            return deceaseCoefficient;
        }

        public double GetInfectionCoefficient(Person person) {
            return infectionCoefficient;
        }

        public double GetRecoveryCoefficient(Person person) {
            return recoveryCoefficient;
        }
    }

    class GenderDependentDisease(double maleInfectionCoefficient,
                                 double maleRecoveryCoefficient,
                                 double maleDeceaseCoefficient,
                                 double femaleInfectionCoefficient,
                                 double femaleRecoveryCoefficient,
                                 double femaleDeceaseCoefficient) : IDisease {
        readonly double maleInfectionCoefficient = maleInfectionCoefficient;
        readonly double maleRecoveryCoefficient = maleRecoveryCoefficient;
        readonly double maleDeceaseCoefficient = maleDeceaseCoefficient;
        readonly double femaleInfectionCoefficient = femaleInfectionCoefficient;
        readonly double femaleRecoveryCoefficient = femaleRecoveryCoefficient;
        readonly double femaleDeceaseCoefficient = femaleDeceaseCoefficient;

        public double GetDeceaseCoefficient(Person person) {
            if(person.GetGender() == Gender.Male) {
                return maleDeceaseCoefficient;
            }
            return femaleDeceaseCoefficient;
        }

        public double GetInfectionCoefficient(Person person) {
            if(person.GetGender() == Gender.Male) {
                return maleInfectionCoefficient;
            }
            return femaleInfectionCoefficient;
        }

        public double GetRecoveryCoefficient(Person person) {
            if(person.GetGender() == Gender.Male) {
                return maleRecoveryCoefficient;
            }
            return femaleRecoveryCoefficient;
        }
    }
}