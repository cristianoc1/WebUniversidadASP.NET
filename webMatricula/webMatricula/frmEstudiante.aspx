<%@ Page Title="" Language="C#" MasterPageFile="~/frmPrincipal.Master" AutoEventWireup="true" CodeBehind="frmEstudiante.aspx.cs" Inherits="webMatricula.Formulario_web14" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Cuerpo" runat="server">
    <table align="center" class="auto-style7">
        <tr>
            <td class="auto-style8" colspan="2" style="background-color: #003366">Maestro de Estudiantes</td>
        </tr>
        <tr>
            <td class="auto-style9">&nbsp;</td>
            <td class="auto-style14">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13"><span class="auto-style16">Facultad</span>:&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td class="auto-style10">
                <asp:DropDownList ID="ddlFacultad" runat="server" AutoPostBack="True" Width="266px" OnSelectedIndexChanged="ddlFacultad_SelectedIndexChanged1">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style13">Programa:&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td class="auto-style10">
                <asp:DropDownList ID="ddlPrograma" runat="server" Width="427px" OnSelectedIndexChanged="ddlPrograma_SelectedIndexChanged1">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style13">Carné:&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td class="auto-style10">
                <asp:TextBox ID="txtCarne" runat="server" MaxLength="10" OnTextChanged="txtCarne_TextChanged1"></asp:TextBox>
                <asp:ImageButton ID="ibtnBuscar" runat="server" ImageUrl="~/Imagenes/Buscar.jpg" Visible="False" OnClick="ibtnBuscar_Click1" />
            </td>
        </tr>
        <tr>
            <td class="auto-style17">Nro.Documento:&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td class="auto-style14">
                <asp:TextBox ID="txtNroDoc" runat="server" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style18">Nombre:&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td class="auto-style19">
                <asp:TextBox ID="txtNombre" runat="server" Width="416px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style13">&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td class="auto-style10">
                <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" TextAlign="Left" />
            </td>
        </tr>
        <tr>
            <td class="auto-style17">&nbsp;</td>
            <td class="auto-style14">
                <asp:RadioButtonList ID="rblJornada" runat="server" AutoPostBack="True" 
                    style="text-align: center" OnSelectedIndexChanged="rblJornada_SelectedIndexChanged" RepeatDirection="Horizontal" Width="90%">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="auto-style17">Observaciones:&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td class="auto-style14">
                <asp:TextBox ID="txtObservac" runat="server" Rows="3" TextMode="MultiLine" 
                    Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style12">
                &nbsp;</td>
            <td class="auto-style14">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="2">
                <asp:Menu ID="mnuOpciones" runat="server" BackColor="#000066" 
                    BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" Font-Bold="True" 
                    Font-Size="Medium" ForeColor="White" 
                    OnMenuItemClick="mnuOpciones_MenuItemClick" Orientation="Horizontal" 
                    RenderingMode="Table" Width="98%" DynamicHorizontalOffset="2">
                    <Items>
                        <asp:MenuItem Text="Buscar" Value="opcBuscar"></asp:MenuItem>
                        <asp:MenuItem Text="Agregar" Value="opcAgregar"></asp:MenuItem>
                        <asp:MenuItem Text="Modificar" Value="opcModificar"></asp:MenuItem>
                        <asp:MenuItem Text="Grabar" Value="opcGrabar"></asp:MenuItem>
                        <asp:MenuItem Text="Cancelar" Value="opcCancelar"></asp:MenuItem>
                        <asp:MenuItem Text="Imprimir" Value="opcImprimir"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle BackColor="Black" BorderStyle="Groove" BorderWidth="2px" BorderColor="White" />
                    <StaticSelectedStyle BackColor="#003366" 
                        BorderStyle="Solid" BorderWidth="2px" Font-Size="Large" HorizontalPadding="12px" />
                </asp:Menu>
            </td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="2">
                            <asp:GridView ID="grvDatos" runat="server" Width="98%" OnSelectedIndexChanged="grvDatos_SelectedIndexChanged1" OnRowCommand="grvDatos_RowCommand1">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Imagenes/Buscar.jpg" ShowSelectButton="True">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                        </td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="2">
                <table align="center" class="auto-style5">
                    <tr>
                        <td>
                <asp:Label ID="lblMsj" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
                </asp:Content>
<asp:Content ID="Content5" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style7 {
            width: 96%;
            border: 4px solid #003366;
        }
        .auto-style8 {
            font-size: large;
            font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande", "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
            color: #FFFFFF;
            height: 36px;
        }
        .auto-style9 {
            font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande", "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
            text-align: right;
        }
        .auto-style10 {
            height: 31px;
            text-align: left;
        }
        .auto-style12 {
            text-align: right;
        }
        .auto-style13 {
            height: 31px;
            text-align: right;
            font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif;
        }
        .auto-style14 {
            text-align: left;
        }
        .auto-style16 {
            font-size: medium;
        }
        .auto-style17 {
            font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif;
            text-align: right;
        }
        .auto-style18 {
            font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif;
            text-align: right;
            height: 38px;
        }
        .auto-style19 {
            text-align: left;
            height: 38px;
        }
    </style>
</asp:Content>

