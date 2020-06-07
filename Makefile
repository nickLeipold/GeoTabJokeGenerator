all:
	dotnet build
	dotnet run --project ChuckNorrisJokeGenerator
build:
	dotnet build

test1: build
	dotnet run --project ChuckNorrisJokeGenerator < test/NnCategory3.test
test2: build
	dotnet run --project ChuckNorrisJokeGenerator < test/names.test
test3: build
	dotnet run --project ChuckNorrisJokeGenerator < test/nameCategory5.test
test4: build
	dotnet run --project ChuckNorrisJokeGenerator < test/customName.test
testCli: build
	dotnet run --project ChuckNorrisJokeGenerator --cli < test/nameCategory5.test
