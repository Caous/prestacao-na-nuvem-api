namespace SmartOficina.Api.Domain.Model;

public class EmailConfigHost
{
    public EmailConfigHost(string host, int port, string userName, string password)
    {
        Host = host;
        Port = port;
        UserName = userName;
        Password = password;
    }
    public string Host { get; private set; }
    public int Port { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
}
