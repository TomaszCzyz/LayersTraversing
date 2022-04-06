using System.Text;

namespace LayersTraversing.Graphs;

public class Graph
{
    public Graph()
    {
    }

    public Graph(IEnumerable<int> vertices, IEnumerable<Tuple<int, int>> edges)
    {
        foreach (var vertex in vertices)
        {
            AddVertex(vertex);
        }

        foreach (var edge in edges)
        {
            AddEdge(edge);
        }
    }

    public Dictionary<int, HashSet<int>> AdjacencyList { get; } = new();

    public void AddVertex(int vertex)
    {
        AdjacencyList[vertex] = new HashSet<int>();
    }

    public void AddEdge(Tuple<int, int> edge)
    {
        var (v1, v2) = edge;
        if (!AdjacencyList.ContainsKey(v1) || !AdjacencyList.ContainsKey(v2)) return;

        AdjacencyList[v1].Add(v2);
        // AdjacencyList[v2].Add(v1);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append("Adjacency list of the graph:\n");
        foreach (var (key, vertices) in AdjacencyList)
        {
            sb.Append(key);
            sb.Append(" -> ");
            sb.Append(string.Join(", ", vertices));
            sb.AppendLine();
        }

        return sb.ToString();
    }
    
    public string ToString(Dictionary<int, string> verticesNames)
    {
        var sb = new StringBuilder();

        sb.Append("Adjacency list of the graph:\n");
        foreach (var (key, vertices) in AdjacencyList)
        {
            sb.Append(verticesNames.TryGetValue(key, out var keyValue) ? keyValue : key);
            sb.Append(" -> ");

            var vertexNames = new List<string?>();
            foreach (var vertex in vertices)
            {
                var vertexName = verticesNames.TryGetValue(vertex, out var vertexValue) ? vertexValue : vertex.ToString();
                vertexNames.Add(vertexName);
            }
            sb.Append(string.Join(", ", vertexNames));
            sb.AppendLine();
        }

        return sb.ToString();
    }
}