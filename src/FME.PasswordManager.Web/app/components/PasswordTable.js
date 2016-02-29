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
                                    <td></td>
                                    <td><b>Account</b></td>
                                    <td><b>Password</b></td>
                                    <td><b>Email</b></td>
                                    <td><b>Username</b></td>
                                    <td></td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr className="text-center">
                                    <td>
                                        <Link to={'/table/'}>
                                            <button type='submit' className='btn btn-warning'>Details</button>
                                        </Link>
                                    </td>
                                    <td>facebook.com</td>
                                    <td>p@ssword1</td>
                                    <td>email@gmail.com</td>
                                    <td>user1</td>
                                    <td>
                                        <Link to={'/table/'}>
                                            <button type='submit' className='btn btn-info'>Edit</button>
                                        </Link>
                                    </td>
                                </tr>
                                <tr className="text-center">
                                    <td>
                                        <Link to={'/table/'}>
                                            <button type='submit' className='btn btn-warning'>Details</button>
                                        </Link>
                                    </td>
                                    <td>twitter.com</td>
                                    <td>password1!</td>
                                    <td>email.twitter@gmail.com</td>
                                    <td>user2</td>
                                    <td>
                                        <Link to={'/table/'}>
                                            <button type='submit' className='btn btn-info'>Edit</button>
                                        </Link>
                                    </td>
                                </tr>
                                <tr className="text-center">
                                    <td>
                                        <Link to={'/table/'}>
                                            <button type='submit' className='btn btn-warning'>Details</button>
                                        </Link>
                                    </td>
                                    <td>bank.com</td>
                                    <td>p@ssword1</td>
                                    <td>email.bank@gmail.com</td>
                                    <td>user3</td>
                                    <td>
                                        <Link to={'/table/'}>
                                            <button type='submit' className='btn btn-info'>Edit</button>
                                        </Link>
                                    </td>
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