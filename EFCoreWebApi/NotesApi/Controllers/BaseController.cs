using Microsoft.AspNetCore.Mvc;
using NotesApi.Context;
using NotesApi.Models;

namespace NotesApi.Controllers;

public class BaseController : ControllerBase
{
    public readonly INotesDbContext context;
    public UsersModel User;
    public BaseController(INotesDbContext context)
    {
        this.context = context;
    }
}
