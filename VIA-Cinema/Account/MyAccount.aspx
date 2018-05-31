<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="VIA_Cinema.Account.MyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>My Account</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <h1>Your Account</h1>
    <h3><small>Here you can edit all your active bookings and your personal data</small></h3>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">

    <!-- hi message and logout button -->
    <div style="text-align: center; font-size: 20px;">
        <h3><asp:Label ID="hi" runat="server" Text=""></asp:Label></h3>
        <a href="LogOut.aspx" class="btn btn-primary">Log out</a>
    </div>

    <!-- booking management -->
    <h4 data-toggle="collapse" data-target="#mainContent_bookingsListWrapper" style="cursor: pointer" title="click to show">Your active bookings:</h4>
    <div id="bookingsListWrapper" runat="server" class="collapse">
    <table id="bookingsList" class="table table-hover" runat="server">
        <tbody>
        </tbody>
    </table>
    </div>
    <br /><br />
    
    <!-- edit personal data -->
    <h4 data-toggle="collapse" data-target="#dataChange" style="cursor: pointer" title="click to show">Your data:</h4>
    <!-- error -->
    <div class="alert alert-danger" id="formError1" runat="server">
    </div>
    <div id="dataChange" class="collapse">
    <!-- email -->
    <div class="form-group">
        <label for="email">Email:</label>
        <input disabled type="text" class="form-control" id="email" runat="server">
    </div>
    <!-- name -->
    <div class="form-group">
        <label for="name">Name:</label>
        <input type="text" class="form-control" id="name" runat="server">
    </div>
    <!-- surname -->
    <div class="form-group">
        <label for="surname">Surname:</label>
        <input type="text" class="form-control" id="surname" runat="server">
    </div>
    <!-- button -->
    <asp:Button ID="Button2" onclick="EditData" runat="server" Text="Edit Data" CssClass="btn btn-secondary" />
    </div>
    <br /><br />

    <!-- reset password -->
    <h4 data-toggle="collapse" data-target="#passwordReset" style="cursor: pointer" title="click to show">Reset password:</h4>
    <!-- error -->
    <div class="alert alert-danger" id="formError2" runat="server">
    </div>
    <div id="passwordReset" class="collapse">
        <!-- old password -->
        <div class="form-group">
            <label for="pswOld">Old Password:</label>
            <input type="password" class="form-control" id="pswOld" runat="server" placeholder="Enter your Old password">
        </div>
        <!-- new password -->
        <div class="form-group">
            <label for="password">New Password:</label>
            <input type="password" class="form-control" id="password" runat="server" placeholder="Enter your New password">
        </div>
        <!-- confirm new password -->
        <div class="form-group">
            <label for="password2">Confirm Password:</label>
            <input type="password" class="form-control" id="password2" runat="server" placeholder="Enter again your new password">
        </div>
        <!-- button to change password -->
        <asp:Button ID="Button1" onclick="ResetPassword" runat="server" Text="Reset" CssClass="btn btn-secondary" />
    </div>
</asp:Content>