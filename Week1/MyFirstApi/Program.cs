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

// ----     CHALLENGE 1 ----
app.MapGet("/calculator/add/{a}/{b}", (int a, int b) =>
{
    return new
    {
        operation = "add",
        inputa = a,
        inputb = b,
        result = a + b
    };
});

app.MapGet("/calculator/subtract/{a}/{b}", (int a, int b) =>
{
    return new
    {
        operation = "subtract",
        inputa = a,
        inputb = b,
        result = a - b
    };
});

app.MapGet("/calculator/multiply/{a}/{b}", (int a, int b) =>
{
    return new
    {
        operation = "multiply",
        inputa = a,
        inputb = b,
        result = a * b
    };
});

app.MapGet("/calculator/divide/{a}/{b}", (int a, int b) =>
{
    if (b == 0)
        return Results.BadRequest(new { error = "Cannot divide by zero" });
    else {
    var result = a / b;
    return Results.Ok(new { operation = "divide", input1 = a, input2 = b, result = result });
    }

});

// ---- CHALLENGE 2 ---- 

app.MapGet("/text/reverse/{a}", (string a) =>
{
    return new
    {
        operation = "Reverse",
        text = a,
        reversed = new string(a.Reverse().ToArray())
    };
});

app.MapGet("/text/uppercase/{a}", (string a) =>
{
    return new
    {
        operation = "Uppercase",
        text = a,
        uppercased = a.ToUpper()
    };
});

app.MapGet("/text/lowercase/{a}", (string a) =>
{
    return new
    {
        operation = "Lowercase",
        text = a,
        lowercased = a.ToLower()
    };
});

app.MapGet("/text/count/{a}", (string a) =>
{
    var vowels = "aeiouAEIOU";
    var space = " ";
    return new
    {
        operation = "Count",
        text = a,
        count = a.Length,
        vowelCount = a.Count(x => vowels.Contains(x)),
        wordCount = a.Count(x => space.Contains(x)) + 1
    };
});

app.MapGet("/text/palindrome/{a}", (string a) =>
{
    var emordnilap = new string(a.Reverse().ToArray());

    if(a == emordnilap.ToLower()) {
        return Results.Ok(new { operation = "Palindrome", text = a, result = true });
    } else {
        return Results.Ok(new { operation = "Palindrome", text = a, result = false });
    }
});

// ---- CHALLENGE 3 ---- 

app.MapGet("/numbers/fizzbuzz/{a}", (int a) =>
{
    string ans = "";
    for(int i = 1; i <= a; i++) {
        bool fizz = i % 3 == 0;
        bool buzz = i % 5 == 0;
        if(fizz && buzz)
            ans += "fizzbuzz ";
        else if(fizz)
            ans += "fizz ";
        else if(buzz)
            ans += "buzz ";
        else 
            ans += i + " ";
    }
    return Results.Ok(new { operation = "Fizzbuzz", count = a, result = ans});
}); 

app.MapGet("/numbers/prime/{a}", (int a) =>
{
    int test = 0;
    for(int i = 2; i < a; i++) {
        if(a % i == 0 || a == 2)
            return Results.Ok(new { operation = "Prime", number = a, result = false});
        test = i;
    }
    return Results.Ok(new { operation = "Prime", number = a, result = true});
});

app.MapGet("/numbers/fibonacci/{a}", (int a) =>
{
    string fib = "0";
    int before = 0;
    int now = 1;
    for(int i = 1; i < a; i++) {
        int next = before + now;
        fib += " " + now;
        before = now;
        now = next;
    }
    return Results.Ok(new { operation = "Fibonacci", number = a, result = fib});
});

app.MapGet("/numbers/factor/{a}", (int a) =>
{
    string factors = "";
    for(int i = 1; i < a; i++) {
        if(a % i == 0) {
            factors += " " + i;
        }
    }
    return Results.Ok(new { operation = "Factors", number = a, result = factors});
}); 


// ---- CHALLENGE 4 ---- 

app.MapGet("/date/today", () => 
{
    return new
    {
        operation = "Todays Date",
        date = DateTime.Today.ToShortDateString()
    };

}); 

app.MapGet("/date/age/{x}", (int x) => 
{
    return new
    {
        operation = "Age",
        age = DateTime.Now.Year - x 
    };
}); 

app.MapGet("/date/daysbetween/{x}/{y}", (string x, string y) => 
{
    return new
    {
        operation = "Days Between",
        days = DateTime.Parse(x) - DateTime.Parse(y)
    };
}); 

app.MapGet("/date/weekday/{x}", (string x) =>
{
    return new
    {
        operation = "Weekday",
        day = DateTime.Parse(x).DayOfWeek.ToString()
    };
});

// ---- CHALLENGE 5 ---- 

var colorList = new List<string> { "red", "orange", "yellow", "green", "blue", "purple" };
app.MapGet("/colors", () => 
{
    return new
    {
        operation = "Color List",
        colors = colorList
    };

}); 

app.MapGet("/colors/random", () => 
{
    var index = Random.Shared.Next(colorList.Count);
    var rdmColor = colorList[index];

    return new
    {
        operation = "Random Color",
        randomColor = rdmColor
    };
}); 

app.MapGet("/colors/search/{a}", (char a) => 
{
    var resultList = new List<string> {};
    foreach(string i in colorList) {
        if(a == i[0]) {
            resultList.Add(i);
        }
    }
    return new
    {
        operation = "Search Color",
        startingLetter = a,
        result = resultList
    };
}); 

app.MapGet("/colors/add/{a}", (string a) => 
{
    colorList.Add(a);
    return new
    {
        operation = "Add Color",
        newColor = a,
        newColorList = colorList
    };
}); 

// ---- CHALLENGE 6 ---- 

app.MapGet("/temp/celsius-to-fahrenheit/{a}", (double a) => 
{
    return new
    {
        operation = "C to F",
        Celsius = a,
        Fahrenheit = (a * 9/5) + 32
    };

}); 

app.MapGet("/temp/fahrenheit-to-celsius/{a}", (double a) => 
{
    return new
    {
        operation = "F to C",
        Fahrenheit = a,
        Celsius = (a - 32) * 5/9
    };
}); 

app.MapGet("/temp/kelvin-to-celsius/{a}", (double a) => 
{
    return new
    {
        operation = "K to C",
        Kelvin = a,
        Celsius = a - 273.15
    };
}); 

app.MapGet("/temp/compare/{a}/{x}/{b}/{y}", (double a, char x, double b, char y) => 
{   
    if(x == y) {
        if(a == b) {

        } else if (a > b) {

        } else if (a < b) {

        }
    }
    return new
    {
        operation = "Compare",
        Temp1 = a + " of " + x,
        Temp2 = b + " of " + y,
        Result = "Temperature "
    };
}); 

// ---- CHALLENGE 7 ---- 

app.MapGet("/password/simple/{length}", () => 
{

}); 

app.MapGet("/password/complex/{length}", () => 
{
    
}); 

app.MapGet("/password/memorable/{words}", () => 
{
    
}); 

app.MapGet("/password/strength/{password}", (string a) => 
{
    int strength = 0;
    string builder = "";
    for(int i = 0; i < a.length; i++) {
        if(builder.Contains(current)) {

        }
    }
}); 
// ---- CHALLENGE 8 ---- 

app.MapGet("/", () => 
{
    "Hello World!"

}); 

// ---- CHALLENGE 9 ---- 

app.MapGet("/", () => 
{
    "Hello World!"

}); 

// ---- CHALLENGE 10 ----

app.MapGet("/", () => 
{
    "Hello World!"

});  
/*
// ---- CHALLENGE 11 ----  

app.MapGet("/", () => 
{
    "Hello World!"

}); 
*/
app.Run();//tells it to run and await its purpose, comes from web application class at the top

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)//String? = nullable
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

