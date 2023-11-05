namespace NotesApi.Controllers;

public class TokenFactory
{
    public List<Tuple<string, string>> Tokens { get; set; }
    public TokenFactory()
    {
        Tokens = new List<Tuple<string, string>>();
    }
    public bool ValidateToken(string email, string token)
    {
        return this.Tokens.Any(x=> x.Item1 == email && x.Item2 == token);
    }
    public string GetToken(string email)
    {
        var token = Guid.NewGuid().ToString().ToLower();
        Tokens.Add(Tuple.Create(email, token));
        return token;
    }
}
