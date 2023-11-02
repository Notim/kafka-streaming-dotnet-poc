FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Freemarket.Ordering.Requested.Consumer/Freemarket.Ordering.Requested.Consumer.csproj", "src/Freemarket.Ordering.Requested.Consumer/"]
COPY ["src/Freemarket.Ordering.Infrastructure.Kafka/Freemarket.Ordering.Infrastructure.Kafka.csproj", "src/Freemarket.Ordering.Infrastructure.Kafka/"]
COPY ["src/Freemarket.Ordering.Domain/Freemarket.Ordering.Domain.csproj", "src/Freemarket.Ordering.Domain/"]
COPY ["src/Freemarket.Ordering.Application/Freemarket.Ordering.Application.csproj", "src/Freemarket.Ordering.Application/"]
RUN dotnet restore "src/Freemarket.Ordering.Requested.Consumer/Freemarket.Ordering.Requested.Consumer.csproj"
COPY . .
WORKDIR "/src/src/Freemarket.Ordering.Requested.Consumer"
RUN dotnet build "Freemarket.Ordering.Requested.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Freemarket.Ordering.Requested.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Freemarket.Ordering.Requested.Consumer.dll"]
