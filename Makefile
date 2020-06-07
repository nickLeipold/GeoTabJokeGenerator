all:
	dotnet build
	dotnet run --project ConsoleApp1
build:
	dotnet build

test: build
	dotnet run --project ConsoleApp1 < NnCategory3.test
test2: build
	dotnet run --project ConsoleApp1 < names.test

