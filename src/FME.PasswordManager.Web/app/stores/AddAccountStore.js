import alt from '../alt';
import AddAccountActions from '../actions/AddAccountActions';

class AddAccountStore {
    constructor() {
        this.bindActions(AddAccountActions);
        this.account = '';
        this.password = '';
        this.email = '';
        this.username = '';
        this.helpBlock = '';
        this.accountValidationState = '';
        this.passwordValidationState = '';
        this.emailValidationState = '';
        this.usernameValidationState = '';
    }

    onAddAccountSuccess(successMessage) {
        this.accountValidationState = 'has-success';
        this.helpBlock = successMessage;
    }

    onAddAccountFail(errorMessage) {
        this.accountValidationState = 'has-error';
        this.helpBlock = errorMessage;
    }

    onUpdateAccount(event) {
        this.account = event.target.value;
        this.accountValidationState = '';
        this.helpBlock = '';
    }

    onUpdatePassword(event) {
        this.password = event.target.value;
        this.passwordValidationState = '';
        this.helpBlock = '';
    }

    onUpdateEmail(event) {
        this.email = event.target.value;
        this.emailValidationState = '';
        this.helpBlock = '';
    }

    onUpdateUsername(event) {
        this.username = event.target.value;
        this.usernameValidationState = '';
    }

    onInvalidAccount() {
        this.accountValidationState = 'has-error';
        this.helpBlock = 'Please enter an account name.';
    }

    onInvalidPassword() {
        this.passwordValidationState = 'has-error';
        this.helpBlock = 'Please enter a valid.';
    }
    onInvalidEmail() {
        this.emailValidationState = 'has-error';
        this.helpBlock = 'Please enter an email';
    }

    onInvalidUsername() {
        this.usernameValidationState = 'has-error';
        this.helpBlock = 'Please enter a username';
    }
}

export default alt.createStore(AddAccountStore);