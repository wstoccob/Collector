namespace Collector.Models;

public class UserDto
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
}