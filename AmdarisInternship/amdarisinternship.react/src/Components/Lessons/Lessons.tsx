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
import Paper from '@material-ui/core/Paper/Paper';
import Card from '@material-ui/core/Card/Card';


const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            flexGrow: 1,
            marginTop: theme.spacing(1),
        },

        card: {
            backgroundColor: 'red',
            height: '100%',
            display: 'flex',
            flexDirection: 'column',
        },
        cardMedia: {
            paddingTop: '56.25%', // 16:9
        },
        cardContent: {
            flexGrow: 1,
        },
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
            <ButtonAppBar />

            <Grid container className={classes.root} spacing={2}>
                <Grid item xs={12}>
                    <Grid container justify="center" spacing={2}>
                        {lessons.map((x) => (
                            <Grid key={x.lesson.id} item>
                                <Card className={classes.card}>
                                    <CardContent className={classes.cardContent}>
                                        <Typography variant="h6">
                                            <b> {x.lesson.name}</b>
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