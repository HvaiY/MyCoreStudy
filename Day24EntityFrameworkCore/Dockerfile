FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY Day24EntityFrameworkCore/Day24EntityFrameworkCore.csproj Day24EntityFrameworkCore/
RUN dotnet restore Day24EntityFrameworkCore/Day24EntityFrameworkCore.csproj
COPY . .
WORKDIR /src/Day24EntityFrameworkCore
RUN dotnet build Day24EntityFrameworkCore.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Day24EntityFrameworkCore.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Day24EntityFrameworkCore.dll"]
