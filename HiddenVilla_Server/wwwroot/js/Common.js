window.ShowToastr = (type, message) => {
    if (type == "success") {
        toastr.success(message, "Operation Successful");
    }
    if (type == "error") {
        toastr.error(message, "Operation Failed");
    }
}

window.ShowSwal = (type, message) => {
    if (type == "success") {
        Swal.fire(
            'Success Notification',
            message,
            'success'
        )
    }
    if (type == "error") {
        Swal.fire(
            'Error Notification',
            message,
            'error'
        )
    }
}

function ShowDeleteConfirmationModel() {
    $('#deleteConfirmationModel').modal('show');
}

function HideDeleteConfirmationModel() {
    $('#deleteConfirmationModel').modal('hide');
}




function triggerFileUpload() {
    let fileInput = document.createElement('input');
    fileInput.type = 'file';
    fileInput.onchange = function (event) {
        let file = event.target.files[0];
    };
    fileInput.click();
}