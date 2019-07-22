$(document).ready(function() {

    window.CreateData = CreateData;
    window.DeleteData = DeleteData;
    window.EditData = EditData;
    window.UpdateData = UpdateData;

    GenerateGridList();


    function GenerateGridList() {

        ResetForm();

        $.ajax({

            type: "GET",
            url: "/api/Countries/GetAll",
            success: function (result) {
                if (result['countries'].length == 0) {
                    $('table').addClass('hidden');
                } else {
                    $('table').removeClass('hidden');
                    $('#tbody').children().remove();

                    $(result['countries']).each(function (i) {
                        var tbody = $('#tbody');
                        var tr = "<tr>";
                        tr += "<td>" + result['countries'][i].id;
                        tr += "<td>" + result['countries'][i].name;
                        tr += "<td>" + "<button class='btn btn-info btn-xs' onclick=EditData(" + result['countries'][i].id + ")>" + "Edit";
                        tr += "<td>" + "<button class='btn btn-danger btn-xs' onclick=DeleteData(" + result['countries'][i].id + ")>" + "Delete";
                        tbody.append(tr);
                    });
                }
            }
        });
    }

    function CreateData() {
        console.log("Haha");
        var formCountry = $('#formCountry').serialize();

         $.ajax({
            type: "POST",
            url: "/api/Countries/Create",
            data: formCountry,
            success: function () {

                     Message("Data successfuly saved.");

                     GenerateGridList();
             },
             error: function () {
                     Message("Data fail to saved.");
             }
          });
    }

    function DeleteData(id) {

        $.ajax({
            type: "DELETE",
            url: "/api/Countries/Delete/" + id,
            success: function () {
                GenerateGridList();
                Message('Delete success!');
            },
            error: function () {
                Message('Delete failed!');
            }
        });
    }

    function EditData(id) {

        if (id != null && id > 0) {

            $.ajax({
                type: "GET",
                url: "/api/Countries/Get/" + id,
                //data: { id: id },
                success: function (result) {

                    $('#id').val(result.id);
                    $('#name').val(result.name);

                    $('#Create').addClass('hidden');
                    $('#Update').removeClass('hidden');
                }
            });
        }
    }

    function UpdateData() {

        var formCountry = $('#formCountry').serialize();

        $.ajax({
            type: "PUT",
            url: "/api/Countries/Update",
            data: formCountry,
            success: function () {

                $('#Create').removeClass('hidden');
                $('#Update').addClass('hidden');

                $('#id').val(0);

                Message('Update success!');

                GenerateGridList();
            },
            error: function () {
                Message('Update failed!');
            }
        });
    }

        function ResetForm() {
            $('#formCountry').each(function () {
                this.reset();
            });
        }

        function Message(text) {
            toastr.success(text)
        }

});