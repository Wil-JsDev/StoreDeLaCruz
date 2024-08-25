using StoreDeLaCruz.Infraestructura.Persistencia;
using StoreDeLaCruz.Insfraestructura.Shared;
using StoreDeLaCruz.Core.Aplication;
using StoreDeLaCruz.Infraestructura.Identity;
using StoreDeLaCruz.Extensions;
using StoreDeLaCruz.Core.Aplication.Mapping;
using FluentValidation;
using StoreDeLaCruz.Core.Aplication.DTOs.Folder;
using StoreDeLaCruz.Validation;
using StoreDeLaCruz.Core.Aplication.DTOs.Nota;
using StoreDeLaCruz.Middlewares;
using Microsoft.AspNetCore.Identity;
using StoreDeLaCruz.Infraestructura.Identity.Entities;
using StoreDeLaCruz.Infraestructura.Identity.Seeds;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwagerExtension();
builder.Services.AddVersioning();

builder.Services.AddInfraestructura(configuration);
builder.Services.AddSharedInfraestura(configuration);
builder.Services.AddApplication();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddIdentity(configuration);
//Validaciones
builder.Services.AddScoped<IValidator<FolderInsertDTos>, ValidationFolderDTos>();
builder.Services.AddScoped<IValidator<FolderUpdate>, ValidationFolderUpdate>();
builder.Services.AddScoped<IValidator<NotaInsertDTos>, ValidationNoteInsert>();
builder.Services.AddScoped<IValidator<NotaUpdateDTos>, ValidationNotaUpdate>();

var app = builder.Build();

//Configuracion para crear los roles por default y son los que configure
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
	{
		var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var rolManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, rolManager);
        await DefaultSuperAdmin.SeedAsync(userManager, rolManager);
        await DefaultBasicUser.SeedAsync(userManager, rolManager);

    }
	catch (Exception ex)
	{
	}
}


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerExtension();
//app.UseMiddleware<CustomMiddleware>(); //Asi se agrega un Middleware

app.MapControllers();

app.Run();
