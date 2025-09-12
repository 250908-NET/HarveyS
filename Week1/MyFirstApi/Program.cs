using Serilog;
using System.Text.RegularExpressions;



var builder = WebApplication.CreateBuilder(args); //WebApplication is the class

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger(); //read config from app settings
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //isDevelopment checks if its development or production
{
    app.MapOpenApi(); 
    app.UseSwagger();
    app.UseSwaggerUI();  
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

app.MapGet("/password/simple/{length}", (int length) => 
{
    string password = "";
    string abc123 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    for(int i = 0; i < length; i++) {
        password += abc123[Random.Shared.Next(abc123.Length)];
    }

    return new
    {
        operation = "Simple Password",
        Length = length,
        Password = password
    };

}); 

app.MapGet("/password/complex/{length}", (int length) => 
{
    string password = "";
    string abc123 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+=-";
    for(int i = 0; i < length; i++) {
        password += abc123[Random.Shared.Next(abc123.Length)];
    }
    return new
    {
        operation = "Complex Password",
        Length = length,
        Password = password
    };
}); 

app.MapGet("/password/memorable/{words}", (int words) => 
{
    string memorable = "";
    var wordList = new List<string> { "red", "orange", "yellow", "green", "blue", "purple", "insure", "person", "element", "inspector", "arise", "stake", "land", "ridge", "code", "publish", "unanimous", "explicit", "ally", "judge", "beard", "legend", "age", "suppress", "spontaneous" };
    for(int i = 0; i < words; i++) {
        memorable += wordList[Random.Shared.Next(wordList.Count)];
    }
    return new
    {
        operation = "Simple Password",
        Length = words,
        Password = memorable
    };
}); 

app.MapGet("/password/strength/{a}", (string a) => 
{
    string abcd = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    int strength = 0;
    string letters = "";
    string symbols = "";
    for(int i = 0; i < a.Length; i++) {
        if(!abcd.Contains(a[i])) {
            symbols += a[i];
            strength += 4;
        } else if(!letters.Contains(a[i])) {
            letters += a[i];
            strength += 3;
        }
    }
    return new
    {
        operation = "Password Strength",
        Password = a,
        Strength = a.Length + strength
    };
    
}); 

// ---- CHALLENGE 8 ---- 

app.MapGet("/validate/email/{email}", (string email) => 
{
    //email with just 1 @ somewhere not at the ends of the email
    string reg = @"^[^@]+@[^@]+\.[^@]+$";
    return new
    {
        operation = "Validating email",
        email = email,
        Valid = Regex.IsMatch(email, reg)
    };
});     

app.MapGet("/validate/phone/{phone}", (string phone) => 
{
    //a phone number in format of (xxx)xxxxxxx, xxx-xxx-xxxx, xxxxxxxxxx, with +xx at the front possible
    string reg = @"^(\+\d{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";
    return new
    {
        Operation = "Validating number",
        Number = phone,
        Valid = Regex.IsMatch(phone, reg)
    };
}); 

app.MapGet("/validate/creditcard/{number}", (long number, ILogger<Program> logger) => 
{
    long num = number;
    long sum = 0;
    long temp = 0;
    long count = 1;
    bool valid = false;
    while(number > 0)
    {   
        if(count % 2 == 0) {
            temp = (number % 10) * 2;
            if(temp < 10) {
                sum += temp;
            } else {
                temp = (temp) % 10;
                sum += temp + 1;
            }
        } else {
            sum += number % 10;
        }
        number = number / 10;
        count++;
    }
    if(sum % 10 == 0) {
        valid = true;
    }
    return new
    {
        Operation = "Validating credit card",
        Card = num,
        Valid = valid
    };
}); 

app.MapGet("/validate/strongpassword/{password}", (string password) => 
{
    //10 digit password that requires an upercase, lowercase, special character, and a number
    string reg = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{10,}$";
    return new
    {
        Operation = "Validating password",
        password = password,
        Valid = Regex.IsMatch(password, reg)
    };
});

// ---- CHALLENGE 9 ---- 


app.MapGet("/convert/length/{value}/{fromUnit}/{toUnit}", (double value, string fromUnit, string toUnit) => 
{
    double answer = value;
    bool broke = false;
    switch(fromUnit)
    {
        case "meters":
            if(toUnit == "feet") {
                answer = value * 3.28;
                break;
            } else if(toUnit == "inches") {
                answer = value * 39.37;
                break;
            }
            broke = true;
            break;
        case "feet":
            if(toUnit == "meters") {
                answer = value / 3.28;
                break;
            } else if(toUnit == "inches") {
                answer = value * 12;
                break;
            }
            broke = true;
            break;
        case "inches":
            if(toUnit == "feet") {
                answer = value / 12;
                break;
            } else if(toUnit == "meters") {
                answer = value / 39.37;
                break;
            }
            broke = true;
            break;
        default:
            broke = true;
            break;
    }
    if(broke == true && fromUnit != toUnit) {
        return Results.BadRequest(new { error = "Please correctly enter length units in lowercase" });
    }
    return Results.Ok(new {
        Operation = "Length conversion",
        From = fromUnit,
        To = toUnit,
        Units = answer
    });
}); 
app.MapGet("/convert/weight/{value}/{fromUnit}/{toUnit}", (double value, string fromUnit, string toUnit) => 
{
    //kg, lbs, ounces)
    double answer = value;
    bool broke = false;
    switch(fromUnit)
    {
        case "kg":
            if(toUnit == "lbs") {
                answer = value * 2.2;
                break;
            } else if(toUnit == "ounces") {
                answer = value * 35.27;
                break;
            }
            broke = true;
            break;
        case "lbs":
            if(toUnit == "kg") {
                answer = value / 2.2;
                break;
            } else if(toUnit == "ounces") {
                answer = value * 16;
                break;
            }
            broke = true;
            break;
        case "ounces":
            if(toUnit == "lbs") {
                answer = value / 16;
                break;
            } else if(toUnit == "kg") {
                answer = value / 35.27;
                break;
            }
            broke = true;
            break;
        default:
            broke = true;
            break;
    }
    if(broke == true && fromUnit != toUnit) {
        return Results.BadRequest(new { error = "Please correctly enter weight units in lowercase" });
    }
    return Results.Ok(new {
        Operation = "Weight conversion",
        From = fromUnit,
        To = toUnit,
        Units = answer
    });
}); 
app.MapGet("/convert/volume/{value}/{fromUnit}/{toUnit}", (double value, string fromUnit, string toUnit) => 
{
    //liters, gallons, cups
    double answer = value;
    bool broke = false;
    switch(fromUnit)
    {
        case "liters":
            if(toUnit == "gallons") {
                answer = value / 4.55;
                break;
            } else if(toUnit == "cups") {
                answer = value * 3.52;
                break;
            }
            broke = true;
            break;
        case "gallons":
            if(toUnit == "liters") {
                answer = value * 4.55;
                break;
            } else if(toUnit == "cups") {
                answer = value * 16;
                break;
            }
            broke = true;
            break;
        case "cups":
            if(toUnit == "gallons") {
                answer = value / 16;
                break;
            } else if(toUnit == "liters") {
                answer = value / 3.52;
                break;
            }
            broke = true;
            break;
        default:
            broke = true;
            break;
    }
    if(broke == true && fromUnit != toUnit) {
        return Results.BadRequest(new { error = "Please correctly enter volume units in lowercase" });
    }
    return Results.Ok(new {
        Operation = "Length conversion",
        From = fromUnit,
        To = toUnit,
        Units = answer
    });

}); 
app.MapGet("/convert/list-units/{type}", (string type) => 
{
    string ans;
    string length = "Meters, Feet, Inches";
    string weight = "Kgs, Lbs, Ounces";
    string volume = "Liters, Gallons, Cups";
    if(type ==  "length") {
        ans = length;
    } else if (type == "weight") {
        ans = weight;
    } else if (type == "volume") {
        ans = volume;
    } else {
        return Results.BadRequest(new { error = "Please enter length, weight, or volume" });
    }
    return Results.Ok(new {
        Operation = "List units",
        Type = type,
        Units = ans
    });

}); 
/*
// ---- CHALLENGE 10 ----

app.MapGet("/", () => 
{
    "Hello World!"

});  

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

