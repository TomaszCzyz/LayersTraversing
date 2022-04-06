using System.Text;

namespace LayersTraversing.Input;

public class InputData
{
    /// <summary>
    /// List of all layers containing node IDs (0,1,2,...)
    /// </summary>
    public List<List<int>> Layers { get; private set; } = new();

    /// <summary>
    /// mapping: NodeId -> Quantity
    /// </summary>
    public Dictionary<int, int> RequiredNodes { get; } = new();

    /// <summary>
    /// Required number of paths in the final solution 
    /// </summary>
    public int RequiredNoOfPaths { get; private set; }

    public Dictionary<int, string> NodeMappings { get; } = new();

    /// <summary>
    /// Moves layers that contains any of requires nodes to the beginning of the list
    /// </summary>
    public void SortLayers()
    {
        var sortedLayers = new List<List<int>>();
        var counter = 0;

        foreach (var layer in Layers)
        {
            // todo: use method that does not allocate memory. Use fact that in a layer nodes have id in certain range  
            if (layer.Intersect(RequiredNodes.Keys).Any())
            {
                sortedLayers.Insert(counter, layer);
                counter++;
            }
            else
            {
                sortedLayers.Add(layer);
            }
        }

        Layers = sortedLayers;
    }

    public void ReadFromFile(string path)
    {
        using var sr = new StreamReader(path);

        var line = sr.ReadLine();
        if (!int.TryParse(line, out var noOfLayers))
        {
            throw new ArgumentException("First line must contain number of layers");
        }

        var nodeNumber = 0;
        for (var i = 1; i <= noOfLayers; ++i)
        {
            line = sr.ReadLine() ?? throw new ArgumentException("Layer cannot be empty");

            var nodes = line.Split(' ').ToList();
            var layer = new List<int>();
            foreach (var node in nodes)
            {
                NodeMappings[nodeNumber] = node;
                layer.Add(nodeNumber);
                nodeNumber++;
            }

            Layers.Add(layer);
        }

        line = sr.ReadLine();
        if (!int.TryParse(line, out var requiredNoOfPaths))
        {
            throw new ArgumentException("Line after layers must contain required number of paths");
        }

        RequiredNoOfPaths = requiredNoOfPaths;

        while (!sr.EndOfStream)
        {
            line = sr.ReadLine() ?? throw new ArgumentException("Empty line in this section are not allowed");
            var data = line.Split(' ');

            var (nodeName, quantity) = (data[0], int.Parse(data[1]));
            int nodeId;
            try
            {
                nodeId = NodeMappings.First(pair => pair.Value == nodeName).Key;
            }
            catch (InvalidOperationException e)
            {
                throw new ArgumentException($"Node {nodeName} does not exists in any layer, {e}");
            }

            RequiredNodes.Add(nodeId, quantity);
        }
    }

    public void GenerateValid(int layersNo, int minLayerSize, int maxLayerSize)
    {
        
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append("Layers = {\n");
        foreach (var layer in Layers)
        {
            sb.Append(string.Join(',', layer.Select(i => NodeMappings[i])));
            sb.Append('\n');
        }

        sb.Append("},\n");
        sb.Append($"RequiredNoOfPaths = {RequiredNoOfPaths}");
        sb.Append(",\n");
        sb.Append("RequiredNodes = {\n");

        var pairToPrint = RequiredNodes.Select(node => $"{NodeMappings[node.Key]}: {node.Value}").ToList();
        sb.Append(string.Join(",\n", pairToPrint));
        sb.Append('\n');
        sb.Append("}\n");

        return sb.ToString();
    }
}