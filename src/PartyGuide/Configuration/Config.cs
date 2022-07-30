namespace PartyGuide.Configuration;

public class Config
{
    public ConnectionStrings ConnectionStrings { get; set; } = new();
}

public class ConnectionStrings
{
    public string Sqlite { get; set; } = string.Empty;
}