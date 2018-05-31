<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddMovie.aspx.cs" Inherits="VIA_Cinema.Administration.AddMovie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <div class="formField">
        <asp:TextBox ID="title" runat="server"></asp:TextBox>
    </div>
    <div class="formField">
        <asp:FileUpload ID="cover" runat="server" />
    </div>
    <div class="formField">
        <asp:Literal ID="description" runat="server"></asp:Literal>
    </div>
    <div class="formField">
        <asp:TextBox ID="duration" runat="server"></asp:TextBox>
    </div>
    <div class="formField">
        <asp:TextBox ID="trailer" runat="server"></asp:TextBox>
    </div>
    <div class="formField">
        <asp:Calendar ID="relDate" runat="server"></asp:Calendar>
    </div>
    <div class="formField">
        <asp:DropDownList ID="rooms" runat="server"></asp:DropDownList>
    </div>
    <div class="formField">
        Select days:
    </div>
    <asp:Button onclick="CreateMovie" runat="server" Text="Create!" />
</asp:Content>
