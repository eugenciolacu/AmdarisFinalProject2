export interface AddLessonForm {
    name: string;
    description: string;
    startTime: Date;
    endTime: Date;
    lecturerId: number;
    moduleId: number;
    promotionId: number;
    attachments: AttachmentForm[];
}

export interface AttachmentForm {
    attachmentExtension: string,
    attachmentName: string,
    attachment: Uint8Array[],
    lessonId: number,
}