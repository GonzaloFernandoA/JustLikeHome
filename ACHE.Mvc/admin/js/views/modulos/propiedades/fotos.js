$(function () {
    $('.sortable').sortable().bind('sortupdate', function () {
        var stringDiv = "";
        //Triggered when the user stopped sorting and the DOM position has changed.
        $(".sortable").children().each(function (i) {
            var li = $(this);
            stringDiv += "" + li.attr("id") + '=' + i + '&';
        });
        $.ajax({
            type: "POST",
            url: "fotos.aspx/UpdateOrder?",
            data: "{ fotos: '" + stringDiv + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
    });
});

function DeleteFoto(idFoto) {
    if (confirm("¿Esta seguro que desea eliminar la foto seleccionada?")) {
        $.ajax({
            type: "POST",
            url: "fotos.aspx/Delete",
            data: "{ id: " + parseInt(idFoto) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                //window.location.href = "fotos.aspx?Id=" + $("#hdnID").val();
                $("#" + idFoto).hide();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
    }
}

function verFoto(foto) {
    foto = "/files/propiedades/" + foto;
    var src = foto;
    $("#imgFoto").attr("src", src);
    $("#modalFoto").modal("show");
}