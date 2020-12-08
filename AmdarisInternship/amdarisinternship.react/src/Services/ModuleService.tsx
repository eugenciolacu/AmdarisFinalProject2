import { AddModuleForm } from '../Models/AddModuleForm';

import http from './http';
const authPath = 'Module/';

async function getModules(): Promise<any> {
    const data = await http.get(`${authPath}`);

    return data;
}

async function addModule(data: AddModuleForm): Promise<any> {
    const tmp = await http.post(`${authPath}`, {
        module:  { name: data.nameM },
        moduleGradings: data.moduleGradings
    });
}

export default {
    getModules,
    addModule
}