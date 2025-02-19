namespace Server;

public interface IPuzzleRepository
{
    List<PlywoodSheet> GetAllPlywoodSheets();
    void AddPlywoodSheet(PlywoodSheet plywoodSheet);
    void DeletePlywoodSheet(string title);

    List<Puzzle> GetAllPuzzles();
    Puzzle GetPuzzleByTitle(string title);
    void AddPuzzle(Puzzle puzzle);
    void DeletePuzzle(string title);
}