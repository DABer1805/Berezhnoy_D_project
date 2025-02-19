namespace Server;

// Описание интерфейса IPuzzleRepository
public interface IPuzzleRepository
{
    // Методы для работы с фанерными листами (PlywoodSheet)

    // Получает список всех фанерных листов
    List<PlywoodSheet> GetAllPlywoodSheets();

    // Добавляет новый фанерный лист
    void AddPlywoodSheet(PlywoodSheet plywoodSheet);

    // Удаляет фанерный лист по его названию (title)
    void DeletePlywoodSheet(string title);

    // Методы для работы с пазлами (Puzzle)

    // Получает список всех пазлов
    List<Puzzle> GetAllPuzzles();

    // Получает пазл по ее названию (title)
    Puzzle GetPuzzleByTitle(string title);

    // Добавляет новый пазл в репозиторий
    void AddPuzzle(Puzzle puzzle);

    // Удаляет пазл из репозитория по ее названию (title)
    void DeletePuzzle(string title);

    // Методы для работы с прайс-листом

    // Получает прайс-лист в виде списка словарей
    List<Dictionary<string, string>> GetPriceList();
}

