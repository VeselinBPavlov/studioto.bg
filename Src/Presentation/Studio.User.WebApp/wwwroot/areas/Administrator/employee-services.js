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
            url: "/api/EmployeeServices/GetAll",
            success: function (result) {
                if (result['employeeServices'].length == 0) {
                    $('table').addClass('hidden');
                } else {
                    $('table').removeClass('hidden');
                    $('#tbody').children().remove();

                    

                    $(result['employeeServices']).each(function (i) {
                        var tbody = $('#tbody');
                        var tr = "<tr>";
                        tr += "<td>" + result['employeeServices'][i].employeeId;
                        tr += "<td>" + result['employeeServices'][i].employeeName;
                        tr += "<td>" + result['employeeServices'][i].serviceId;
                        tr += "<td>" + result['employeeServices'][i].serviceName;
                        tr += "<td>" + result['employeeServices'][i].price;
                        tr += "<td>" + result['employeeServices'][i].durationInMinutes;

                        tr += "<td>" + `<button class='btn btn-info btn-xs' onclick=EditData(${result['employeeServices'][i].employeeId},${result['employeeServices'][i].serviceId})>` + "Промяна";
                        tr += "<td>" + `<button class='btn btn-danger btn-xs' onclick=DeleteData(${result['employeeServices'][i].employeeId},${result['employeeServices'][i].serviceId})>` + "Изтриване";
                        tbody.append(tr);
                    });
                }
            }
        });
        
        FillEmployeesDropdown();
        FillServicesDropdown();
    }

    function CreateData() {

        var formEmployeeService = $('#formEmployeeService').serialize();

         $.ajax({
            type: "POST",
            url: "/api/EmployeeServices/Create",
            data: formEmployeeService,
            success: function () {

                     Message("Служителят с услугата е запазен успешно!", 'success');

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

    function DeleteData(employeeId, serviceId) {

        $.ajax({
            type: "DELETE",
            url: "/api/EmployeeServices/Delete/" + employeeId + "/" + serviceId,
            success: function () {
                GenerateGridList();
                Message('Служителят с услугата е изтрит успешно!', 'success');
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

    function EditData(employeeId, serviceId) {

        
        if (employeeId != null && employeeId > 0 && serviceId != null && serviceId > 0) {
            
            FillEmployeesDropdown();
            FillServicesDropdown();

            $.ajax({
                type: "GET",
                url: "/api/EmployeeServices/Get/" + employeeId + "/" + serviceId,
                //data: { id: id },
                success: function (result) {

                    $('#employeeId').val(result.employeeId).prop('selected', true);
                    $('#serviceId').val(result.serviceId).prop('selected', true);
                    $('#price').val(result.price);
                    $('#durationInMinutes').val(result.durationInMinutes);

                    $('#Create').addClass('hidden');
                    $('#Update').removeClass('hidden');
                }
            });
        }
    }

    function UpdateData() {

        var formEmployeeService = $('#formEmployeeService').serialize();

        $.ajax({
            type: "PUT",
            url: "/api/EmployeeServices/Update",
            data: formEmployeeService,
            success: function () {

                $('#Create').removeClass('hidden');
                $('#Update').addClass('hidden');

                $('#employeeId').val(0);
                $('#serviceId').val(0);

                Message('Служителят с услугата е променен успешно!', 'success');

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
            $('#formEmployeeService').each(function () {
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

        function FillEmployeesDropdown() {
            $.ajax({

                type: "GET",
                url: "/api/Employees/GetAllNames",
                success: function (result) {
                    var options = $('#employeeId');
                    options.empty();
                    //don't forget error handling!
                    $(result['employees']).each(function (i) {
                        options.append($("<option />").val(result['employees'][i].id).text(result['employees'][i].name));
                    });
                }
            });
        }

        function FillServicesDropdown() {
            $.ajax({

                type: "GET",
                url: "/api/Services/GetAllNames",
                success: function (result) {
                    var options = $('#serviceId');
                    options.empty();
                    //don't forget error handling!
                    $(result['services']).each(function (i) {
                        options.append($("<option />").val(result['services'][i].id).text(result['services'][i].name));
                    });
                }
            });
        }

});