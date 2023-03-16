using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    // The [ApiController] attribute lets ASP.NET know that this is a controller that responds to HTTP requests
    [ApiController]
    [Route("")]
    public class WebApiControllers : Controller
    {
        private readonly WebApiDbContext _dBContext;
        public WebApiControllers(WebApiDbContext dBContext)
        {
            _dBContext = dBContext;
        }

        [HttpGet("highscores")]
        public async Task<ActionResult<IEnumerable<Highscore>>> Get()
        {
            // If there are no highscores in the database, we return a 404 Not Found response
            if (_dBContext.Highscores == null)
            {
                return NotFound();
            }

            return await _dBContext.Highscores.ToListAsync();
        }

        [HttpGet("highscores/top10")]
        public async Task<ActionResult<IEnumerable<Highscore>>> GetTop10()
        {
            var highscores = await _dBContext.Highscores.OrderBy(hs => hs.Time).Take(10).ToListAsync();

            if (highscores.Count == 0)
            {
                return new Highscore[0];
            }

            return highscores;
        }


        [HttpPost("highscores")]
        public async Task<IActionResult> Post(Highscore highScore)
        {
            _dBContext.Highscores.Add(highScore);
            await _dBContext.SaveChangesAsync();

            // Return a 201 Created response with the new highscore in the body
            return CreatedAtAction(nameof(highScore), new { id = highScore.Id }, highScore);
        }

        [HttpDelete("highscores/deleteall")]
        public async Task<IActionResult> DeleteAll()
        {
            _dBContext.Highscores.RemoveRange(_dBContext.Highscores);
            await _dBContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
