import React from 'react';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import MenuIcon from '@material-ui/icons/Menu';
import LoginService from '../../Services/LoginService';
import { useHistory } from 'react-router-dom';

import Menu from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      flexGrow: 1,
    },
    menuButton: {
      marginRight: theme.spacing(0),
    },
    title: {
      flexGrow: 1,
    },
  }),
);



export default function ButtonAppBar() {
  const classes = useStyles();
  const history = useHistory();
  const handleLogoutClick = async () => {
    LoginService.logOut();
    history.replace("/Login")
  }

  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = (path: string) => {
    setAnchorEl(null);
    history.push(path);
  };

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
            <Button aria-controls="simple-menu" aria-haspopup="true" onClick={handleClick} style={{color: 'white'}}>
            <MenuIcon />
              
            </Button>
            <Menu
              id="simple-menu"
              anchorEl={anchorEl}
              keepMounted
              open={Boolean(anchorEl)}
              onClose={handleClose}
              getContentAnchorEl={null}
              anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'center',
              }}
              transformOrigin={{
                vertical: 'top',
                horizontal: 'center',
              }}
            >
              <MenuItem onClick={ () => handleClose("/Lessons") }>Lessons</MenuItem>
              <MenuItem onClick={ () => handleClose("/Grades") }>Grades</MenuItem>
              <MenuItem onClick={ () => handleClose("/Contacts") }>Contacts</MenuItem>

              { (() => {
                if (LoginService.getCurrentUser().maxRole == "Administrator")
                {
                  return <MenuItem onClick={ () => handleClose("/Administration")}>Administration</MenuItem>
                }
              }) ()}     
            </Menu>

          <Typography variant="h6" className={classes.title}>
            {LoginService.getCurrentUser().firstName + ' ' + LoginService.getCurrentUser().lastName}
          </Typography>
          <Button color="inherit" onClick={handleLogoutClick}>Logout</Button>
        </Toolbar>
      </AppBar>
    </div>
  );
}