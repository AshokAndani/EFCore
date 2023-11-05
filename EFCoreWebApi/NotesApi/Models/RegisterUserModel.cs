namespace NotesApi.Models;

public class RegisterUserModel
{
    public string UserName { get; set; }
    public  string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string  Email { get; set; }
}