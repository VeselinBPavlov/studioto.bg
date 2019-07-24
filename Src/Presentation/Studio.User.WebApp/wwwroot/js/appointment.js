$(document).ready(function () {
    $('#TimeBlockHelper').append($("<option></option>").val("placeholder").text("Избери дата и услуга"));
    $('#TimeBlockHelper').val("placeholder");
    $("#TimeBlockHelper").prop("disabled", true);
    $("#ReservationDate, #ServiceId").change(function () {
        var now = new Date();
        var date = new Date($("#ReservationDate").val());

        if (date > now && $("#ServiceId").val() != 0) {
            var formAppointment = $('#FormAppointment').serialize();
            $("#TimeBlockHelper").empty();
        $.ajax({
           type: "POST",
           url: "/Appointment/GetAvailableAppointments",
           data: formAppointment,
           success: function (data) {               
               if (data.length > 1) {
                $("#TimeBlockHelper").find('[value="busy"]').remove();
               }
               for (i = 0; i < data.length; i++) {
                   var option = $("<option value=" + data[i]["value"] + ">" + data[i]["text"] + "</option>");
                   option.appendTo($("#TimeBlockHelper"));
               }
                $("#TimeBlockHelper").find('[value="placeholder"]').remove();
                $("#TimeBlockHelper").prop("disabled", false);
            },
            error: function () { alert("Error retrieving available appointments!"); }
        });
        }
        else {
            $("#TimeBlockHelper").empty();
            $('#TimeBlockHelper').append($("<option></option>").val("placeholder").text("Избери дата и услуга"));
            $('#TimeBlockHelper').val("placeholder");
            $("#TimeBlockHelper").prop("disabled", true);
        }
    });
});
