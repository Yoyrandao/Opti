import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import { Explorer } from '../components';

import './App.global.css';
import styles from './App.css';

const Application = () => {
  return (
    <div className={styles.applicationContainer}>
      <div className={styles.leftPanel}>
        <div className={styles.logo}>Opti Manager</div>
        <div className={styles.user}>aaron</div>
      </div>
      <div className={styles.explorer}>
        <Explorer directory={`${process.env.HOME}\\.opti\\data`} />
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
