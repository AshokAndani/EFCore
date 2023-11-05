using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApi.Models;

public class TokenModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Token { get; set; } = null;
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
}
