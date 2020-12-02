import { LoginForm } from '../Models/LoginForm';
import http from './http';
import { UserResponse } from '../Models/UserResponse';
const authPath = 'Account/';


async function login (loginForm : LoginForm) : Promise<UserResponse>
{
    const { data } = await http.post(`${authPath}login`, loginForm);
    // console.log(data);
    const token = (data as UserResponse).token;
    await localStorage.setItem('user', JSON.stringify(data));
    await localStorage.setItem('token', token);
    await http.setToken(token);

    return await data;
}

async function logOut(): Promise <any> {
    // await http.postNoParam(`${authPath}logout`);
    localStorage.removeItem('user');
    localStorage.removeItem('token');
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