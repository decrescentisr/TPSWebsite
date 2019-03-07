<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="staffingrequestinfo.aspx.cs" Inherits="WebApplication1.staffingrequestinfo" %>

<!DOCTYPE html>
<html>
<head>
    <link rel="stylesheet" href="css/w3.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="css/Site.css" />
    <link rel="stylesheet" href="css/style.css" />

    <link rel="stylesheet" href="css/w3.css" />
    <title></title>
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
        <img src="images/staffingrequestinfo.jpeg" class="responsive" />
        <div class="hero-text">
            <h1 style="color: black; font-size: 35px;">Staffing Request Info</h1>
        </div>
    </div>
    <form id="form1" runat="server">
        <div class="content-container">
            <asp:Panel ID="pnlSearch" runat="server" Visible="True">
                <br />
                <asp:TextBox ID="tbSearch" runat="server" placeholder="Enter a Request ID" CssClass="text-field" ></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                <br />
                <asp:Label ID="lblError" runat="server" ForeColor="Maroon"></asp:Label>
            </asp:Panel>

            <br />
            <asp:Panel ID="component_listing" runat="server" Visible="False">
                <div class="grid-overwrap">
                    <asp:GridView ID="staff_grid" runat="server" CssClass="staff-request-table" OnRowCreated="RowCreated" AllowSorting="True" OnSorting="SortDataSet" OnSelectedIndexChanged="staff_grid_SelectedIndexChanged" >
                    </asp:GridView>
                </div>
            
                <div class="component-list">
                    <br />
                    <asp:Label ID="StaffList" runat="server" Text="Selected Staff:"></asp:Label>
                    <asp:Label ID="Staff1" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="Staff2" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="Staff3" runat="server" Text="Label"></asp:Label><br />
                    <asp:Label ID="Label1" runat="server" Text="Work Type:"></asp:Label>
                    <asp:DropDownList ID="ddlWork" runat="server">
                        <asp:ListItem>Temp</asp:ListItem>
                        <asp:ListItem Value="Full Time">Full Time</asp:ListItem>
                    </asp:DropDownList><br /><br />
                    <asp:Label ID="Label2" runat="server" Text="Location:"></asp:Label>
                    <asp:TextBox ID="tbLocation" runat="server" CssClass="text-field"></asp:TextBox><br />
                    <asp:Label ID="Label3" runat="server" Text="Salary Offered:"></asp:Label>
                    <asp:TextBox ID="tbSalary" runat="server" CssClass="text-field"></asp:TextBox><br />
                    <asp:Button ID="btnSubmitRequest" runat="server" Text="Update Request" CssClass="btn btn-primary" OnClick="btnUpdateRequest_Click" /><br />
                    <asp:Button ID="btnSubmitRequest0" runat="server" Text="Delete Request" CssClass="btn btn-primary" OnClick="btnDeleteRequest_Click" />
                </div>
            </asp:Panel>
    </form>


    <footer>
        <div class="jumbotron-footer">
            <hr style="color:grey" />
            <div class="row">
                <div class="col-sm-2">
                    <p><img src="images/TPSlogo.png" style="width: 150px; height: 159px;" /> </p><br />
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

