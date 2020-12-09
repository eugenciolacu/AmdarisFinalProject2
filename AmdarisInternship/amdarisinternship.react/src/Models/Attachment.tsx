export interface Attachment {
    id: number,
    attachmentExtension: string,
    attachmentName: string,
    attachment: Uint8Array[],
    lessonId: number,
}