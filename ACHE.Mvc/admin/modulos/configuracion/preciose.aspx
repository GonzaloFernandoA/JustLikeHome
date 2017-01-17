<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeBehind="preciose.aspx.cs" Inherits="ACHE.Mvc.admin.modulos.configuracion.preciose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin ::
    <asp:Literal runat="server" ID="litModo" />
    de Precio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script src="/admin/js/advanced-form-components.js"></script>
    <link rel="stylesheet" type="text/css" href="/admin/assets/bootstrap-datepicker/css/datepicker.css" />
    <script type="text/javascript" src="/admin/assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <ul class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>&nbsp;Home</a></li>
                <li><a href="#">Configuración</a></li>
                <li><a href="precios.aspx">Precios</a></li>
                <li class="active">
                    <asp:Literal runat="server" ID="litModo2" /></li>
            </ul>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    <i class="fa fa-money"></i> <asp:Literal runat="server" ID="litModo3" /> de Precio
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
                        <asp:CustomValidator ID="valCustom" runat="server" Display="None" OnServerValidate="ServerValidate"></asp:CustomValidator>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Nombre</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvNombre" ControlToValidate="txtNombre" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Lista de precios</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="cmbListaPrecios"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rqvListaPrecios" ControlToValidate="cmbListaPrecios" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe seleccionar un item" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Valor diario</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control numeric" ID="txtValor1"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvValor1" ControlToValidate="txtValor1" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Valor semanal</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control numeric" ID="txtValor2"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvValor2" ControlToValidate="txtValor2" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Valor quincenal</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control numeric" ID="txtValor3"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvValor3" ControlToValidate="txtValor3" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Valor mensual</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control numeric" ID="txtValor4"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvValor4" ControlToValidate="txtValor4" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Vigencia desde</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control form-control-inline input-medium default-date-picker" ID="txtDesde" MaxLength="8"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvDesde" ControlToValidate="txtDesde" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Vigencia hasta</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control form-control-inline input-medium default-date-picker" ID="txtHasta" MaxLength="8"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvHasta" ControlToValidate="txtHasta" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                                <asp:CompareValidator ID="cmvFecha" runat="server" ControlToCompare="txtDesde"
                                    ControlToValidate="txtHasta" Display="Dynamic" CssClass="errorRequired" ErrorMessage="Vigencia desde debe ser menor a vigencia hasta"
                                    Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-offset-2 col-lg-10">
                                <asp:Button runat="server" CssClass="btn btn-success" ID="btnGuardar" Text="Guardar" OnClick="btnAceptar_OnClick" />
                                <asp:Button runat="server" CssClass="btn btn-danger" ID="btnCancelar" Text="Cancelar" OnClick="btnCancelar_OnClick" CausesValidation="false" />
                            </div>
                        </div>
                    </form>
                </div>
            </section>
        </div>
    </div>
</asp:Content>