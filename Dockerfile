FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
EXPOSE 80

ENV ASPNETCORE_HTTP_PORTS=80

COPY ./src/Aslanta.Mvc/Aslanta.Mvc.csproj ./Aslanta.Mvc/
RUN dotnet restore ./Aslanta.Mvc/Aslanta.Mvc.csproj

COPY ./src/Aslanta.Mvc ./Aslanta.Mvc
RUN dotnet publish ./Aslanta.Mvc/Aslanta.Mvc.csproj -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

EXPOSE 80
ENV ASPNETCORE_HTTP_PORTS=80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Aslanta.Mvc.dll"]