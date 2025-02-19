using Server;
using System.Data.SQLite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPuzzleRepository>(provider =>
{
    string connectPath = "Data Source= DataBase.db"; 
    IPuzzleRepository puzzleRepository = new SQLitePuzzleRepository(connectPath);
    return puzzleRepository;
});

var app = builder.Build();

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