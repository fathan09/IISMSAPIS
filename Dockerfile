
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


RUN apt-get update && apt-get install -y \
    libfontconfig1 \
    libfreetype6 \
    libpng16-16 \
    libjpeg62-turbo \
    libwebp7 \
    libgif7 \
    libtiff6 \
    libharfbuzz0b \
    libfribidi0 \
    liblcms2-2 \
    libbz2-1.0 \
    liblzma5 \
    libexpat1 \
    && rm -rf /var/lib/apt/lists/*


COPY *.csproj ./
RUN dotnet restore


COPY . ./
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .


ENV ASPNETCORE_URLS=http://+:8080
CMD ["dotnet", "YourProjectName.dll"]
