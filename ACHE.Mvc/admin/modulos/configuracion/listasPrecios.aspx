<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeBehind="listasPrecios.aspx.cs" Inherits="ACHE.Mvc.admin.modulos.configuracion.listasPrecios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: Listas de Precio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script src="/admin/js/views/modulos/configuracion/listasPrecios.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<div class="row">
        <div class="col-lg-12">
            <ul class="breadcrumb">
                <li><a href="/admin/home.aspx"><i class="fa fa-home"></i>&nbsp;Home</a></li>
                <li><a href="#">Configuración</a></li>
                <li class="active">Listas de Precio</li>
            </ul>
        </div>
    </div>--%>
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    <i class="fa fa-list"></i> Administración de listas de precio
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
                            <label>Nombre: </label>
                            <asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <button class="btn btn-info" id="btnBuscar" onclick="filter(); return false;">Buscar</button>
                            <button class="btn btn-default" id="btnVerTodos" onclick="verTodos(); return false;">Ver todos</button>
                            <button class="btn btn-warning" id="btnNuevo" onclick="nuevo(); return false;">Agregar lista</button>
                        </div>
                        <br /><br />
                        <div id="grid"></div>
                        
                    </form>
                </div>
            </section>
        </div>
    </div>
</asp:Content>