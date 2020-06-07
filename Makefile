all:
	dotnet build
	dotnet run --project ConsoleApp1
build:
	dotnet build

test: build
	dotnet run --project ConsoleApp1 < NnCategory3.test
test2: build
	dotnet run --project ConsoleApp1 < names.test
test3: build
	dotnet run --project ConsoleApp1 < nameCategory5.test
testApi: build
	dotnet run --project ConsoleApp1 --api < nameCategory5.test
