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
    <div class="alert alert-info">
        <h4>Check your purchase:</h4>
        <asp:label runat="server" text="" id="summary"></asp:label>
    </div>
    
    <div class="alert alert-danger" id="formError" runat="server">
    </div>
    
    <div class="form-group" id="savedCardsWrapper" runat="server">
        <h4>Old saved (non-expired) credit cards: </h4>
        <asp:dropdownlist runat="server" id="savedCards" CssClass="form-control"></asp:dropdownlist>
    </div>

    <div id="newCreditCard">
        <h3>Credit Card Data: </h3>
        <div class="form-group">
            <label for="name">Credit Card Number:</label>
            <input type="text" maxlength="16" class="form-control" id="cardn" runat="server" placeholder="Enter your Credit Card Number">
        </div>
        
        <div class="form-group">
            <label for="owner">Owner:</label>
            <input type="text" class="form-control" id="owner" runat="server" placeholder="Enter the name of the Owner">
        </div>

        <div class="form-group">
            <label for="exp">Expiration Date:</label>
            <input type="month" class="form-control" id="exp" runat="server">
        </div>
        
        <div class="form-group">
            <label for="name">Security Code:</label>
            <input type="text" maxlength="3" class="form-control" id="code" runat="server" placeholder="Enter your 3-digit Security Code">
        </div>

        <div id="saveCardWrapper" runat="server">
            <asp:Label runat="server" Text="Save the card for future purchases"></asp:Label>
            <asp:checkbox runat="server" id="saveCard"></asp:checkbox>
        </div>
    </div>
    <div style="text-align: center">
    <asp:button runat="server" text="Pay!" onclick="CheckOrder" class="btn btn-primary" />
    </div>

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
