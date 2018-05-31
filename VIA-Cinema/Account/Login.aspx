<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VIA_Cinema.Account.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Login</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <h1>Login Form</h1>
    <h3><small>Fill your data to log into the system</small></h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <div class="alert alert-danger" id="formError" runat="server">
    </div>

    <div class="form-group">
        <label for="email">Email address:</label>
        <input type="email" class="form-control" id="email" runat="server" placeholder="Enter your email">
    </div>

    <div class="form-group">
        <label for="password">Password:</label>
        <input type="password" class="form-control" id="password" runat="server" placeholder="Enter your password">
    </div>

    <asp:button runat="server" text="Login" onclick="UserLogin" cssClass="btn btn-secondary" />

    <p>
        Don't you have an account? <a href="Registration.aspx">Register here!</a>
    </p>
</asp:Content>
