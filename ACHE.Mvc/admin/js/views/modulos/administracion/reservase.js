function nuevoCliente() {
    $("#modalCliente").modal("show");
}

function validarCliente() {
    var isValid = true;
    var msg = "";

    var nombre = $("#txtNombreCliente").val();
    var apellido = $("#txtApellidoCliente").val();
    var email = $("#txtEmailCliente").val();
    var telefono = $("#txtTelefonoCliente").val();
    var pais = $("#txtPaisCliente").val();
    var documento = $("#txtDocumentoCliente").val();

    if (nombre == "") {
        isValid = false;
        msg = "Debe ingresar el nombre del cliente";
    }
    else if (apellido == "") {
        isValid = false;
        msg = "Debe ingresar el apellido del cliente";
    }
    else if (email == "") {
        isValid = false;
        msg = "Debe ingresar el email del cliente";
    }
    else if (!IsValidEmail(email)) {
        isValid = false;
        msg = "Debe ingresar un email válido";
    }
    else if (telefono == "") {
        isValid = false;
        msg = "Debe ingresar el teléfono del cliente";
    }
    else if (pais == "") {
        isValid = false;
        msg = "Debe ingresar el país del cliente";
    }
    else if (documento == "") {
        isValid = false;
        msg = "Debe ingresar el número de documento del cliente";
    }

    if (!isValid) {
        $("#divErrorCliente").show();
        $("#spnErrorCliente").html(msg);
    }
    else {
        $("#divErrorCliente").hide();

        var info = "{ nombre: '" + nombre
            + "', apellido: '" + apellido
            + "', email: '" + email
            + "', telefono: '" + telefono
            + "', pais: '" + pais
            + "', documento: '" + documento
            + "' }";

        $.ajax({
            type: "POST",
            url: "reservase.aspx/CrearCliente",
            data: info,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                if (data.d != "") {
                    $('#cmbCliente').append("<option value='" + data.d + "'>" + apellido + ", " + nombre + "</option>");
                    //$('#cmbCliente').chosen().trigger("chosen:updated");
                    $('#cmbCliente').val(data.d).chosen().trigger("chosen:updated");
                    $("#modalCliente").modal("hide");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var err = eval("(" + xhr.responseText + ")");
                //alert(err.Message);
                $("#divErrorCliente").show();
                $("#spnErrorCliente").html(err.Message);
            }
        });
    }
}

function guardarIDCliente() {
    $('#hdnIDCliente').val($('#cmbCliente').val());
    var id = $('#hdnIDCliente').val();
    if (id == "") {
        $('#lblCliente').show();
        return false;
    }
    else
        return true;
}

$(document).ready(function () {
    $(".chosen-select").chosen();
});