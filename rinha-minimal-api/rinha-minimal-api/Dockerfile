FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#Copiando todo csproj para ser poss�vel baixar os pacotes com base nas ref�rencias 
COPY *.csproj ./
RUN dotnet restore

#Copiando todo resto da aplica��o, separando essa etapa da etapa anterior facilita no debug caso de algum erro 
COPY . ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY --from=base /app/out . 
ENTRYPOINT ["dotnet", "rinha-minimal-api.dll"]


