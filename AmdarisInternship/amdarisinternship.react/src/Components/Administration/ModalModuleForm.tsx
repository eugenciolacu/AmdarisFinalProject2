import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import Reactfrom from 'react';
import React from 'react';

export default function ModalModuleForm({isOpen, setOpen}: any) {

    return (
        <Dialog
            open={isOpen}
            onClose={() => setOpen(false)}
        >
            <DialogContent style={{height: 600}}>
                klhklhljkh
                erthetjhej
            </DialogContent>
        </Dialog>
    );
}