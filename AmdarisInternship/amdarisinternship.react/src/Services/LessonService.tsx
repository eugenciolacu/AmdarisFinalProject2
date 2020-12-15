import { LessonWithAttachments } from '../Models/LessonWithAttachments';
import http from './http';

const authPath = 'Lesson';

async function getLessons(promotionId: number): Promise<any> {
    const data = await http.get(`${authPath}/promotion` + promotionId);

    return data;
}

async function addLesson(data: LessonWithAttachments): Promise<any> {
    await http.post(`${authPath}`, data);
}

export default {
    getLessons,
    addLesson,
}