<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="VIA_Cinema.ThankYou" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Thank you</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <h1>Thank you!</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
    <small>Thank you for the purchase! See you soon!</small>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <h3><small>Your Reservation ID is </small>
        <asp:Label ID="id" runat="server" Text=""></asp:Label></h3>
    <br />
    <div style="text-align: center">
        <a href="index.aspx" class="btn btn-primary">Go to the Homepage</a>
    </div>
</asp:Content>
