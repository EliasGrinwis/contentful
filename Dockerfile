FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY Project.csproj .

RUN dotnet restore "Project.csproj"

COPY . .

RUN dotnet publish "Project.csproj" -c release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as final
WORKDIR /app
COPY --from=build /publish .

ENTRYPOINT [ "dotnet", "Project.dll" ]
