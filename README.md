# Pokemon Translator
Use the app to search for a pokemon and receive the description translated into a Shakespearean verse. 

Disclaimer: I have never used React before so I am very open to comments, improvements etc! 

## Tech
To develop this app I used
- .NET 6
- React

## Running the app
The app runs in docker. To start run the following commmands:

### Step 1
`docker build . -t pokemon-shakespear`

### Step 2
`docker run -p 5000:80 pokemon-shakespeare`

### Step 3
Navigate to `http://localhost:5000/` on your browser

## Further work
Given more time I would look to improve on the following:
- Comprehensive testing on the react side
- Having the data in a DB instead of calling to both API's -> this would also workaround the rate limit of 5 requests/hour for the Shakespeare translations
- More accessibility features -> need to do more research on this as I have never had accessibility as a requirement before
