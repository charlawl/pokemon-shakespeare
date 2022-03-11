# Pokemon Translator
Use the app to search for a pokemon and receive the description translated into a Shakespearean verse. 

Disclaimer: I have never used React before so I am very open to comments, improvements etc! 

## Tech
To develop this app I used
- .NET 6
- React

### Backend
I build the API using the new .NET 6 minimal API's which was really interesting as I haven't worked with them before. It allowed me to spin up a fully functional API really quick although I found that testing it wasn't as straight forward as I thought it would be. This seems to be a bit of a consensus with the new minimal API's and was certainly a learning for me here. 

### Frontend
As I said above this was a first for me. I spent a fair few hours trying to familiarise myself with React fundementals before starting to build anything. I would really welcome a chat about how I went about it and the areas (probably a fair few) in which the code could be improved. I haven't written any tests for this part as I would welcome a discussion around the approach to testing in React. 

## Running the app
The app runs in docker. To start run the following commmands:

### Step 1
`docker build . -t pokemon-shakespeare`

### Step 2
`docker run -p 5000:80 pokemon-shakespeare`

### Step 3
Navigate to `http://localhost:5000/` on your browser

## Further work
Given more time I would look to improve on the following:
- Comprehensive testing on the react side
- Having the data in a DB instead of calling to both API's -> this would also workaround the rate limit of 5 requests/hour for the Shakespeare translations
- More accessibility features -> need to do more research on this as I have never had accessibility as a requirement before
