<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WhatsOn.aspx.cs" Inherits="VIA_Cinema.WhatsOn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>What's on</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <h1>What's on</h1>
    <h3><small>All the movies for this week.</small></h3>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row">
        <div class="col-10">
            <h3>This week movies:</h3>
            <h4><small>To book, press the time you would like to go to the Cinema.</small></h4>
        </div>
        <div class="col-2">
            <div class="btn-group">
              <a href="#" class="btn btn-primary active">Week</a>
              <a href="Movies.aspx" class="btn btn-secondary">All</a>
            </div>
        </div>
    </div>
    <!-- Nav tabs -->
    <ul class="nav nav-tabs">
      <li class="nav-item">
        <a class="nav-link active" data-toggle="tab" href="#mainContent_day0">Today</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" data-toggle="tab" href="#mainContent_day1" runat="server" id="d1">Tomorrow</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" data-toggle="tab" href="#mainContent_day2" runat="server" id="d2"></a>
      </li>
      <li class="nav-item">
        <a class="nav-link" data-toggle="tab" href="#mainContent_day3" runat="server" id="d3"></a>
      </li>
      <li class="nav-item">
        <a class="nav-link" data-toggle="tab" href="#mainContent_day4" runat="server" id="d4"></a>
      </li>
      <li class="nav-item">
        <a class="nav-link" data-toggle="tab" href="#mainContent_day5" runat="server" id="d5"></a>
      </li>
      <li class="nav-item">
        <a class="nav-link" data-toggle="tab" href="#mainContent_day6" runat="server" id="d6"></a>
      </li>
    </ul>

    <!-- Tab panes -->
    <div class="tab-content">
      <div class="tab-pane container active" runat="server" id="day0">...</div>
      <div class="tab-pane container fade" runat="server" id="day1">...</div>
      <div class="tab-pane container fade" runat="server" id="day2">...</div>
      <div class="tab-pane container fade" runat="server" id="day3">...</div>
      <div class="tab-pane container fade" runat="server" id="day4">...</div>
      <div class="tab-pane container fade" runat="server" id="day5">...</div>
      <div class="tab-pane container fade" runat="server" id="day6">...</div>
    </div>

    <!-- script to enable tooltips on the shows times -->
    <script>
        $(document).ready(function(){
            $('[data-toggle="tooltip"]').tooltip(); 
        });
    </script>
</asp:Content>
