var builder = WebApplication.CreateBuilder(args);

// Agregar Swagger al servicio
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenCorsPolicy",
        builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        /*.AllowCredentials() fail with Swagger ...*/
                        );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PedidosApi V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseAuthorization();
app.UseCors("OpenCorsPolicy");
app.MapControllers();

app.Run();
