<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/EILDesign.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var i = 0;
        function changeImage() {
            i = (i + 1) % 6;
            var im = document.getElementById("slide");
            var img = new Array("Images/1.jpg", "Images/2.jpg", "Images/3.jpg", "Images/4.jpg", "Images/5.jpg", "Images/6.jpg");
            im.innerHTML = '<img src="' + img[i] + '" width="494" height="330"/>';
        }
        timer2 = setInterval(changeImage, 5000);
    </script>
 
</head>
<body>
    <form id="form1" runat="server">
    <div class="myPageHeader">
        <h1>
            Material Allocation at construction site</h1>
    </div>
    <fieldset style="height: auto; width: 180px; float: right">
        <legend>Account Information</legend>
        <asp:Label ID="lblusername" runat="server" CssClass="myLabel" Text="Enter userID: "></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtusername" runat="server" CssClass="myInput" MaxLength="25"></asp:TextBox>
        &nbsp;&nbsp;<br />
        <asp:Label ID="lblpassword" runat="server" CssClass="myLabel" Text="Enter password: "></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtpassword" runat="server" CssClass="myInput" MaxLength="25"
            TextMode="Password"></asp:TextBox>
        &nbsp;<br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="butlogin" runat="server" Text="LOGIN" OnClick="butlogin_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:Label ID="error" runat="server" CssClass="myError"></asp:Label>
    </fieldset>
    <div id="slide">
        <br />
        <br />
        <img src="Images/1.jpg" style="height: 330px; width: 494px" />
    </div>
   
    <br />
    <br />
    <br />
    &nbsp;<div class="footer">
        <br />
        <img alt="EIL" class="style1" src="Images/eil_logo.jpg" />
        <div style="float: right">
            Copyrights EIL- All rights reserved.
        </div>
    </div>
    </form>
</body>
</html>
