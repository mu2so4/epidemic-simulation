using monte_carlo_simulation.src;
using monte_carlo_simulation.src.disease;
using ScottPlot;

static void PlotLine(Plot plot, int[] x, int[] y, string legendText) {
    var susceptileScatter = plot.Add.Scatter(x, y);
    susceptileScatter.MarkerSize = 0;
    susceptileScatter.LegendText = legendText;
}

DiseaseFactory factory = new();
factory.Add("genderless", new SimpleDiseaseProducer());
factory.Add("gender", new GenderDependentProducer());

const double MALE_PROBABILITY = 0.5;

int individualCount = Convert.ToInt32(Console.ReadLine());
int initInfectedCount = Convert.ToInt32(Console.ReadLine());
int iterationCount = Convert.ToInt32(Console.ReadLine());
string diseaseModelName = Console.ReadLine()!;


IDisease disease = factory.NewInstance(diseaseModelName);
IIndividualGenerator generator = new SimpleIndividualGenerator(MALE_PROBABILITY);
SirdSimulator simulator = new(generator.GenerateIndividuals(individualCount - initInfectedCount, initInfectedCount), disease);

var type = simulator.GetType();

List<EpidemicStatiscics> commonStatistics = [];
List<EpidemicStatiscics> maleStatistics = [];
List<EpidemicStatiscics> femaleStatistics = [];


for(int index = 0; index <= iterationCount; index++) {
    commonStatistics.Add(new(index, simulator.GetIndividuals()));
    maleStatistics.Add(new(index, simulator.GetIndividuals().Where(p => p.GetGender() == Gender.Male)));
    femaleStatistics.Add(new(index, simulator.GetIndividuals().Where(p => p.GetGender() == Gender.Female)));
    simulator.Next();
}

int[] x = commonStatistics.Select(s => s.TimeUnit).ToArray();
Plot plot = new();
plot.Title("Epidemic simulation");
PlotLine(plot, x, commonStatistics.Select(s => s.SusceptibleCount).ToArray(), "Susceptible");
PlotLine(plot, x, commonStatistics.Select(s => s.InfectedCount).ToArray(), "Infected");
PlotLine(plot, x, commonStatistics.Select(s => s.RecoveredCount).ToArray(), "Recovered");
PlotLine(plot, x, commonStatistics.Select(s => s.DeceasedCount).ToArray(), "Deceased");

plot.XLabel("Days");
plot.YLabel("Individuals");
plot.Legend.IsVisible = true;
plot.SavePng("quickstart.png", 400, 300);

