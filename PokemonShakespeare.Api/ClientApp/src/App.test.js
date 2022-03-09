import React from 'react';
import ReactDOM, {render} from 'react-dom';
import { MemoryRouter } from 'react-router-dom';
import App from './App';
import {PokemonSearcher} from "./components/SearchBox";

it('renders without crashing', async () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <App />
    </MemoryRouter>, div);
  await new Promise(resolve => setTimeout(resolve, 1000));
});

// it('renders search bar on page', async () => {
//     render(<PokemonSearcher />);
//
//     const search = screen.getByTestId("test-search");
//     expect(search).toBeInTheDocument();
// });
