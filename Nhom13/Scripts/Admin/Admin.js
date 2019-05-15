$(document).ready(function () {
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.keyboard = false;
    $("#dtable").DataTable({
        ajax: {
            dataType: 'json',
            type: 'Post',//server side only works well with type "POST" !!!
            url: '/Admin/User/GetUserList',
        },
        columns: [
            { data: 'GroupID', name: 'GroupID' },
            { data: 'Name', name: 'Name' },
            { data: 'Email', name: 'Email' },
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
});
function Delete(id) {
    $.ajax({
        url: '/Admin/User/DeleteUser',
        dataType: 'json',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            if (data.data == true) {
                swal("Yes!!", "Xóa thành công!", "success")
                $('#dtable').DataTable().ajax.reload();
            }
            else {
                swal("Xóa không thành công!", "error")
                alert("Xóa không thành công!");
                $('#dtable').DataTable.ajax.reload();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "success");
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
    username = $('#iUserName').val();
    pass = $('#iPassword').val();
    name = $('#iName').val();
    email = $('#iEmail').val();
    group = $('#iGroupID').val();
   
    $.ajax({
        url: '/Admin/User/AddUser',
        type: 'POST',
        data: { userN:username, pass:pass, name: name, email: email, group:group },
        dataType: 'json',
        success: function (data) {
            swal("Thêm thành viên không thành công!", "success");
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
    username = $('#iUserName').val();
    pass = $('#iPassword').val();
    name = $('#iName').val();
    email = $('#iEmail').val();
    group = $('#iGroup').val();

    $.ajax({
        url: '/Admin/User/EditUser',
        type: 'POST',
        data: {id:id, userN: username, pass: pass, name: name, email: email, group:group },
        dataType: 'json',
        success: function (data) {
            swal("Sửa thành viên thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#ModalCreate').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }

    });
}
function LoadUserGroup() {
    
    $.ajax({
        url: '/Admin/User/LoadGroup',
        type: 'GET',
        success: function (data) {
            
            html = "";
            $.each(data.data, function (i, row) {
                html += "<option value=" + row.ID + ">" + row.Name + "</option>";
            });
            $('#iGroup').html(html);
        }
    });

}
function fnShowModal(id) {
    
    $.ajax({
        url: '/Admin/User/AddandEditUserModal',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $('#containershow1').html(data);
            $('#AddandEditUserModal').modal('show');
            LoadUserGroup();
        }
    });
}