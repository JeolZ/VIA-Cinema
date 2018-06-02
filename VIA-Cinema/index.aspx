<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="VIA_Cinema.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>VIA Cinema</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <h1>Welcome to the VIA Cinema</h1>
    <h3><small>The web portal to keep updated about the shown movies and book a seat whenever you want.</small></h3>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    Welcome to the <b>VIA Cinema</b>, a web portal to check all the movies on the screen, as well as booking a seat.<br />
    Have a look at our movies this week checking the <b>What's On</b> page.
    <br /><br />
    <h5>Test user:</h5>
    <h6>test@test.com</h6>
    <h6>test1234</h6>
</asp:Content>
