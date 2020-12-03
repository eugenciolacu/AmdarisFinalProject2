import React, { useEffect, useState } from "react";
import { makeStyles } from "@material-ui/core";
import ModuleService from "../../Services/ModuleService";
import { ModuleWithModuleGrading } from "../../Models/ModuleWithModuleGrading";
import { Module } from "../../Models/Module";
import { ModuleGrading } from "../../Models/ModuleGrading";
import Typography from "@material-ui/core/Typography/Typography";
import Table from "@material-ui/core/Table/Table";
import TableHead from "@material-ui/core/TableHead/TableHead";
import TableRow from "@material-ui/core/TableRow/TableRow";
import TableCell from "@material-ui/core/TableCell/TableCell";
import TableBody from "@material-ui/core/TableBody/TableBody";
import IconButton from "@material-ui/core/IconButton/IconButton";
import KeyboardArrowDownIcon from '@material-ui/icons/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@material-ui/icons/KeyboardArrowUp';


const useStyles = makeStyles((theme) => ({
    seeMore: {
        marginTop: theme.spacing(3),
    },
}));




export default function ModuleForm() {
    const classes = useStyles();

    const [open, setOpen] = React.useState(false);

    const [modules, setModules] = useState([] as ModuleWithModuleGrading[]);

    useEffect(() => {
        fetchModules();
    }, [])

    let getData: Array<ModuleWithModuleGrading>;

    async function fetchModules() {
        const data = await ModuleService.getModules();
        // setModules(data);

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
            {/* {setModules} */}







            <React.Fragment>
                <Typography component="h2" variant="h6" color="primary" gutterBottom>
                    Table name
                </Typography>
                <Table size="small">
                    <TableHead>
                        <TableRow>
                            <TableCell>Module name</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {modules.map((x) => (
                            <TableRow key={x.module.id}>
                                {/* <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
                                    {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                                </IconButton> */}
                                <TableCell>{x.module.name}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </React.Fragment>






        </>
    );
}