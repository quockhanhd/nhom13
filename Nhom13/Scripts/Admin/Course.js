$(document).ready(function () {
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.keyboard = false;
    $("#dtable").DataTable({
        ajax: {
            dataType: 'json',
            type: 'Post',//server side only works well with type "POST" !!!
            url: '/Admin/Course/GetCourseList',
        },
        columns: [
            { data: 'Name', name: 'Course Name' },
            { data: 'Description', name: 'Description' },
            //{
            //    data: 'Image', name: 'Image', render: function (data) {
            //        return
            //        ` <img src="~/Image/${data}" style="width:150px; height:150px"/>`;
            //    }
            //},
            {
                data: 'Image', render: function (data) {
                    return ` <img src="../../Image/${data}" style="width:100px; height:100px"/>`;
                }
            },
            { data: 'Price', name: 'Price' },
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
        url: '/Admin/Course/DeleteCourse',
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
    var des = CKEDITOR.instances.iDescription.getData();
    var encrypt = encodeURI(des);
    var data = new FormData();
    var files = $("#iImage").get(0).files;

    data.append("Anh", files[0]); 
    data.append("Ten", $('#iName').val());
    data.append("Mota", encrypt); 
    data.append("Gia", $('#iPrice').val()); 

    $.ajax({
        url: '/Admin/Course/AddCourse',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Thêm khóa học thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#ModalCreate').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }

    });
}
function Edit() {
    debugger;
    var des = CKEDITOR.instances.iDescription.getData();
    var encrypt = encodeURI(des);
    var data = new FormData();
    var files = $("#iImage").get(0).files;
    data.append("Anh", files[0]);
    data.append("ID", $('#iID').val());   
    data.append("Ten", $('#iName').val());
    data.append("Mota", encrypt);
    data.append("Gia", $('#iPrice').val());

    $.ajax({
        url: '/Admin/Course/EditCourse',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Sửa khóa học thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#ModalCreate').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }

    });
}
function LoadCartegoryGroup() {

    $.ajax({
        url: '/Admin/Course/LoadCategory',
        type: 'GET',
        success: function (data) {

            html = "";
            $.each(data.data, function (i, row) {
                html += "<option value=" + row.ID + ">" + row.Name + "</option>";
            });
            $('#iCartegory').html(html);
        }
    });
}
function ReadImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#image').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
function fnShowModal(id) {
    $.ajax({
        url: '/Admin/Course/AddandEditCourseModal',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $('#containershow1').html(data);
            $('#AddandEditCourseModal').modal('show');
            LoadCartegoryGroup();
        }
    });
}
