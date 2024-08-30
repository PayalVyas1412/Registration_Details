$('#btnAddrecord').click(function () {
    $('#mdlregistration').modal('show');
    $('#modaltitle').text('Add Record');
    $('#btnUpdate').hide();
    getStateList();
    getCityList();
});


$('#btnSubmit').click(function (e) {
    var result = Validate(e);
    e.preventDefault();
    if (result == false) {
        e.preventDefault();
        return false;
    }
    let formData = {
        Name: $('#txtname').val(),
        Email: $('#txtemail').val(),
        phone: $('#txtphoneno').val(),
        Address: $('#txtAddress').val(),
        stateID: $('#dpState').val(),
        cityID: $('#dpCities').val()
    }
    $.ajax({
        url: "/Registration/SaveRegistrationdetails",
        type: "POST",
        data: formData,
        success: function (response) {
          
            
            window.location.href = "/Registration/Index";
            HideModal();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
});
$('#btnUpdate').click(function () {
    let formData = {
        Id:$('#txtId').val(),
        Name: $('#txtname').val(),
        Email: $('#txtemail').val(),
        phone: $('#txtphoneno').val(),
        Address: $('#txtAddress').val(),
        stateID: $('#dpState').val(),
        cityID: $('#dpCities').val()
    }
    $.ajax({
        url: "/Registration/SaveRegistrationdetails",
        type: "POST",
        data: formData,
        success: function (response) {
            window.location.href = "/Registration/Index";
            HideModal();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
});
$('#btnCancel').click(function () {
    window.location.href = "/Registration/Index";
    HideModal();
});
$('#chkAgree').click(function () {
    if ($(this).is(":checked")) {
        $("#btnSubmit").removeAttr("disabled");
        $("#btnUpdate").removeAttr("disabled");
    }
    else {
        $("#btnSubmit").attr("disabled", "disabled");
        $("#btnUpdate").attr("disabled", "disabled");
    }
});
function getRegistrationDetailsByID(id) {
    $.ajax({
        type: "GET",
        url: '/Registration/getRegistrationById?Id=' + id,
        contentType: "text/plain;charset=utf-8",
        data: {},
        dataType: "json",
        cache: false,
        async: false,
        success: function (response) {
            
            if (response != null || response != undefined || response.length != 0) {
                console.log(response);
                $('#mdlregistration').modal('show');
                $('#modaltitle').text('Update Record');
                $('#btnSubmit').hide();
                $('#btnUpdate').show();
                $('#txtId').val(response[0].id)
                $('#txtname').val(response[0].name);
                $('#txtemail').val(response[0].email);
                $('#txtphoneno').val(response[0].phone);
                $('#txtAddress').val(response[0].address);
                var optState = '<option value="' + response[0].stateID + '">' + response[0].stateName + '</option>';
                $("#dpState").html(optState);
                var optCity = '<option value="' + response[0].cityID + '">' + response[0].cityName + '</option>';
                $("#dpCities").html(optCity); 

            }
            else {
                alert("someting went wrong");
            }
        },
        error: function () {
            alert("Unable to read data");
        }


    });
}
function deleteRecordByID(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        conformButtonText: 'Yes, delete it!',

    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: '/Registration/deleteRegistrationDetails?Id=' + id,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != null || data != undefined || data.length != 0) {
                        
                        window.location.href = '/Registration/Index';
                    }
                }


            });
        }


    });

}
function getStateList() {
    $.ajax({
        type: "Get",
        url: "/Registration/getStateList",
        data: "{}",
        success: function (data) {
            var opt = '<option value="-1">-- Select --</option>';
            for (var i = 0; i < data.length; i++) {
                opt += '<option value="' + data[i].id + '">' + data[i].stateName + '</option>';
            }
            $("#dpState").html(opt);
        }
    });
}
function getCityList() {
    $("#dpState").change(function () {
        var StateID = parseInt($('#dpState').val());
        if (!isNaN(StateID)) {
            var ddlCity = $('#dpCities');
            ddlCity.empty();

            $.ajax({
                url: "/Registration/getCitiesList",
                type: "GET",
                data: { StateID: StateID },
                success: function (data) {
                    ddlCity.empty();
                    var opt = '<option value="-1">-- Select --</option>';
                    for (var i = 0; i < data.length; i++) {
                        opt += '<option value="' + data[i].id + '">' + data[i].cityName + '</option>';
                    }
                    $("#dpCities").html(opt);
                }

            });
        }
    });
}
function Validate(e) {
    const emailregex = /^[^@]+@[^@]+\.[^@]+$/;
    const mobileregex = /^(0|91)?[6-9][0-9]{9}$/;


    var isValid = true;
    if ($('#txtname').val().trim() == "") {
        $('#errorName').html("Please Enter Name");
        $('#errorName').css("color", "red");
        isValid = false;
        e.preventDefault();
        e.stopPropagation();
    }
    else {
        isValid = true;
    }
    if ($('#txtemail').val().trim() == "") {
        $('#errorEmail').html("Please Enter Email");
        $('#errorEmail').css("color", "red");
        isValid = false;
        
        e.preventDefault();
        e.stopPropagation();
    }
    else if (!emailregex.test("#txtemail")) {
        $('#errorEmail').html("Please enter email in correct format");
        $('#errorEmail').css("color", "red");
        e.preventDefault();
        e.stopPropagation();
        isValid = false;
    }
    else {
        isValid = true;
    }
    if ($('#txtphoneno').val().trim() == "") {
        $('#errorPhone').html("Please Enter phone no.");
        $('#errorPhone').css("color", "red");
        isValid = false;
        e.preventDefault();
        e.stopPropagation();
    }
    else if (!mobileregex.test("#txtphoneno"))
    {
        $('#errorPhone').html("Please Enter phone no in correct format");
        $('#errorPhone').css("color", "red");
        isValid = false;
        e.preventDefault();
        e.stopPropagation();
    }
    else {
        isValid = true;
    }
    if ($('#dpState').val() == "-- Select --" || $('#dpState').val() == null||$('#dpState').val() == "") {
        $('#errorState').html("Please select State");
        $('#errorState').css("color", "red");
        isValid = false;

        e.preventDefault();
        e.stopPropagation();
    }
    else {
        isValid = true;
    }
    if ($('#dpCities').val() == null || $('#dpCities').val() == "" || $('#dpCities').val() == "-- Select --") {
        $('#errorCity').html("Please select City");
        $('#errorCity').css("color", "red");
        isValid = false;

        e.preventDefault();
        e.stopPropagation();
    }
    else {
        isValid = true;
    }
    return isValid;
}
function HideModal() {
    ClearData();
    $('#mdlregistration').modal('hide');
}
function ClearData() {
    $('#txtId').val('')
    $('#txtname').val('');
    $('#txtemail').val('');
    $('#txtphoneno').val('');
    $('#txtAddress').val('');
    $('#dpState').val('');
    $('#dpCities').val('');
}
