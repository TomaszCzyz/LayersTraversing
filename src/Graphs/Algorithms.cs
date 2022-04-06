namespace LayersTraversing.Graphs;

public class Algorithms
{
    public static IEnumerable<GraphPath> PathFinder(Graph graph, Dictionary<int, int> requiredNodes, int requiredNoOfPaths)
    {
        var requiredNodesCopy = new Dictionary<int, int>(requiredNodes);
        var currentNoOfPaths = 0;
        var start = int.MinValue;

        if (!graph.AdjacencyList.ContainsKey(start))
        {
            throw new Exception($"No start vertex with index {start}");
        }

        var currentVertex = start;
        var path = new List<int>();

        while (true)
        {
            if (currentNoOfPaths == requiredNoOfPaths)
            {
                Console.WriteLine("Required number of paths has been reached");
                break;
            }

            var nextVertices = graph.AdjacencyList[currentVertex];

            // return path if we are in the last vertex
            if (nextVertices.Count == 0)
            {
                currentNoOfPaths++;
                path.Add(currentVertex);
                yield return new GraphPath(new List<int>(path));
                
                path.Clear();
                currentVertex = start;
                continue;
            }

            var foundNext = false;
            foreach (var nextVertex in nextVertices)
            {
                // if required number of vertex visits is not set, then we can visit given vertex any number of times
                if (requiredNodesCopy.TryGetValue(nextVertex, out var visitsLeft))
                {
                    if (visitsLeft == 0) continue;

                    requiredNodesCopy[nextVertex]--;
                }

                if (currentVertex != start)
                {
                    path.Add(currentVertex);
                }
                currentVertex = nextVertex;
                foundNext = true;
                break;
            }

            if (!foundNext)
            {
                throw new Exception("All possible vertex has been used");
            }
        }
    }
}