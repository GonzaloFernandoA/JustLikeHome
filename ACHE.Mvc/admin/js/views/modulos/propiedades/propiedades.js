function configControls() {
    $("#txtNombre").keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            filter();
            return false;
        }
    });

    $("#grid").kendoGrid({
        dataSource: {
            serverSorting: true,
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "d.Data",
                total: "d.Total",
                model: {
                    fields: {
                        IDPropiedad: { type: "integer" },
                        IDZona: { type: "integer" },
                        IDListaPrecio: { type: "integer" },
                        Codigo: { type: "string" },
                        Nombre: { type: "string" },
                        Direccion: { type: "string" },
                        Zona: { type: "string" },
                        ListaPrecio: { type: "string" },
                        Tipo: { type: "string" },
                        CantAmbientes: { type: "integer" },
                        CantAmbientes: { type: "integer" },
                        CantAmbientes: { type: "integer" },
                        Activo: { type: "string" },
                        Destacado: { type: "string" },
                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "propiedades.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
                    contentType: "application/json; charset=utf-8", // tells the web service to serialize JSON
                    type: "POST" //use HTTP POST request as the default GET is not allowed for ASMX
                },
                parameterMap: function (data, operation) {
                    if (operation != "read") {
                        // web service method parameters need to be send as JSON. The Create, Update and Destroy methods have a "products" parameter.
                        return JSON.stringify({ products: data.models })
                    } else {
                        // web services need default values for every parameter
                        data = $.extend({ sort: null, filter: null }, data);

                        return JSON.stringify(data);
                    }
                }
            }
        },
        height: 700,
        scrollable: true,
        sortable: true,
        //filterable: true,
        pageable: {
            refresh: true,
            pageSizes: false,
            input: false,
            numeric: true
        },
        columns: [
            //{ field: "IDPropiedad", title: "ID", width: "50px", hidden: "hidden" },
            { field: "Codigo", title: "Codigo", width: "100px" },
            { field: "Nombre", title: "Nombre", width: "100px" },
            { field: "IDZona", title: "IDZona", width: "100px", hidden: "hidden" },
            { field: "Zona", title: "Zona", width: "100px" },
            { field: "Direccion", title: "Direccion", width: "200px" },
            //{ field: "IDListaPrecio", title: "IDListaPrecio", width: "100px", hidden: "hidden" },
            //{ field: "ListaPrecio", title: "Lista de Precio", width: "100px" },
            { field: "Tipo", title: "Tipo", width: "100px" },
            { field: "CantAmbientes", title: "Cant. Ambientes", width: "100px" },
            { field: "CantHuespedes", title: "Cant. Huespedes", width: "100px" },
            { field: "CantCamas", title: "Cant. Camas", width: "100px" },
            { field: "Activo", title: "Activa", width: "50px" },
            { field: "Destacado", title: "Destacada", width: "50px" },
            { command: { text: "", template: "<div align='center'><i class='fa fa-pencil editColumn' style='cursor:pointer'></i></div>" }, title: "Editar", width: "50px" },
            { command: { text: "", template: "<div align='center'><i class='fa fa-trash-o deleteColumn' style='cursor:pointer'></i></div>" }, title: "Eliminar", width: "50px" }
        ]
    });

    $("#grid").delegate(".editColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        window.location.href = "propiedadese.aspx?Mode=E&Id=" + dataItem.IDPropiedad;
    });

    $("#grid").delegate(".deleteColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea eliminar el item seleccionado?")) {
            $.ajax({
                type: "POST",
                url: "propiedades.aspx/Delete",
                data: "{ id: " + dataItem.IDPropiedad + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, text) {
                    $("#divError").hide();
                    $("#spnError").text("");
                    filter();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    var err = eval("(" + xhr.responseText + ")");
                    // Display the specific error raised by the server (e.g. not a
                    //   valid value for Int32, or attempted to divide by zero).
                    //alert(err.Message);
                    $("#divError").show();
                    $("#spnError").text(err.Message);
                }
            });
        }
    });
}

function nuevo() {
    window.location.href = "propiedadese.aspx?Mode=A";
}

function verTodos() {
    $("#txtNombre").val("");
    $("#txtCodigo").val("");
    $("#txtDireccion").val("");
    $("#cmbZona").val("");
    //$("#cmbListaPrecio").val("");
    //$("#cmbTipo").val("");
    //$("#cmbActivo").val("");
    filter();
}

function filter() {
    var grid = $("#grid").data("kendoGrid");
    var $filter = new Array();

    var nombre = escape($("#txtNombre").val());
    var codigo = escape($("#txtCodigo").val());
    var direccion = escape($("#txtDireccion").val());
    var zona = $("#cmbZona").val();
    //var lista = $("#cmbListaPrecio").val();
    //var tipo = $("#cmbTipo").val();
    //var activo = $("#cmbActivo").val();

    if (zona != "")
        $filter.push({ field: "IDZona", operator: "equal", value: parseInt(zona) });

    if (nombre != "")
        $filter.push({ field: "Nombre", operator: "contains", value: nombre });

    if (codigo != "")
        $filter.push({ field: "Codigo", operator: "contains", value: codigo });

    if (direccion != "")
        $filter.push({ field: "Direccion", operator: "contains", value: direccion });

    //if (lista != "")
    //    $filter.push({ field: "IDListaPrecio", operator: "equal", value: parseInt(lista) });

    //if (tipo != "")
    //    $filter.push({ field: "Tipo", operator: "contains", value: tipo });

    //if (activo != "")
    //    $filter.push({ field: "Activo", operator: "contains", value: activo });

    grid.dataSource.filter($filter);
}

$(document).ready(function () {
    configControls()

    var gridElement = $("#grid");
    var dataArea = gridElement.find(".k-grid-content");
    dataArea.height(480);
});