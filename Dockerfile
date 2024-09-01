FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build-env
WORKDIR /App

COPY . .
RUN dotnet restore
RUN dotnet publish StudentPortal.Web/StudentPortal.Web.csproj -c Release -o ./out
COPY  StudentPortal.Web/wwwroot ./out

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.20
WORKDIR /App
COPY --from=build-env /App/out .
RUN apk add --no-cache icu-libs
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENTRYPOINT ["./StudentPortal.Web"]
