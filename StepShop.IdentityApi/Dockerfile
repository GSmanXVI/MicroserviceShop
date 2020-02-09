#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["StepShop.IdentityApi/StepShop.IdentityApi.csproj", "StepShop.IdentityApi/"]
COPY ["StepShop.Shared/StepShop.Shared.csproj", "StepShop.Shared/"]
RUN dotnet restore "StepShop.IdentityApi/StepShop.IdentityApi.csproj"
COPY . .
WORKDIR "/src/StepShop.IdentityApi"
RUN dotnet build "StepShop.IdentityApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StepShop.IdentityApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StepShop.IdentityApi.dll"]