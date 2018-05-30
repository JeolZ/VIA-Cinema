<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="VIA_Cinema.Account.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <asp:label id="formError" runat="server" text=""></asp:label>
    
    <div class="formField">
        <div class="label"><asp:label runat="server" text="Name"></asp:label></div>
        <div class="field"><asp:textbox id="name" runat="server"></asp:textbox></div>
    </div>

    <div class="formField">
        <div class="label"><asp:label runat="server" text="Surname"></asp:label></div>
        <div class="field"><asp:textbox id="surname" runat="server"></asp:textbox></div>
    </div>

    <div class="formField">
        <div class="label"><asp:label runat="server" text="Email"></asp:label></div>
        <div class="field"><asp:textbox id="email" runat="server"></asp:textbox></div>
    </div>

    <div class="formField">
        <div class="label"><asp:label runat="server" text="Password"></asp:label></div>
        <div class="field"><asp:textbox id="password" runat="server" TextMode="password"></asp:textbox></div>
    </div>

    <div class="formField">
        <div class="label"><asp:label runat="server" text="Confirm password"></asp:label></div>
        <div class="field"><asp:textbox id="password2" runat="server" TextMode="password"></asp:textbox></div>
    </div>

    <asp:button runat="server" text="Register" onclick="UserRegistration" />
</asp:Content>
