import React from 'react';
import {Link} from 'react-router';


class PasswordTable extends React.Component {
    render() {
    {/*TODO: add state and lifecycle hooks for pulling data from .NET API*/}
        return (
            <div className="container">
                {/* Banner */}
                <div className="row">
                    <div className='alert alert-info'>
                        Table Component
                    </div>
                </div>

                {/* table */}
                <div className="row">
                    <div className="table-responsive">
                        <table className="table table-hover">
                            <thead>
                                <tr className="text-center">
                                    <td>Account</td>
                                    <td>Password</td>
                                    <td>Email</td>
                                    <td>Username</td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr className="text-center">
                                    <td>facebook.com</td>
                                    <td>p@ssword1</td>
                                    <td>email@gmail.com</td>
                                    <td>user1</td>
                                </tr>
                                <tr className="text-center">
                                    <td>twitter.com</td>
                                    <td>password1!</td>
                                    <td>email.twitter@gmail.com</td>
                                    <td>user2</td>
                                </tr>
                                <tr className="text-center">
                                    <td>bank.com</td>
                                    <td>p@ssword1</td>
                                    <td>email.bank@gmail.com</td>
                                    <td>user3</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <Link to={'/add/'}>
                        <button type='submit' className='btn btn-primary pull-right'>Add Account</button>
                    </Link>
                </div>
            </div>
        );
    }
}

export default PasswordTable;