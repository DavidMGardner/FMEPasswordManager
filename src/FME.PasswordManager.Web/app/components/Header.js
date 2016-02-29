import React from 'react';
import {Link} from 'react-router';
import HeaderStore from '../stores/HeaderStore';
import HeaderActions from '../actions/HeaderActions';

class Header extends React.Component {
    constructor(props) {
        super(props);
        {/*this.state = HeaderStore.getState();*/}
        this.onChange = this.onChange.bind(this);
    }

    componentDidMount() {

    }

    componentWillUnmount() {
        {/*HeaderStore.unlisten(this.onChange);*/}
    }

    onChange(state) {
        this.setState(state);
    }

    handleSubmit(event) {
        event.preventDefault();

    }

    render() {
        return (
            <nav className="navbar navbar-default">
                <div className="container">
                    <div className="navbar-header">
                        <div className="pull-left">
                            <h2>Full Metal Engineering Password Manager</h2>
                        </div>
                    </div>
                </div>
            </nav>
        );
    }
}

export default Header;











