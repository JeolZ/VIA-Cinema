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
        <div id="log" runat="server"></div>
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
    
    <!--error-->
    <div class="alert alert-danger" id="newCardError" runat="server">
    </div>
    <!-- credit cards management -->
    <h4 data-toggle="collapse" data-target="#mainContent_creditCardsWrapper" style="cursor: pointer" title="click to show">Your saved credit cards:</h4>
    <div id="creditCardsWrapper" runat="server" class="collapse">
    <table class="table table-hover">
        <thead>
        </thead>
    </table>
    <table id="creditCards" class="table table-hover" runat="server">
        <thead>
        <tr>
            <th>Credit Card Number</th>
            <th>Expiration date</th>
            <th>Owner</th>
            <th></th>
        </tr>
        </thead>
    </table>
    <i id="creditCardsNotFound" runat="server"></i>
    <b data-toggle="collapse" data-target="#newCardWrapper" style="cursor: pointer">Click here to add a new one!</b>
    <div id="newCardWrapper" class="collapse">
        <!--credit card number-->
        <div class="form-group">
            <label for="name">Credit Card Number:</label>
            <input type="text" maxlength="16" class="form-control" id="cardn" runat="server" placeholder="Enter your Credit Card Number">
        </div>
        <!--owner name-->
        <div class="form-group">
            <label for="owner">Owner:</label>
            <input type="text" class="form-control" id="owner" runat="server" placeholder="Enter the name of the Owner">
        </div>
        <!--expiration date-->
        <div class="form-group">
            <label for="exp">Expiration Date:</label>
            <input type="month" class="form-control" id="exp" runat="server">
        </div>
        <!--CVV-->
        <div class="form-group">
            <label for="name">Security Code:</label>
            <input type="text" maxlength="3" class="form-control" id="code" runat="server" placeholder="Enter your 3-digit Security Code">
        </div>

        <asp:button runat="server" text="Add the new Card" onclick="AddCard" class="btn btn-primary" />
    </div>
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