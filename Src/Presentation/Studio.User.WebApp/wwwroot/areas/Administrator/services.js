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
            url: "/api/Services/GetAll",
            success: function (result) {
                if (result['services'].length == 0) {
                    $('table').addClass('hidden');
                } else {
                    $('table').removeClass('hidden');
                    $('#tbody').children().remove();                    

                    $(result['services']).each(function (i) {
                        var tbody = $('#tbody');
                        var tr = "<tr>";
                        tr += "<td>" + result['services'][i].id;
                        tr += "<td>" + result['services'][i].name;
                        tr += "<td>" + result['services'][i].industryName;
                        tr += "<td>" + "<button class='btn btn-info btn-xs' onclick=EditData(" + result['services'][i].id + ")>" + "Промени";
                        tr += "<td>" + "<button class='btn btn-danger btn-xs' onclick=DeleteData(" + result['services'][i].id + ")>" + "Изтрий";
                        tbody.append(tr);
                    });
                }
            }
        });
        
        FillIndustriesDropdown();
    }

    function CreateData() {

        var formService = $('#formService').serialize();

         $.ajax({
            type: "POST",
            url: "/api/Services/Create",
            data: formService,
            success: function () {

                     Message("Услугата е запазена успешно!", 'success');

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
            url: "/api/Services/Delete/" + id,
            success: function () {
                GenerateGridList();
                Message('Услугата е изтрита успешно!', 'success');
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
            
            FillIndustriesDropdown()

            $.ajax({
                type: "GET",
                url: "/api/Services/Get/" + id,
                //data: { id: id },
                success: function (result) {

                    $('#id').val(result.id);
                    $('#name').val(result.name);
                    $('#industryId').val(result.industryId).prop('selected', true);

                    $('#Create').addClass('hidden');
                    $('#Update').removeClass('hidden');
                }
            });
        }
    }

    function UpdateData() {

        var formService = $('#formService').serialize();

        $.ajax({
            type: "PUT",
            url: "/api/Services/Update",
            data: formService,
            success: function () {

                $('#Create').removeClass('hidden');
                $('#Update').addClass('hidden');

                $('#id').val(0);

                Message('Услугата е променена успешно!', 'success');

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
            $('#formService').each(function () {
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

        function FillIndustriesDropdown() {
            $.ajax({

                type: "GET",
                url: "/api/Industries/GetAllNames",
                success: function (result) {
                    var options = $('#industryId');
                    options.empty();
                    //don't forget error handling!
                    $(result['industries']).each(function (i) {
                        options.append($("<option />").val(result['industries'][i].id).text(result['industries'][i].name));
                    });
                }
            });
        }

});