import React from 'react';
import AddAccountStore from '../stores/AddAccountStore';
import AddAccountActions from '../actions/AddAccountActions';

class AddAccount extends React.Component {
    constructor(props) {
        super(props);
        this.state = AddAccountStore.getState();
        this.onChange = this.onChange.bind(this);
    }

    componentDidMount() {
        AddAccountStore.listen(this.onChange);
    }

    componentWillUnmount() {
        AddAccountStore.unlisten(this.onChange);
    }

    onChange(state) {
        this.setState(state);
    }

    handleSubmit(event) {
        event.preventDefault();

        var account = this.state.account.trim();
        var password = this.state.password.trim();
        var email = this.state.email.trim();
        var username = this.state.username.trim();

        if (!account) {
            AddAccountActions.onInvalidAccount();
            this.refs.accountTextField.getDOMNode().focus();
        }

        if (!password) {
            AddAccountActions.onInvalidPassword();
            this.refs.passwordTextField.getDOMNode().focus();
        }

        if (!email) {
            AddAccountActions.onInvalidEmail();
            this.refs.emailTextField.getDOMNode().focus();
        }

        if (!username) {
            AddAccountActions.onInvalidUsername();
            this.refs.usernameTextField.getDOMNode().focus();
        }

        if (account && password && email && username) {
            AddAccountActions.addAccount(account, password, email, username);
            this.refs.accountTextField.getDOMNode().focus();
        }
    }

    render() {
        return (
            <div className='container'>
                <div className='row flipInX animated'>
                    <div className='col-sm-8'>
                        <div className='panel panel-default'>
                            <div className='panel-body'>
                                <form onSubmit={this.handleSubmit.bind(this)}>

                                    {/* account input */}
                                    <div className={'form-group ' + this.state.accountValidationState}>
                                        <label className='control-label'>Account</label>
                                        <input type='text' className='form-control' ref='accountTextField' value={this.state.account}
                                               onChange={AddAccountActions.updateAccount} autoFocus/>
                                        <span className='help-block'>{this.state.helpBlock}</span>
                                    </div>

                                    {/* password input */}
                                    <div className={'form-group ' + this.state.passwordValidationState}>
                                        <label className='control-label'>Password</label>
                                        <input type='text' className='form-control' ref='passwordTextField' value={this.state.password}
                                               onChange={AddAccountActions.updatePassword} autoFocus/>
                                        <span className='help-block'>{this.state.helpBlock}</span>
                                    </div>

                                    {/* email */}
                                    <div className={'form-group ' + this.state.emailValidationState}>
                                        <label className='control-label'>Email</label>
                                        <input type='text' className='form-control' ref='accountTextField' value={this.state.email}
                                               onChange={AddAccountActions.updateEmail} autoFocus/>
                                        <span className='help-block'>{this.state.helpBlock}</span>
                                    </div>

                                    {/* username */}
                                    <div className={'form-group ' + this.state.usernamelValidationState}>
                                        <label className='control-label'>Username</label>
                                        <input type='text' className='form-control' ref='accountTextField' value={this.state.username}
                                               onChange={AddAccountActions.updateUsername} autoFocus/>
                                        <span className='help-block'>{this.state.helpBlock}</span>
                                    </div>

                                    {/* submit button */}
                                    <button type='submit' className='btn btn-primary'>Submit</button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default AddAccount;