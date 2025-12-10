using System.Text.Json;
using System.Text.Json.Serialization;

namespace Client.Shared;

public class GameTheoryTableData
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    
    [JsonIgnore]
    public bool Editable { get; set; }
    
    [JsonIgnore]
    public int RowCount => SideHeaders.Count;
    [JsonIgnore]
    public int ColumnCount => TopHeaders.Count;
    [JsonIgnore]
    public string TableIdentifier => string.IsNullOrWhiteSpace(Name) ? "(Unnamed)" : Name;

    private readonly List<List<int>> _data = [[]];
    public List<List<int>> Data
    {
        get
        {
            while (_data.Count < RowCount)
            {
                Console.WriteLine("Adding data row");
                _data.Add([]);
            }
            
            while (_data.Count > RowCount)
            {
                Console.WriteLine("removing data row");
                _data.RemoveAt(_data.Count - 1);
            }

            foreach (var rowData in _data)
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

            return _data;
        }
        init => _data = value;
    }

    private readonly List<string> _topHeaders = [];
    public List<string> TopHeaders
    {
        get
        {
            if (_topHeaders.Count == 0)
            {
                _topHeaders.Add("");
                Editable = true;
            }
            
            if (Editable && _topHeaders.Last() is not "")
            {
                _topHeaders.Add("");
            }

            if (!Editable && _topHeaders.Count > 1 && _topHeaders.Last() is "")
            {
                _topHeaders.RemoveAt(_topHeaders.Count - 1);
            }

            return _topHeaders;
        }
        init => _topHeaders = value;
    }

    private readonly List<string> _sideHeaders = [];
    public List<string> SideHeaders
    {
        get
        {
            if (_sideHeaders.Count == 0)
            {
                _sideHeaders.Add("");
                Editable = true;
            }
            
            if (Editable && _sideHeaders.Last() is not "")
            {
                _sideHeaders.Add("");
            }

            if (!Editable && _sideHeaders.Count > 1 && _sideHeaders.Last() is "")
            {
                _sideHeaders.RemoveAt(_sideHeaders.Count - 1);
            }

            return _sideHeaders;
        }
        init => _sideHeaders = value;
    }

    private readonly List<int> _probabilities = [];
    public List<int> Probabilities
    {
        get
        {
            while (_probabilities.Count < ColumnCount)
            {
                _probabilities.Add(0);
            }
            
            while (_probabilities.Count > ColumnCount)
            {
                _probabilities.RemoveAt(_probabilities.Count - 1);
            }

            if (_probabilities.Count > 0 && _probabilities.All(x => x == 0))
            {
                _probabilities[0] = 1;
            }
            
            return _probabilities;
        }
        init => _probabilities = value;
    }

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

    public void Clear()
    {
        Data.Clear();
        TopHeaders.Clear();
        SideHeaders.Clear();
        Probabilities.Clear();
    }

    public bool IsEmpty => TopHeaders.Count == 0 && SideHeaders.Count == 0;
}

public class GameTheoryRow
{
    public int Index { get; set; }
    public List<int> Values { get; set; }
    public float ExpectedValue { get; set; }
}

public static class GameTheoryTableDefaults
{
    public static GameTheoryTableData WakeupVsSol => JsonSerializer.Deserialize<GameTheoryTableData>(_wakeupVsSol);
    private static readonly string _wakeupVsSol = """
                                                  {"Id":"f9787bfc-e59d-4379-bfcd-71584db66575","Name":"Baiken Wakeup vs Sol","Data":[[0,-80,0],[0,-80,0],[-200,70,35],[100,-80,-200],[130,130,-150]],"TopHeaders":["c.S","Throw","Bait"],"SideHeaders":["Block","Up-Back","5P","Parry","Sword Super"],"Probabilities":[4,2,1],"IsEmpty":false}
                                                  """;

    public static GameTheoryTableData BaikenMirrorHkabRPS => JsonSerializer.Deserialize<GameTheoryTableData>(_baikenMirrorHkabRPS);
    private static readonly string _baikenMirrorHkabRPS = """
                                                          {"Id":"3306ea04-cd5f-47cf-8990-8f39a5b5f907","Name":"Baiken Mirror hkab RPS","Data":[[0,0,100,150,150],[-150,0,-70,150,150],[48,-70,48,-100,-80],[150,0,150,-100,-80]],"TopHeaders":["f.S","Block","6P","Parry","Throw"],"SideHeaders":["Block","f.S","~H Follow-up","~H RRC"],"Probabilities":[5,3,1,1,1],"IsEmpty":false}
                                                          """;
}
