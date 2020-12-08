import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Box from '@material-ui/core/Box';
import Collapse from '@material-ui/core/Collapse';
import IconButton from '@material-ui/core/IconButton';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import KeyboardArrowDownIcon from '@material-ui/icons/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@material-ui/icons/KeyboardArrowUp';
import { ModuleWithModuleGrading } from '../../Models/ModuleWithModuleGrading';
import ModuleService from "../../Services/ModuleService";
import { Module } from '../../Models/Module';
import { ModuleGrading } from '../../Models/ModuleGrading';
import { useEffect } from 'react';
import { useState } from 'react';
import Button from '@material-ui/core/Button';
import ModalModuleForm from './ModalModuleForm';

const useRowStyles = makeStyles({
    root: {
        '& > *': {
            borderBottom: 'unset',
        },
    },
});

function Row(props: { row: ModuleWithModuleGrading }) {
    const { row } = props;
    const [open, setOpen] = React.useState(false);
    const classes = useRowStyles();

    return (
        <React.Fragment>
            <TableRow className={classes.root}>
                <TableCell>
                    <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
                        {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                    </IconButton>
                </TableCell>
                <TableCell component="th" scope="row">
                    {row.module.name}
                </TableCell>
            </TableRow>
            <TableRow>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box margin={1}>
                            <Table size="small" aria-label="purchases">
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Grading component name</TableCell>
                                        <TableCell>Grading component name</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {row.moduleGradings.map((x) => (
                                        <TableRow key={x.id}>
                                            <TableCell component="th" scope="row">
                                                {x.name}
                                            </TableCell>
                                            <TableCell>{x.weight}</TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </React.Fragment>
    );
}


export default function ModuleView() {

    const [isOpen, setOpen] = useState(false);
    const [trigger, setTrigger] = useState(0);

    const [modules, setModules] = useState([] as ModuleWithModuleGrading[]);


    useEffect(() => {
        fetchModules();
    }, [trigger])

    let getData: Array<ModuleWithModuleGrading>;

    async function fetchModules() {
        const data = await ModuleService.getModules();

        getData = new Array(data.data.length);
        let i: number = 0;

        for (i; i < getData.length; i++) {
            getData[i] = new Object as ModuleWithModuleGrading;
            getData[i].module = new Object as Module;
            getData[i].moduleGradings = new Array<ModuleGrading>(data.data[i].moduleGradings.length);
            getData[i].module = data.data[i].module;

            let j: number = 0;
            for (j; j < getData[i].moduleGradings.length; j++) {
                getData[i].moduleGradings[j] = data.data[i].moduleGradings[j];
            }
        }

        console.log(getData);

        setModules(getData);
    }

    return (
        <>
            <TableContainer>
                <Table aria-label="collapsible table">
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            <TableCell>Module name</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {modules.map((x) => (
                            <Row key={x.module.id} row={x} />
                        ))}
                        <TableRow>
                            <TableCell>
                                <Button variant="contained" color="primary" onClick={() => setOpen(true)}>Add module</Button>
                            </TableCell>
                            <TableCell />
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>

            <ModalModuleForm isOpen={isOpen} setOpen={setOpen} setTrigger={setTrigger} trigger={trigger} />
        </>
    );
}
