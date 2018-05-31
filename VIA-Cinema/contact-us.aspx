<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="contact-us.aspx.cs" Inherits="VIA_Cinema.contact_us" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Contact us</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="headerContent" runat="server">
    <h1>Contact Us</h1>
    <h3><small>Everything you need to get in touch with us</small></h3>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentTop" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row">
        <div class="col-md-4 col-sm-12">
            <h3 class="title">Info</h3>

            <div class="details-wrapper">
                <p>If you want to contact us, please use the following data (the contact form doesn't work):</p>

                <ul class="contact-details">
                    <li>
                        <i class="icon-phone"></i>
                        <strong>Phone:</strong>
                        <span>(123) 123-456 </span>
                    </li>
                    <li>
                        <i class="icon-printer"></i>
                        <strong>Fax:</strong>
                        <span>(123) 123-456 </span>
                    </li>
                    <li>
                        <i class="icon-paper-plane"></i>
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
