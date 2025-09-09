var builder = WebApplication.CreateBuilder(args); //WebApplication is the class

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //isDevelopment checks if its development or production
{
    app.MapOpenApi();   
}

app.UseHttpsRedirection();

var summaries = new[]//btw string = data type, String = class
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
}; //array of strings

/*http requests:
- Get - Read
- Post - Create
- Put - Update/Replace
- Patch - Update(partial)
- Delete - Delete
- Head - Get request for without body (header/meta data)
- Options - 


what the requests look like:

HTTP Get v1.1
localhost:5001/weatherforcast
headers
{
    "Accept": "application/json"
    "Response-Type: "application/json"
}

header          POST request
body


*/
app.MapGet("/weatherforecast", () => //end point, accepts requests, "GET" request
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55), //shared = makes all instance of variable share thread, making variable actually "random"
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/", () => "Hello World!"); 

app.MapGet("/number", () => 
{
    return 10;
}); 

//app.MapGet("/number2", newval()); 

app.MapGet("/add/{a}/{b}", (int a, int b) =>
{
    return new
    {
        operation = "add",
        inputa = a,
        inputb = b,
        sum = a + b
    };
});


app.Run();//tells it to run and await its purpose, comes from web application class at the top

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)//String? = nullable
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

