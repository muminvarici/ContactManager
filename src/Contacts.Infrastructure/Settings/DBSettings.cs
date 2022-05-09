namespace Contacts.Infrastructure.Settings;

public class DbSettings
{
    public string Database { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }

    public string ConnectionString
    {
        get => string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password)
            ? $@"mongodb://{Host}:{Port}"
            : $@"mongodb://{User}:{Password}@{Host}:{Port}/?authSource=admin";
    }
}
