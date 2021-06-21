﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AEContacto.aspx.cs" Inherits="Agenda.AEContacto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <table width="100%" id ="TablaDestino" runat ="server" >
            <tr> 
                 <td> 
                        <asp:Label ID="LabelNombre" runat="server" Text="Apellido y Nombre" CssClass="TextoConsulta"></asp:Label> 
                    </td>
                    <td > 
                        <asp:TextBox ID="TextBoxNombre" runat="server"  Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                    </td>
                    <td> 
                        <asp:Label ID="LabelGenero" runat="server" Text="Genero"  CssClass="TextoConsulta" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ListaDeGenero" runat="server" Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:DropDownList>
                    </td>
                    <td> 
                        <asp:Label ID="LabelPais" runat="server" Text="País"  CssClass="TextoConsulta" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ListaDePaises" runat="server" Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:DropDownList>
                    </td>
            </tr>
             <tr>
                  <td> 
                        <asp:Label ID="LabelLocalidad" runat="server" Text="Localidad" CssClass="TextoConsulta"></asp:Label> 
                    </td>
                    <td > 
                        <asp:TextBox ID="TextBoxLocalidad" runat="server"  Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                    </td>
                     <td> 
                        <asp:Label ID="LabelCI" runat="server" Text="Contacto Interno"  CssClass="TextoConsulta" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownCI" runat="server" Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:DropDownList>
                    </td>
                     <td> 
                        <asp:Label ID="LabelOrganizacion" runat="server" Text="Organizacion" CssClass="TextoConsulta"></asp:Label> 
                     </td>
                     <td > 
                        <asp:TextBox ID="TextBoxOrganizacion" runat="server"  Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                     </td>
             </tr>
             <tr> 
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
                    
                    <td> 
                        <asp:Label ID="LabeDireccion" runat="server" Text="Direccion" CssClass="TextoConsulta"></asp:Label> 
                    </td>
                    <td > 
                        <asp:TextBox ID="TextBoxDireccion" runat="server"  Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                    </td>  
                </tr>
             <tr>
                  <td> 
                        <asp:Label ID="LabelTF" runat="server" Text="Telefono Fijo - Interno" CssClass="TextoConsulta"></asp:Label> 
                    </td>
                    <td > 
                        <asp:TextBox ID="TextBoxTF" runat="server"  Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                    </td>
                    <td> 
                        <asp:Label ID="LabelTC" runat="server" Text="Telefono Celular"  CssClass="TextoConsulta" ></asp:Label>
                    </td>
                     <td > 
                        <asp:TextBox ID="TextBoxTC" runat="server"  Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                    </td>
                    <td> 
                        <asp:Label ID="LabelEmail" runat="server" Text="E-mail" CssClass="TextoConsulta" ></asp:Label> 
                    </td>
                    <td >
                        <asp:TextBox ID="TextBoxEmail" runat="server" Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                    </td>
             </tr>
             <tr>
                  <td> 
                        <asp:Label ID="LabelCS" runat="server" Text="Cuenta Skype" CssClass="TextoConsulta"></asp:Label> 
                    </td>
                    <td > 
                        <asp:TextBox ID="TextBoxCS" runat="server"  Width="250px" Height="30px" Font-Size="Larger" CssClass="Consulta"></asp:TextBox>
                    </td>
             </tr>
              <tr> 
                    <td>
                        <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar"
                            Width="100px" Height="40px" BackColor="Green" Font-Size="Larger" Font-Bold="true" ForeColor="WhiteSmoke" onClick="Guardar" />
                    </td>
                    <td>
                        <asp:Button ID="ButtonSalir" runat="server" Text="Salir"
                            Width="100px" Height="40px" BackColor="Blue" Font-Size="Larger" Font-Bold="true" ForeColor="WhiteSmoke"  />
                    </td>
                </tr>
          </table>
    </form>
</body>
</html>
