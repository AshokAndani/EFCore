using Microsoft.EntityFrameworkCore;
using NotesApi.Models;

namespace NotesApi.Context;

public interface INotesDbContext
{
    public DbSet<TokenModel> Tokens { get; set; }
    public DbSet<UsersModel> Users { get; set; }
    public DbSet<NoteModel> Notes { get; set; }
    Task SaveChangesAsync();
}
