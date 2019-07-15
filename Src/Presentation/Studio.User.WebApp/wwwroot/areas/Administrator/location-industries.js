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
            url: "/api/LocationIndustries/GetAll",
            success: function (result) {
                if (result['locationIndustries'].length == 0) {
                    $('table').addClass('hidden');
                } else {
                    $('table').removeClass('hidden');
                    $('#tbody').children().remove();                    

                    $(result['locationIndustries']).each(function (i) {
                        var tbody = $('#tbody');
                        var tr = "<tr>";
                        tr += "<td>" + result['locationIndustries'][i].locationId;
                        tr += "<td>" + result['locationIndustries'][i].locationName;
                        tr += "<td>" + result['locationIndustries'][i].industryId;
                        tr += "<td>" + result['locationIndustries'][i].industryName;
                        tr += "<td>" + result['locationIndustries'][i].description;

                        tr += "<td>" + `<button class='btn btn-info btn-xs' onclick=EditData(${result['locationIndustries'][i].locationId},${result['locationIndustries'][i].industryId})>` + "Edit";
                        tr += "<td>" + `<button class='btn btn-danger btn-xs' onclick=DeleteData(${result['locationIndustries'][i].locationId},${result['locationIndustries'][i].industryId})>` + "Delete";
                        tbody.append(tr);
                    });
                }
            }
        });
        
        FillLocationsDropdown();
        FillIndustriesDropdown();
    }

    function CreateData() {

        var formLocationIndustry = $('#formLocationIndustry').serialize();

         $.ajax({
            type: "POST",
            url: "/api/LocationIndustries/Create",
            data: formLocationIndustry,
            success: function () {

                     Message("Data successfuly saved.");

                     GenerateGridList();
             },
             error: function () {
                     Message("Data fail to saved.");
             }
          });
    }

    function DeleteData(locationId, industryId) {

        $.ajax({
            type: "DELETE",
            url: "/api/LocationIndustries/Delete/" + locationId + "/" + industryId,
            success: function () {
                GenerateGridList();
                Message('Delete success!');
            },
            error: function () {
                Message('Delete failed!');
            }
        });
    }

    function EditData(locationId, industryId) {

        
        if (locationId != null && locationId > 0 && industryId != null && industryId > 0) {
            
            FillLocationsDropdown();
            FillIndustriesDropdown();

            $.ajax({
                type: "GET",
                url: "/api/LocationIndustries/Get/" + locationId + "/" + industryId,
                //data: { id: id },
                success: function (result) {

                    $('#locationId').val(result.locationId).prop('selected', true);
                    $('#industryId').val(result.industryId).prop('selected', true);
                    $('#description').val(result.description);
                    $('#durationInMinutes').val(result.durationInMinutes);

                    $('#Create').addClass('hidden');
                    $('#Update').removeClass('hidden');
                }
            });
        }
    }

    function UpdateData() {

        var formLocationIndustry = $('#formLocationIndustry').serialize();

        $.ajax({
            type: "PUT",
            url: "/api/LocationIndustries/Update",
            data: formLocationIndustry,
            success: function () {

                $('#Create').removeClass('hidden');
                $('#Update').addClass('hidden');

                $('#employeeId').val(0);
                $('#serviceId').val(0);

                Message('Update success!');

                GenerateGridList();
            },
            error: function () {
                Message('Update failed!');
            }
        });
    }

        function ResetForm() {
            $('#formLocationIndustry').each(function () {
                this.reset();
            });
        }

        function Message(text) {
            toastr.success(text)
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