dotnet new webapi -n <project>

*validate that the database is up and running

*add packages to project


dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

*tool install for dotnet 
dotnet new tool-manifest
dotnet tool install --local dotnet-ef

*scaffold the database
connection string - "Server=localhost,1433;Database=Chinook;User Id=sa;Password=P4s5w0rd123;"
provider - Microsoft.EntityFrameworkCore.SqlServer

dotnet ef dbcontext scaffold "Server=localhost,1433;Database=Chinook;User Id=sa;Password=K8Z10P4L09a;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o ./Models -c ChinookContext

