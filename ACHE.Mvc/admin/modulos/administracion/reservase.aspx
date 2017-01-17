<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="reservase.aspx.cs" Inherits="ACHE.Mvc.admin.modulos.administracion.reservase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin ::
    <asp:Literal runat="server" ID="litModo" />
    de Reserva
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script src="/admin/js/advanced-form-components.js"></script>
    <link rel="stylesheet" type="text/css" href="/admin/assets/bootstrap-datepicker/css/datepicker.css" />
    <script type="text/javascript" src="/admin/assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="/admin/css/chosen.min.css" />
    <script type="text/javascript" src="/admin/js/chosen.jquery.min.js"></script>
    <script type="text/javascript" src="/admin/js/views/modulos/administracion/reservase.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <ul class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>&nbsp;Home</a></li>
                <li><a href="#">Administración</a></li>
                <li><a href="reservas.aspx">Reservas</a></li>
                <li class="active">
                    <asp:Literal runat="server" ID="litModo2" /></li>
            </ul>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    <i class="fa fa-shopping-cart"></i>
                    <asp:Literal runat="server" ID="litModo3" />
                    de Reserva
                </header>
                <div class="panel-body">
                    <form id="Form1" runat="server" class="form-horizontal" role="form">
                        <asp:HiddenField runat="server" ID="hdnIDCliente" ClientIDMode="Static" />
                        <asp:Panel runat="server" ID="pnlError" Visible="false" class="alert alert-block alert-danger fade in">
                            <button data-dismiss="alert" class="close close-sm" type="button">
                                <i class="fa fa-times"></i>
                            </button>
                            <strong>Error</strong>
                            <asp:Literal runat="server" ID="litError" />
                        </asp:Panel>
                        <asp:CustomValidator ID="valCustom" runat="server" Display="None" OnServerValidate="ServerValidate"></asp:CustomValidator>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Propiedad</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:DropDownList runat="server" ID="cmbPropiedad" CssClass="form-control chosen-select" data-placeholder="Seleccione una propiedad..."></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rqvPropiedad" ControlToValidate="cmbPropiedad" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe seleccionar una propiedad" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Cliente</label>
                            <div class="col-md-3 col-xs-10">
                                <asp:DropDownList runat="server" ID="cmbCliente" ClientIDMode="Static" CssClass="form-control chosen-select" data-placeholder="Seleccione un cliente..."></asp:DropDownList>
                                <%--<asp:RequiredFieldValidator runat="server" ID="rqvCliente" ControlToValidate="cmbCliente" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe seleccionar un cliente" />--%>
                                <label id="lblCliente" class="errorRequired" style="display: none;" >Debe seleccionar un cliente</label>
                            </div>
                            <div class="col-md-3 col-xs-2">
                                <button class="btn btn-info btn-xs" style="vertical-align: middle;" onclick="nuevoCliente(); return false;"><i class="fa fa-plus"></i></button>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Precio Total</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" ID="txtPrecioTotal" CssClass="form-control numeric"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvPrecioTotal" ControlToValidate="txtPrecioTotal" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Estado</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:DropDownList runat="server" ID="cmbEstado" CssClass="form-control">
                                    <asp:ListItem Value="" Text="Seleccione un estado..."></asp:ListItem>
                                    <asp:ListItem Value="Aceptada" Text="Aceptada"></asp:ListItem>
                                    <asp:ListItem Value="Pendiente" Text="Pendiente"></asp:ListItem>
                                    <asp:ListItem Value="Cancelada" Text="Cancelada"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rqvEstado" ControlToValidate="cmbEstado" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe seleccionar un estado" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Fecha ingreso</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control form-control-inline input-medium default-date-picker" ID="txtDesde" MaxLength="8"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvDesde" ControlToValidate="txtDesde" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Meses</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:DropDownList runat="server" ID="cmbMeses" CssClass="form-control">
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rqvHasta" ControlToValidate="cmbMeses" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">Observaciones</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" ID="txtObservaciones" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-offset-2 col-lg-10">
                                <asp:Button runat="server" CssClass="btn btn-success" ID="btnGuardar" Text="Guardar" OnClientClick="return guardarIDCliente();" OnClick="btnAceptar_OnClick" />
                                <asp:Button runat="server" CssClass="btn btn-danger" ID="btnCancelar" Text="Cancelar" OnClick="btnCancelar_OnClick" CausesValidation="false" />
                            </div>
                        </div>
                    </form>
                </div>
            </section>
        </div>
    </div>
    <div class="modal fade" id="modalCliente" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Creación de cliente</h4>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div id="divErrorCliente" class="alert alert-block alert-danger fade in" style="display: none;">
                            <button data-dismiss="alert" class="close close-sm" type="button">
                                <i class="fa fa-times"></i>
                            </button>
                            <strong>Error</strong>
                            <span id="spnErrorCliente" />
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label class="col-lg-6 col-sm-2 control-label">
                                    <abbr class="errorRequired">*&nbsp;</abbr>Nombre</label>
                                <div class="col-md-6">
                                    <input type="text" class="form-control required" id="txtNombreCliente" />
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="form-group">
                                <label class="col-lg-6 col-sm-2 control-label">
                                    <abbr class="errorRequired">*&nbsp;</abbr>Apellido</label>
                                <div class="col-md-6">
                                    <input type="text" class="form-control" id="txtApellidoCliente" />
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="form-group">
                                <label class="col-lg-6 col-sm-2 control-label">
                                    <abbr class="errorRequired">*&nbsp;</abbr>Email</label>
                                <div class="col-md-6">
                                    <input type="text" class="form-control" id="txtEmailCliente" />
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="form-group">
                                <label class="col-lg-6 col-sm-2 control-label">
                                    <abbr class="errorRequired">*&nbsp;</abbr>Teléfono</label>
                                <div class="col-md-6">
                                    <input type="text" class="form-control" id="txtTelefonoCliente" />
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="form-group">
                                <label class="col-lg-6 col-sm-2 control-label">
                                    <abbr class="errorRequired">*&nbsp;</abbr>País</label>
                                <div class="col-md-6">
                                    <input type="text" class="form-control" id="txtPaisCliente" />
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="form-group">
                                <label class="col-lg-6 col-sm-2 control-label">
                                    <abbr class="errorRequired">*&nbsp;</abbr>Nro. Documento</label>
                                <div class="col-md-6">
                                    <input type="text" class="form-control" id="txtDocumentoCliente" />
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="form-group">
                                <div class="col-lg-6 col-sm-2">
                                    <input type="submit" class="btn btn-success" value="Crear" onclick="validarCliente(); return false;" />
                                    <button data-dismiss="modal" class="btn btn-danger" type="button">Cancelar</button>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-10">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button data-dismiss="modal" class="btn btn-default" type="button">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
