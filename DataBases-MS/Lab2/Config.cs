public class Config
{
    public string ConnectionString { get; set; }
    public MasterConfig Master { get; set; }
    public DetailConfig Detail { get; set; }
}

public class MasterConfig
{
    public string Table { get; set; }
    public string IdColumn { get; set; }
    public string DisplayColumn { get; set; }
    public string Query { get; set; }
}

public class DetailConfig
{
    public string Table { get; set; }
    public string ForeignKey { get; set; }
    public string Query { get; set; }
}