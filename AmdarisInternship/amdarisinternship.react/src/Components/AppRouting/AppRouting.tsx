import React from 'react';
import {
    BrowserRouter as Router,
    Switch,
    Route,
} from 'react-router-dom';
import Administration from '../Administration/Administration';
import Grades from '../Grades/Grades';
import Lessons from '../Lessons/Lessons';
import LogIn from '../LogIn/LogIn';
import Default from '../Default/Default';

function AppRouting() {
    return (
        <Router>
            <Switch>
                <Route exact path="/" component={Default}></Route>
                <Route exact path="/LogIn" component={LogIn}></Route>
                <Route exact path="/Lessons" component={Lessons}></Route>
                <Route exact path="/Grades" component={Grades}></Route>
                <Route exact path="/Administration" component={Administration}></Route>
            </Switch>
        </Router>
    );
}

export default AppRouting;