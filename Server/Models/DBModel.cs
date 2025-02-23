namespace Server;

using System.Data.SQLite;
using System.Collections.Generic;

public class SQLitePuzzleRepository : IPuzzleRepository
{
    // Строка подключения к базе данных
    private string _connectionString;

    // SQL-запрос для создания таблиц, если они не существуют
    private const string CreateTableQuery = @"
        CREATE TABLE IF NOT EXISTS PlywoodSheets (
            Id INTEGER PRIMARY KEY,
            Title TEXT NOT NULL,
            Material TEXT NOT NULL,
            Thickness INTEGER NOT NULL
        );
        CREATE TABLE IF NOT EXISTS Puzzles (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Title TEXT NOT NULL,
            SheetType TEXT NOT NULL,
            PieceCount INTEGER NOT NULL,
            Price DECIMAL NOT NULL
        );
        CREATE TABLE IF NOT EXISTS Orders (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Status TEXT NOT NULL, -- Changed to TEXT to store enum string values
            RegistrationDate DATETIME NOT NULL,
            Client TEXT NOT NULL,
            CompletionDate DATETIME NULL
        );
        CREATE TABLE IF NOT EXISTS OrderItems (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            OrderId INTEGER NOT NULL,
            Name TEXT NOT NULL,
            Quantity INTEGER NOT NULL,
            Price DECIMAL NOT NULL,
            FOREIGN KEY (OrderId) REFERENCES Orders(Id)
    )";

    // Конструктор, принимающий строку подключения и создающий базу данных
    public SQLitePuzzleRepository(string connectionString)
    {
        _connectionString = connectionString;
        CreateDatabase();
    }

    // Метод для создания базы данных и таблиц
    private void CreateDatabase()
    {
        // Создаем подключение к базе данных
        SQLiteConnection connection = new SQLiteConnection(_connectionString);
        connection.Open();

        // Выполняем SQL-запрос для создания таблиц
        using (SQLiteCommand command = new SQLiteCommand(CreateTableQuery, connection))
        {
            Console.WriteLine($"БД: {_connectionString} создана.");
            command.ExecuteNonQuery();
        }
    }

    // Метод для получения всех записей из таблицы PlywoodSheets
    public List<PlywoodSheet> GetAllPlywoodSheets()
    {
        List<PlywoodSheet> plywoodSheets = new List<PlywoodSheet>();

        // Открываем соединение с базой данных
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM PlywoodSheets";

            // Выполняем SQL-запрос и читаем данные
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Читаем данные построчно и создаем объекты PlywoodSheet
                    while (reader.Read())
                    {
                        PlywoodSheet plywoodSheet = new PlywoodSheet(
                            reader["Title"].ToString(),
                            reader["Material"].ToString(),
                            Convert.ToInt32(reader["Thickness"])
                        );

                        plywoodSheets.Add(plywoodSheet);
                    }
                }
            }
        }
        return plywoodSheets;
    }

    // Метод для получения записи из таблицы PlywoodSheets по названию
    public PlywoodSheet GetPlywoodSheetByTitle(string title)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            // SQL-запрос для поиска записи по названию
            string query = "SELECT * FROM PlywoodSheets WHERE Title = @Title";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", title);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Если запись найдена, возвращаем объект PlywoodSheet
                    if (reader.Read())
                    {
                        return new PlywoodSheet(
                            reader["Title"].ToString(),
                            reader["Material"].ToString(),
                            Convert.ToInt32(reader["Thickness"])
                        );
                    }
                }
            }
        }
        return null; // Если запись не найдена, возвращаем null
    }

    // Метод для добавления новой записи в таблицу PlywoodSheets
    public void AddPlywoodSheet(PlywoodSheet plywoodSheet)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            // SQL-запрос для добавления записи
            string query = "INSERT INTO PlywoodSheets (Title, Material, Thickness) " +
                            "VALUES (@Title, @Material, @Thickness)";

            // Выполняем запрос с параметрами
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", plywoodSheet.Title);
                command.Parameters.AddWithValue("@Material", plywoodSheet.Material);
                command.Parameters.AddWithValue("@Thickness", plywoodSheet.Thickness);
                command.ExecuteNonQuery();
            }
        }
    }

    // Метод для удаления записи из таблицы PlywoodSheets по названию
    public void DeletePlywoodSheet(string title)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            // SQL-запрос для удаления записи
            string query = "DELETE FROM PlywoodSheets WHERE Title = @Title";

            // Выполняем запрос с параметром
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", title);
                command.ExecuteNonQuery();
            }
        }
    }

    // Метод для обновления записи из таблицы PlywoodSheets по названию
    public void UpdatePlywoodSheet(PlywoodSheet plywoodSheet)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            string query = "UPDATE PlywoodSheets SET Title = @Title, Material = @Material, " +
            "Thickness = @Thickness WHERE Title = @Title";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", plywoodSheet.Title);
                command.Parameters.AddWithValue("@Material", plywoodSheet.Material);
                command.Parameters.AddWithValue("@Thickness", plywoodSheet.Thickness);
                command.ExecuteNonQuery();
            }
        }
    }

    // Метод для получения всех записей из таблицы Puzzles
    public List<Puzzle> GetAllPuzzles()
    {
        List<Puzzle> puzzles = new List<Puzzle>();

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Puzzles";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Читаем данные построчно и создаем объекты Puzzle
                    while (reader.Read())
                    {
                        Puzzle puzzle = new Puzzle(
                            reader["Title"].ToString(),
                            reader["SheetType"].ToString(),
                            Convert.ToInt32(reader["PieceCount"]),
                            Convert.ToDecimal(reader["Price"])
                        );

                        puzzles.Add(puzzle);
                    }
                }
            }
        }
        return puzzles;
    }

    // Метод для получения записи из таблицы Puzzles по названию
    public Puzzle GetPuzzleByTitle(string title)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            // SQL-запрос для поиска записи по названию
            string query = "SELECT * FROM Puzzles WHERE Title = @Title";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", title);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Если запись найдена, возвращаем объект Puzzle
                    if (reader.Read())
                    {
                        return new Puzzle(
                            reader["Title"].ToString(),
                            reader["SheetType"].ToString(),
                            Convert.ToInt32(reader["PieceCount"]),
                            Convert.ToDecimal(reader["Price"])
                        );
                    }
                }
            }
        }
        return null; // Если запись не найдена, возвращаем null
    }

    // Метод для добавления новой записи в таблицу Puzzles
    public void AddPuzzle(Puzzle puzzle)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            // SQL-запрос для добавления записи
            string query = "INSERT INTO Puzzles (Title, SheetType, PieceCount, Price) " +
                           "VALUES (@Title, @SheetType, @PieceCount, @Price)";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                // Добавляем параметры в запрос
                command.Parameters.AddWithValue("@Title", puzzle.Title);
                command.Parameters.AddWithValue("@SheetType", puzzle.SheetType);
                command.Parameters.AddWithValue("@PieceCount", puzzle.PieceCount);
                command.Parameters.AddWithValue("@Price", puzzle.Price);
                command.ExecuteNonQuery();
            }
        }
    }

    // Метод для удаления записи из таблицы Puzzles по названию
    public void DeletePuzzle(string title)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            // SQL-запрос для удаления записи
            string query = "DELETE FROM Puzzles WHERE Title = @Title";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", title);
                command.ExecuteNonQuery();
            }
        }
    }
    
    // Метод для обновления записи из таблицы Puzzles по названию
    public void UpdatePuzzle(Puzzle puzzle)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            string query = "UPDATE Puzzles SET Title = @Title, SheetType = @SheetType, " +
            "PieceCount = @PieceCount, Price = @Price WHERE Title = @Title";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", puzzle.Title);
                command.Parameters.AddWithValue("@SheetType", puzzle.SheetType);
                command.Parameters.AddWithValue("@PieceCount", puzzle.PieceCount);
                command.Parameters.AddWithValue("@Price", puzzle.Price);
                command.ExecuteNonQuery();
            }
        }
    }

    // Метод для получения прайс-листа всех пазлов
    public List<Dictionary<string, string>> GetPriceList()
    {
        List<Dictionary<string, string>> priceList = new List<Dictionary<string, string>>();

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT Title, Price FROM Puzzles"; // Выбираем только название и цену

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Читаем данные построчно и добавляем в список
                    while (reader.Read())
                    {
                        string title = reader["Title"].ToString();
                        string price = reader["Price"].ToString();
                        priceList.Add(new Dictionary<string, string>(){
                            {"Title", title},
                            {"Price", price}
                        });
                    }
                }
            }
        }
        return priceList;
    }

     // Методы для работы с заказами

    public List<Order> GetAllOrders()
    {
        List<Order> orders = new List<Order>();

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Orders";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), reader["Status"].ToString()), // Convert string to enum
                            RegistrationDate = DateTime.Parse(reader["RegistrationDate"].ToString()),
                            Client = reader["Client"].ToString(),
                            CompletionDate = reader["CompletionDate"] != DBNull.Value ? DateTime.Parse(reader["CompletionDate"].ToString()) : null,
                            Items = GetOrderItems(Convert.ToInt32(reader["Id"]))
                        };
                        orders.Add(order);
                    }
                }
            }
        }
        return orders;
    }

    public Order GetOrderById(int id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Orders WHERE Id = @Id";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Order order = new Order
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), reader["Status"].ToString()),
                            RegistrationDate = DateTime.Parse(reader["RegistrationDate"].ToString()),
                            Client = reader["Client"].ToString(),
                            CompletionDate = reader["CompletionDate"] != DBNull.Value ? DateTime.Parse(reader["CompletionDate"].ToString()) : null,
                            Items = GetOrderItems(Convert.ToInt32(reader["Id"]))
                        };
                        return order;
                    }
                }
            }
        }
        return null;
    }

    public void AddOrder(Order order)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Orders (Status, RegistrationDate, Client, CompletionDate) " +
                           $"VALUES ('{order.Status.ToString()}', @RegistrationDate, @Client, @CompletionDate); SELECT last_insert_rowid()";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@RegistrationDate", order.RegistrationDate);
                command.Parameters.AddWithValue("@Client", order.Client);
                command.Parameters.AddWithValue("@CompletionDate", order.CompletionDate.HasValue ? order.CompletionDate.Value : DBNull.Value);

                long orderId = (long)command.ExecuteScalar();

                foreach (var item in order.Items)
                {
                    AddOrderItem(connection, (int)orderId, item);
                }
            }
        }
    }

    private void AddOrderItem(SQLiteConnection connection, int orderId, OrderItem item)
    {
        string query = "INSERT INTO OrderItems (OrderId, Name, Quantity, Price) " +
                       "VALUES (@OrderId, @Name, @Quantity, @Price)";

        using (SQLiteCommand command = new SQLiteCommand(query, connection))
        {
            command.Parameters.AddWithValue("@OrderId", orderId);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Quantity", item.Quantity);
            command.Parameters.AddWithValue("@Price", item.Price);
            command.ExecuteNonQuery();
        }
    }

    public void DeleteOrder(int id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            string deleteItemsQuery = "DELETE FROM OrderItems WHERE OrderId = @OrderId";
            using (SQLiteCommand deleteItemsCommand = new SQLiteCommand(deleteItemsQuery, connection))
            {
                deleteItemsCommand.Parameters.AddWithValue("@OrderId", id);
                deleteItemsCommand.ExecuteNonQuery();
            }

            string deleteOrderQuery = "DELETE FROM Orders WHERE Id = @Id";
            using (SQLiteCommand deleteOrderCommand = new SQLiteCommand(deleteOrderQuery, connection))
            {
                deleteOrderCommand.Parameters.AddWithValue("@Id", id);
                deleteOrderCommand.ExecuteNonQuery();
            }
        }
    }

    public void UpdateOrder(Order order)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            string query = "UPDATE Orders SET Status = @Status, RegistrationDate = @RegistrationDate, " +
                           "Client = @Client, CompletionDate = @CompletionDate WHERE Id = @Id";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", order.Id);
                command.Parameters.AddWithValue("@Status", order.Status.ToString());
                command.Parameters.AddWithValue("@RegistrationDate", order.RegistrationDate);
                command.Parameters.AddWithValue("@Client", order.Client);
                command.Parameters.AddWithValue("@CompletionDate", order.CompletionDate.HasValue ? order.CompletionDate.Value : DBNull.Value);
                command.ExecuteNonQuery();
            }

            string deleteItemsQuery = "DELETE FROM OrderItems WHERE OrderId = @OrderId";
            using (SQLiteCommand deleteItemsCommand = new SQLiteCommand(deleteItemsQuery, connection))
            {
                deleteItemsCommand.Parameters.AddWithValue("@OrderId", order.Id);
                deleteItemsCommand.ExecuteNonQuery();
            }

            foreach (var item in order.Items)
            {
                AddOrderItem(connection, order.Id, item);
            }
        }
    }

    private List<OrderItem> GetOrderItems(int orderId)
    {
        List<OrderItem> orderItems = new List<OrderItem>();

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM OrderItems WHERE OrderId = @OrderId";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderId", orderId);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OrderItem item = new OrderItem
                        {
                            Name = reader["Name"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            Price = Convert.ToDecimal(reader["Price"])
                        };
                        orderItems.Add(item);
                    }
                }
            }
        }
        return orderItems;
    }
}