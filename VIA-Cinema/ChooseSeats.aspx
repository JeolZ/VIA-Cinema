<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChooseSeats.aspx.cs" Inherits="VIA_Cinema.ChooseSeats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Book a seat for <%=title%></title>
    <!-- style to set up the table -->
    <style>
        div.custom-header {
            background: linear-gradient( rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0.7) ), url('<%=imgSrc%>') no-repeat fixed 50% 20%;
            background-size: cover;
        }
        #headerContent_info {
            color: white;
        }
        table.seats{
            margin: 10px auto;
            text-align: center;
            background-color: yellowgreen;
            opacity: 0.4;
        }
        table.seats td {
            width: 15px;
            height: 10px;
            opacity: 1;
            padding: 2px;
        }
        td.AvailableSeat {
            background-color: green;
            opacity: 0.5;
        }
        td.TakenSeat {
            background-color: red;
            opacity: 1;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <!-- this label will contain the title of the movie and the info for the show -->
    <asp:Label ID="info" runat="server" Text="Label"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <div style="margin:0 auto; text-align: center; width: 400px;">
        <!--explanation-->
        <p style="font-size: 12px;">
            <b style="color: red">Red</b> seats are <b>not</b> available.<br />
            <b style="color: green">Green</b> seats are available.
        </p>

        <!--draw "screen" using an hr-->
        <hr style="height: 5px; width: 300px; background-color: grey; border: none; opacity: 0.6;" title="Screen" data-toggle="tooltip" />
        
        <!--table for the seats-->
        <asp:table runat="server" id="seats" CssClass="seats"></asp:table>

        <!--button to confirm-->
        <asp:button runat="server" text="Confirm" onclick="ConfirmSeats" CssClass="btn btn-primary" />
        
        <!--error-->
        <div class="alert alert-danger" id="formError" runat="server" style="margin-top: 10px;">
    </div>
    </div>

    <!-- script for the tooltip to show the seat number on the mouse over -->
    <script>
        $(document).ready(function () {
            $('input:checkbox').each(function () {
                var id = $(this).attr("id");
                $(this).attr("data-toggle", "tooltip").attr("title", id.substring(12));
            });
            $('.TakenSeat').html("<input type=\"checkbox\" checked disabled style=\"pointer-events: none;\" />").attr("data-toggle", "tooltip").attr("title", "Taken");
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
</asp:Content>
