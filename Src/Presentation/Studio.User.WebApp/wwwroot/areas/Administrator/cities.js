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
            url: "/api/Cities/GetAll",
            success: function (result) {
                if (result['cities'].length == 0) {
                    $('table').addClass('hidden');
                } else {
                    $('table').removeClass('hidden');
                    $('#tbody').children().remove();

                    

                    $(result['cities']).each(function (i) {
                        var tbody = $('#tbody');
                        var tr = "<tr>";
                        tr += "<td>" + result['cities'][i].id;
                        tr += "<td>" + result['cities'][i].name;
                        tr += "<td>" + result['cities'][i].countryName;
                        tr += "<td>" + "<button class='btn btn-info btn-xs' onclick=EditData(" + result['cities'][i].id + ")>" + "Edit";
                        tr += "<td>" + "<button class='btn btn-danger btn-xs' onclick=DeleteData(" + result['cities'][i].id + ")>" + "Delete";
                        tbody.append(tr);
                    });
                }
            }
        });
        
        FillCountriesDropdown();
    }

    function CreateData() {

        var formCity = $('#formCity').serialize();

         $.ajax({
            type: "POST",
            url: "/api/Cities/Create",
            data: formCity,
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
            url: "/api/Cities/Delete/" + id,
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
            
            FillCountriesDropdown()

            $.ajax({
                type: "GET",
                url: "/api/Cities/Get/" + id,
                //data: { id: id },
                success: function (result) {

                    $('#id').val(result.id);
                    $('#name').val(result.name);
                    $('#countryId').val(result.countryId).prop('selected', true);

                    $('#Create').addClass('hidden');
                    $('#Update').removeClass('hidden');
                }
            });
        }
    }

    function UpdateData() {

        var formCity = $('#formCity').serialize();

        $.ajax({
            type: "PUT",
            url: "/api/Cities/Update",
            data: formCity,
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
            $('#formCity').each(function () {
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

        function FillCountriesDropdown() {
            $.ajax({

                type: "GET",
                url: "/api/Countries/GetAll",
                success: function (result) {
                    var options = $('#countryId');
                    options.empty();
                    //don't forget error handling!
                    $(result['countries']).each(function (i) {
                        options.append($("<option />").val(result['countries'][i].id).text(result['countries'][i].name));
                    });
                }
            });
        }

});