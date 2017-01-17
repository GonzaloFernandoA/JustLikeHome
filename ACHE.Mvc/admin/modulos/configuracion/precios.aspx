<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeBehind="precios.aspx.cs" Inherits="ACHE.Mvc.admin.modulos.configuracion.precios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: Precios
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script src="/admin/js/advanced-form-components.js"></script>
    <link rel="stylesheet" type="text/css" href="/admin/assets/bootstrap-datepicker/css/datepicker.css" />
    <script type="text/javascript" src="/admin/assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="/admin/js/views/modulos/configuracion/precios.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
   <%-- <div class="row">
        <div class="col-lg-12">
            <ul class="breadcrumb">
                <li><a href="/admin/home.aspx"><i class="fa fa-home"></i>&nbsp;Home</a></li>
                <li><a href="#">Configuración</a></li>
                <li class="active">Precios</li>
            </ul>
        </div>
    </div>--%>
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    <i class="fa fa-money"></i> Administración de precios
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
                            <%--<label>Lista de precios: </label>--%>
                            <asp:DropDownList runat="server" ID="cmbListaPrecios" ClientIDMode="Static" CssClass="form-control" onchange="filter(); return false;"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Vigencia Desde: </label>
                            <asp:TextBox runat="server" CssClass="form-control form-control-inline input-medium default-date-picker" ID="txtDesde" ClientIDMode="Static" MaxLength="8" placeholder="Vigencia Desde"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Vigencia Hasta: </label>
                            <asp:TextBox runat="server" CssClass="form-control form-control-inline input-medium default-date-picker" ID="txtHasta" ClientIDMode="Static" MaxLength="8" placeholder="Vigencia Hasta"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <button class="btn btn-info" id="btnBuscar" onclick="filter(); return false;">Buscar</button>
                            <button class="btn btn-default" id="btnVerTodos" onclick="verTodos(); return false;">Ver todos</button>
                            <button class="btn btn-warning" id="btnNuevo" onclick="nuevo(); return false;">Agregar precio</button>
                        </div>
                        <br /><br />
                        <div id="grid"></div>
                        
                    </form>
                </div>
            </section>
        </div>
    </div>
</asp:Content>