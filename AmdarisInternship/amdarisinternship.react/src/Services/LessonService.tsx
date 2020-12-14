import { AddLessonForm, AttachmentForm } from '../Models/AddLessonForm';
import http from './http';

const authPath = 'Lesson';

async function getLessons(promotionId: number): Promise<any> {
    const data = await http.get(`${authPath}/promotion` + promotionId);

    return data;
}

async function addLesson(data: AddLessonForm ): Promise<any> {
    await http.post(`${authPath}`, data);
}

export default {
    getLessons,
    addLesson,
}