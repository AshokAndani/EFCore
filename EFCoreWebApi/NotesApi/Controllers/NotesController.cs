using Microsoft.AspNetCore.Mvc;
using NotesApi.Filters;
using NotesApi.Context;
using Microsoft.EntityFrameworkCore;
using NotesApi.Models;

namespace NotesApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NotesController : BaseController
{
    private readonly INotesDbContext notesDbContext;

    public NotesController(INotesDbContext notesDbContext) : base(notesDbContext)
    {
        this.notesDbContext = notesDbContext;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <header name="x-session-id" type="guid" allowEmpty="No" allowNull="No">To authenticate the request.</header>
    /// <returns></returns>
    [HttpGet]
    [Protected]
    public async Task<IActionResult> GetAllNotes()
    {
        var notes = await this.context.Notes
            .Where(x => x.UserId == this.User.Id).ToListAsync();

        return notes?.Count > 0 ? Ok(notes) : NotFound();
    }

    [HttpPost]
    [Protected]
    public async Task<IActionResult> AddNote([FromBody]NoteModel model)
    {
        model.UserId = this.User.Id;
        model.CreateDateInUtc = DateTime.UtcNow;
        model.UpdateDateInUtc = DateTime.UtcNow;
        await this.context.Notes.AddAsync(model);
        await this.context.SaveChangesAsync();
        return Ok(model);
    }

    [HttpGet]
    [Route("{id}")]
    [Protected]
    public async Task<IActionResult> GetNote(int id)
    {
        var model = await this.context.Notes.FindAsync(("Id", id), ("UserId", this.User.Id));
        return model ==null ? NotFound() : Ok(model);
    }

}
