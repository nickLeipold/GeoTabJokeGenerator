all:
	dotnet build
	dotnet run --project ConsoleApp1
build:
	dotnet build

test: build
	dotnet run --project ConsoleApp1 < test/NnCategory3.test
test2: build
	dotnet run --project ConsoleApp1 < test/names.test
test3: build
	dotnet run --project ConsoleApp1 < test/nameCategory5.test
test4: build
	dotnet run --project ConsoleApp1 < test/customName.test
testApi: build
	dotnet run --project ConsoleApp1 --api < test/nameCategory5.test
