import React from 'react';
import {Route} from 'react-router';
import App from './components/App';
import AddAccount from './components/AddAccount';

export default (
    <Route component={App}>
        <Route path='/add' component={AddAccount} />
    </Route>
);