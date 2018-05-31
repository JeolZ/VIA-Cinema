<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="VIA_Cinema.Account.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Registration</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <h1>Registration form</h1>
    <h3><small>Fill all the fields and start use our system at 100%!</small></h3>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <!-- error -->
    <div class="alert alert-danger" id="formError" runat="server">
    </div>
    
    <!-- name -->
    <div class="form-group">
        <label for="name">Name:</label>
        <input type="text" class="form-control" id="name" runat="server" placeholder="Enter your name">
    </div>
    
    <!-- surname -->
    <div class="form-group">
        <label for="surname">Surname:</label>
        <input type="text" class="form-control" id="surname" runat="server" placeholder="Enter your surname">
    </div>
    
    <!-- email -->
    <div class="form-group">
        <label for="email">Email address:</label>
        <input type="email" class="form-control" id="email" runat="server" placeholder="Enter your email">
    </div>
    
    <!-- password -->
    <div class="form-group">
        <label for="password">Password:</label>
        <input type="password" class="form-control" id="password" runat="server" placeholder="Enter your password">
    </div>
    
    <!-- password confirmation -->
    <div class="form-group">
        <label for="password2">Password:</label>
        <input type="password" class="form-control" id="password2" runat="server" placeholder="Enter again your password">
    </div>
    
    <!-- button to register -->
    <asp:button runat="server" text="Register" onclick="UserRegistration" cssClass="btn btn-secondary" />
</asp:Content>
