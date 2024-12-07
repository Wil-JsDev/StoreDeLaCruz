FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY StoreDeLaCruz.sln ./
COPY StoreDeLaCruz/StoreDeLaCruz.csproj ./StoreDeLaCruz/
COPY StoreDeLaCruz.Core.Domain/StoreDeLaCruz.Core.Domain.csproj  ./StoreDeLaCruz.Core.Domain/
COPY StoreDeLaCruz.Core.Aplication/StoreDeLaCruz.Core.Aplication.csproj ./StoreDeLaCruz.Core.Aplication/ 
COPY StoreDeLaCruz.Infraestructura.Identity/StoreDeLaCruz.Infraestructura.Identity.csproj ./StoreDeLaCruz.Infraestructura.Identity/
COPY  StoreDeLaCruz.Infraestructura.Persistencia/StoreDeLaCruz.Infraestructura.Persistencia.csproj ./StoreDeLaCruz.Infraestructura.Persistencia/
COPY StoreDeLaCruz.Insfraestructura.Shared/StoreDeLaCruz.Insfraestructura.Shared.csproj ./StoreDeLaCruz.Insfraestructura.Shared/
RUN dotnet restore
COPY . ./
WORKDIR /app/StoreDeLaCruz
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
CMD [ "dotnet", "StoreDeLaCruz.dll" ]


