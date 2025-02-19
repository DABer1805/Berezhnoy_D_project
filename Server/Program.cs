using Server;
using System.Data.SQLite; // Добавляем пространство имен для работы с SQLite

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер.
builder.Services.AddControllers();

// Добавляем поддержку Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем IPuzzleRepository
builder.Services.AddSingleton<IPuzzleRepository>(provider =>
{
    // Создаем базу данных и передаем путь к ней
    string connectPath = "Data Source= DataBase.db"; 
    // Создаем экземпляр репозитория и передаем путь к базе данных SQLite
    IPuzzleRepository puzzleRepository = new SQLitePuzzleRepository(connectPath);
    return puzzleRepository; // Путь к файлу базы данных SQLite
});

var app = builder.Build();

// Настраиваем конвейер обработки HTTP-запросов.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();