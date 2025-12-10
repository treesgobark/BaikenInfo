using System.Text.Json.Serialization;

namespace Client.Shared;

public class GameTheoryTableData
{
    [JsonIgnore]
    public bool Editable { get; set; }
    
    [JsonIgnore]
    public int RowCount => SideHeaders.Count;
    [JsonIgnore]
    public int ColumnCount => TopHeaders.Count;

    public List<List<int>> Data
    {
        get
        {
            while (field.Count < RowCount)
            {
                Console.WriteLine("Adding data row");
                field.Add([]);
            }
            
            while (field.Count > RowCount)
            {
                Console.WriteLine("removing data row");
                field.RemoveAt(field.Count - 1);
            }

            foreach (var rowData in field)
            {
                while (rowData.Count < ColumnCount)
                {
                    Console.WriteLine("Adding data column");
                    rowData.Add(0);
                }
            
                while (rowData.Count > ColumnCount)
                {
                    Console.WriteLine("removing data column");
                    rowData.RemoveAt(rowData.Count - 1);
                }
            }

            return field;
        }
        init;
    } = [[]];

    public List<string> TopHeaders
    {
        get
        {
            if (Editable && (field.Count == 0 || field.Last() != ""))
            {
                field.Add("");
            }
            else if (!Editable && field.Last() == "")
            {
                field.RemoveAt(field.Count - 1);
            }

            return field;
        }
        init;
    } = [];

    public List<string> SideHeaders
    {
        get
        {
            if (Editable && (field.Count == 0 || field.Last() != ""))
            {
                field.Add("");
            }
            else if (!Editable && field.Last() == "")
            {
                field.RemoveAt(field.Count - 1);
            }

            return field;
        }
        init;
    } = [];

    public List<int> Probabilities
    {
        get
        {
            while (field.Count < ColumnCount)
            {
                field.Add(0);
            }
            
            while (field.Count > ColumnCount)
            {
                field.RemoveAt(field.Count - 1);
            }

            if (field.Count > 0 && field.All(x => x == 0))
            {
                field[0] = 1;
            }
            
            return field;
        }
        init;
    } = [];

    [JsonIgnore]
    public GameTheoryRow ProbabilitiesRow =>
        new()
        {
            Values = Probabilities,
            ExpectedValue = Probabilities.Sum(),
        };

    [JsonIgnore]
    public IEnumerable<GameTheoryRow> DataRows
    {
        get
        {
            foreach ((int index, var row) in Data.Index())
            {
                float expectedValue = row.Select((x, i) => x * Probabilities[i] / (float)Probabilities.Sum()).Sum();
                if (string.IsNullOrWhiteSpace(SideHeaders[index]))
                {
                    expectedValue = float.MinValue;
                }
                
                yield return new GameTheoryRow
                {
                    Index         = index,
                    Values        = row,
                    ExpectedValue = expectedValue,
                };
            }
        }
    }

    public override string ToString()
    {
        return string.Join(", ", Data.Select(d => $"[{string.Join(", ", d)}]"));
    }
}

public class GameTheoryRow
{
    public int Index { get; set; }
    public List<int> Values { get; set; }
    public float ExpectedValue { get; set; }
}

public static class GameTheoryTableDefaults
{
    public static string VsSol =>
        """
        {
        	"Data": [
        		[
        			0,
        			-80,
        			0
        		],
        		[
        			0,
        			-80,
        			0
        		],
        		[
        			-200,
        			70,
        			35
        		],
        		[
        			100,
        			-80,
        			-200
        		],
        		[
        			130,
        			130,
        			-150
        		]
        	],
        	"TopHeaders": [
        		"c.S",
        		"Throw",
        		"Bait"
        	],
        	"SideHeaders": [
        		"Block",
        		"Up-Back",
        		"5P",
        		"Parry",
        		"Sword Super"
        	],
        	"Probabilities": [
        		4,
        		2,
        		1
        	]
        }
        """;
}
