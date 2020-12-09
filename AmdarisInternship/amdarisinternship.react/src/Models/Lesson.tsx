export interface Lesson {
    id: number,
    name: string,
    description: string,
    startTime: Date,
    endTime: Date,
    lecturerId: number,
    moduleId: number,
    promotionId: number,
}