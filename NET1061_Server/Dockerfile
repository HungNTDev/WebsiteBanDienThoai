# ========================
# 🚧 STAGE 1: BUILD
# ========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file và project file
COPY NET1061_Server/NET1061_Server.csproj NET1061_Server/
RUN dotnet restore NET1061_Server/NET1061_Server.csproj

# Copy toàn bộ project
COPY . .

# Publish app
RUN dotnet publish NET1061_Server/NET1061_Server.csproj -c Release -o /app/publish

# =========================
# 🚀 STAGE 2: RUNTIME
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published output từ stage build
COPY --from=build /app/publish .

# Mở cổng (tùy chọn)
EXPOSE 80

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "NET1061_Server.dll"]