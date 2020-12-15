import React from 'react';
import { Dialog, makeStyles } from "@material-ui/core";
import { DialogTitle } from '@material-ui/core';
import { DialogContent } from '@material-ui/core';
import { useState } from 'react';
import { Box } from '@material-ui/core';
import { Table } from '@material-ui/core';
import { TableRow } from '@material-ui/core';
import { TableCell } from '@material-ui/core';
import { Button } from '@material-ui/core';

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

export default function ModalPromotionForm({ isOpen, setOpenModalForm, setTrigger, trigger }: any) {
    const classes = useStyles();


    const onSubmit = async () => {
        setOpenModalForm(false);
        setTrigger(trigger + 1);
    }

    return (
        <Dialog
            open={isOpen}
            onClose={() => setOpenModalForm(false)}
            aria-labelledby="form-dialog-title"
            maxWidth='xl'
        >

            <DialogTitle id="form-dialog-title">Add promotion form</DialogTitle>

            <DialogContent style={{ height: 800, width: 1000 }}>

                <div className={classes.paper}>

                    <form className={classes.form} onSubmit={() => onSubmit()}>

                        <Box className={classes.box}>

                            <Table className={classes.table} size="small" aria-label="a dense table">
                                <TableRow>
                                    <TableCell>
                                        <Button
                                            type="submit"
                                            variant="contained"
                                            color="primary"
                                            className={classes.submit}
                                        >
                                            Add module with grading system
                                        </Button>
                                    </TableCell>
                                    <TableCell align="left"></TableCell>
                                </TableRow>
                            </Table>
                        </Box>



                    </form>
                </div>
            </DialogContent>

        </Dialog >
    )
}