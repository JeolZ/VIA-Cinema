<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="VIA_Cinema.Payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <asp:label runat="server" text="" id="summarize"></asp:label>

    <asp:label id="formError" runat="server" text=""></asp:label>

    <asp:dropdownlist runat="server" id="savedCards"></asp:dropdownlist>

    <div runat="server" id="newCreditCard">
        <div class="formField">
            <div class="label"><asp:label runat="server" text="Credit Card Number"></asp:label></div>
            <div class="field"><asp:textbox id="cardn" runat="server"></asp:textbox></div>
        </div>
    
        <div class="formField">
            <div class="label"><asp:label runat="server" text="Owner"></asp:label></div>
            <div class="field"><asp:textbox id="owner" runat="server"></asp:textbox></div>
        </div>

        <div class="formField">
            <div class="label"><asp:label runat="server" text="Expiration Date"></asp:label></div>
            <div class="field"><asp:textbox id="exp" runat="server"></asp:textbox></div>
        </div>

        <div class="formField">
            <div class="label"><asp:label runat="server" text="Code"></asp:label></div>
            <div class="field"><asp:textbox id="code" runat="server"></asp:textbox></div>
        </div>

        <asp:button runat="server" text="Pay!" onclick="CheckOrder" />
    </div>

    <br /><br />
    <asp:Label runat="server" Text="Save the card for future purchases"></asp:Label>
    <asp:checkbox runat="server" id="saveCard"></asp:checkbox>

</asp:Content>
