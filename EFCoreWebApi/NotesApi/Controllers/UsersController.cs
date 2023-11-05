using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApi.Context;
using NotesApi.Models;

namespace NotesApi.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly INotesDbContext notesDbContext;

    public UsersController(INotesDbContext notesDbContext)
    {
        this.notesDbContext = notesDbContext;
    }
    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody]UsersModel userModel)
    {
        // there is no validation for password can be implemented with SHA Hashing. :-)

         var user = await notesDbContext.Users.Where(x => x.Email.ToLower().Equals(userModel.Email.ToLower()) && x.Enabled == true).FirstOrDefaultAsync();
        if(user == null)
        {
            return Unauthorized("Incorrect Username or Password");
        }
        if (user.Enabled)
        {
            // create new token.
            var token = new TokenModel
            {
                CreatedDate = DateTime.UtcNow,
                Token = Guid.NewGuid().ToString().ToLower(),
                UserId = user.Id,
            };
            await this.notesDbContext.Tokens.AddAsync(token);
            await this.notesDbContext.SaveChangesAsync();
            return Ok(token);
        }
        return Unauthorized("User is disabled");
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]RegisterUserModel userModel)
    {
        // --- validate model values

        // check if there is already an user with same email exists
        var exists =await this.notesDbContext.Users
            .AnyAsync(x=>x.Email.ToLower().Equals(userModel.Email.ToLower()));

        if (exists)
        {
            return BadRequest("User with Email already Exists");
        }
        var result = await this.notesDbContext.Users.AddAsync(new UsersModel
        {
            Email = userModel.Email,
            UserName = userModel.UserName,
            Password = userModel.Password,
            Enabled = true,
        });

        await this.notesDbContext.SaveChangesAsync();
        return Ok();
    }

}
