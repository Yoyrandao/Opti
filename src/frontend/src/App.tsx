import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import './App.global.css';
import { Explorer } from './components';

const Hello = () => {
  return (
    <div style={{ width: '100%' }}>
      <Explorer directory={`${process.env.HOME}\\.opti\\data`} />
    </div>
  );
};

export default function App() {
  return (
    <Router>
      <Switch>
        <Route path="/" component={Hello} />
      </Switch>
    </Router>
  );
}
