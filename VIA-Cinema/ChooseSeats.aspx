<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChooseSeats.aspx.cs" Inherits="VIA_Cinema.ChooseSeats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
    <asp:Label ID="info" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <div style="margin:0 auto; text-align: center; width: 400px;">
        <p style="font-size: 12px;">
            <b style="color: red">Red</b> seats are <b>not</b> available.<br />
            <b style="color: green">Green</b> seats are available.
        </p>
        <hr style="height: 5px; width: 300px; background-color: grey; border: none; opacity: 0.6;" title="Screen" data-toggle="tooltip" />
        <asp:table runat="server" id="seats" CssClass="seats"></asp:table>
        <asp:button runat="server" text="Confirm" onclick="ConfirmSeats" CssClass="btn btn-primary" />
        <div class="alert alert-danger" id="formError" runat="server" style="margin-top: 10px;">
    </div>
    </div>
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
