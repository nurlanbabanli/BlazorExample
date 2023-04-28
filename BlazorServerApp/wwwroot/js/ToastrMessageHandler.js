

function ShowToastrSuccess(message) {

    toastrConfiguration();
    toastr.success(message, "Success", { timeOut:5000 })
}

function ShowToastrError(message) {

    toastrConfiguration();
    toastr.error(message, "Error");
}

const toastrConfiguration = function () {
    toastr.options.positionClass = "toast-bottom-right";
    toastr.options.progressBar = true;
    toastr.options.showMethod = 'slideDown';
    toastr.options.hideMethod = 'slideUp';
    toastr.options.closeMethod = 'slideUp';
}

