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
import { AddModuleForm, ModuleGradingForm } from '../../Models/AddModuleForm';
import { useForm } from "react-hook-form";
import ModuleService from "../../Services/ModuleService";

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


export default function ModalModuleForm({ isOpen, setOpen, setTrigger, trigger }: any) {
    const classes = useStyles();

    const [btnDisabled, setBtnDisabled] = useState(true);

    const { register, handleSubmit, errors } = useForm<AddModuleForm>({
        defaultValues: {
            nameM: "",
            moduleGradings: [{
                name: "",
                weight: 0   
            }]
        }
    });

    const onChange = async () => {
        computeSumOfWeights();
    }

    const onSubmit = async () => {
        let inputs = document.getElementsByTagName('input');

        let moduleName: string = '';
        let componentName: Array<string> = new Array(0);
        let componentWeight: Array<string> = new Array(0);

        let i: number = 0;

        for (i = 0; i < inputs.length; i++) {
            if (inputs[i].id.includes('newModuleName', 0)) {
                moduleName = inputs[i].value;
            }

            if (inputs[i].id.includes('componentName', 0)) {
                let tmp1: Array<string> = new Array(componentName.length + 1);
                tmp1 = Object.assign([], componentName);
                tmp1[tmp1.length] = inputs[i].value;
                componentName = tmp1;
            }

            if (inputs[i].id.includes('weight', 0)) {
                let tmp2: Array<string> = new Array(componentWeight.length + 1);
                tmp2 = Object.assign([], componentWeight);
                tmp2[tmp2.length] = inputs[i].value;
                componentWeight = tmp2;
            }
        }

        let mg: Array<ModuleGradingForm> = new Array<ModuleGradingForm>(componentName.length);

        for (i = 0; i < mg.length; i++) {
            mg[i] = {
                name: componentName[i],
                weight: Number(componentWeight[i])
            }
        }

        let data: AddModuleForm = {
            nameM: moduleName,
            moduleGradings: mg
        }

        // console.log(data);

        await ModuleService.addModule(data).then(() => {
            setOpen(false);
            setTrigger(trigger + 1);
        });
    }
    const createModuleExamComponents = (column_1: any, column_2: any) => {
        return { column_1, column_2 }
    }

    const [moduleExamComponents, updateModuleExamComponents]: [Array<any>, any] = useState([
        createModuleExamComponents(
            <>
                <TextField
                    name={"name"}
                    required
                    autoFocus
                    margin="dense"
                    id="componentName"
                    label="Component name"
                    type="text"
                    inputRef={register({
                        required: {
                            value: true,
                            message: "Please fill this field"
                        },
                    })}
                />
                {/* <p>{errors.moduleName?.message}</p> */}
            </>,
            <>
                <TextField
                    name="weight"
                    required
                    autoFocus
                    margin="dense"
                    id="weight"
                    label="Weight 0.0 - 1.0"
                    type="text"
                    onChange={() => { onChange(); }}
                    inputRef={register({
                        required: {
                            value: true,
                            message: "Please fill this field"
                        },
                        pattern: {
                            value: /^(0(\.\d+)?|1(\.0+)?)$/,
                            message: "Provide valid weight"
                        }
                    })}
                />
                {/* {errors.weight && (<div> {errors.weight.message} </div>)} */}
            </>
        ),
    ]);



    const createModuleData = (column_1: any) => {
        return { column_1 };
    }

    const moduleRows = [
        createModuleData(
            <>
                <TextField
                    name="nameM"
                    required
                    autoFocus
                    margin="dense"
                    id="newModuleName"
                    label="Module name"
                    type="text"
                    fullWidth
                    inputRef={register({
                        required: {
                            value: true,
                            message: "Please fill this field",
                        },
                    })}
                />
                {errors.nameM && (<div> {errors.nameM.message} </div>)}
            </>
        ),
    ];

    const addExamComponentRow = () => {
        let i = moduleExamComponents.length;

        let tmp: Array<any> = new Array(moduleExamComponents.length + 1);
        tmp = Object.assign([], moduleExamComponents);
        tmp[moduleExamComponents.length] = createModuleExamComponents(
            <>
                <TextField
                    name={"name" + i}
                    required
                    autoFocus
                    margin="dense"
                    id={"componentName" + i}
                    label="Component name"
                    type="text"
                    inputRef={register({
                        required: {
                            value: true,
                            message: "Please fill this field"
                        },
                    })}
                />
                {/* {errors.componentName && (<div> {errors.componentName.message} </div>)} */}
            </>,
            <>
                <TextField
                    name={"weight" + i}
                    required
                    autoFocus
                    margin="dense"
                    id={"weight" + i}
                    label="Weight 0.0 - 1.0"
                    type="text"
                    onChange={() => { onChange(); }}
                    inputRef={register({
                        required: {
                            value: true,
                            message: "Please fill this field"
                        },
                        pattern: {
                            value: /^(0(\.\d+)?|1(\.0+)?)$/,
                            message: "Provide valid weight"
                        }
                    })}
                />
                {/* {errors.weight && (<div> {errors.weight.message} </div>)} */}
            </>
        );

        updateModuleExamComponents(tmp);
    }

    const removeLastRow = () => {
        let tmp: Array<any> = new Array(moduleExamComponents.length - 1);

        let i: number = 0;

        if (moduleExamComponents.length == 1) {
            return;
        }

        for (i; i < tmp.length; i++) {
            tmp[i] = moduleExamComponents[i];
        }

        updateModuleExamComponents(tmp);
    }

    const computeSumOfWeights = () => {
        let inputs = document.getElementsByTagName('input');

        let counter: number = 0;
        let sum: number = 0;

        let i: number = 0;

        for (i = 0; i < inputs.length; i++) {
            if (inputs[i].id.includes('weight', 0)) {
                counter++;
                sum += parseFloat(inputs[i].value);
            }
        }

        sum = parseFloat(sum.toFixed(2));

        let sumElem = document.getElementById('sum') as HTMLInputElement;
        sumElem.value = (sum as unknown) as string;

        if (sum == 1) {
            setBtnDisabled(false);
        }
        else {
            setBtnDisabled(true);
        }
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
                                        <TableRow key={moduleExamComponents[row]}>
                                            <TableCell component="th" scope="row">
                                                {row.column_1}
                                            </TableCell>
                                            <TableCell align="left">
                                                {row.column_2}
                                            </TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>

                                <TableRow>
                                    <TableCell component="th" scope="row">
                                        <Button
                                            onClick={() => { addExamComponentRow(); }}
                                            variant="contained"
                                            color="primary"
                                            className={classes.submit}
                                        >
                                            Add grading component
                                        </Button>

                                        <span> </span>

                                        <Button
                                            onClick={() => { removeLastRow(); }}
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
                                            disabled={btnDisabled}
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