import React, { Component } from 'react';
import {PokemonSearcher} from "./components/SearchBox";

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <PokemonSearcher/>
    );
  }
}
