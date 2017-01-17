<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeBehind="propiedadese.aspx.cs" Inherits="ACHE.Mvc.admin.modulos.propiedades.propiedadese" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin ::
    <asp:Literal runat="server" ID="litModo" />
    de Propiedad
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script src="https://maps.googleapis.com/maps/api/js?sensor=true"></script>
    <script type="text/javascript" src="/admin/js/jquery.sortable.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("html, body").animate({ scrollTop: 0 }, "slow");
        });

        var drag = false;
        var infowindow;

        function computepos(point) {
            $("#txtLatitud").val(point.lat().toFixed(6));
            $("#txtLongitud").val(point.lng().toFixed(6));
        }

        function drawMap(latitude, longitude) {
            var point = new google.maps.LatLng(latitude, longitude);

            var mapOptions = {
                zoom: 14,
                center: point,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
            var marker = new google.maps.Marker({
                map: map,
                draggable: true,
                position: point
            })
            marker.setMap(map);
            map.setCenter(point);

            google.maps.event.addListener(map, 'click', function (event) {
                if (drag) { return; }
                if (map.getZoom() < 10) { map.setZoom(10); }
                map.panTo(event.latLng);
                computepos(event.latLng);
            });

            google.maps.event.addListener(marker, 'click', function () {
                var html = "<div style='color:#000;background-color:#fff;padding:3px;width:150px;'><p>Latitude - Longitude:<br />" + String(point.toUrlValue()) + "</p></div>";

                infowindow = new google.maps.InfoWindow({ content: html });
                infowindow.open(map, marker);
            });

            google.maps.event.addListener(marker, 'dragstart', function () { if (infowindow) { infowindow.close(); } });

            google.maps.event.addListener(marker, 'dragend', function (event) {
                //if (map.getZoom() < 10) { map.setZoom(10); }
                map.setCenter(event.latLng);
                computepos(event.latLng);
                drag = true;
                setTimeout(function () { drag = false; }, 250);
            });

            google.maps.event.addListenerOnce(map, 'idle', function () {
                google.maps.event.trigger(map, 'resize');
                map.setCenter(point);
            });

            $("#myMapModal").modal("show");
        }

        function showMap() {
            var direccion = $("#txtDireccion").val();
            if (direccion != "") {
                var embAddr = direccion + ', Buenos Aires, Argentina';
                $("#modalTitle").html(embAddr);

                if ($("#txtLatitud").val() == "" && $("#txtLongitud").val() == "") {
                    var geocoder = new google.maps.Geocoder();
                    geocoder.geocode({ 'address': embAddr }, function (results, status) {
                        if (status == google.maps.GeocoderStatus.OK) {
                            var latitude = results[0].geometry.location.lat();
                            var longitude = results[0].geometry.location.lng();

                            drawMap(latitude, longitude);

                            $("#txtLatitud").val(latitude);
                            $("#txtLongitud").val(longitude);
                        }
                        else {
                            $("#map-canvas").html('Could not find this location from the address given.<p>' + embAddr + '</p>');
                        }
                    });
                }
                else {
                    var latitude = $("#txtLatitud").val();
                    var longitude = $("#txtLongitud").val();

                    drawMap(latitude, longitude);
                }
            }
            else {
                var embAddr = 'Ciudad Autonoma de Buenos Aires, Buenos Aires, Argentina';
                $("#modalTitle").html(embAddr);

                if ($("#txtLatitud").val() == "" && $("#txtLongitud").val() == "") {
                    var geocoder = new google.maps.Geocoder();
                    geocoder.geocode({ 'address': embAddr }, function (results, status) {
                        if (status == google.maps.GeocoderStatus.OK) {
                            var latitude = results[0].geometry.location.lat();
                            var longitude = results[0].geometry.location.lng();

                            drawMap(latitude, longitude);

                            $("#txtLatitud").val(latitude);
                            $("#txtLongitud").val(longitude);
                        }
                        else {
                            $("#map-canvas").html('Could not find this location from the address given.<p>' + embAddr + '</p>');
                        }
                    });
                }
                else {
                    var latitude = $("#txtLatitud").val();
                    var longitude = $("#txtLongitud").val();

                    drawMap(latitude, longitude);
                }
            }
        };
    </script>
    <style type="text/css">
        #map-canvas {
            height: 280px;
            width: 48%;
            background-color: transparent;
        }

        ul {
            margin: 0 auto;
        }

        .divFoto {
            display: inline-block;
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <ul class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>&nbsp;Home</a></li>
                <%--<li><a href="#">Propiedades</a></li>--%>
                <li><a href="propiedades.aspx">Propiedades</a></li>
                <li class="active">
                    <asp:Literal runat="server" ID="litModo2" /></li>
            </ul>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    <i class="fa fa-home"></i>
                    <asp:Literal runat="server" ID="litModo3" />
                    de Propiedad
                </header>
                <div class="panel-body">
                    <form id="Form1" runat="server" class="form-horizontal" role="form">
                        <asp:Panel runat="server" ID="pnlError" Visible="false" class="alert alert-block alert-danger fade in">
                            <button data-dismiss="alert" class="close close-sm" type="button">
                                <i class="fa fa-times"></i>
                            </button>
                            <strong>Error</strong>
                            <asp:Literal runat="server" ID="litError" />
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlOk" Visible="false" CssClass="alert alert-success fade in">
                            <button data-dismiss="alert" class="close close-sm" type="button">
                                <i class="fa fa-times"></i>
                            </button>
                            Se han actualizado los datos correctamente!
                        </asp:Panel>
                        <asp:CustomValidator ID="valCustom" runat="server" Display="None" OnServerValidate="ServerValidate"></asp:CustomValidator>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"><abbr class="errorRequired">*&nbsp;</abbr>Código</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCodigo"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvCodigo" ControlToValidate="txtCodigo" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"><abbr class="errorRequired">*&nbsp;</abbr>Nombre</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvNombre" ControlToValidate="txtNombre" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"><abbr class="errorRequired">*&nbsp;</abbr>Descripción breve (listados)</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDescripcionBreve" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtNombre" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"><abbr class="errorRequired">*&nbsp;</abbr>Descripción detallada</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDescripcion" TextMode="MultiLine" Rows="8"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvDescripcion" ControlToValidate="txtDescripcion" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"><abbr class="errorRequired">*&nbsp;</abbr>Zona</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="cmbZona"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rqvZona" ControlToValidate="cmbZona" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe seleccionar un item" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"><abbr class="errorRequired">*&nbsp;</abbr>Lista de precios</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="cmbListaPrecios"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rqvListaPrecios" ControlToValidate="cmbListaPrecios" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe seleccionar un item" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"><abbr class="errorRequired">*&nbsp;</abbr>Tipo</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="cmbTipo">
                                    <asp:ListItem Value="" Text="Seleccione un tipo" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="C" Text="Casa"></asp:ListItem>
                                    <asp:ListItem Value="D" Text="Departamento"></asp:ListItem>
                                    <asp:ListItem Value="X" Text="Duplex"></asp:ListItem>
                                    <asp:ListItem Value="L" Text="Loft"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="PH"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rqvTipo" ControlToValidate="cmbTipo" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe seleccionar un item" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"><abbr class="errorRequired">*&nbsp;</abbr>Mínima cant de meses</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control numeric" ID="txtMinCantDias"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvMinCantDias" ControlToValidate="txtMinCantDias" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"><abbr class="errorRequired">*&nbsp;</abbr>Dirección</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDireccion" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvDireccion" ControlToValidate="txtDireccion" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">Latitud</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control numeric" ID="txtLatitud" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">Longitud</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control numeric" ID="txtLongitud" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"></label>
                            <div class="col-md-3 col-xs-11">
                                <a href="javascript:showMap();" id="btnMap" class="btn">Ver mapa</a>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Servicios</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:CheckBoxList runat="server" ID="chkServicios" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"><abbr class="errorRequired">*&nbsp;</abbr>Activo</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:CheckBox runat="server" ID="chkActivo" Checked="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"><abbr class="errorRequired">*&nbsp;</abbr>Destacado</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:CheckBox runat="server" ID="chkDestacado" Checked="false" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label"><abbr class="errorRequired">*&nbsp;</abbr>Metros</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control numeric" ID="txtMetros"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvMetros" ControlToValidate="txtMetros" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Cantidad de ambientes</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control numeric" ID="txtCantAmbientes"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvCantAmbientes" ControlToValidate="txtCantAmbientes" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Cantidad de huespedes</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control numeric" ID="txtCantHuespedes"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvCantHuespedes" ControlToValidate="txtCantHuespedes" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Cantidad de camas</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control numeric" ID="txtCantCamas"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvCantCamas" ControlToValidate="txtCantCamas" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">Video</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVideo"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-offset-2 col-lg-10">
                                <asp:Button runat="server" CssClass="btn btn-success" ID="btnGuardar" Text="Guardar" OnClick="btnAceptar_OnClick" />
                                <asp:Button runat="server" CssClass="btn btn-danger" ID="btnCancelar" Text="Cancelar" OnClick="btnCancelar_OnClick" CausesValidation="false" />
                                <asp:Button runat="server" CssClass="btn btn-info" ID="btnFotos" Text="Ir a fotos" OnClick="btnFotos_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </form>
                </div>
            </section>
        </div>
    </div>
    <div class="modal fade" id="myMapModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title" id="modalTitle">Mapa</h4>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div id="map-canvas"></div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
