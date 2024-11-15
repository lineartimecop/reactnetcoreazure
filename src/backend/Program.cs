/* ****************************************************************************
 * File name: Program.cs
 *
 * Author: Tamás Kiss
 * Created: Oct/15/2024
 *
 * Last Editor: Tamás Kiss
 * Last Modified: Oct/15/2024
 *
 * Copyright (C) Tamás Kiss, 2024.
 * ************************************************************************* */

var builder = WebApplication.CreateBuilder(args);

var origins = "localhostPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(origins, builder => builder.WithOrigins("https://localhost")
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(origins);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
