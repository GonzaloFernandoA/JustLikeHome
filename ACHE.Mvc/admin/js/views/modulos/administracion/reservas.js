function configControls() {
    $("#txtDesde, #txtHasta").keypress(function (event) {
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
                        IDReserva: { type: "integer" },
                        IDPropiedad: { type: "integer" },
                        IDCliente: { type: "integer" },
                        Propiedad: { type: "string" },
                        Cliente: { type: "string" },
                        Estado: { type: "string" },
                        FechaReserva: { type: "date" },
                        TipoReserva: { type: "string" },
                        FechaIngreso: { type: "date" },
                        FechaEgreso: { type: "date" },
                        CantMeses: { type: "integer" }
                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "reservas.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
                    contentType: "application/json; charset=utf-8", // tells the web service to serialize JSON
                    type: "POST" //use HTTP POST request as the default GET is not allowed for ASMX
                },
                parameterMap: function (data, operation) {
                    if (operation != "read") {
                        // web service method parameters need to be send as JSON. The Create, Update and Destroy methods have a "products" parameter.
                        return JSON.stringify({ products: data.models })
                    } else {
                        // web services need default values for every parameter
                        data = $.extend({ sort: null, filter: null, fDesde: $("#txtDesde").val(), fHasta: $("#txtHasta").val() }, data);

                        return JSON.stringify(data);
                    }
                }
            }
        },
        height: 500,
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
            //{ field: "IDReserva", title: "ID", width: "50px", hidden: "hidden" },
            { field: "Propiedad", title: "Propiedad", width: "100px" },
            { field: "Cliente", title: "Cliente", width: "100px" },
            { field: "IDPropiedad", title: "IDPropiedad", width: "50px", hidden: "hidden" },
            { field: "IDCliente", title: "IDCliente", width: "50px", hidden: "hidden"},
            { field: "Estado", title: "Estado", width: "100px" },
            { field: "FechaReserva", title: "Fecha Reserva", width: "100px", format: "{0:dd/MM/yyyy}" },
            { field: "TipoReserva", title: "Tipo Reserva", width: "100px", hidden: true },
            { field: "CantMeses", title: "Cant. Meses", width: "50px" },
            { field: "FechaIngreso", title: "Fecha Ingreso", width: "100px", format: "{0:dd/MM/yyyy}" },
            { field: "FechaEgreso", title: "Fecha Egreso", width: "100px", format: "{0:dd/MM/yyyy}" },
            { command: { text: "", template: "<div align='center'><i class='fa fa-pencil editColumn' style='cursor:pointer'></i></div>" }, title: "Editar", width: "50px" },
            { command: { text: "", template: "<div align='center'><i class='fa fa-trash-o deleteColumn' style='cursor:pointer'></i></div>" }, title: "Eliminar", width: "50px" }
        ]
    });

    $("#grid").delegate(".editColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        window.location.href = "reservase.aspx?Mode=E&Id=" + dataItem.IDReserva;
    });

    $("#grid").delegate(".deleteColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea eliminar el item seleccionado?")) {
            $.ajax({
                type: "POST",
                url: "reservas.aspx/Delete",
                data: "{ id: " + dataItem.IDReserva + "}",
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
    window.location.href = "reservase.aspx?Mode=A";
}

function verTodos() {
    $("#cmbEstado").val("");
    $("#txtDesde").val("");
    $("#txtHasta").val("");
    filter();
}

function filter() {
    var grid = $("#grid").data("kendoGrid");
    var $filter = new Array();

    var estado = $("#cmbEstado").val();

    if (estado != "")
        $filter.push({ field: "Estado", operator: "contains", value: estado });

    grid.dataSource.filter($filter);
}

$(document).ready(function () {
    configControls()

    var gridElement = $("#grid");
    var dataArea = gridElement.find(".k-grid-content");
    dataArea.height(480);
});