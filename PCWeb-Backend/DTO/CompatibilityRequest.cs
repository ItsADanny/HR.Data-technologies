public class CompatibilityCheckRequest
{
    public int ProductId1 { get; set; }
    public int ProductId2 { get; set; }
}

public class CompatibilityCheckBatchRequest
{
    public Dictionary<string, int> SelectedParts { get; set; } = new();
}
