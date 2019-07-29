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
            url: "/api/Industries/GetAll",
            success: function (result) {
                if (result['industries'].length == 0) {
                    $('table').addClass('hidden');
                } else {
                    $('table').removeClass('hidden');
                    $('#tbody').children().remove();                    

                    $(result['industries']).each(function (i) {
                        var tbody = $('#tbody');
                        var tr = "<tr>";
                        tr += "<td>" + result['industries'][i].id;
                        tr += "<td>" + result['industries'][i].name;
                        tr += "<td>" + result['industries'][i].possition;
                        tr += "<td>" + "<button class='btn btn-info btn-xs' onclick=EditData(" + result['industries'][i].id + ")>" + "Edit";
                        tr += "<td>" + "<button class='btn btn-danger btn-xs' onclick=DeleteData(" + result['industries'][i].id + ")>" + "Delete";
                        tbody.append(tr);
                    });
                }
            }
        });
        
    }

    function CreateData() {

        var formIndustry = $('#formIndustry').serialize();

         $.ajax({
            type: "POST",
            url: "/api/Industries/Create",
            data: formIndustry,
            success: function () {

                     Message("Data successfuly saved.", 'success');

                     GenerateGridList();
             },
             error: function (response) {                     
                var message = "";
                var errors = response["responseJSON"]["errors"];
                var error = response["responseJSON"]["error"];
                if(error !== undefined) {
                    message += error;
                    } else {
                    Object.keys(errors).forEach(function(key) {
                        message += `${key} - ${errors[key]}!<br/>`;                        
                    });
                }
                Message(message);
            }
          });
    }

    function DeleteData(id) {

        $.ajax({
            type: "DELETE",
            url: "/api/Industries/Delete/" + id,
            success: function () {
                GenerateGridList();
                Message('Delete success!', 'success');
            },
            error: function (response) {                     
                var message = "";
                var errors = response["responseJSON"]["errors"];
                var error = response["responseJSON"]["error"];
                if(error !== undefined) {
                    message += error;
                    } else {
                    Object.keys(errors).forEach(function(key) {
                        message += `${key} - ${errors[key]}!<br/>`;                        
                    });
                }
                Message(message);
            }
        });
    }

    function EditData(id) {

        
        if (id != null && id > 0) {
            
            $.ajax({
                type: "GET",
                url: "/api/Industries/Get/" + id,
                //data: { id: id },
                success: function (result) {

                    $('#id').val(result.id);
                    $('#name').val(result.name);
                    $('#possition').val(result.possition);


                    $('#Create').addClass('hidden');
                    $('#Update').removeClass('hidden');
                }
            });
        }
    }

    function UpdateData() {

        var formIndustry = $('#formIndustry').serialize();

        $.ajax({
            type: "PUT",
            url: "/api/Industries/Update",
            data: formIndustry,
            success: function () {

                $('#Create').removeClass('hidden');
                $('#Update').addClass('hidden');

                $('#id').val(0);

                Message('Update success!', 'success');

                GenerateGridList();
            },
            error: function (response) {                     
                var message = "";
                var errors = response["responseJSON"]["errors"];
                var error = response["responseJSON"]["error"];
                if(error !== undefined) {
                    message += error;
                    } else {
                    Object.keys(errors).forEach(function(key) {
                        message += `${key} - ${errors[key]}!<br/>`;                        
                    });
                }
                Message(message);
            }
        });
    }

        function ResetForm() {
            $('#formIndustry').each(function () {
                this.reset();
            });
        }

        function Message(text, status) {
            if (status == "success") {
                toastr.success(text)
            } else {
                toastr.error(text)
            }
        }
});