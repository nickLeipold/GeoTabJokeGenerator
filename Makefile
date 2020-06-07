all:
	dotnet build
	dotnet run --project ConsoleApp1
build:
	dotnet build

test: build
	dotnet run --project ConsoleApp1 < NnCategory3.test
