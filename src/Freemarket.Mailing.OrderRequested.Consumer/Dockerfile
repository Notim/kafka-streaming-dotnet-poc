FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Freemarket.Mailing.OrderRequested.Consumer/Freemarket.Mailing.OrderRequested.Consumer.csproj", "src/Freemarket.Mailing.OrderRequested.Consumer/"]
COPY ["src/Freemarket.Ordering.Application/Freemarket.Ordering.Application.csproj", "src/Freemarket.Ordering.Application/"]
COPY ["src/Freemarket.Ordering.Infrastructure.Kafka/Freemarket.Ordering.Infrastructure.Kafka.csproj", "src/Freemarket.Ordering.Infrastructure.Kafka/"]
RUN dotnet restore "src/Freemarket.Mailing.OrderRequested.Consumer/Freemarket.Mailing.OrderRequested.Consumer.csproj"
COPY . .
WORKDIR "/src/src/Freemarket.Mailing.OrderRequested.Consumer"
RUN dotnet build "Freemarket.Mailing.OrderRequested.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Freemarket.Mailing.OrderRequested.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Freemarket.Mailing.OrderRequested.Consumer.dll"]
