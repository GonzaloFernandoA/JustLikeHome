<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeBehind="reservas.aspx.cs" Inherits="ACHE.Mvc.admin.modulos.administracion.reservas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: Reservas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script src="/admin/js/advanced-form-components.js"></script>
    <link rel="stylesheet" type="text/css" href="/admin/assets/bootstrap-datepicker/css/datepicker.css" />
    <script type="text/javascript" src="/admin/assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="/admin/js/views/modulos/administracion/reservas.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<div class="row">
        <div class="col-lg-12">
            <ul class="breadcrumb">
                <li><a href="/admin/home.aspx"><i class="fa fa-home"></i>&nbsp;Home</a></li>
                <li><a href="#">Administración</a></li>
                <li class="active">Reservas</li>
            </ul>
        </div>
    </div>--%>
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    <i class="fa fa-shopping-cart"></i> Administración de Reservas
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
                            <asp:DropDownList runat="server" ID="cmbEstado" ClientIDMode="Static" CssClass="form-control" onchange="filter(); return false;">
                                <asp:ListItem Value="" Text="Todos los estados" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Aceptada" Text="Aceptada"></asp:ListItem>
                                <asp:ListItem Value="Pendiente" Text="Pendiente"></asp:ListItem>
                                <asp:ListItem Value="Cancelada" Text="Cancelada"></asp:ListItem>
                            </asp:DropDownList>
                        <div class="form-group">
                            <asp:TextBox runat="server" CssClass="form-control form-control-inline input-medium default-date-picker" ID="txtDesde" ClientIDMode="Static" MaxLength="8" placeholder="Fecha Ingreso"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <%--<label>Fecha Egreso: </label>--%>
                            <asp:TextBox runat="server" CssClass="form-control form-control-inline input-medium default-date-picker" ID="txtHasta" ClientIDMode="Static" MaxLength="8" placeholder="Fecha Egreso"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <button class="btn btn-info" id="btnBuscar" onclick="filter(); return false;">Buscar</button>
                            <button class="btn btn-default" id="btnVerTodos" onclick="verTodos(); return false;">Ver todos</button>
                            <button class="btn btn-warning" id="btnNuevo" onclick="nuevo(); return false;">Agregar reserva</button>                            
                        </div>
                        <br /><br />
                        <div id="grid"></div>
                        
                    </form>
                </div>
            </section>
        </div>
    </div>
</asp:Content>