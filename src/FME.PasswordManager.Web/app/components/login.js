import React from 'react';
import {Link} from 'react-router';

class Login extends React.Component {
    render() {
        return (
            <div>
                {/* Labels to be deleted whenever */}
                <div className="container">
                    <div className="row">
                        <div className='alert alert-info'>
                            Landing Component
                        </div>
                        <Link to={'/table/'}>
                            <div className='alert alert-warning'>
                                Table Component
                            </div>
                        </Link>
                    </div>
                </div>

                {/* Login Panel */}
                <div className='container'>
                    <div className='row flipInX animated'>

                        {/* Login */}
                        <div className='col-sm-6'>
                            <div className='panel panel-default'>
                                <div className='panel-body'>
                                    <form>
                                        {/* username input */}
                                        <div className={'form-group '}>
                                            <label className='control-label'>Email</label>
                                            <input type='text' className='form-control' ref='usernameTextField'/>
                                        </div>

                                        {/* password input */}
                                        <div className={'form-group '}>
                                            <label className='control-label'>Password</label>
                                            <input type='text' className='form-control' ref='passwordTextField'/>
                                        </div>

                                        {/* submit button */}
                                        <button type='submit' className='btn btn-primary pull-right'>Login</button>
                                    </form>
                                </div>
                            </div>
                        </div>

                        {/* Register */}
                        <div className='col-sm-6'>
                            <div className='panel panel-default'>
                                <div className='panel-body'>
                                    <form>
                                        {/* username input */}
                                        <div className={'form-group '}>
                                            <label className='control-label'>Email</label>
                                            <input type='text' className='form-control' ref='usernameTextField'/>
                                        </div>

                                        {/* password input */}
                                        <div className={'form-group '}>
                                            <label className='control-label'>Password</label>
                                            <input type='text' className='form-control' ref='passwordTextField'/>
                                        </div>

                                        {/* confirmPassword input */}
                                        <div className={'form-group '}>
                                            <label className='control-label'>Confirm Password</label>
                                            <input type='text' className='form-control' ref='confirmPasswordTextField'/>
                                        </div>

                                        {/* submit button */}
                                        <button type='submit' className='btn btn-primary pull-right'>Register</button>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Login;