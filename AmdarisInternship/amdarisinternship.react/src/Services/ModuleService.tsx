import http from './http';
const authPath = 'Module/';

async function getModules() : Promise<any>
{
    const data = await http.get(`${authPath}`);

    return data;
}

export default {
    getModules,
}