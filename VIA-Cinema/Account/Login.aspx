<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VIA_Cinema.Account.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <div class="alert alert-danger">
        <asp:label id="formError" runat="server" text=""></asp:label>
    </div>
    <div class="formField">
        <div class="label"><asp:label runat="server" text="Email"></asp:label></div>
        <div class="field"><asp:textbox id="email" runat="server"></asp:textbox></div>
    </div>

    <div class="formField">
        <div class="label"><asp:label runat="server" text="Password"></asp:label></div>
        <div class="field"><asp:textbox id="password" runat="server" TextMode="password"></asp:textbox></div>
    </div>

    <asp:button runat="server" text="Login" onclick="UserLogin" />
</asp:Content>
