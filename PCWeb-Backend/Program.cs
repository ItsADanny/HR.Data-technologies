public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
 
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Loading database connection variables into a DBConfig object at program launch
        DBConfig? MySQL_dbConfig = builder.Configuration.GetSection("MySQLDatabase").Get<DBConfig>();
        DBConfig? Redis_dbConfig = builder.Configuration.GetSection("RedisDatabase").Get<DBConfig>();
        //If database connection variables can't be loaded exit program with error
        if (MySQL_dbConfig is null) throw new NullReferenceException("Can't load user-secrets into DBConfig (MySQL)");
        if (Redis_dbConfig is null) throw new NullReferenceException("Can't load user-secrets into DBConfig (Redis)");
        //Check if the HOST and PORT for the DBConfig are filled
        if (MySQL_dbConfig.HST is null || MySQL_dbConfig.PRT is null) throw new NullReferenceException("Can't load user-secrets into DBConfig (MySQL)");
        if (Redis_dbConfig.HST is null || Redis_dbConfig.PRT is null) throw new NullReferenceException("Can't load user-secrets into DBConfig (Redis)");
        //Set the DBConfig into the DBHelper
        DBHandler.DBConfig_MySQL = MySQL_dbConfig;
        DBHandler.DBConfig_REDIS = Redis_dbConfig;

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            //This will generate a OpenAPI yaml document 
            //when the application is run in DEV mode
            app.MapOpenApi("/openapi/{documentName}.yaml");
            
            app.UseSwagger();
            app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    //This will generate a OpenAPI yaml document 
    //when the application is run in DEV mode
    app.MapOpenApi("/openapi/{documentName}.yaml");
    
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}


// app.UseHttpsRedirection();
// app.MapSwagger().RequireAuthorization();
app.MapSwagger();
app.MapControllers();
app.Run();


Console.WriteLine($"Server started, Listening to port: {port}");
//Log.WriteLine($"Server started, Listening to port: {port}");

// Application records
public record registerDto(string firstName, string lastName, string email, string password);
public record loginDto(string email, string password);
