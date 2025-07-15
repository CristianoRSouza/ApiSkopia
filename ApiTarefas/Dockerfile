
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ApiTarefas/ApiTarefas.csproj", "ApiTarefas/"]
RUN dotnet restore "ApiTarefas/ApiTarefas.csproj"

COPY ApiTarefas/appsettings*.json ApiTarefas/
COPY . .

WORKDIR "/src/ApiTarefas"
RUN dotnet publish "ApiTarefas.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

ENTRYPOINT ["dotnet", "ApiTarefas.dll"]
