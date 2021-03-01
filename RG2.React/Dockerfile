#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
ENV TZ=Europe/Berlin
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src

## RUN apt-get update
RUN apt-get -y install curl
RUN curl -sL https://deb.nodesource.com/setup_15.x | bash -
RUN apt-get install -y nodejs
RUN npm install -g yarn

COPY ["Framework/Signum.Utilities/Signum.Utilities.csproj", "Framework/Signum.Utilities/"]
COPY ["Framework/Signum.Entities/Signum.Entities.csproj", "Framework/Signum.Entities/"]
COPY ["Framework/Signum.Engine/Signum.Engine.csproj", "Framework/Signum.Engine/"]
COPY ["Framework/Signum.React/Signum.React.csproj", "Framework/Signum.React/"]
COPY ["Framework/Signum.React/package.json", "Framework/Signum.React/"]
COPY ["Framework/Signum.React/yarn.lock", "Framework/Signum.React/"]


COPY ["Extensions/Signum.Entities.Extensions/Signum.Entities.Extensions.csproj", "Extensions/Signum.Entities.Extensions/"]
COPY ["Extensions/Signum.Engine.Extensions/Signum.Engine.Extensions.csproj", "Extensions/Signum.Engine.Extensions/"]
COPY ["Extensions/Signum.Engine.MachineLearning.TensorFlow/Signum.Engine.MachineLearning.TensorFlow.csproj", "Extensions/Signum.Engine.MachineLearning.TensorFlow/"]
COPY ["Extensions/Signum.React.Extensions/Signum.React.Extensions.csproj", "Extensions/Signum.React.Extensions/"]
COPY ["Extensions/Signum.React.Extensions/package.json", "Extensions/Signum.React.Extensions/"]
COPY ["Extensions/Signum.React.Extensions/yarn.lock", "Extensions/Signum.React.Extensions/"]

COPY ["RG2.Entities/RG2.Entities.csproj", "RG2.Entities/"]
COPY ["RG2.Logic/RG2.Logic.csproj", "RG2.Logic/"]
COPY ["RG2.React/RG2.React.csproj", "RG2.React/"]
COPY ["RG2.React/package.json", "RG2.React/"]
COPY ["RG2.React/yarn.lock", "RG2.React/"]

WORKDIR "/src/RG2.React"
RUN yarn install

WORKDIR /src
RUN dotnet restore "RG2.React/RG2.React.csproj"

COPY . .
WORKDIR "/src/RG2.React"
RUN dotnet build "RG2.React.csproj" -c Release # -o /app/build
# RUN yarn run build-production
RUN yarn run build-development

FROM build AS publish
RUN dotnet publish "RG2.React.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RG2.React.dll"]
