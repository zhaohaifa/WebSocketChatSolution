<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <h1>Your Ip：<%= Ip.ToString() %></h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a  class="btn btn-primary btn-lg"  id="caption"  href="javascript:;">Learn more &raquo;</a></p>
        <img src="/PicturesHandler.ashx" alt="Alternate Text" />
       <button id="send" disabled="disabled">send</button> <input type="Text" name="name" value="" id="txt" />
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default">Learn more &raquo;</a>
            </p>
        </div>
    </div>
    
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server" ID="cScript">
    <script>
        $(function () {
            window.onbeforeunload = function () { return "确认关闭或刷洗么，将丢失聊天数据";}

            //$(window).on('beforeunload', function () { return 'Your own message goes here...'; });
            $("#caption").click(function () {
                var socket = new WebSocket("ws://192.168.16.87:8080/");
                socket.onopen = function () {
                    $("#send")[0].disabled = false;
                    socket.send("123");
                    $("#send").click(function (e) {
                        socket.send($("#txt").val());
                        e.preventDefault();
                        e.stopPropagation();
                    })
                }
                socket.onmessage = function (e) {
                    var msg = document.createElement('div');
                    msg.style.color = '#fff';
                    msg.innerHTML = e.data;
                    $(".jumbotron").prepend(msg);
                };
            })
        })
    </script>
</asp:Content>
