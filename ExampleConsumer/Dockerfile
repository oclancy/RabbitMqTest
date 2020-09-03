#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ExampleConsumer/ExampleConsumer.csproj", "ExampleConsumer/"]
RUN dotnet restore "ExampleConsumer/ExampleConsumer.csproj"
COPY . .
WORKDIR "/src/ExampleConsumer"
RUN dotnet build "ExampleConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExampleConsumer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-exe
WORKDIR /src
COPY ["../RabbitService/RabbitService.csproj", "RabbitService/"]
RUN dotnet restore "RabbitService/RabbitService.csproj"
COPY . .
WORKDIR "/src/RabbitService"
RUN dotnet build "RabbitService.csproj" -c Release -o /app/build

FROM build-exe AS publish-exe
RUN dotnet publish "RabbitService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish-exe /app/publish .
ENTRYPOINT ["dotnet", "RabbitService.dll"]