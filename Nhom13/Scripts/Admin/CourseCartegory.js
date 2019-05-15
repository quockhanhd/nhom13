$(document).ready(function () {
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.keyboard = false;
    $("#dtable").DataTable({
        ajax: {
            dataType: 'json',
            type: 'Post',//server side only works well with type "POST" !!!
            url: '/Admin/Cartegory/GetCartegoryList',
        },
        columns: [
            { data: 'Name', name: 'Course Name' },
            { data: 'Description', name: 'Description' },
            { data: 'Created Date', name: 'Created Date' },
            {
                data: 'ID', render: function (data) {
                    return `

                                <button class="btn btn-sm btn-warning" onclick="fnShowModal(${data})"> Sửa</button>
                                <button class="btn btn-sm btn-danger" onclick="Delete(${data})"> Xóa</button>

                            `;
                }
            }

        ]
    });
    CKEDITOR.replace('iDescription');
});
function Delete(id) {
    $.ajax({
        url: '/Admin/Cartegory/DeleteCartegory',
        dataType: 'json',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            swal("Yes!!", "Xóa thành công!", "success")
            $('#dtable').DataTable().ajax.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError);
        }
    });
}

function Save(id) {
    if (id == 0) {
        Create();
    }
    else {
        Edit();
    }
}

function Create() {
    name = $('#iName').val();
    var des = CKEDITOR.instances.iDescription.getData();
    debugger;
    $.ajax({
        url: '/Admin/Cartegory/AddCartegory',
        type: 'POST',
        data: {name:name, des:des},
        //dataType: false,
        //contentType: false,
        //processData: false,
        success: function (data) {
            swal("Thêm loại khóa học thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#ModalCreate').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }

    });
}
function Edit() {
    id = $('#iID').val();
    name = $('#iName').val();
    var des = CKEDITOR.instances.iDescription.getData();
    debugger;
    $.ajax({
        url: '/Admin/Cartegory/EditCartegory',
        type: 'POST',
        data: {id:id, name: name, des: des },
        //dataType: false,
        //contentType: false,
        //processData: false,
        success: function (data) {
            swal("Sửa loại khóa học thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#ModalCreate').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }

    });
}
function fnShowModal(id) {
    debugger;
    $.ajax({
        url: '/Admin/Cartegory/AddandEditCartegoryModal',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $('#containershow1').html(data);
            $('#AddandEditCartegoryModal').modal('show');
        }
    });
}
