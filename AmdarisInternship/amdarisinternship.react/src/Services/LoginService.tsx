import { LoginForm } from '../Models/LoginForm';
import http from './http';
import { UserResponse } from '../Models/UserResponse';
const authPath = 'Account/';


async function login (loginForm : LoginForm) : Promise<UserResponse>
{
    const { data } = await http.post(`${authPath}login`, loginForm);
    // console.log(data);
    const token = (data as UserResponse).token;
    localStorage.setItem('user', JSON.stringify(data));
    localStorage.setItem('token', token);
    http.setToken(token);

    return data;
}

export default {
    login
}