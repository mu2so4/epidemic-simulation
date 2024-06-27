// See https://aka.ms/new-console-template for more information

using monte_carlo_simulation.src;

static void PrintStats(SirdSimulator simulator) {
    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", simulator.GetStepNumber(),
        simulator.GetSusceptibleCount(), simulator.GetInfectedCount(),
        simulator.GetRecoveredCount(), simulator.GetDeceasedCount());
}

int individualCount = Convert.ToInt32(Console.ReadLine());
int initInfectedCount = Convert.ToInt32(Console.ReadLine());
double infectionCoef = Convert.ToDouble(Console.ReadLine());
double recoverCoef = Convert.ToDouble(Console.ReadLine());
double deceaseCoef = Convert.ToDouble(Console.ReadLine());
int iterationCount = Convert.ToInt32(Console.ReadLine());

IDisease disease = new SimpleDisease(infectionCoef, recoverCoef, deceaseCoef);
SirdSimulator simulator = new(individualCount, initInfectedCount, disease);

PrintStats(simulator);

for(int index = 0; index < iterationCount; index++) {
    simulator.Next();
    PrintStats(simulator);
}