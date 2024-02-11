FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine AS base
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_URLS=http://*:80;http://*:443
WORKDIR /app
EXPOSE 80 443
ENTRYPOINT /app/PixelApi

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS restore

WORKDIR /src

COPY . .
RUN dotnet restore -r linux-musl-x64 "src/PixelApi/PixelApi.csproj"

FROM restore AS publish
WORKDIR /src/src/PixelApi
RUN dotnet publish "PixelApi.csproj" --self-contained true --no-restore -r linux-musl-x64 -c Release -o /app/publish

FROM base AS final
COPY --from=publish /app/publish .
