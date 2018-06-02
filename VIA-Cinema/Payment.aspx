<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="VIA_Cinema.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Payment</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <h1>Payment</h1>
    <h3><small>Pay and get your tickets</small></h3>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <!--Go back button-->
    <asp:HyperLink ID="back" runat="server" CssClass="btn btn-primary">Go back</asp:HyperLink>

    <!--Summary of the purchase-->
    <div class="alert alert-info">
        <h4>Check your purchase:</h4>
        <asp:label runat="server" text="" id="summary"></asp:label>
    </div>
    
    <!--error-->
    <div class="alert alert-danger" id="formError" runat="server">
    </div>
    
    <!--drop-down list for the saved cards (enabled only if logged in)-->
    <asp:dropdownlist runat="server" id="savedCards" CssClass="form-control"></asp:dropdownlist>
    <div class="form-group" id="savedCardsWrapper" runat="server" style="text-align: center">
    </div>

    <!--fields to use a new credit card-->
    <div id="newCreditCard">
        <h3>Credit Card Data: </h3>
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
        <!--checkbox to save the card for later use (only if logged in)-->
        <div id="saveCardWrapper" runat="server">
            <asp:Label runat="server" Text="Save the card for future purchases"></asp:Label>
            <asp:checkbox runat="server" id="saveCard"></asp:checkbox>
        </div>
    </div>
    <!--button to pay-->
    <div style="text-align: center">
    <asp:button runat="server" text="Pay!" onclick="CheckOrder" class="btn btn-primary" />
    </div>

    <!--script to set limits to the expiration date-->
    <script>
        $(document).ready(function () {
            var d = new Date();
            $('#mainContent_exp').attr("min", d.getFullYear() + '-' + (d.getMonth()+1))
                .attr("max", (d.getFullYear() + 20) + '-12');

            $('#mainContent_savedCards').change(function () {
                if ($(this).val() != "None")
                    $('#newCreditCard').hide();
                else
                    $('#newCreditCard').show();
            })
        });
    </script>
</asp:Content>
