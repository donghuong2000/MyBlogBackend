$(document).ready(function () {
    $('#dataTable').DataTable({
        "ajax": {
            "url": "/Admin/User/GetAll"
        },
        "columns": [
            {
                "data": {
                    Avatar:"avatar",id:"id"
                },
                "render": function (data) {
                    return `
                            <div class="d-flex justify-content-between">
                                <a class="none-line d-flex align-items-center" href="">
                                    <img class="rounded-circle"src="https://via.placeholder.com/150" height="50" width ="50" alt="">
                                </a>
                            </div>
                            `
                },
                "width": "10%"
            },
            { "data": "username", "width": "20%" },
            { "data": "name", "width": "20%" },
            { "data": "phone", "width": "20%" },
            { "data": "mail", "width": "20%" },
            {
                "data": {
                    id: "id", active:"active"
                },
                "render": function (data) {
                    if (data.active == true) {
                        return `
                            <div class="text-center">
                                <a onClick=LockUnlock("${data.id}") class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-lock-open"></i>
                                </a>
                            </div>                           
                        `
                    }
                    else {
                        return `
                             <div class="text-center">
                                <a onClick=LockUnlock("${data.id}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-lock"></i>
                                </a>
                            </div>      
                            `
                    }
                },
                "width": "5%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                             <div class="text-center">
                                <a onClick=Delete("/Admin/User/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>                           
                            
                           `;
                }, "width": "5%"
            }
        ]
    });
});
const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
})
function Delete(url) {
    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        swalWithBootstrapButtons.fire(
                            'Deleted!',
                            'Your file has been deleted.',
                            'success'
                        );
                        $('#dataTable').DataTable().ajax.reload();
                    }
                    else {
                        swalWithBootstrapButtons.fire(
                            'Error',
                            'Can not delete this, maybe it not exit or error from sever',
                            'error'
                        )
                    }
                }

            })
            
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
                    swalWithBootstrapButtons.fire(
                        'Cancelled',
                        'Your record is safe :)',
                        'error'
                    )
        }
    })
}

function LockUnlock(idnum){
    $.ajax({
        type: "POST",
        url: "/Admin/User/Lock",
        data: JSON.stringify(idnum),
        contentType: "application/json",
        success: function (data){
            if (data.success == true) {
                toastr.success(data.message);
                $('#dataTable').DataTable().ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }

    })
}
