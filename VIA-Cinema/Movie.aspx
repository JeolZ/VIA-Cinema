<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Movie.aspx.cs" Inherits="VIA_Cinema.Movie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%=title.Text%></title>
    <!--style for the image-->
    <style>
        img.movieImage {
            width: 100%;
            max-width: 300px;
            margin: 20px;
            margin-top: -80px;
            box-shadow: 0 8px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }
        
        #mainContent_carousel_wrapper {
            position: relative;
            width: 100%;
            padding-top: 56.5%;
        }

        .carousel, .carousel-inner, .carousel-item {
            position: absolute !important; 
            top: 0;
            bottom: 0; 
            left: 0;
            right: 0;
            height: 100% !important;
        }

        iframe {
            width: 100% !important;
            height: 100% !important;
        }

        div.custom-header {
            background: linear-gradient( rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0.7) ), url('<%=imgSrc%>') no-repeat fixed 50% 20%;
            background-size: cover;
            height: 350px;
        }

        @media(max-width:767px) {
            h1.title {
                color: #0c0c0c;
                text-align: center;
            }
            img.movieImage {
                width: 300px;
            }
        }
        @media(min-width:768px) {
            h1.title {
                color: #FFF;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row" style="margin-top: -250px;">
        <div class="col-md-4 col-sd-12" style="text-align: center">
            <asp:Image ID="image" runat="server" />
        </div>
        <div class="col-md-8 col-sm-12">
            <h1 class="title"><asp:Label ID="title" runat="server" Text=""></asp:Label></h1>
        </div>
    </div>
    <div class="row">
        <!--movie info-->
        <div class="col-md-7 col-sm-12" style="padding: 50px;">
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

            <h3 id="carousel_title" style="margin-top: 50px;" runat="server">Gallery</h3>
            <div id="carousel_wrapper" runat="server">
            <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
              <ol id="selectors" class="carousel-indicators" runat="server">
              </ol>
              <div id="images" class="carousel-inner" runat="server">
              </div>
              <a id="prev" runat="server" class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
              </a>
              <a id="next" runat="server" class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
              </a>
            </div>
        </div>
        </div>
        <!--shows info (for the week)-->
        <div class="col-md-5 col-sd-12" style="padding: 50px;">
            <h3>Next Shows:</h3>
            <div id="showsWrapper" runat="server">
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
    </div>

    <!--script to show the room as tooltip, on the mouse over the show time-->
    <script>
        $(document).ready(function(){
            $('[data-toggle="tooltip"]').tooltip(); 
            $('.carousel').carousel({
                interval: false
            });
        });
    </script>
</asp:Content>
