import logo from './logo.svg';
import './App.css';
import './Chat.css'
import 'bootstrap/dist/css/bootstrap.min.css';

import Main from './Components/Main_WhatsappComponent';
import LoginComponent from './Components/LoginComponent';
import SignUpComponent from './Components/SignUpComponent';

import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect
} from "react-router-dom";
import { useState } from 'react';

function App() {

  const [isAuthenticatedState, setIsAuthenticatedState] = useState(false);
  const [tokenState, setTokenState] = useState(null);
  const [idState, setIdState] = useState(null);


  function setToken(token, id){
    setIdState(id)
    setTokenState(token);
    setIsAuthenticatedState(true);
  }

  return (
    <div className="App">
      <Router>
        <Switch>
          <Route exact path="/">
            {isAuthenticatedState ?
              <Main token={tokenState} id={idState} /> :
              <Redirect to={{ pathname: '/login' }} />
            }
          </Route>
          <Route path="/login">
            <LoginComponent setToken={setToken} />
          </Route>
          <Route path="/register">
            <SignUpComponent />
          </Route>
          <Route path="*">
            <Redirect to={{ pathname: '/' }} />
          </Route>
        </Switch>
      </Router>
    </div>
  );
}

export default App;
