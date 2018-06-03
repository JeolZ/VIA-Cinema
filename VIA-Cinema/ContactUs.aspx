<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="VIA_Cinema.ContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Contact us</title>
    <style>
        div.custom-header {
            height: 300px;
        }
        .map-Wrapper {
            position:relative;
            margin: -140px auto 50px auto;
            width: 90%;
            padding-top: 30%;
            text-align: center;
        }
        .map-Wrapper > iframe {
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            width: 100%;
            height: 100%;
            border: 10px solid #fff;
            border-radius: 30px 30px 0 0;
            margin: 0 auto;
        }
        @media(max-width:767px) {
            .map-Wrapper {
                padding-top: 50% !important;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <h1>Contact Us</h1>
    <h3><small>Everything you need to get in touch with us</small></h3>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <div class="map-Wrapper">
        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2238.4810738863252!2d9.88399121534375!3d55.871669290286164!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x464c63be87e17d19%3A0xa65113d28ba174ba!2sVIA+University+College!5e0!3m2!1sit!2sit!4v1528020012449" frameborder="0" style="border:0" allowfullscreen></iframe>
    </div>
    <div class="row">
        <div class="col-md-4 col-sm-12">
            <h3 class="title">Info</h3>

            <div>
                <p>If you want to contact us, please use the following data (the contact form doesn't work):</p>

                <ul class="list-group list-group-flush">
                    <li class="list-group-item" style="border-top: 0">
                        <strong>Address:</strong>
                        <span>Chr M Østergaards Vej 4, 8700 Horsens, Denmark</span>
                    </li>
                    <li class="list-group-item">
                        <strong>Phone:</strong>
                        <span>(123) 123-456 </span>
                    </li>
                    <li class="list-group-item">
                        <strong>Fax:</strong>
                        <span>(123) 123-456 </span>
                    </li>
                    <li class="list-group-item">
                        <strong>E-Mail:</strong>
                        <span><a href="#">info@viacinema.com</a></span>
                    </li>
                </ul>
            </div>
        </div>

        <div class="col-md-8 col-sm-12">
            <h3 class="title">Contact Form</h3>

            <div class="form-group">
                <input class="form-control input-box" type="text" name="name" placeholder="Your Name" autocomplete="off">
            </div>

            <div class="form-group">
                <input class="form-control input-box" type="email" name="email" placeholder="your-email@viacinema.com" autocomplete="off">
            </div>

            <div class="form-group">
                <input class="form-control input-box" type="text" name="subject" placeholder="Subject" autocomplete="off">
            </div>

            <div class="form-group mb20">
                <textarea class="form-control textarea-box" rows="8" name="message" placeholder="Type your message..."></textarea>
            </div>

            <div class="form-group text-center">
                <button class="btn btn-main btn-effect" type="submit">Send message</button>
            </div>
        </div>
    </div>
</asp:Content>
