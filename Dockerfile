FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY NuGet.Config .
COPY Directory.Build.props .
COPY Veltis.Workspace.sln .
COPY src/Veltis.Workspace.Domain/Veltis.Workspace.Domain.csproj src/Veltis.Workspace.Domain/
COPY src/Veltis.Workspace.Application/Veltis.Workspace.Application.csproj src/Veltis.Workspace.Application/
COPY src/Veltis.Workspace.Infrastructure/Veltis.Workspace.Infrastructure.csproj src/Veltis.Workspace.Infrastructure/
COPY src/Veltis.Workspace.Web/Veltis.Workspace.Web.csproj src/Veltis.Workspace.Web/
RUN dotnet restore src/Veltis.Workspace.Web/Veltis.Workspace.Web.csproj --configfile NuGet.Config
COPY . .
RUN dotnet publish src/Veltis.Workspace.Web/Veltis.Workspace.Web.csproj -c Release -o /app/publish --no-restore

FROM runtime AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Veltis.Workspace.Web.dll"]
