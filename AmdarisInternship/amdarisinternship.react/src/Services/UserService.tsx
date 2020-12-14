import http from './http';
const authPath = 'Account/GetLecturers/';

async function getLecturers(): Promise<any> {
    const data = await http.get(`${authPath}`);

    return data;
}

export default {
    getLecturers,
}