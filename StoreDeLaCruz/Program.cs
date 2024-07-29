using StoreDeLaCruz.Infraestructura.Persistencia;
using StoreDeLaCruz.Core.Aplication;
using StoreDeLaCruz.Extensions;
using StoreDeLaCruz.Core.Aplication.Mapping;
using FluentValidation;
using StoreDeLaCruz.Core.Aplication.DTOs.Folder;
using StoreDeLaCruz.Validation;
using StoreDeLaCruz.Core.Aplication.DTOs.Nota;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwagerExtension();
//builder.Services.AddVersioning();

builder.Services.AddInfraestructura(configuration);
builder.Services.AddApplication();
builder.Services.AddAutoMapper(typeof(MappingProfile));
//Validaciones
builder.Services.AddScoped<IValidator<FolderInsertDTos>, ValidationFolderDTos>();
builder.Services.AddScoped<IValidator<FolderUpdate>, ValidationFolderUpdate>();
builder.Services.AddScoped<IValidator<NotaInsertDTos>, ValidationNoteInsert>();
builder.Services.AddScoped<IValidator<NotaUpdateDTos>, ValidationNotaUpdate>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

//app.UseSwaggerExtension();

app.MapControllers();

app.Run();
