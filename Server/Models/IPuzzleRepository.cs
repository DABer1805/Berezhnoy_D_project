namespace Server;

// Описание интерфейса IPuzzleRepository
public interface IPuzzleRepository
{
    // Методы для работы с фанерными листами (PlywoodSheet)

    // Получает список всех фанерных листов
    List<PlywoodSheet> GetAllPlywoodSheets();

    // Получает фанерный лист по его названию (title)
    PlywoodSheet GetPlywoodSheetByTitle(string title);

    // Добавляет новый фанерный лист
    void AddPlywoodSheet(PlywoodSheet plywoodSheet);

    // Удаляет фанерный лист по его названию (title)
    void DeletePlywoodSheet(string title);

    // Обновлят информацию о фанерном листе
    void UpdatePlywoodSheet(PlywoodSheet plywoodSheet);

    // Методы для работы с пазлами (Puzzle)

    // Получает список всех пазлов
    List<Puzzle> GetAllPuzzles();

    // Получает пазл по ее названию (title)
    Puzzle GetPuzzleByTitle(string title);

    // Добавляет новый пазл в репозиторий
    void AddPuzzle(Puzzle puzzle);

    // Удаляет пазл из репозитория по ее названию (title)
    void DeletePuzzle(string title);

    // Обновлят информацию о пазле
    void UpdatePuzzle(Puzzle puzzle);

    // Методы для работы с прайс-листом

    // Получает прайс-лист в виде списка словарей
    List<Dictionary<string, string>> GetPriceList();

    // Методы для работы с заказами (Order)
    List<Order> GetAllOrders();
    Order GetOrderById(int id);
    void AddOrder(Order order);
    void DeleteOrder(int id);
    void UpdateOrder(Order order);

    List<ProductionTask> GetAllProductionTasks();
    ProductionTask GetProductionTaskById(int id);
    ProductionTask CreateProductionTaskFromOrder(int orderId);
    void UpdateProductionTask(ProductionTask task);
    void DeleteProductionTask(int id);

    List<SalesReportItem> GetSalesReport(DateTime startDate, DateTime endDate, string? productName = null);
}

