FROM microsoft/aspnetcore:1.1.0
ENTRYPOINT ["dotnet", "Api.dll"]
ARG source=.
WORKDIR /app
ENV ASPNETCORE_URLS http://*:8081
EXPOSE 8081
COPY $source .
