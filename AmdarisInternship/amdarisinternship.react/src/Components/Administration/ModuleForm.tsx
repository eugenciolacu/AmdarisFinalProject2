import React, { Component, useState } from "react";
import { Box, Collapse, makeStyles, TableContainer, Typography } from "@material-ui/core";
import Paper from "@material-ui/core/Paper/Paper";
import Table from "@material-ui/core/Table/Table";
import TableBody from "@material-ui/core/TableBody/TableBody";
import TableRow from "@material-ui/core/TableRow/TableRow";
import TableCell from "@material-ui/core/TableCell/TableCell";
import TextField from "@material-ui/core/TextField/TextField";
import Button from "@material-ui/core/Button/Button";
import { isRegularExpressionLiteral } from "typescript";

const useStyles = makeStyles({
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

    redColor: {
        color: "#FF0000",
    }
});

function createModuleData(column_1: any, column_2: any) {
    return { column_1, column_2 };
}

const moduleRows = [
    createModuleData(
        'Module Name *',
        <TextField id="newModuleName" label="Module name" variant="outlined" />
    ),
];

function createModuleExamComponents(column_1: any, column_2: any, column_3: any, column_4: any) {
    return { column_1, column_2, column_3, column_4 }
}

let moduleExamComponents = [
    createModuleExamComponents(
        'Exam component name *',
        <TextField id="examComponentName" label="Component name" variant="outlined" />,
        'Weight *',
        <TextField id="weight" label="Weight 0.0 - 1.0" variant="outlined" />
    ),
];

function addExamComponentRow(i: number) {
    let tmp: Array<any> = new Array(moduleExamComponents.length + 1);
    tmp = Object.assign([], moduleExamComponents);
    tmp[moduleExamComponents.length] = createModuleExamComponents(
        'Exam component name *',
        <TextField id={"examComponentName" + i} label="Component name" variant="outlined" />,
        'Weight *',
        <TextField id={"weight" + i} label="Weight 0.0 - 1.0" variant="outlined" />
    );

    moduleExamComponents = tmp;
}

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
    
    debugger;

    if (sum != 1)
    {
        alert ('Sum of weights should be 1');
    }

    let sumElem = document.getElementById('sum') as HTMLInputElement;
    sumElem.value = (sum as unknown) as string;
}

export default function ModuleForm() {
    const [counter, setCount] = useState(1);

    const [someUselessValue, setValue] = React.useState(0);
    const handleChange = (event: React.ChangeEvent<{}>, newValue: number) => {
        setValue(someUselessValue);
        
        computeSumOfWeights();
    };

    const classes = useStyles();

    return (
        <Box className={classes.box}>
            <Typography>
                Add module
            </Typography>

            <Typography>
                Please complete the form. Mandatory fields are marked with a *
            </Typography>

            {/* <TableContainer component={Paper}> */}
                <Table className={classes.table} size="small" aria-label="a dense table">
                    <TableBody>
                        {moduleRows.map((row) => (
                            <TableRow key={row.column_1}>
                                <TableCell component="th" scope="row">
                                    {row.column_1}
                                </TableCell>
                                <TableCell align="left">
                                    {row.column_2}
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            {/* </TableContainer> */}

            <Typography>
                Module Exam Components
            </Typography>

            <Typography>
                Sum of weights should be equal to 1
            </Typography>

            {/* <TableContainer component={Paper}> */}
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
                                <TableCell align="left">
                                    {row.column_3}
                                </TableCell>
                                <TableCell align="left">
                                    {row.column_4}
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>

                    <TableRow>
                        <TableCell component="th" scope="row">
                            <Button onClick={() => { addExamComponentRow(counter); setCount(counter + 1);}} variant="contained">Add new component</Button>
                        </TableCell>
                        <TableCell align="left">
                            
                        </TableCell>
                        <TableCell align="left">
                            Sum
                        </TableCell>
                        <TableCell align="left">
                            <TextField id="sum" /* label="Sum" */ variant="outlined" disabled />
                        </TableCell>
                    </TableRow>

                    <TableRow>
                        <TableCell component="th" scope="row">
                        <Button onClick={() => { computeSumOfWeights(); }} variant="contained">Save</Button>
                        </TableCell>
                        <TableCell align="left">
                            
                        </TableCell>
                        <TableCell align="left">
                            
                        </TableCell>
                        <TableCell align="left">
                            
                        </TableCell>
                    </TableRow>
                </Table>
            {/* </TableContainer> */}
        </Box>

        
    );
}