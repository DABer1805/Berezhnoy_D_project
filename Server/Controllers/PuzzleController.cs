namespace Server;

using Microsoft.AspNetCore.Mvc;

// Атрибут, указывающий, что этот класс является контроллером API
[ApiController]
// Базовый маршрут для всех endpoint'ов в этом контроллере
[Route("api/")]
public class PuzzleController : ControllerBase
{
    // Приватное поле для хранения экземпляра репозитория
    private readonly IPuzzleRepository _puzzleRepository;

    // Конструктор, принимающий экземпляр репозитория через Dependency Injection
    public PuzzleController(IPuzzleRepository puzzleRepository)
    {
        _puzzleRepository = puzzleRepository;
    }

    // Endpoint для получения всех записей о листах фанеры
    [HttpGet("puzzle/sheet/show")]
    public IActionResult Show()
    {
        // Возвращает все записи из таблицы PlywoodSheets
        return Ok(_puzzleRepository.GetAllPlywoodSheets());
    }

    // Endpoint для получения записи о листе фанеры по названию
    [HttpGet("puzzle/sheet/show/{title}")]
    public IActionResult ShowPlywoodSheetByName(string title)
    {
        // Получает лист фанеры по названию
        var puzzle = _puzzleRepository.GetPlywoodSheetByTitle(title);
        // Если лист фанеры не найден, возвращает статус 404 (Not Found)
        if (puzzle == null)
        {
            return NotFound();
        }
        // Возвращает найденный лист фанеры
        return Ok(puzzle);
    }

    // Endpoint для добавления новой записи о листе фанеры
    [HttpPost("puzzle/sheet/add")]
    public IActionResult Add([FromBody] PlywoodSheet plywoodSheet)
    {
        // Добавляет новый лист фанеры в базу данных
        _puzzleRepository.AddPlywoodSheet(plywoodSheet);
        // Возвращает обновленный список всех листов фанеры
        return Ok(_puzzleRepository.GetAllPlywoodSheets());   
    }

    // Endpoint для удаления записи о листе фанеры по названию
    [HttpDelete("puzzle/sheet/del")]
    public IActionResult Delete(string title)
    {
        // Удаляет лист фанеры по названию
        _puzzleRepository.DeletePlywoodSheet(title);
        // Возвращает обновленный список всех листов фанеры
        return Ok(_puzzleRepository.GetAllPlywoodSheets());
    }

    // Endpoint для обновления записи о листе фанеры по названию
    [HttpPut("puzzle/sheet/update")]
    public IActionResult UpdatePlywoodSheet([FromBody] PlywoodSheet plywoodSheet)
    {
        _puzzleRepository.UpdatePlywoodSheet(plywoodSheet);
        return Ok(_puzzleRepository.GetAllPlywoodSheets());
    }


    // Endpoint для получения всех записей о пазлах
    [HttpGet("puzzle/show")]
    public IActionResult ShowPuzzle()
    {
        // Возвращает все записи из таблицы Puzzles
        return Ok(_puzzleRepository.GetAllPuzzles());
    }

    // Endpoint для получения записи о пазле по названию
    [HttpGet("puzzle/show/{title}")]
    public IActionResult ShowPuzzleByName(string title)
    {
        // Получает пазл по названию
        var puzzle = _puzzleRepository.GetPuzzleByTitle(title);
        // Если пазл не найден, возвращает статус 404 (Not Found)
        if (puzzle == null)
        {
            return NotFound();
        }
        // Возвращает найденный пазл
        return Ok(puzzle);
    }

    // Endpoint для добавления новой записи о пазле
    [HttpPost("puzzle/add")]
    public IActionResult PuzzleAdd([FromBody] Puzzle puzzle)
    {
        // Добавляет новый пазл в базу данных
        _puzzleRepository.AddPuzzle(puzzle);
        // Возвращает обновленный список всех пазлов
        return Ok(_puzzleRepository.GetAllPuzzles());
    }

    // Endpoint для удаления записи о пазле по названию
    [HttpDelete("puzzle/delete/{title}")]
    public IActionResult PuzzleDelete(string title)
    {
        // Удаляет пазл по названию
        _puzzleRepository.DeletePuzzle(title);
        // Возвращает обновленный список всех пазлов
        return Ok(_puzzleRepository.GetAllPuzzles());
    }

    [HttpPut("puzzle/update")]
    public IActionResult Update([FromBody] Puzzle puzzle)
    {
        _puzzleRepository.UpdatePuzzle(puzzle);
        return Ok(_puzzleRepository.GetAllPuzzles());
    }


    // Endpoint для получения прайс-листа всех пазлов
    [HttpGet("puzzle/price-list")]
    public IActionResult GetPriceList()
    {
        // Получает список всех пазлов с их названиями и ценами
        var priceList = _puzzleRepository.GetPriceList();
        // Возвращает прайс-лист
        return Ok(priceList);
    }


    [HttpGet("order/show")]
    public IActionResult GetAllOrders()
    {
        return Ok(_puzzleRepository.GetAllOrders());
    }

    [HttpGet("order/show/{id}")]
    public IActionResult GetOrderById(int id)
    {
        var order = _puzzleRepository.GetOrderById(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPost("order/add")]
    public IActionResult AddOrder([FromBody] Order order)
    {
        _puzzleRepository.AddOrder(order);
        return Ok(_puzzleRepository.GetAllOrders());
    }

    [HttpPut("order/update")]
    public IActionResult UpdateOrder([FromBody] Order order)
    {
        _puzzleRepository.UpdateOrder(order);
        return Ok(_puzzleRepository.GetAllOrders());
    }

    [HttpDelete("order/delete/{id}")]
    public IActionResult DeleteOrder(int id)
    {
        _puzzleRepository.DeleteOrder(id);
        return Ok(_puzzleRepository.GetAllOrders());
    }
}