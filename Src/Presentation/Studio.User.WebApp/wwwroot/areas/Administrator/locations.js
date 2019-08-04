$(document).ready(function () {

    window.CreateData = CreateData;
    window.DeleteData = DeleteData;
    window.EditData = EditData;
    window.UpdateData = UpdateData;

    GenerateGridList();


    function GenerateGridList() {

        ResetForm();

        $.ajax({

            type: "GET",
            url: "/api/Locations/GetAll",
            success: function (result) {
                if (result['locations'].length == 0) {
                    $('table').addClass('hidden');
                } else {
                    $('table').removeClass('hidden');
                    $('#tbody').children().remove();

                    $(result['locations']).each(function (i) {
                        var tbody = $('#tbody');
                        var tr = "<tr>";
                        tr += "<td>" + result['locations'][i].id;
                        tr += "<td>" + result['locations'][i].name;
                        tr += "<td>" + result['locations'][i].address;
                        tr += "<td>" + result['locations'][i].workDays;
                        tr += "<td>" + result['locations'][i].workHours;
                        tr += "<td>" + result['locations'][i].phone;
                        tr += "<td>" + result['locations'][i].company;

                        tr += "<td>" + "<button class='btn btn-info btn-xs' onclick=EditData(" + result['locations'][i].id + ")>" + "Промени";
                        tr += "<td>" + "<button class='btn btn-danger btn-xs' onclick=DeleteData(" + result['locations'][i].id + ")>" + "Изтрий";
                        tbody.append(tr);
                    });
                }
            }
        });

        FillAddressesDropdown()
        FillClientsDropdown()
    }

    function CreateData() {

        var formLocation = $('#formLocation').serialize();

        $.ajax({
            type: "POST",
            url: "/api/Locations/Create",
            data: formLocation,
            success: function () {

                Message("Обектът е запазен успешно!", 'success');

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
            url: "/api/Locations/Delete/" + id,
            success: function () {
                GenerateGridList();
                Message('Обектът е изтрит успешно!', 'success');
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
            FillAddressesDropdownForEdit(id);
            FillClientsDropdown();

            $.ajax({
                type: "GET",
                url: "/api/Locations/Get/" + id,
                //data: { id: id },
                success: function (result) {

                    $('#id').val(result.id);
                    $('#name').val(result.name);
                    $('#isOffice').val(result.isOffice).prop('checked');
                    $('#phone').val(result.phone);
                    $('#slogan').val(result.slogan);
                    $('#description').val(result.description);
                    $('#startDay').val(result.startDay);
                    $('#endDay').val(result.endDay);
                    $('#startHour').val(result.startHour);
                    $('#endHour').val(result.endHour);

                    $('#clientId').val(result.clientId).prop('selected', true);
                    $('#addressId').val(result.addressId).prop('selected', true);

                    $('#Create').addClass('hidden');
                    $('#Update').removeClass('hidden');
                }
            });
        }
    }

    function UpdateData() {

        var formLocation = $('#formLocation').serialize();

        $.ajax({
            type: "PUT",
            url: "/api/Locations/Update",
            data: formLocation,
            success: function () {

                $('#Create').removeClass('hidden');
                $('#Update').addClass('hidden');

                $('#id').val(0);

                Message('Обектът е променен успешно!', 'success');

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
        $('#formLocation').each(function () {
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

    function FillClientsDropdown() {
        $.ajax({

            type: "GET",
            url: "/api/Clients/GetAllNames",
            success: function (result) {
                var options = $('#clientId');
                options.empty();
                //don't forget error handling!
                $(result['clients']).each(function (i) {
                    options.append($("<option />").val(result['clients'][i].id).text(result['clients'][i].name));
                });
            }
        });
    }

    function FillAddressesDropdown() {
        $.ajax({

            type: "GET",
            url: "/api/Addresses/GetAllNames",
            success: function (result) {
                var options = $('#addressId');
                options.empty();
                //don't forget error handling!
                $(result['addresses']).each(function (i) {
                    options.append($("<option />").val(result['addresses'][i].id).text(result['addresses'][i].name));
                });
            }
        });
    }

    function FillAddressesDropdownForEdit(id) {
        $.ajax({

            type: "GET",
            url: "/api/Addresses/GetAllNamesForEditLocation/" + id,
            success: function (result) {
                var options = $('#addressId');
                options.empty();
                //don't forget error handling!
                $(result['addresses']).each(function (i) {
                    options.append($("<option />").val(result['addresses'][i].id).text(result['addresses'][i].name));
                });
            }
        });
    }

    $('#isOffice').click(function () {
        if ($('#isOffice').is(':checked')) {
            $("#isOffice").attr('checked', 'checked');   
            $("#isOffice").attr('value', 'true');
        }
        else {
            $("#isOffice").removeAttr('checked');
            $("#isOffice").attr('value', 'false');
        }
    });

});