import React from 'react';
import {Link} from 'react-router';
import FooterStore from '../stores/FooterStore'
import FooterActions from '../actions/FooterActions';

class Footer extends React.Component {
    constructor(props) {
        super(props);
        this.state = FooterStore.getState();
        this.onChange = this.onChange.bind(this);
    }

    componentDidMount() {
        FooterStore.listen(this.onChange);
        FooterActions.getTopCharacters();
    }

    componentWillUnmount() {
        FooterStore.unlisten(this.onChange);
    }

    onChange(state) {
        this.setState(state);
    }

    render() {
        let leaderboardCharacters = this.state.characters.map((character) => {
            return (
                <li key={character.characterId}>
                    <Link to={'/characters/' + character.characterId}>
                        <img className='thumb-md' src={'http://image.eveonline.com/Character/' + character.characterId + '_128.jpg'} />
                    </Link>
                </li>
            )
        });

        return (
            <footer>
                <div className='container'>
                    <div className='row'>
                        <div className='col-sm-5'>
                            <h3 className='lead'><strong>Password Manager</strong></h3>
                            <p>You may view the <a href='https://github.com/DavidMGardner/FMEPasswordManager'>Source Code</a> behind this project on GitHub.</p>
                            <p>© 2016 Full Metal Engineering</p>
                        </div>
                    </div>
                </div>
            </footer>
        );
    }
}

export default Footer;