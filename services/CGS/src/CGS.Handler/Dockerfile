#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CGS.Handler/CGS.Handler.csproj", "src/CGS.Handler/"]
COPY ["src/CGS.Domain/CGS.Domain.csproj", "src/CGS.Domain/"]
COPY ["src/CGS.Infra/CGS.Infra.csproj", "src/CGS.Infra/"]
COPY ["src/CGS.Utils/CGS.Utils.csproj", "src/CGS.Utils/"]
COPY ["src/CGS.SharedKernel/CGS.SharedKernel.csproj", "src/CGS.SharedKernel/"]
RUN dotnet restore "src/CGS.Handler/CGS.Handler.csproj"
COPY . .
WORKDIR "/src/CGS.Handler"
RUN dotnet build "CGS.Handler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CGS.Handler.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CGS.Handler.dll"]