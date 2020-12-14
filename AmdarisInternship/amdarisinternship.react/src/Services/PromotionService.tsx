import http from './http';
const authPath = 'Promotion/';

async function getPromotions(): Promise<any> {
    const data = await http.get(`${authPath}`);

    return data;
}

export default {
    getPromotions,
}