<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="VIA_Cinema.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>VIA Cinema</title>
    <style>
        #header{
            /* here the background image should be changed every page */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    HERE a SLIDESHOW with "Coming soon" and "Just released" movies<br />
    The SLIDESHOW changes the header background image
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
    Don't know yet (this should be a "title" for the content below)
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    Don't know yet (this should be an important content for the page)
</asp:Content>
