using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NotesApi.Models;

public class NoteModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [JsonIgnore]
    public int UserId { get; set; }

    [JsonIgnore]
    public UsersModel? User { get; set; } = null;

    public string? Header { get; set; }

    public string? Content { get; set; }

    [JsonIgnore]
    public DateTime CreateDateInUtc { get; set; }
    [JsonIgnore]
    public DateTime? UpdateDateInUtc { get; set; }
}
