# This is a review of the Revamped GeoTab take home coding challenge, which is a cli of sorts for the Chuck Norris joke api

## changes
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


### Created a Makefile
    I made a Makefile which helps with automation in building the program as well as executing tests for faster debugging

    `make` 
    - will build and run the program

    `make build` 
    - will build the program

### created some test inputs
    These are for visual checking of output from the program as the jokes are dynamic and a dynamic test harness wasnt created in the time window.

    `make test#`
    - will run a test (# is to be replaced with a number from 1-3)

    `make testCli`
    - will run a small test that will only output the jokes new line seperated

### fixed bad formed query strings
    Some of the query strings in the provided JsonFeed.cs were not creating a proper query string
    when they were called. THis was a quick fix of checking the composed url.

### updated urls
    Switched over to the new url that was needed for the names api as well as parsing the json object properly

### removed prompt for instructions
    I made the design decision/assumtion that if you are wanting to use the tool in non --cli mode,
    that you would like prompts of how to use the tool. Throughout the program you will get direction of your next action,
    adding user friendliness.

### added cli functionality
    I went ahead and made a flag for the program that removes all instructional prints so the program can be used through pipes.
    `make testCli`
        - will test
