import React, { useEffect } from 'react';
import { Button, IconButton, makeStyles, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@material-ui/core';
import ModalLessonForm from '../Administration/ModalLessonForm';
import { Lesson } from '../../Models/Lesson';
import { Attachment } from '../../Models/Attachment';
import { LessonWithAttachments } from '../../Models/LessonWithAttachments';
import { useState } from 'react';
import LessonService from '../../Services/LessonService';
import KeyboardArrowDownIcon from '@material-ui/icons/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@material-ui/icons/KeyboardArrowUp';

const useRowStyles = makeStyles({
    root: {
        '& > *': {
            borderBottom: 'unset',
        },
    },
});

function Row(props: { row: LessonWithAttachments }) {
    const { row } = props;
    const [open, setOpen] = useState(false);

    const [isOpen, setOpenModalForm] = useState(false);
    const [trigger, setTrigger] = useState(0);

    const classes = useRowStyles();

    return (
        <>
            <TableRow className={classes.root}>
                <TableCell>
                    <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
                        {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                    </IconButton>
                </TableCell>
                <TableCell component="th" scope="row">
                    {row.lesson.name}
                </TableCell>
                <TableCell>
                    {row.lesson.description}
                </TableCell>
                <TableCell>
                    {row.lesson.startTime}
                </TableCell>
                <TableCell>
                    {row.lesson.endTime}
                </TableCell>
                <TableCell>
                    {row.user.firstName + ' ' + row.user.lastName}
                </TableCell>
            </TableRow>
        </>
    );
}

export default function LessonView() {
    const [isOpen, setOpenModalForm] = useState(false);
    const [trigger, setTrigger] = useState(0);

    const [lessons, setLessons] = useState([] as LessonWithAttachments[]);

    useEffect(() => {
        fetchLessons(1);
    }, [trigger])

    let gotLessons: Array<LessonWithAttachments>;

    async function fetchLessons(promotionId: number) {
        const data = await LessonService.getLessons(promotionId);

        gotLessons = new Array(data.data.length);
        let i: number = 0;

        for (i; i < gotLessons.length; i++) {
            gotLessons[i] = new Object as LessonWithAttachments;
            gotLessons[i].lesson = new Object as Lesson;
            gotLessons[i].attachments = new Array<Attachment>(data.data[i].attachments.length);
            gotLessons[i].lesson = data.data[i].lesson;
            gotLessons[i].user = data.data[i].user;

            let j: number = 0;
            for (j; j < gotLessons[i].attachments.length; j++) {
                gotLessons[i].attachments[j] = data.data[i].attachments[j];
            }
        }

        console.log(gotLessons);

        setLessons(gotLessons);
    }

    return (
        <>
            <TableContainer>
                <Table aria-label="collapsible table">
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            <TableCell>Name</TableCell>
                            <TableCell>Description</TableCell>
                            <TableCell>Start time</TableCell>
                            <TableCell>End time</TableCell>
                            <TableCell>Lecturer</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {lessons.map((x) => (
                            <Row key={x.lesson.id} row={x} />
                        ))}
                        <TableRow>
                            <TableCell>
                                <Button variant="contained" color="primary" onClick={() => setOpenModalForm(true)}>Add lesson</Button>
                            </TableCell>
                            <TableCell />
                            <TableCell />
                            <TableCell />
                            <TableCell />
                            <TableCell />
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>

            <ModalLessonForm isOpen={isOpen} setOpen={setOpenModalForm} setTrigger={setTrigger} trigger={trigger} />
        </>
    );
}