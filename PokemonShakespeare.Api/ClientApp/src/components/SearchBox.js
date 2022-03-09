import React, {useState} from 'react';
import Error from "./Error";

async function submitSearch(name, onSuccess, onFailure) {
    const response = await fetch('pokemon/' + name);
    if (response.ok) {
        const data = await response.json();
        onSuccess(data);
    } else
        onFailure();
}

export function PokemonSearcher() {
    const [letters, setLetters] = useState("");
    const [pokemon, setPokemon] = useState({});
    const [display, setDisplay] = useState("initial");
    
    function onSuccess(data){
        setPokemon(data);
        setDisplay("success");
    }
    
    function onFailure(){
        setDisplay("error")
    }
    
    return <div className="wrap">
            <div className="search">
                <img id="pokemonLogo" src="https://upload.wikimedia.org/wikipedia/commons/thumb/9/98/International_Pok%C3%A9mon_logo.svg/2880px-International_Pok%C3%A9mon_logo.svg.png"/>
                <form aria-label="A search box that returns Shakespearean description of a Pokemon"
                      aria-required="true" 
                      onSubmit={(e) => {submitSearch(letters, onSuccess, onFailure); e.preventDefault();}}>
                    <input aria-label="input for pokemon search"
                           aria-required="true" 
                           type="input" 
                           className="searchTerm" 
                           placeholder="Search for a Shakespearean pokemon..." 
                           onChange={(e) => setLetters(e.target.value)}
                    />
                </form>
                {(() => { switch (display) {
                    case "success":
                        return <Pokemon {... pokemon}/>;
                    case "error":
                        return <Error/>
                    default:
                        return <div> </div>
                }})()}
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