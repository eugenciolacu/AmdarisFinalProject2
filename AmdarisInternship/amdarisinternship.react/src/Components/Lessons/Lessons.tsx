import React from 'react';
import { useEffect } from 'react';
import { CardContent, CardMedia, makeStyles, Typography } from '@material-ui/core';
import { Theme } from '@material-ui/core/styles/createMuiTheme';
import ButtonAppBar from '../NavBar/NavBar';
import createStyles from '@material-ui/styles/createStyles/createStyles';
import LessonService from '../../Services/LessonService';
import { Lesson } from '../../Models/Lesson';
import { Attachment } from '../../Models/Attachment';
import { LessonWithAttachments } from '../../Models/LessonWithAttachments';
import { useState } from 'react';
import Grid from '@material-ui/core/Grid/Grid';
import Card from '@material-ui/core/Card/Card';


const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            flexGrow: 1,
            marginTop: theme.spacing(1),
        },
        card: {
            width: '300px',
            height: '100%',
            display: 'flex',
            flexDirection: 'column',
            overflow: 'auto',
        },
        cardMedia: {
            paddingTop: '56.25%', // 16:9
        },
        cardContent: {
            flexGrow: 1,
        },
        lessonInfo: {
            fontSize: '20px',
        }
    }),
);

export default function Lessons() {
    const classes = useStyles();

    const [lessons, setLessons] = useState([] as LessonWithAttachments[]);

    useEffect(() => {
        fetchLessons(1);
    }, [])

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

    function getDateInfo(start: Date, end: Date): string {
        let dateInfo: string = "";

        let startDate: Date = new Date(start);
        let endDate: Date = new Date(end);

        const dateFormatter = new Intl.DateTimeFormat('en', { year: 'numeric', month: 'short', day: '2-digit' });

        const timeFormatter = new Intl.DateTimeFormat('en', { hour: '2-digit', minute: '2-digit', hour12: false });

        dateInfo += dateFormatter.format(startDate).toString();
        dateInfo += '\n' + 'From: ';
        dateInfo += timeFormatter.format(startDate).toString();
        dateInfo += '\n' + 'To: ';
        dateInfo += timeFormatter.format(endDate).toString();

        return dateInfo;
    }

    return (
        <>
            <ButtonAppBar />

            <Grid container className={classes.root} spacing={2}>
                <Grid item xs={12}>
                    <Grid container justify="center" spacing={2}>
                        {lessons.map((x) => (
                            <Grid key={x.lesson.id} item>
                                <Card className={classes.card}>
                                    <CardContent className={classes.cardContent}>
                                        <Typography className={classes.lessonInfo}>
                                            {x.lesson.name}
                                        </Typography>
                                        <Typography className={classes.lessonInfo}>
                                            Short description
                                        </Typography>
                                        <Typography className={classes.lessonInfo}>
                                            {x.lesson.description}
                                        </Typography>
                                        <Typography component ="pre" className={classes.lessonInfo}>
                                            {getDateInfo(x.lesson.startTime, x.lesson.endTime)}
                                        </Typography>
                                        <Typography className={classes.lessonInfo}>
                                            Lecturer: {x.user.firstName + ' ' + x.user.lastName} 
                                        </Typography>
                                    </CardContent>
                                </Card>
                            </Grid>
                        ))}
                    </Grid>
                </Grid>
            </Grid>
        </>
    );
}