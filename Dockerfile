FROM ghcr.io/architecture-it/net:3.1-sdk as build
WORKDIR /app
COPY . .
RUN dotnet restore
WORKDIR "/app/src/service"
RUN dotnet build "WorkerOE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkerOE.csproj" -c Release -o /app/publish

FROM ghcr.io/architecture-it/net:3.1
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WorkerOE.dll"]
