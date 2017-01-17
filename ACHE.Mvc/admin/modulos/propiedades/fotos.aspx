<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeBehind="fotos.aspx.cs" Inherits="ACHE.Mvc.admin.modulos.propiedades.fotos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: Fotos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script type="text/javascript" src="/admin/js/jquery.sortable.min.js"></script>
    <script type="text/javascript" src="/admin/js/views/modulos/propiedades/fotos.js"></script>
    <style type="text/css">
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
    <%--<div class="row">
        <div class="col-lg-12">
            <ul class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>&nbsp;Home</a></li>
                <li><a href="#">Propiedades</a></li>
                <li class="active">Fotos
                </li>
            </ul>
        </div>
    </div>--%>
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    <i class="fa fa-picture-o"></i>&nbsp;Fotos de la propiedad:
                    <asp:Literal runat="server" ID="litNombreProp" />
                </header>
                <div class="panel-body">
                    <form id="Form1" runat="server" class="form-horizontal" role="form">
                        <ul style="list-style-type: none">
                            <li>
                                <label class="fldTitle" style="width: 100px !important">Nueva foto</label>
                                <div class="fieldwrap">
                                    <asp:FileUpload runat="server" ID="flpFoto" />
                                    <br />
                                    <asp:Button runat="server" ID="btnSubirFoto" Text="Subir" CssClass="btn btn-info" OnClick="btnSubirFoto_Click" CausesValidation="true" ValidationGroup="foto" />
                                    <br />
                                    <asp:RequiredFieldValidator runat="server" ID="rqvFoto" ControlToValidate="flpFoto" ValidationGroup="foto"></asp:RequiredFieldValidator>
                                </div>
                            </li>
                        </ul>
                        <div class="row">
                            <div class="col-lg-6">
                                <ul class="sortable grid">
                                    <asp:Repeater runat="server" ID="rptFotos">
                                        <ItemTemplate>
                                            <li id='<%# Eval("IDFoto") %>' style="width: 200px; min-height: 250px; display: inline-block; margin: 5px; border: 1px solid #B0B0B0;">
                                                <div class="divFoto" id='foto_<%# Eval("IDFoto") %>' style="text-align: center; padding: 1%; margin: 10px;">
                                                    <img src="/files/propiedades/<%# Eval("Foto") %>" width="152" height="120" alt="" />
                                                    <br />
                                                    <br />
                                                    <span class="img-pre">
                                                        <a class="btn btn-default" onclick="verFoto('<%# Eval("Foto") %>'); return false;">
                                                            <i class="fa fa-search-plus"></i>
                                                        </a>
                                                    </span>
                                                    &nbsp;&nbsp;
                                                    <span class="img-del">
                                                        <a href="#" class="btn btn-danger" onclick="DeleteFoto(<%# Eval("IDFoto") %>); return false;" title="Eliminar">
                                                            <i class="fa fa-trash-o"></i>
                                                        </a>
                                                    </span>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <div class="col-lg-10">
                                        <asp:Button runat="server" CssClass="btn btn-success" ID="btnVolver" Text="Volver" OnClick="btnVolver_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </section>
        </div>
    </div>
    <div class="modal fade" id="modalFoto" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Foto</h4>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="modal-body">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <img id="imgFoto" src="" style="max-width: 500px;" />
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