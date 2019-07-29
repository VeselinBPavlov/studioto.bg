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
            url: "/api/Employees/GetAll",
            success: function (result) {
                if (result['employees'].length == 0) {
                    $('table').addClass('hidden');
                } else {
                    $('table').removeClass('hidden');
                    $('#tbody').children().remove();                    

                    $(result['employees']).each(function (i) {
                        var tbody = $('#tbody');
                        var tr = "<tr>";
                        tr += "<td>" + result['employees'][i].id;
                        tr += "<td>" + result['employees'][i].firstName;
                        tr += "<td>" + result['employees'][i].lastName;
                        tr += "<td>" + result['employees'][i].locationName;
                        tr += "<td>" + result['employees'][i].possitions;
                        tr += "<td>" + "<button class='btn btn-info btn-xs' onclick=EditData(" + result['employees'][i].id + ")>" + "Edit";
                        tr += "<td>" + "<button class='btn btn-danger btn-xs' onclick=DeleteData(" + result['employees'][i].id + ")>" + "Delete";
                        tbody.append(tr);
                    });
                }
            }
        });
        
        FillLocationsDropdown();
    }

    function CreateData() {

        var formEmployee = $('#formEmployee').serialize();

         $.ajax({
            type: "POST",
            url: "/api/Employees/Create",
            data: formEmployee,
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
            url: "/api/Employees/Delete/" + id,
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
            
            FillLocationsDropdown()

            $.ajax({
                type: "GET",
                url: "/api/Employees/Get/" + id,
                //data: { id: id },
                success: function (result) {

                    $('#id').val(result.id);
                    $('#firstName').val(result.firstName);
                    $('#lastName').val(result.lastName);

                    $('#locationId').val(result.locationId).prop('selected', true);

                    $('#Create').addClass('hidden');
                    $('#Update').removeClass('hidden');
                }
            });
        }
    }

    function UpdateData() {

        var formEmployee = $('#formEmployee').serialize();

        $.ajax({
            type: "PUT",
            url: "/api/Employees/Update",
            data: formEmployee,
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
            $('#formEmployee').each(function () {
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

        function FillLocationsDropdown() {
            $.ajax({

                type: "GET",
                url: "/api/Locations/GetAllNames",
                success: function (result) {
                    var options = $('#locationId');
                    options.empty();
                    //don't forget error handling!
                    $(result['locations']).each(function (i) {
                        options.append($("<option />").val(result['locations'][i].id).text(result['locations'][i].name));
                    });
                }
            });
        }

});