public class CompatibilityResultDTO
{
    public bool IsCompatible { get; set; }
    public string CheckType { get; set; }
    public string Component1 { get; set; }
    public string Component2 { get; set; }
    public List<string> Warnings { get; set; } = new List<string>();
    public Dictionary<string, string> Details { get; set; } = new Dictionary<string, string>();
}