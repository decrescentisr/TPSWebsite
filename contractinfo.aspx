<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contractinfo.aspx.cs" Inherits="WebApplication1.contractinfo" %>

<!DOCTYPE html>
<html>
<head>
    <link rel="stylesheet" href="css/w3.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="css/Site.css" />
    <link rel="stylesheet" href="css/style.css" />

    <link rel="stylesheet" href="css/w3.css" />
    <title>Contract information</title>
</head>
<body>
    <!-- Navbar (sit on top) -->
    <div class="w3-top">
        <div class="w3-bar" id="myNavbar">
            <a class="w3-bar-item w3-button w3-hover-black w3-hide-medium w3-hide-large w3-right" href="javascript:void(0);" onclick="toggleFunction()" title="Toggle Navigation Menu">
                <i class="fa fa-bars"></i>
            </a>
            <a href="javascript:void(0);" onclick="toggleFunction()" title="School District Demo Website" class="navbar-brand"><p class="w3-bar-item"> <b>TPS Staffing</b></p> </a>
            <a href="contact.html" class="w3-bar-item w3-button w3-hide-small"><i class="fa fa-envelope"></i> CONTACT</a>
            <a href="social.html" class="w3-bar-item w3-button w3-hide-small"><i class="fa fa-th"></i> SOCIAL</a>
            <a href="about.html" class="w3-bar-item w3-button w3-hide-small"><i class="fa fa-user"></i> ABOUT</a>
            <a href="clients.html" class="w3-bar-item w3-button w3-hide-small"><i class="fa fa-briefcase"></i>&nbsp; CLIENTS</a>
            <a href="login.aspx" class="w3-bar-item w3-button w3-hide-small"><i class="fa fa-briefcase"></i>&nbsp;LOGIN PORTAL</a>
            <a href="index.html" class="w3-bar-item w3-button"><i class="fa fa-home"></i> &nbsp; HOME</a>

        </div>

        <!-- Navbar on small screens -->
        <div id="navDemo" class="w3-bar-block w3-white w3-hide w3-hide-large w3-hide-medium">
            <a href="clients.html" class="w3-bar-item w3-button" onclick="toggleFunction()"><i class="fa fa-briefcase"></i>&nbsp;CLIENTS</a>
            <a href="login.aspx" class="w3-bar-item w3-button"><i class="fa fa-rss"></i> &nbsp;LOGIN PORTAL</a>
            <a href="about.html" class="w3-bar-item w3-button" onclick="toggleFunction()"><i class="fa fa-user"></i> &nbsp; ABOUT</a>
            <a href="social.html" class="w3-bar-item w3-button" onclick="toggleFunction()"><i class="fa fa-th"></i>&nbsp;SOCIAL</a>
            <a href="contact.html" class="w3-bar-item w3-button" onclick="toggleFunction()"><i class="fa fa-envelope"></i> &nbsp;CONTACT</a>

        </div>
    </div>

    <div class="hero-image">
        <img src="images/contractinfo.jpeg" class="responsive" />
        <div class="hero-text">
            <h1 style="color: black; text-align: center;"> Contract Information</h1>
        </div>
    </div>

    <form id="form1" runat="server">
        <div class="content-container">
            <div class="grid-overwrap">
                <center><asp:GridView ID="staff_grid" runat="server" AllowSorting="True" CssClass="staff-request-table" OnRowCreated="RowCreated" OnSorting="SortDataSet" OnSelectedIndexChanged="staff_grid_SelectedIndexChanged">
                </asp:GridView></center>
            </div>
            <br />
            <asp:Button ID="btnOpen" runat="server" Text="Open" Visible="False"  CssClass="btn btn-primary" OnClick="btnOpen_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="btn btn-primary" OnClick="btnDelete_Click" />
            <br />
            <br /><center><asp:FileUpload ID="fileupContract" runat="server" CssClass="center" /></center>
            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btnUpload_Click" />
        </div>
    </form>

    <footer>
        <div class="jumbotron-footer">
            <hr style="color:grey" />
            <div class="row">
                <div class="col-sm-2">
                    <p><img src="images/TPSlogo.png" style="width: 150px; height: 90px;" /> </p><br />
                    <p style="font-size:12px;">TPS Staffing is a full service staffing website focusing on providing information and solutions to clients and employers so that their staffing needs are met.</p>
                </div>
                <div class="col-sm-3">
                    <h2 style="font-size:25px">Fun Legal Stuff</h2>

                    <a href="terms.html" style="text-decoration:none;">Terms & Conditions</a><br />
                    <a href="privacy.html" style="text-decoration:none;">Privacy Policy</a><br />
                </div>
                <div class="col-sm-3">
                    <h2 style="font-size: 25px">About TPS Staffing</h2>
                    <a href="about.html" style="text-decoration:none;">About Us</a><br />

                </div>

                <div class="col-sm-3">
                    <h2 style="font-size: 25px">Home</h2>
                    <a href="index.html" style="text-decoration:none;">Home Page</a><br />

                </div>
            </div>

        </div>
        <div class="jumbotron-footer">
            <p style="text-align:center">&copy; 2019 &nbsp;&nbsp;TPS Staffing All Rights Reserved. &nbsp;&nbsp; <a href="sitemap.html" style="text-decoration:none;">Site Map</a></p>
        </div>

    </footer>
    <script>
        // Change style of navbar on scroll
        window.onscroll = function () { myFunction() };
        function myFunction() {
            var navbar = document.getElementById("myNavbar");
            if (document.body.scrollTop > 100 || document.documentElement.scrollTop > 100) {
                navbar.className = "w3-bar" + " w3-card" + " w3-animate-top" + " w3-white";
            } else {
                navbar.className = navbar.className.replace(" w3-card w3-animate-top w3-white", "");
            }
        }
        // Used to toggle the menu on small screens when clicking on the menu button
        function toggleFunction() {
            var x = document.getElementById("navDemo");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
            } else {
                x.className = x.className.replace(" w3-show", "");
            }
        }
    </script>


</body>
</html>

