using LAB7_WEB.Logic;

namespace LAB7_WEB.Models;

public class UserModel
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? PasswordConfirm { get; set; }
    public UserTypes Role { get; set; }

}
