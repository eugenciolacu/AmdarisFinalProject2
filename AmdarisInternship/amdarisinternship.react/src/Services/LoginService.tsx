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

async function logOut(): Promise <any> {
    await http.postNoParam(`${authPath}logout`);
    localStorage.clear();
}

function getCurrentUser(): UserResponse {
    const userString = localStorage.getItem('user');
    const user = userString ? JSON.parse(userString) as UserResponse : {} as UserResponse;
    return user;   
}

export default {
    login,
    getCurrentUser,
    logOut
}