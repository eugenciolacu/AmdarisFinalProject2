import { Lesson } from "./Lesson";
import { Attachment } from "./Attachment";
import { User } from "./User";

export interface LessonWithAttachments {
    lesson: Lesson,
    attachments: Attachment[],
    user: User,
}