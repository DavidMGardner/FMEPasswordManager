import alt from '../alt';

class AddAccountActions {
    constructor() {
        this.generateActions(
            'addAccountSuccess',
            'addAccountFail',
            'updateAccount',
            'updatePassword',
            'updateEmail',
            'updateUsername',
            'invalidAccount',
            'invalidPassword',
            'invalidEmail',
            'invalidUsername'
        );
    }

    addAccount(account, password, email, username) {
        $.ajax({
                type: 'POST',
                url: '/api/accounts',
                data: { account: account, password: password, email: email, username: username }
            })
            .done((data) => {
                this.actions.addAccountSuccess(data.message);
            })
            .fail((jqXhr) => {
                this.actions.addAccountFail(jqXhr.responseJSON.message);
            });
    }
}

export default alt.createActions(AddAccountActions);