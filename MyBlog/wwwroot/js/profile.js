$(document).ready(function () {
    $('#dataTable').DataTable({
        "ajax": {
            "url": "/Client/Profile/GetUserPost"
        },
        "columns": [
            {
                "data": {
                    title: "title", id: "id"
                },
                "render": function (data) {
                    return `
                            <a href="#">${data.title}</a>
                            `
                },
                "width": "40%"
            },
            { "data": "content"},
            { "data": "date"},
            { "data": "tags" },
            {
                "data": {
                    id: "id", confirm: "confirm"
                },
                "render": function (data) {
                    if (data.confirm == true) {
                        return `
                            <div class="text-center">
                                <a class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-lock-open"></i>
                                </a>
                            </div>                           
                        `
                    }
                    else {
                        return `
                             <div class="text-center">
                                <a class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-lock"></i>
                                </a>
                            </div>      
                            `
                    }
                }
               
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                             <div class="text-center">
                                <a href="/Client/Profile/Upsert/${data}"class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onClick=Delete("/Client/Profile/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>                           
                            
                           `;
                }
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




