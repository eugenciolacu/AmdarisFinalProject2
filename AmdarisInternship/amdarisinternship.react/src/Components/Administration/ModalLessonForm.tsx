import React from 'react';
import { Button, makeStyles, TextField } from "@material-ui/core";
import { useState } from "react";
import Dialog from '@material-ui/core/Dialog/Dialog';
import DialogTitle from '@material-ui/core/DialogTitle/DialogTitle';
import DialogContent from '@material-ui/core/DialogContent/DialogContent';
import { AddLessonForm, AttachmentForm } from '../../Models/AddLessonForm';
import { useForm } from 'react-hook-form';
import Box from '@material-ui/core/Box/Box';
import Table from '@material-ui/core/Table/Table';
import TableBody from '@material-ui/core/TableBody/TableBody';
import TableRow from '@material-ui/core/TableRow/TableRow';
import TableCell from '@material-ui/core/TableCell/TableCell';
import Grid from '@material-ui/core/Grid/Grid';
import Autocomplete from '@material-ui/lab/Autocomplete';

const useStyles = makeStyles((theme) => ({
    paper: {
        margin: 'auto',
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        width: 800,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(1),
    },
    table: {
        minWidth: 650,
        maxWidth: 850,
        borderCollapse: "collapse",
        margin: "auto",
    },

    box: {
        maxWidth: 850,
        margin: "auto",
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
    textField: {
        marginLeft: theme.spacing(0),
        marginRight: theme.spacing(0),
        width: 250,
    },
}))


export default function ModalLessonForm({ isOpen, setOpen, setTrigger, trigger }: any) {
    const classes = useStyles();

    const [btnDisabled, setBtnDisabled] = useState(true);

    const { register, handleSubmit, errors } = useForm<AddLessonForm>({
        defaultValues: {
            name: "",
            description: "",
            startTime: "",
            endTime: "",
            lecturerId: 0,
            moduleId: 0,
            promotionId: 0,
            attachments: [{
                attachmentExtension: "",
                attachmentName: "",
                attachment: undefined,
                lessonId: 0,
            }]
        }
    });

    const lecturers = [
        { fullName: 'Test 1', id: 1 },
        { fullName: 'Test 2', id: 2 },
    ]

    const modules = [
        { moduleName: 'Test module name 1', id: 1 },
        { moduleName: 'Test module name 2', id: 2 },
    ]

    const promotions = [
        { promotionName: 'Test promotion name 1', id: 1 },
        { promotionName: 'Test promotion name 2', id: 2 },
    ]

    const onSubmit = async () => {
        alert("ok");
    }

    const createLessondata = (column_1: any) => {
        return { column_1 };
    }

    const lessonRow = [
        createLessondata(
            <>
                <TextField
                    name="name"
                    required
                    autoFocus
                    margin="dense"
                    id="lessonName"
                    label="Lesson Name"
                    type="text"
                    fullWidth
                    inputRef={register({
                        required: {
                            value: true,
                            message: "Please fill this field",
                        },
                    })}
                />
                {errors.name && (<div> {errors.name.message} </div>)}

                <TextField
                    name="description"
                    required
                    autoFocus
                    margin="dense"
                    id="lessonDescription"
                    label="Lesson Description"
                    type="text"
                    fullWidth
                    inputRef={register({
                        required: {
                            value: true,
                            message: "Please fill this field",
                        },
                    })}
                />

                <Grid container justify={'center'}>
                    <TextField
                        name="startTime"
                        required
                        id="lessonStartTime"
                        label="Start time"
                        type="datetime-local"
                        className={classes.textField}
                        InputLabelProps={{
                            shrink: true,
                        }}
                    />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <TextField
                        name="endTime"
                        required
                        id="lessonEndTime"
                        label="End time"
                        type="datetime-local"
                        className={classes.textField}
                        InputLabelProps={{
                            shrink: true,
                        }}
                    />
                </Grid>

                <Autocomplete
                    options={lecturers}
                    getOptionLabel={(option) => option.fullName}
                    renderInput={(params) => <TextField {...params}
                        name="lecturer"
                        required
                        autoFocus
                        id="lessonLecturer"
                        label="Lecturer"
                        fullWidth
                    />}
                />

                <Autocomplete
                    options={modules}
                    getOptionLabel={(option) => option.moduleName}
                    renderInput={(params) => <TextField {...params}
                        name="module"
                        required
                        autoFocus
                        id="lessonModule"
                        label="Module"
                        fullWidth
                    />}
                />

                <Autocomplete
                    options={promotions}
                    getOptionLabel={(option) => option.promotionName}
                    renderInput={(params) => <TextField {...params}
                        name="promotion"
                        required
                        autoFocus
                        id="lessonPromotion"
                        label="Promotion"
                        fullWidth
                    />}
                />
            </>
        )
    ];

    return (
        <Dialog
            open={isOpen}
            onClose={() => setOpen(false)}
            aria-labelledby="form-dialog-title"
            maxWidth='xl'
        >

            <DialogTitle id="form-dialog-title">Add lesson form</DialogTitle>

            <DialogContent style={{ height: 800, width: 1000 }}>

                <div className={classes.paper}>
                    <form className={classes.form} onSubmit={handleSubmit(onSubmit)}>
                        <Box className={classes.box}>
                            <Table className={classes.table} size="small" aria-label="a dense table">
                                <TableBody>
                                    {lessonRow.map((row) => (
                                        <TableRow key={row.column_1}>
                                            <TableCell component="th" scope="row">
                                                {row.column_1}
                                            </TableCell>
                                        </TableRow>
                                    ))}

                                    <TableRow>
                                        <Button
                                            type="submit"
                                            variant="contained"
                                            color="primary"
                                            className={classes.submit}
                                        >
                                            Add lesson
                                        </Button>
                                    </TableRow>
                                </TableBody>
                            </Table>
                        </Box>
                    </form>
                </div>

            </DialogContent>
        </Dialog>
    );
}