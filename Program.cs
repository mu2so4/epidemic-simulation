using monte_carlo_simulation.src;

static void PrintStats(EpidemicStatiscics statiscics, StreamWriter writetext) {
    writetext.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", statiscics.TimeUnit,
        statiscics.SusceptibleCount, statiscics.InfectedCount,
        statiscics.RecoveredCount, statiscics.DeceasedCount);
}

int individualCount = Convert.ToInt32(Console.ReadLine());
int initInfectedCount = Convert.ToInt32(Console.ReadLine());
double infectionCoef = Convert.ToDouble(Console.ReadLine());
double recoverCoef = Convert.ToDouble(Console.ReadLine());
double deceaseCoef = Convert.ToDouble(Console.ReadLine());
int iterationCount = Convert.ToInt32(Console.ReadLine());

const double MALE_PROBABILITY = 0.5;

IDisease disease = new SimpleDisease(infectionCoef, recoverCoef, deceaseCoef);
IIndividualGenerator generator = new SimpleIndividualGenerator(MALE_PROBABILITY);
SirdSimulator simulator = new(generator.GenerateIndividuals(individualCount - initInfectedCount, initInfectedCount), disease);

List<EpidemicStatiscics> commonStatistics = [];
List<EpidemicStatiscics> maleStatistics = [];
List<EpidemicStatiscics> femaleStatistics = [];


for(int index = 0; index <= iterationCount; index++) {
    commonStatistics.Add(new(index, simulator.GetIndividuals()));
    maleStatistics.Add(new(index, simulator.GetIndividuals().Where(p => p.GetGender() == Gender.Male)));
    femaleStatistics.Add(new(index, simulator.GetIndividuals().Where(p => p.GetGender() == Gender.Female)));
    simulator.Next();
}

using(StreamWriter writer = new("out-common.csv")) {
    writer.Write("Time\tSusceptible\tInfected\tRecovered\tDeceased\n");
    foreach(var stat in commonStatistics) {
        PrintStats(stat, writer);
    }
}

using(StreamWriter writer = new("out-male.csv")) {
    writer.Write("Time\tSusceptible\tInfected\tRecovered\tDeceased\n");
    foreach(var stat in maleStatistics) {
        PrintStats(stat, writer);
    }
}

using(StreamWriter writer = new("out-female.csv")) {
    writer.Write("Time\tSusceptible\tInfected\tRecovered\tDeceased\n");
    foreach(var stat in femaleStatistics) {
        PrintStats(stat, writer);
    }
}
