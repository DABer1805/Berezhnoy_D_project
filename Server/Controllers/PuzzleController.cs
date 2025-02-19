namespace Server;

using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/")]
public class PuzzleController : ControllerBase
{
    private readonly IPuzzleRepository _puzzleRepository;

    public PuzzleController(IPuzzleRepository puzzleRepository)
    {
        _puzzleRepository = puzzleRepository;
    }

    [HttpGet("puzzle/sheet/show")]
    public IActionResult Show()
    {
        return Ok(_puzzleRepository.GetAllPlywoodSheets());
    }

    [HttpPost("puzzle/sheet/add")]
    public IActionResult Add([FromBody] PlywoodSheet plywoodSheet)
    {
        _puzzleRepository.AddPlywoodSheet(plywoodSheet);
        return Ok(_puzzleRepository.GetAllPlywoodSheets());   
    }

    [HttpDelete("puzzle/sheet/del")]
    public IActionResult Delete(string title)
    {
        _puzzleRepository.DeletePlywoodSheet(title);
        return Ok(_puzzleRepository.GetAllPlywoodSheets());
    }

    [HttpGet("puzzle/show")]
    public IActionResult ShowPuzzle()
    {
        return Ok(_puzzleRepository.GetAllPuzzles());
    }

    [HttpGet("puzzle/show/{title}")]
    public IActionResult ShowPuzzleByName(string title)
    {
        var puzzle = _puzzleRepository.GetPuzzleByTitle(title);
        if (puzzle == null)
        {
            return NotFound();
        }
        return Ok(puzzle);
    }

    [HttpPost("puzzle/add")]
    public IActionResult PuzzleAdd([FromBody] Puzzle puzzle)
    {
        _puzzleRepository.AddPuzzle(puzzle);
        return Ok(_puzzleRepository.GetAllPuzzles());
    }


    [HttpDelete("puzzle/delete/{title}")]
    public IActionResult PuzzleDelete(string title)
    {
        _puzzleRepository.DeletePuzzle(title);
        return Ok(_puzzleRepository.GetAllPuzzles());
    }
}