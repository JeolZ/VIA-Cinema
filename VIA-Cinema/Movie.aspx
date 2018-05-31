﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Movie.aspx.cs" Inherits="VIA_Cinema.Movie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Movie details</title>
    <!--style for the image-->
    <style>
        img.movieImage {
            width: 100%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <h1><asp:Label ID="title" runat="server" Text=""></asp:Label></h1>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row">
        <!--movie info-->
        <div class="col-8">
            <div class="row">
                <!--image-->
                <div class="col-5">
                    <asp:Image ID="image" runat="server" />
                </div>
                <!--plot and other info-->
                <div class="col-7">
                    <h3>Plot</h3>
                    <p>
                        <asp:Label ID="description" runat="server" Text=""></asp:Label>
                    </p>
                    <p style="font-size: 12px;">
                        Duration: <asp:Label ID="duration" runat="server" Text=""></asp:Label>'
                    </p>
                    <p style="font-size: 12px;">
                        Realeased on: <asp:Label ID="relDate" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
        </div>
        <!--shows info (for the week)-->
        <div class="col-4">
            <h3>Shows:</h3>
            <h4><small>To book, press the time you would like to go to the Cinema.</small></h4>
            <table class="table table-hover">
                <tbody>
                  <tr>
                    <td id="day0" runat="server">Today</td>
                    <td id="day0_content" runat="server"></td>
                  </tr>
                  <tr>
                    <td id="day1" runat="server">Tomorrow</td>
                    <td id="day1_content" runat="server"></td>
                  </tr>
                  <tr>
                    <td id="day2" runat="server"></td>
                    <td id="day2_content" runat="server"></td>
                  </tr>
                  <tr>
                    <td id="day3" runat="server"></td>
                    <td id="day3_content" runat="server"></td>
                  </tr>
                  <tr>
                    <td id="day4" runat="server"></td>
                    <td id="day4_content" runat="server"></td>
                  </tr>
                  <tr>
                    <td id="day5" runat="server"></td>
                    <td id="day5_content" runat="server"></td>
                  </tr>
                  <tr>
                    <td id="day6" runat="server"></td>
                    <td id="day6_content" runat="server"></td>
                  </tr>
                </tbody>
            </table>
        </div>
    </div>

    <!--script to show the room as tooltip, on the mouse over the show time-->
    <script>
        $(document).ready(function(){
            $('[data-toggle="tooltip"]').tooltip(); 
        });
    </script>
</asp:Content>
