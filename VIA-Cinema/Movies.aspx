<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Movies.aspx.cs" Inherits="VIA_Cinema.Movies" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>What's on</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <h1>What's on</h1>
    <h3><small>All the movies.</small></h3>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row">
        <div class="col-md-10 col-sm-12">
            <h3>All the movies:</h3>
        </div>
        <div class="col-md-2 col-sm-12">
            <div class="btn-group">
              <a href="WhatsOn.aspx" class="btn btn-secondary">Week</a>
              <a href="#" class="btn btn-primary active">All</a>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-9 col-sm-12" id="movieList" runat="server">

        </div>
        <div class="col-md-3 col-sm-12">
            <h4>Categories:</h4>
        </div>
    </div>
</asp:Content>
