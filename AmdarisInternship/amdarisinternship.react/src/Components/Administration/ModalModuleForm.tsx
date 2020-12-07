import React, { useState } from "react";
import { Box, makeStyles, Typography } from "@material-ui/core";
import Table from "@material-ui/core/Table/Table";
import TableBody from "@material-ui/core/TableBody/TableBody";
import TableRow from "@material-ui/core/TableRow/TableRow";
import TableCell from "@material-ui/core/TableCell/TableCell";
import TextField from "@material-ui/core/TextField/TextField";
import Button from "@material-ui/core/Button/Button";
import Dialog from "@material-ui/core/Dialog/Dialog";
import DialogTitle from "@material-ui/core/DialogTitle/DialogTitle";
import DialogContent from "@material-ui/core/DialogContent/DialogContent";
import { AddModuleForm } from '../../Models/AddModuleForm';
import { useForm } from "react-hook-form";
import { isRegularExpressionLiteral } from "typescript";
import { useEffect } from "react";

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
}));











function computeSumOfWeights () {
    let inputs = document.getElementsByTagName('input');
    
    let counter : number = 0;
    let sum : number = 0;

    let i : number = 0;

    for(i = 0; i < inputs.length; i++)
    {
        if (inputs[i].id.includes('weight', 0))
        {
            counter++;
            sum += parseFloat (inputs[i].value);
        }
    }

    sum = parseFloat(sum.toFixed(2));

    let sumElem = document.getElementById('sum') as HTMLInputElement;
    sumElem.value = (sum as unknown) as string;

    if (sum != 1)
    { 
        alert ("not ok");
    }
}





export default function ModalModuleForm({ isOpen, setOpen }: any) {
    const classes = useStyles();

    const [counter, setCount] = useState(1);

    // useEffect (() => {
        
    // })

    console.log(";oeirhg;ewhrg;hqrghrq;oguh");

    const { register, handleSubmit, errors } = useForm<AddModuleForm>({
        defaultValues: {
            name: "",
        }
    });

    const onSubmit = async () => {
        computeSumOfWeights();
    }





    const createModuleData = (column_1: any) => {
        return { column_1 };
    }
    
    const moduleRows = [
        createModuleData(
            <TextField
                required
                autoFocus
                margin="dense"
                id="newModuleName"
                label="Module name"
                name = "name"
                type="text"
                fullWidth
                inputRef = {register({
                    required: {
                        value: true,
                        message: "Please fill this field",
                    },
                })}
            />
        ),
    ];

    const createModuleExamComponents = (column_1: any, column_2: any) => {
        return { column_1, column_2 }
    }
    
    let moduleExamComponents = [
        createModuleExamComponents(
            <TextField
                autoFocus
                margin="dense"
                id="examComponentName"
                label="Component name"
                type="text"
                fullWidth
            />,
            <TextField
                autoFocus
                margin="dense"
                id="weight"
                label="Weight 0.0 - 1.0"
                type="text"
                fullWidth
            />
        ),
    ];

    const addExamComponentRow = (i: number) => {
        let tmp: Array<any> = new Array(moduleExamComponents.length + 1);
        tmp = Object.assign([], moduleExamComponents);
        tmp[moduleExamComponents.length] = createModuleExamComponents(
            <TextField
                autoFocus
                margin="dense"
                id={"examComponentName" + i}
                label="Component name"
                type="text"
            />,
            <TextField
                autoFocus
                margin="dense"
                id={"weight" + i}
                label="Weight 0.0 - 1.0"
                type="text"
                fullWidth
            />
        );
    
        moduleExamComponents = tmp;
    }
    
    const removeLastRow = () => {
        let tmp: Array<any> = new Array(moduleExamComponents.length - 1);
        
        let i: number = 0;
    
        if (moduleExamComponents.length == 1)
        {
            return;
        }
    
        for (i; i < tmp.length; i++)
        {
            tmp[i] = moduleExamComponents[i];
        }
    
        moduleExamComponents = tmp;
    }

    





    return (
        <Dialog
            open={isOpen}
            onClose={() => setOpen(false)}
            aria-labelledby="form-dialog-title"
            maxWidth='xl'
        >

            <DialogTitle id="form-dialog-title">Add module form</DialogTitle>

            <DialogContent style={{ height: 800, width: 1000 }}>

                <div className={classes.paper}>

                    <form className={classes.form} onSubmit={handleSubmit(onSubmit)}>

                        <Box className={classes.box}>
                            <Table className={classes.table} size="small" aria-label="a dense table">
                                <TableBody>
                                    {moduleRows.map((row) => (
                                        <TableRow key={row.column_1}>
                                            <TableCell component="th" scope="row">
                                                {row.column_1}
                                                {errors.name && (<div> {errors.name.message} </div>)}
                                            </TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>

                            <Typography>
                                Module Exam Components. Sum of weights should be equal to 1
                            </Typography>

                            <Table className={classes.table} size="small" aria-label="a dense table">
                                <TableBody>
                                    {moduleExamComponents.map((row) => (
                                        <TableRow key={row.column_1}>
                                            <TableCell component="th" scope="row">
                                                {row.column_1}
                                            </TableCell>
                                            <TableCell align="left">
                                                {row.column_2}
                                            </TableCell>
                                        </TableRow>
                                    ))}
                                    {/* , {console.log(moduleExamComponents.length)} */}
                                </TableBody>

                                <TableRow>
                                    <TableCell component="th" scope="row">
                                        <Button
                                            onClick={() => { addExamComponentRow(counter); setCount(counter + 1); }}
                                            variant="contained"
                                            color="primary"
                                            className={classes.submit}
                                        >
                                            Add grading component
                                        </Button>

                                        <span> </span>

                                        <Button
                                            onClick={() => { removeLastRow(); setCount(counter - 1); }}
                                            variant="contained"
                                            color="secondary"
                                            className={classes.submit}
                                        >
                                            Remove last
                                        </Button>
                                    </TableCell>
                                    <TableCell align="left">
                                        <TextField
                                            autoFocus
                                            margin="dense"
                                            id={"sum"}
                                            type="text"
                                            disabled
                                            fullWidth
                                        />
                                    </TableCell>
                                </TableRow>

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

        </Dialog>

    );
}