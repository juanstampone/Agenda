<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Agenda._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="background-color: #f1f1f1" >
        <div id="DivMiPagina" runat="server">        
            <table width="100%" id ="TablaDeMiPagina" runat ="server" >
                <tr> 
                    <td> 
                        <asp:Label ID="LabelNombre" runat="server" Text="Apellido y Nombre" CssClass="TextoConsulta"></asp:Label> 
                    </td>
                    <td > 
                        <asp:TextBox ID="TextBoxNombre" runat="server"  Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                    </td>
                    <td> 
                        <asp:Label ID="LabelPais" runat="server" Text="País"  CssClass="TextoConsulta" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ListaDePaises" runat="server" Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:DropDownList>
                    </td>
                    <td> 
                        <asp:Label ID="LabelLocalidad" runat="server" Text="Localidad" CssClass="TextoConsulta" ></asp:Label> 
                    </td>
                    <td >
                        <asp:TextBox ID="TextBoxLocalidad" runat="server" Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                    </td>
                </tr>
                <tr> 
                    <td> 
                        <asp:Label ID="LabelFID" runat="server" Text="Fecha Ingreso Desde" CssClass="TextoConsulta"></asp:Label> 
                    </td>
                    <td > 
                        <asp:TextBox ID="TextBoxFID" runat="server"  Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                    </td>
                     <td> 
                        <asp:Label ID="LabelFIH" runat="server" Text="Fecha Ingreso Hasta" CssClass="TextoConsulta"></asp:Label> 
                    </td>
                    <td > 
                        <asp:TextBox ID="TextBoxFIH" runat="server"  Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                    </td>
                     <td> 
                        <asp:Label ID="LabelCI" runat="server" Text="Contacto Interno"  CssClass="TextoConsulta" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownCI" runat="server" Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:DropDownList>
                    </td>
                </tr> 
                <tr> 
                    <td> 
                        <asp:Label ID="LabelOrganizacion" runat="server" Text="Organización" CssClass="TextoConsulta"></asp:Label> 
                    </td>
                    <td > 
                        <asp:TextBox ID="TextBoxOrganizacion" runat="server"  Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                    </td>
                     <td> 
                        <asp:Label ID="LabelArea" runat="server" Text="Área"  CssClass="TextoConsulta" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListArea" runat="server" Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:DropDownList>
                    </td>
                     <td> 
                        <asp:Label ID="LabelActivo" runat="server" Text="Activo"  CssClass="TextoConsulta" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListActivo" runat="server" Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:DropDownList>
                    </td>
                    
                </tr>
                <tr> 
                    <td>
                        <asp:Button ID="ButtonLimpiar" runat="server" Text="Limpiar Campos" OnClick="LimpiarCampos"
                            Width="100px" Height="40px" BackColor="Green" Font-Size="Larger" Font-Bold="true" ForeColor="WhiteSmoke" />
                    </td>
                    <td>
                        <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar"
                            Width="100px" Height="40px" BackColor="Green" Font-Size="Larger" Font-Bold="true" ForeColor="WhiteSmoke" OnClick="Consultar" />
                    </td>
                    <td>
                        <asp:Button ID="ButtonNuevoContacto" runat="server" Text="Nuevo Contacto"
                            Width="100px" Height="40px" BackColor="Blue" Font-Size="Larger" Font-Bold="true" ForeColor="WhiteSmoke" OnClick="AltaContacto" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridViewConsulta" runat="server" Text="Texto" AutoGenerateColumns="false" RowStyle-HorizontalAlign="Center"
                         HeaderStyle-CssClass ="TextoConsulta" Width="100%" GridLines="Horizontal" >
                <Columns> 
                      <asp:boundfield datafield="id" headertext="Id"/>
                      <asp:boundfield datafield="nombreApellido" headertext="Nombre y Apellido"/>
                      <asp:boundfield datafield="genero" headertext="Genero"/>
                      <asp:boundfield datafield="pais" headertext="Pais"/>
                      <asp:boundfield datafield="localidad" headertext="Localidad"/>
                      <asp:boundfield datafield="contactoInterno" headertext="Contacto Interno"/>   
                      <asp:boundfield datafield="organizacion" headertext="Organizacion"/>
                      <asp:boundfield datafield="area" headertext="Área"/>
                      <asp:boundfield datafield="fechaIngreso" headertext="Fecha Ingreso"/>   
                      <asp:boundfield datafield="activo" headertext="Activo"/>
                      <asp:boundfield datafield="direccion" headertext="Direccion"/>
                      <asp:boundfield datafield="telefonoFijo" headertext="Telefono Fijo - Interno"/>   
                      <asp:boundfield datafield="telefonoCelular" headertext="Telefono Celular"/>   
                      <asp:boundfield datafield="email" headertext="E-mail"/>
                      <asp:boundfield datafield="cuentaSkype" headertext="Cuenta Skype"/>
                      <asp:TemplateField HeaderText="Acciones">
                           <ItemTemplate>
                                <asp:ImageButton ToolTip="Consultar"  ImageUrl="/Images/zoom.png" ID="BtnConsultar" CommandName="DetalleContacto"   runat="server"></asp:ImageButton>
                                <asp:ImageButton ToolTip="Editar" ImageUrl="/Images/edit.png" ID="BtnEditar" CommandName="EditarContacto" onClick="EditarContacto" runat="server"></asp:ImageButton>
                                <asp:ImageButton ToolTip="Eliminar" ImageUrl="/Images/delete.png" ID="BtnEliminar" CommandName="Eliminar" onClick="EliminarContacto" runat="server"></asp:ImageButton>
                                <asp:ImageButton ToolTip="Pausar/Activar" ImageUrl="/Images/play_pause.png" ID="BtnActivar"  CommandName="Activar" runat="server" ></asp:ImageButton>          
                            </ItemTemplate>
                        </asp:TemplateField>
                </Columns>
           </asp:GridView>
        </div>
    </form>
</body>
</html>

