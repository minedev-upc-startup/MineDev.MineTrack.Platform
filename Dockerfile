FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /app
COPY MineDev.MineTrack.Platform/*.csproj MineDev.MineTrack.Platform/
RUN dotnet restore ./MineDev.MineTrack.Platform
COPY . .
RUN dotnet publish ./MineDev.MineTrack.Platform -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "MineDev.MineTrack.Platform.dll"]
