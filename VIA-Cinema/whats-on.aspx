<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="whats-on.aspx.cs" Inherits="VIA_Cinema.whats_on" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>What's on</title>
    <style>
        #header{
            /* here the background image should be changed every page */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <h1>What's on</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
    <h3>This week movies</h3>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <div id="weekMovies" runat="server"></div>
</asp:Content>
