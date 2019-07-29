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
            url: "/api/Addresses/GetAll",
            success: function (result) {
                if (result['addresses'].length == 0) {
                    $('table').addClass('hidden');
                } else {
                    $('table').removeClass('hidden');
                    $('#tbody').children().remove();

                    

                    $(result['addresses']).each(function (i) {
                        var tbody = $('#tbody');
                        var tr = "<tr>";
                        tr += "<td>" + result['addresses'][i].id;
                        tr += "<td>" + result['addresses'][i].address;
                        tr += "<td>" + result['addresses'][i].longitude;
                        tr += "<td>" + result['addresses'][i].latitude;
                        tr += "<td>" + result['addresses'][i].cityName;
                        tr += "<td>" + "<button class='btn btn-info btn-xs' onclick=EditData(" + result['addresses'][i].id + ")>" + "Edit";
                        tr += "<td>" + "<button class='btn btn-danger btn-xs' onclick=DeleteData(" + result['addresses'][i].id + ")>" + "Delete";
                        tbody.append(tr);
                    });
                }
            }
        });
        
        FillCitiesDropdown();
    }

    function CreateData() {

        var formAddress = $('#formAddress').serialize();

         $.ajax({
            type: "POST",
            url: "/api/Addresses/Create",
            data: formAddress,
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
            url: "/api/Addresses/Delete/" + id,
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
            
            FillCitiesDropdown()

            $.ajax({
                type: "GET",
                url: "/api/Addresses/Get/" + id,
                //data: { id: id },
                success: function (result) {

                    $('#id').val(result.id);
                    $('#street').val(result.street);
                    $('#number').val(result.number);
                    $('#building').val(result.building);
                    $('#entrance').val(result.entrance);
                    $('#floor').val(result.floor);
                    $('#apartment').val(result.apartment);
                    $('#district').val(result.district);
                    $('#longitude').val(result.longitude);                  
                    $('#latitude').val(result.latitude);                 

                    $('#cityId').val(result.cityId).prop('selected', true);

                    $('#Create').addClass('hidden');
                    $('#Update').removeClass('hidden');
                }
            });
        }
    }

    function UpdateData() {

        var formAddress = $('#formAddress').serialize();

        $.ajax({
            type: "PUT",
            url: "/api/Addresses/Update",
            data: formAddress,
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
            $('#formAddress').each(function () {
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

        function FillCitiesDropdown() {
            $.ajax({

                type: "GET",
                url: "/api/Cities/GetAllNames",
                success: function (result) {
                    var options = $('#cityId');
                    options.empty();
                    //don't forget error handling!
                    $(result['cities']).each(function (i) {
                        options.append($("<option />").val(result['cities'][i].id).text(result['cities'][i].name));
                    });
                }
            });
        }

});