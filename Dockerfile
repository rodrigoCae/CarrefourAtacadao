#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Carrefour_Atacadao_BackEnd.csproj", "."]
RUN dotnet restore "./Carrefour_Atacadao_BackEnd.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Carrefour_Atacadao_BackEnd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Carrefour_Atacadao_BackEnd.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV SQLCONNSTR_DBTeste "Data Source=DESKTOP-LSEMRNN;Initial Catalog=Carrefour_Atacadao;Integrated Security=True;"
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Carrefour_Atacadao_BackEnd.dll"]