import React, { useEffect } from 'react';
import { TableCell } from '@material-ui/core';
import { makeStyles } from '@material-ui/core';
import TableRow from '@material-ui/core/TableRow';
import { TableContainer } from '@material-ui/core';
import { Table } from '@material-ui/core';
import { TableHead } from '@material-ui/core';
import { TableBody } from '@material-ui/core';
import { Button } from '@material-ui/core';
import { useState } from 'react';
import { Promotion } from '../../Models/Promotion';
import PromotionService from '../../Services/PromotionService';
import ModalPromotionForm from '../Administration/ModalPromotionForm';

const useRowStyles = makeStyles({
    root: {
        '& > *': {
            borderBottom: 'unset',
        },
    },
});


export default function PromotionView() { 
    const classes = useRowStyles();

    const [isOpen, setOpenModalForm] = useState(false);
    const [trigger, setTrigger] = useState(0);

    const [promotions, setPromotions] = useState([] as Promotion[]);

    useEffect(() => {
        fetchPromotions();
    }, [trigger]);

    async function fetchPromotions() {
        const data = await PromotionService.getPromotions();

        setPromotions(data.data);
    }

    return (
        <>
            <TableContainer>
                <Table aria-label="collapsible table">
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            <TableCell>Promotion Name</TableCell>
                            <TableCell>Start date</TableCell>
                            <TableCell>End date</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {promotions.map(({name, startDate, endDate}) => (
                            <TableRow className={classes.root}>
                                <TableCell />
                                <TableCell component="th" scope="row">
                                    {name}
                                </TableCell>
                                <TableCell>
                                    {startDate}
                                </TableCell>
                                <TableCell>
                                    {endDate}
                                </TableCell>
                            </TableRow>
                        ))}
                        <TableRow>
                            <TableCell>
                                <Button variant="contained" color="primary" onClick={() => setOpenModalForm(true)}>Add promotion</Button>
                            </TableCell>
                            <TableCell />
                            <TableCell />
                            <TableCell />
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>

            <ModalPromotionForm isOpen={isOpen} setOpen={setOpenModalForm} setTrigger={setTrigger} trigger={trigger} />
        </>
    );
}