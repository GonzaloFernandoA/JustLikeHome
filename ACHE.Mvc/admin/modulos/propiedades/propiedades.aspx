<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeBehind="propiedades.aspx.cs" Inherits="ACHE.Mvc.admin.modulos.propiedades.propiedades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: Propiedades
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script src="/admin/js/views/modulos/propiedades/propiedades.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<div class="row">
        <div class="col-lg-12">
            <ul class="breadcrumb">
                <li><a href="/admin/home.aspx"><i class="fa fa-home"></i>&nbsp;Home</a></li>
                <li class="active">Propiedades</li>
            </ul>
        </div>
    </div>--%>
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    <i class="fa fa-home"></i> Administración de propiedades
                </header>
                <div class="panel-body">
                    <form id="Form1" runat="server" class="form-inline" role="form">
                        <div id="divError" style="display: none;" class="alert alert-block alert-danger fade in">
                            <button data-dismiss="alert" class="close close-sm" type="button">
                                <i class="fa fa-times"></i>
                            </button>
                            <strong>Error</strong>
                            <span id="spnError" />
                        </div>
                        
                        <div class="form-group">
                            <%--<label>Zona: </label>--%>
                            <asp:DropDownList runat="server" ID="cmbZona" ClientIDMode="Static" CssClass="form-control" onchange="filter(); return false;"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <%--<label>Nombre: </label>--%>
                            <asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" CssClass="form-control" onchange="filter(); return false;" placeholder="Nombre"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <%--<label>Nombre: </label>--%>
                            <asp:TextBox runat="server" ID="txtCodigo" ClientIDMode="Static" CssClass="form-control" onchange="filter(); return false;" placeholder="Código"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <%--<label>Nombre: </label>--%>
                            <asp:TextBox runat="server" ID="txtDireccion" ClientIDMode="Static" CssClass="form-control" onchange="filter(); return false;" placeholder="Direccion"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <button class="btn btn-info" id="btnBuscar" onclick="filter(); return false;">Buscar</button>
                            <button class="btn btn-default" id="btnVerTodos" onclick="verTodos(); return false;">Ver todos</button>
                            <button class="btn btn-warning" id="btnNuevo" onclick="nuevo(); return false;">Agregar propiedad</button>
                        </div>
                        <br /><br />
                        <div id="grid"></div>
                    </form>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
