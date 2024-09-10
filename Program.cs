
/** 

    The task is graph problem where 
    solution is the distance to COM for all 
    objects in the graph.

    One can use a modified DFS search algorithm
    to calculate the total orbit count. We shall read all
    vertices to a Dictionary structure and use the search to calculate
    distances.
*/
class Program
{
    /// <summary>
    /// Pass filepath to the data file
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Unexpected number of arguments. Please provide path to input data");
            return;
        }

        // Read data
        var lines = File.ReadAllLines(args[0]).ToList();
        var orbits = new Dictionary<string, List<string>>();

        foreach (var line in lines)
        {
            var split = line.Split(")");
            var key = split[0];
            var value = split[1];
            // In python try-catch is faster than if-in. Don't know about C# spesifically.
            try
            {
                orbits[key].Add(value);
            }
            catch
            {
                orbits[key] = [value];
            }

        }
        var orbitNum = CalculateOrbits(orbits);
        Console.WriteLine($"The total number of direct and indirect orbits is {orbitNum}");
    }
    /// <summary>
    /// Search returns the amount indirect and direct orbits in the graph. Uses recusive search.
    /// </summary>
    /// <param name="graph">Dictionmary of the graph vertices</param>
    /// <param name="node">Start node of depth search</param>
    /// <param name="depth">Current depth</param>
    /// <returns>Total number of orbits</returns>
    static int CalculateOrbits(Dictionary<string, List<string>> graph, string node = "COM", int depth = 0)
    {
        var total = depth;
        // The node does not necessary have any orbiters, so
        // it might not exist in the dictionary
        var nodes = graph.GetValueOrDefault(node, []);
        foreach (var obs in nodes)
        {
            total += CalculateOrbits(graph, obs, depth + 1);
        }
        return total;
    }

}


