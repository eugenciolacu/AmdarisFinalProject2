import React from 'react';
import { makeStyles, Theme } from '@material-ui/core/styles';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import { Paper } from '@material-ui/core';
import ModuleForm from './ModuleForm';

export default function Navigation() {
    const classes = useStyles();
    const [value, setValue] = React.useState(0);

    const handleChange = (event: React.ChangeEvent<{}>, newValue: number) => {
        setValue(newValue);

        if (newValue == 0)
        {
            <ModuleForm />
        }
    };

    let tmp : any = <ModuleForm />;

    if (value != 0)
    {
        tmp = null;
    }

    return (
        <>
            <Paper className={classes.root}>
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
            </Paper>

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