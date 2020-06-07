# This is a review of the Revamped GeoTab take home coding challenge, which is a cli of sorts for the Chuck Norris joke api

## Changes
- created a Makefile
- created some test inputs
- fixed bad formed query strings
- updated urls
- removed prompt for instructions
- added cli functionality
- added ability to enter a custom name
- increased the amount of jokes
- category selection is working
- invalid input retries
- parsing the jokes response
- refactoring


## Created a Makefile
  I made a Makefile which helps with automation in building the program as well as executing tests for faster debugging

  `make` 
  - will build and run the program

  `make build` 
  - will build the program

## Created some test inputs
  These are for visual checking of output from the program as the jokes are dynamic and a dynamic test harness wasnt created in the time window.

  `make test#`
  - will run a test (# is to be replaced with a number from 1-3)

  `make testCli`
  - will run a small test that will only output the jokes new line seperated

## Fixed bad formed query strings
  Some of the query strings in the provided JsonFeed.cs were not creating a proper query string
  when they were called. THis was a quick fix of checking the composed url.

## Updated urls
  Switched over to the new url that was needed for the names api as well as parsing the json object properly

## Removed prompt for instructions
  I made the design decision/assumtion that if you are wanting to use the tool in non --cli mode,
  that you would like prompts of how to use the tool. Throughout the program you will get direction of your next action,
  adding user friendliness.

## Added cli functionality
  I went ahead and made a flag for the program that removes all instructional prints so the program can be used through pipes.

  `make testCli`
  - will test the cli, with using pipes you can pipe the jokes newline seperated into another program

## Added ability to enter custom name
  I added the ability to enter a custom name in the name replacement for the jokes, you are promted for a custom name when creating a joke
  and if you choose not to provide a custom name, it will prompt for a random name. (if you decide not to choose a random name it will stay as Chuck Norris)

## Increased the amount of jokes
  I increase the amount of jokes allowed. 20 was selected arbitralily, and the program should be able to create a list of jokes the size of the buffers
  it has been alotted. On input of a size, I cap that at 20

## Category Selection
  The category selection now works and gets sent through to the joke generator api.
  Upon choosing to select a category, the categories will be displayed for the user.

## Invalid input retires
  I added recursive function calls to deal with invalid input to the user prompts. These could be limited to a certain depth of recursion with an added counter variable to the function, but I thought that to not be necessary

## Parsing the joke responses
  The joke responses from the api were not being properly parsed and passed back to the caller.

## Refactoring
  Worked on breaking the code blocks down into small descriptive functions.
  Got rid of un-needed global variables
  