var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var type = typeof(Program);
app.MapGet("/", () => "Hello World!");

app.Run();


public partial class Program { }