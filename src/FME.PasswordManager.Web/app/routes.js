import React from 'react';
import {Route} from 'react-router';
import App from './components/App';
import Login from './components/Login';
import AddAccount from './components/AddAccount';
import PasswordTable from './components/PasswordTable';

export default (
    <Route component={App}>
        <Route path='/' component={Login} />
        <Route path='/add' component={AddAccount} />
        <Route path='/table' component={PasswordTable} />
    </Route>
);