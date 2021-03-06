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
                    {row.module.name}
                </TableCell>
                <TableCell>


                    {/* <Button variant="contained" color="secondary" onClick={() => setOpenModalForm(true)}> Edit </Button> */}
                    {/* create similar form for edit module */}


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
        </>
    );
}


export default function ModuleView() {
    const [isOpen, setOpenModalForm] = useState(false);
    const [trigger, setTrigger] = useState(0);

    const [modules, setModules] = useState([] as ModuleWithModuleGrading[]);


    useEffect(() => {
        fetchModules();
    }, [trigger])

    let gotModules: Array<ModuleWithModuleGrading>;

    async function fetchModules() {
        const data = await ModuleService.getModules();

        gotModules = new Array(data.data.length);
        let i: number = 0;

        for (i; i < gotModules.length; i++) {
            gotModules[i] = new Object as ModuleWithModuleGrading;
            gotModules[i].module = new Object as Module;
            gotModules[i].moduleGradings = new Array<ModuleGrading>(data.data[i].moduleGradings.length);
            gotModules[i].module = data.data[i].module;

            let j: number = 0;
            for (j; j < gotModules[i].moduleGradings.length; j++) {
                gotModules[i].moduleGradings[j] = data.data[i].moduleGradings[j];
            }
        }

        setModules(gotModules);
    }

    return (
        <>
            <TableContainer>
                <Table aria-label="collapsible table">
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            <TableCell>Module name</TableCell>
                            <TableCell />
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {modules.map((x) => (
                            <Row key={x.module.id} row={x} />
                        ))}
                        <TableRow>
                            <TableCell>
                                <Button variant="contained" color="primary" onClick={() => setOpenModalForm(true)}>Add module</Button>
                            </TableCell>
                            <TableCell />
                            <TableCell />
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>

            <ModalModuleForm isOpen={isOpen} setOpen={setOpenModalForm} setTrigger={setTrigger} trigger={trigger} />
        </>
    );
}
