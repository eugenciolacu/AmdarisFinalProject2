import React, { useState } from 'react';
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Redirect,
} from 'react-router-dom';
import Administration from '../Administration/Administration';
import Grades from '../Grades/Grades';
import Lessons from '../Lessons/Lessons';
import LogIn from '../LogIn/LogIn';
import Default from '../Default/Default';
import Contacts from '../Contacts/Contacts';
import LoginService from '../../Services/LoginService';

function AppRouting() {

    const isLogged = () => {
        if (LoginService.getCurrentUser().isSuccess)
            return true;

        return false;
    }

    return (
        <Router>
            <Switch>
                {(() => {
                    if (LoginService.getCurrentUser().isSuccess) {
                        return <>
                            <Route exact path="/" component={Default}>
                                <Redirect to="/Lessons" />
                            </Route>
                            <Route exact path="/Lessons" component={Lessons}></Route>
                            <Route exact path="/Grades" component={Grades}></Route>
                            <Route exact path="/Contacts" component={Contacts}></Route>
                            <Route exact path="/Administration" component={Administration}></Route>
                            <Route exact path="/LogIn" component={LogIn}></Route>
                        </>
                    }
                    else {
                        return <>
                            <Redirect to="/LogIn" />

                            <Route exact path="/" component={Default}></Route>
                            <Route exact path="/Lessons" component={Lessons}></Route>
                            <Route exact path="/Grades" component={Grades}></Route>
                            <Route exact path="/Contacts" component={Contacts}></Route>
                            <Route exact path="/Administration" component={Administration}></Route>
                            <Route exact path="/LogIn" component={LogIn}></Route>
                        </>
                    }
                })()}
            </Switch>
        </Router>
    );
}

export default AppRouting;