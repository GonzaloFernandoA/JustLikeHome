<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeBehind="clientese.aspx.cs" Inherits="ACHE.Mvc.admin.modulos.administracion.clientese" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin ::
    <asp:Literal runat="server" ID="litModo" />
    de Cliente
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <ul class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>&nbsp;Home</a></li>
                <li><a href="#">Administración</a></li>
                <li><a href="clientes.aspx">Clientes</a></li>
                <li class="active">
                    <asp:Literal runat="server" ID="litModo2" /></li>
            </ul>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    <i class="fa fa-users"></i> <asp:Literal runat="server" ID="litModo3" /> de Cliente
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
                                <abbr class="errorRequired">*&nbsp;</abbr>Apellido</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvApellido" ControlToValidate="txtApellido" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Email</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvEmail" ControlToValidate="txtEmail" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                                <asp:RegularExpressionValidator ID="rqvEmailValido" runat="server" ErrorMessage="Debe ingresar una direccion de email valida."
                                    Display="Dynamic" CssClass="errorRequired" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Teléfono</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTelefono"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvTelefono" ControlToValidate="txtTelefono" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>País</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtPais"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvPais" ControlToValidate="txtPais" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 col-sm-2 control-label">
                                <abbr class="errorRequired">*&nbsp;</abbr>Número de documento</label>
                            <div class="col-md-3 col-xs-11">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDocumento"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvDocumento" ControlToValidate="txtDocumento" Display="Dynamic"
                                    CssClass="errorRequired" ErrorMessage="Debe completar este campo" />
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