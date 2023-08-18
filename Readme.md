# Step to run the app
- clone the repo
- cd App
- npm install
- npm run dev
# In a new Terminal
- cd TodoAPI
- dotnet run --project .\TodoAPI\TodoAPI.csproj

# Prerequisites
- Postgresql database named "Todo"
- Host for the DB has to be localhost:5432
- username = postgres;
- There shoudl be a env variable "POSTGRESPSWD", whose content must be the password for the user
# if any of the above is not same, you need to change it in the TodoAPI/TodoAPI/Data/DataContext.cs file
