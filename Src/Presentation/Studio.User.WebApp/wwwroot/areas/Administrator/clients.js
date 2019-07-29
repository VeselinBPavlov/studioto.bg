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
            url: "/api/Clients/GetAll",
            success: function (result) {
                if (result['clients'].length == 0) {
                    $('table').addClass('hidden');
                } else {
                    $('table').removeClass('hidden');
                    $('#tbody').children().remove();

                    $(result['clients']).each(function (i) {
                        var tbody = $('#tbody');
                        var tr = "<tr>";
                        tr += "<td>" + result['clients'][i].id;
                        tr += "<td>" + result['clients'][i].companyName;
                        tr += "<td>" + result['clients'][i].vatNumber;
                        tr += "<td>" + result['clients'][i].managerNames;
                        tr += "<td>" + result['clients'][i].phone;
                        tr += "<td>" + "<button class='btn btn-info btn-xs' onclick=EditData(" + result['clients'][i].id + ")>" + "Промяна";
                        tr += "<td>" + "<button class='btn btn-danger btn-xs' onclick=DeleteData(" + result['clients'][i].id + ")>" + "Изтриване";
                        tbody.append(tr);
                    });
                }
            }
        });        
    }

    function CreateData() {

        var formClient = $('#formClient').serialize();

         $.ajax({
            type: "POST",
            url: "/api/Clients/Create",
            data: formClient,
            success: function () {

                     Message("Фирмата е записана успешно!", 'success');

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
                        message += `${errors[key]}!<br/>`;                        
                    });
                }
                Message(message);
            }
          });
    }

    function DeleteData(id) {

        $.ajax({
            type: "DELETE",
            url: "/api/Clients/Delete/" + id,
            success: function () {
                GenerateGridList();
                Message('Фирмата е изтрита успешно!', 'success');
            },
            error: function (response) {                     
                var message = "";
                var errors = response["responseJSON"]["errors"];
                var error = response["responseJSON"]["error"];
                if(error !== undefined) {
                    message += error;
                    } else {
                    Object.keys(errors).forEach(function(key) {
                        message += `${errors[key]}!<br/>`;                        
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
                url: "/api/Clients/Get/" + id,
                //data: { id: id },
                success: function (result) {

                    $('#id').val(result.id);
                    $('#companyName').val(result.companyName);
                    $('#vatNumber').val(result.vatNumber);
                    $('#managerFirstName').val(result.managerFirstName);
                    $('#managerLastName').val(result.managerLastName);
                    $('#phone').val(result.phone);

                    $('#Create').addClass('hidden');
                    $('#Update').removeClass('hidden');
                }
            });
        }
    }

    function UpdateData() {

        var formClient = $('#formClient').serialize();

        $.ajax({
            type: "PUT",
            url: "/api/Clients/Update",
            data: formClient,
            success: function () {

                $('#Create').removeClass('hidden');
                $('#Update').addClass('hidden');

                $('#id').val(0);

                Message('Фирмата е променена успешно!', 'success');

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
                        message += `${errors[key]}!<br/>`;                        
                    });
                }
                Message(message);
            }
        });
    }

        function ResetForm() {
            $('#formClient').each(function () {
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