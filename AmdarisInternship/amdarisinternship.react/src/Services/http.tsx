import axios from 'axios';

axios.defaults.baseURL = 'https://localhost:44336/api/';

function get(path: string) {
    if(!axios.defaults.headers['Authorization'] && localStorage.getItem('token'))
    {
        setToken(localStorage.getItem('token') || '');
    }
    
    return axios.get(path);
}

function post(path: string, body: any) {
    if(!axios.defaults.headers['Authorization'] && localStorage.getItem('token'))
    {
        setToken(localStorage.getItem('token') || '');
    }

    return axios.post(path, body);
}

function postNoParam(path: string) {
    if(!axios.defaults.headers['Authorization'] && localStorage.getItem('token'))
    {
        setToken(localStorage.getItem('token') || '');
    }

    return axios.post(path);
}

function put(path: string, body: any) {
    if(!axios.defaults.headers['Authorization'] && localStorage.getItem('token'))
    {
        setToken(localStorage.getItem('token') || '');
    }

    return axios.put(path, body);
}

function remove(path: string, body: any) {
    if(!axios.defaults.headers['Authorization'] && localStorage.getItem('token'))
    {
        setToken(localStorage.getItem('token') || '');
    }
    
    return axios.delete(path);
}

async function setToken(token: string) {
    axios.defaults.headers['Authorization'] = `Bearer ${token}`;
}

export default {
    get, post, postNoParam, put, remove, setToken
}