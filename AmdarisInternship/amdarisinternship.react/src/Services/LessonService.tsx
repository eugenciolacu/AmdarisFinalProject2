import http from './http';
const authPath = 'Lesson';

async function getLessons(promotionId: number): Promise<any> {
    const data = await http.get(`${authPath}/promotion` + promotionId);

    return data;
}

export default {
    getLessons,
}