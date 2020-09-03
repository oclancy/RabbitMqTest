#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-lib
WORKDIR /src
COPY ["ExampleProducer/ExampleProducer.csproj", "ExampleProducer/"]
RUN dotnet restore "ExampleProducer/ExampleProducer.csproj"
COPY . .
WORKDIR "/src/ExampleProducer"
RUN dotnet build "ExampleProducer.csproj" -c Release -o /app/build

FROM build-lib AS publish-lib
RUN dotnet publish "ExampleProducer.csproj" -c Release -o /app/publish

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
COPY --from=publish-lib /app/publish .
COPY --from=publish-exe /app/publish .
ENTRYPOINT ["dotnet", "RabbitService.dll"]

