import React from 'react';
import { homedir } from 'os';
import { join } from 'path';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import { Explorer } from '../components';

import './App.global.css';

const Application = () => {
  return (
    <div className="applicationContainer">
      <div className="leftPanel">
        <div className="logo">Opti Manager</div>
        <div className="user">aaron</div>
      </div>
      <div className="explorer">
        <Explorer directory={join(homedir(), '.opti', 'data')} />
      </div>
    </div>
  );
};

function App() {
  return (
    <Router>
      <Switch>
        <Route path="/" component={Application} />
      </Switch>
    </Router>
  );
}

export { App };
