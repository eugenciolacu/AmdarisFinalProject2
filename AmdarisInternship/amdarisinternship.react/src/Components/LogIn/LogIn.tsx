import React from 'react';
import { useForm } from 'react-hook-form';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import { LoginForm } from '../../Models/LoginForm';
import LoginService from '../../Services/LoginService';
import { useHistory } from 'react-router-dom';


const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(10),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main,
  },
  form: {
    width: '100%', // Fix IE 11 issue.
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
}));


export default function LogIn() {
  const classes = useStyles();
  const history = useHistory();
  

  const { register, handleSubmit, errors } = useForm<LoginForm>({
    defaultValues:{
      email: "",
      password: ""
    }
  });

  const onSubmit = async (data : LoginForm) => {
    console.log(data);

    const response = await LoginService.login(data);
    if (response.isSuccess)
    {
        return history.push("/Lessons")
    }

    console.log(response);


    // axios.post(`https://localhost:44336/api/Account/Login`,data)
    //   .then(res => {
    //     console.log(res);
    //     console.log(res.data);
    //   })
  };

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Log in
        </Typography>
        <form className={classes.form} noValidate onSubmit={handleSubmit(onSubmit)}>
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            id="email"
            label="Email Address"
            name="username"
            autoComplete="email"
            autoFocus
            inputRef={register({
              required: {
                value: true,
                message: "Please fill this field"
              },
              pattern: {
                value: /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/,
                message: "Provide valid email"
              }
            })}
          />
          {errors.email && (<div> {errors.email.message} </div>)}

          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="password"
            label="Password"
            type="password"
            id="password"
            autoComplete="current-password"
            inputRef={register({
              required: {
                value: true,
                message : "Please fill this field"
              },
              minLength: {
                value : 8,
                message : "Password should have at least 8 characters"
              }
            })}
          />
          {errors.password && (<div> {errors.password.message} </div>)}

          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
          >
            Log In
          </Button>
        </form>
      </div>
    </Container>
  );
}