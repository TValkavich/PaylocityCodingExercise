using Microsoft.OpenApi.Models;
using PaylocityCodingExercise.DataAccess.Repositories;
using PaylocityCodingExercise.Model.Models;

var builder = WebApplication.CreateBuilder(args);

//Create CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        string subDomain = "http://localhost:";

        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
        //Bad CORS security but I want to make sure you it is runnable on all localhost configurations.
        // React's default port number is 3000 which should be okay but I'm not sure what your config environment looks like
        builder.AllowAnyOrigin();
        //builder.WithOrigins("http://localhost:3000");
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions => {
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Paylocity Coding Exercise API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swaggerUIOptions => {
        swaggerUIOptions.DocumentTitle = "Paylocity Coding Exercise API";
        swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Web Api");
        swaggerUIOptions.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

//Apply CORS Policy
app.UseCors("CORSPolicy");

var empRepo = new EmployeeRepository();
var depRepo = new DependentRepository();

// -- Employee Requests
app.MapGet("/get-all-employees", async () =>
{
    return await empRepo.GetEmployeesAsync();
});

app.MapGet("/get-employee-by-id/{employeeId}", async (int employeeId) =>
{
    Employee employeeToReturn = await empRepo.GetEmployeeByIdAsync(employeeId);

    if(employeeToReturn != null)
    {
        return Results.Ok(employeeToReturn);
    }
    else
    {
        //Log the Error - We will console log for time
        Console.WriteLine("Get Employee by id Failed");
        return Results.BadRequest();
    }
});

app.MapPost("/create-employee", async (Employee emp) =>
{
    Employee employeeCreated = await empRepo.CreateEmployeeAsync(emp);

    if (employeeCreated != null)
    {
        return Results.Ok(employeeCreated);
    }
    else
    {
        //Log the Error - We will console log for time
        Console.WriteLine("Create Employee Failed");
        return Results.BadRequest();
    }
});

app.MapPut("/update-employee", async (Employee emp) =>
{
    bool employeeUpdated = await empRepo.UpdateEmployeeAsync(emp);

    if (employeeUpdated)
    {
        return Results.Ok("Employee Successfully Updated");
    }
    else
    {
        //Log the Error - We will console log for time
        Console.WriteLine("Update Employee Failed");
        return Results.BadRequest();
    }
});

app.MapDelete("/delete-employee-by-id/{employeeId}", async (int employeeId) =>
{
    bool employeeDeleted = await empRepo.DeleteEmployeeByIdAsync(employeeId);

    if (employeeDeleted) 
    {
        return Results.Ok("Employee Successfully Deleted");
    }
    else
    {
        //Log the Error - We will console log for time
        Console.WriteLine("Delete Employee Failed");
        return Results.BadRequest();
    }
});

// -- Dependent Requests
app.MapGet("/get-all-dependents", async () =>
{
    return await depRepo.GetAllDependentsAsync();
});

app.MapGet("/get-dependents-by-employee-id/{employeeId}", async (int employeeId) =>
{
    return await depRepo.GetDependentsByEmployeeIdAsync(employeeId);
});

app.MapGet("/get-dependents-by-id/{dependentId}", async (int dependentId) =>
{
    Dependent dependentToReturn = await depRepo.GetDependentByIdAsync(dependentId);

    if (dependentToReturn != null)
    {
        return Results.Ok(dependentToReturn);
    }
    else
    {
        //Log the Error - We will console log for time
        Console.WriteLine("Get Dependent by id Failed");
        return Results.BadRequest();
    }
});

app.MapPost("/create-dependent", async (Dependent dep) =>
{
    bool dependentCreated = await depRepo.CreateDependentAsync(dep);

    if (dependentCreated)
    {
        return Results.Ok("Dependent Successfully Created");
    }
    else
    {
        //Log the Error - We will console log for time
        Console.WriteLine("Create Dependent Failed");
        return Results.BadRequest();
    }
});

app.MapPut("/update-dependent", async (Dependent dep) =>
{
    bool dependentUpdated = await depRepo.UpdateDependentAsync(dep);

    if (dependentUpdated)
    {
        return Results.Ok("Dependent Successfully Updated");
    }
    else
    {
        //Log the Error - We will console log for time
        Console.WriteLine("Update Dependent Failed");
        return Results.BadRequest();
    }
});

app.MapDelete("/delete-dependent-by-id/{dependentId}", async (int dependentId) =>
{
    bool dependentDeleted = await depRepo.DeleteDependenetByIdAsync(dependentId);

    if (dependentDeleted)
    {
        return Results.Ok("Dependent Successfully Deleted");
    }
    else
    {
        //Log the Error - We will console log for time
        Console.WriteLine("Delete Dependent Failed");
        return Results.BadRequest();
    }
});


app.Run();

