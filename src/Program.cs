using LayersTraversing.Extensions;
using LayersTraversing.Graphs;
using LayersTraversing.Input;

const string exampleFileName = "In02.txt";
const string examplesFolder = @"C:\Users\tczyz\Source\repos\LayersTraversing\examples";

var inputData = new InputData();

inputData.ReadFromFile(Path.Join(examplesFolder, exampleFileName));
// inputData.SortLayers();

var graph = inputData.ToGraph();

Console.WriteLine(inputData);
Console.WriteLine(graph.ToString());
// Console.WriteLine(graph.ToString(inputData.NodeMappings));

var paths = Algorithms.DfsModified(graph, inputData.RequiredNodes, inputData.RequiredNoOfPaths);

Console.WriteLine();
foreach (var graphPath in paths)
{
    // Console.WriteLine(graphPath);
    Console.WriteLine(graphPath.ToString(inputData.NodeMappings));
}