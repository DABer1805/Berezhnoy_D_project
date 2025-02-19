namespace Server;

using System.Data.SQLite;
using System.Collections.Generic;


public class SQLitePuzzleRepository : IPuzzleRepository
{

    private string _connectionString;
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
        )";

    public SQLitePuzzleRepository(string connectionString)
    {
        _connectionString = connectionString;
        CreateDatabase();
    }

    private void CreateDatabase()
    {
        SQLiteConnection connection = new SQLiteConnection(_connectionString);
        connection.Open();
        using(SQLiteCommand command = new SQLiteCommand(CreateTableQuery, connection))
        {    
            Console.WriteLine($"БД: {_connectionString} создана.");
            command.ExecuteNonQuery();
        }
    }
 
    public List<PlywoodSheet> GetAllPlywoodSheets()
    {
        List<PlywoodSheet> plywoodSheets = new List<PlywoodSheet>();
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM PlywoodSheets";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PlywoodSheet plywoodSheet = new PlywoodSheet(reader["Title"].ToString(),
                        reader["Material"].ToString(), Convert.ToInt32(reader["Thickness"]));

                        plywoodSheets.Add(plywoodSheet);

                    }
                } 
            }
        }
        return plywoodSheets;
    }

    public void AddPlywoodSheet(PlywoodSheet plywoodSheet)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            string query = "INSERT INTO PlywoodSheets (Title, Material, Thickness) " +
            "VALUES (@Title, @Material, @Thickness)";
            
            using(SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", plywoodSheet.Title);
                command.Parameters.AddWithValue("@Material", plywoodSheet.Material);
                command.Parameters.AddWithValue("@Thickness", plywoodSheet.Thickness);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeletePlywoodSheet(string title)
    {
        using(SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM PlywoodSheets WHERE Title = @Title";
            
            using(SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", title);
                command.ExecuteNonQuery();
            }
        }
    }

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
                    while (reader.Read())
                    {
                        Puzzle puzzle = new Puzzle(reader["Title"].ToString(), reader["SheetType"].ToString(),
                            Convert.ToInt32(reader["PieceCount"]), Convert.ToDecimal(reader["Price"]));

                        puzzles.Add(puzzle);
                    }
                }
            }
        }
        return puzzles;
    }

    public Puzzle GetPuzzleByTitle(string title)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Puzzles WHERE Title = @Title";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", title);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Puzzle(reader["Title"].ToString(), reader["SheetType"].ToString(),
                        Convert.ToInt32(reader["PieceCount"]),Convert.ToDecimal(reader["Price"]));
                    }
                }
            }
        }
        return null;
    }

    public void AddPuzzle(Puzzle puzzle)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Puzzles (Title, SheetType, PieceCount, Price) " +
            "VALUES (@Title, @SheetType, @PieceCount, @Price)";

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

    public void DeletePuzzle(string title)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Puzzles WHERE Title = @Title";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", title);
                command.ExecuteNonQuery();
            }
        }
    }
}