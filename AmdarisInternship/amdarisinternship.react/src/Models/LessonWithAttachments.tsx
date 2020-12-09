import { Lesson } from "./Lesson";
import { Attachment } from "./Attachment";

export interface LessonWithAttachments {
    lesson: Lesson,
    attachments: Attachment[],
}