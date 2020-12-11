import React from 'react';
import { makeStyles, Theme } from '@material-ui/core/styles';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import ModuleView from './ModuleView';
import LessonView from './LessonView';
import PromotionView from './PromotionView';
import UserView from './UserView';

export default function Navigation() {
    const classes = useStyles();
    const [value, setValue] = React.useState(0);

    const handleChange = (event: React.ChangeEvent<{}>, newValue: number) => {
        setValue(newValue);
    };

    let tmp: any = null;

    if (value == 0) {
        tmp = <ModuleView />;
    }
    else if (value == 1) {
        tmp = <PromotionView />
    }
    else if (value == 2) {
        tmp = <LessonView />;
    }
    else if (value == 3) {
        tmp = <UserView />;
    }

    return (
        <>
            <Tabs
                value={value}
                onChange={handleChange}
                indicatorColor="primary"
                textColor="primary"
                centered
            >
                <Tab label="Modules" />
                <Tab label="Promotions" />
                <Tab label="Lessons" />
                <Tab label="Users" />
            </Tabs>
            {tmp}
        </>
    );
}

const useStyles = makeStyles((theme: Theme) => ({
    root: {
        flexGrow: 1,
        backgroundColor: theme.palette.background.paper,
    },
}));