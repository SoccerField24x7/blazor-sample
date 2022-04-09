window.ShowToastr = (type, message) => {
    if (type === 'success') {
        toastr.success(message, 'Operation Successful');
    }
    if (type === 'error') {
        toastr.error(message, 'Operation Failed');
    }
}

window.ShowModal = (type , header, message) => {
    Swal.fire(
        header,
        message,
        type
    );
}

function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}