import React, {useState} from 'react';
import Error from "./Error";

async function submitSearch(name, f) {
        const response = await fetch('pokemon/'+name);
        const data = await response.json();
        return f(data);
}

export function PokemonSearcher() {
    const [letters, setLetters] = useState("");
    const [pokemon, setPokemon] = useState({});
    
    return <div className="wrap">
            <div className="search">
                <img id="pokemonLogo" src="https://upload.wikimedia.org/wikipedia/commons/thumb/9/98/International_Pok%C3%A9mon_logo.svg/2880px-International_Pok%C3%A9mon_logo.svg.png"/>
                <form aria-label="A search box that returns Shakespearean description of a Pokemon"
                      aria-required="true" 
                      onSubmit={(e) => {submitSearch(letters, setPokemon); e.preventDefault();}}>
                    <input aria-label="input for pokemon search"
                           aria-required="true" 
                           type="input" 
                           className="searchTerm" 
                           placeholder="Search for a Shakespearean pokemon..." 
                           onChange={(e) => setLetters(e.target.value)}
                    />
                </form>
                <Pokemon {... pokemon}/>
            </div>
    </div>
}

function Pokemon({sprite, description, shakespeareDescription, name}) {
        return <div className="pokemonResult">
                    <h2>{name}</h2>
                    <img src={sprite} alt={name}/>
                    <p>{shakespeareDescription || description}</p>
                </div>
}