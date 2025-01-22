namespace CookBook.Configuration.DataBase;

public class CookBookDbConnectionSettings
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 5432;
    public string Username { get; set; } = "postgres";
    public string Password { get; set; } = "Kosmos_12";
    public string Database { get; set; } = "postgres";
    
    public string ConnectionString 
        => $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database}";
}