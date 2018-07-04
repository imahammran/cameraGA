<%@ Page Language="C#" AutoEventWireup="true" CodeFile="next.aspx.cs" Inherits="next" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
		<title>DSLR Recommender</title>
		<meta charset="utf-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<link rel="stylesheet" href="assets/css/main.css" />        <link id="Link1" runat="server" rel="shortcut icon" href="images/mix.ico" type="image/x-icon" />
        <link id="Link2" runat="server" rel="icon" href="images/mix.ico" type="image/ico" />
</head>
<body>
    <div class="page-wrap">

			<!-- Nav -->
				<nav id="nav">
					<ul>
						<li><a href="index.aspx"><span class="icon fa-home"></span></a></li>
					</ul>
				</nav>

			<!-- Main -->
				<section id="main">

                <!-- Header -->
						<header id="header">
							<div>Recommendation Result</div>
						</header>

					<!-- Section -->
						<section>
							<div class="inner">
								<p>Here are some of the recommendations of DSLR camera and lens based on your previous selection...</p>                                                                <div class="box">                                Entered budget: RM<asp:Label ID="Label4" runat="server"></asp:Label><br />                                Selected photography style(s): <asp:Label ID="Label5" runat="server"></asp:Label><br />                                Selected brand: <asp:Label ID="Label6" runat="server"></asp:Label><br />                                Selected location: <asp:Label ID="Label7" runat="server"></asp:Label>                                </div>                                <center>                                <form id="form2" runat="server">                                <div class=gallery>                                <header class="special">
						           <h3>RECOMMENDATION 1 : RM<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
						        </header><br />                                <asp:Table ID="Table1" runat="server"  border="2" class="table text-center" 
                                           Width="90%" Height="90%" BorderColor="Black" BorderStyle="Solid" 
                                           CellPadding="2" CellSpacing="2">
       
                                    <asp:TableRow ID="TableRow1" runat="server" BackColor="#CCCCFF">
                                        <asp:TableHeaderCell><center><b>BODY</b></center></asp:TableHeaderCell>
                                        <asp:TableHeaderCell><center><b>LENS</b></center></asp:TableHeaderCell>
                                    </asp:TableRow>

                                    <asp:TableRow ID="TableRow2" runat="server" BackColor="White">
                                        <asp:TableCell><center>
                                                        <asp:Label ID="bodyName1" runat="server" Font-Bold="true"></asp:Label><br />
                                                        <asp:Image ID="bodyImage1"  width = "200px" height="200px" runat="server" CssClass="myIMG"></asp:Image>
                                        </center></asp:TableCell>
                                        <asp:TableCell><center>
                                                        <asp:Label ID="lensName1" runat="server" Font-Bold="true"></asp:Label><br /><br />
                                                        <asp:Image ID="lensImage1"  width = "150px" height="150px" runat="server" CssClass="myIMG"></asp:Image>
                                        </center></asp:TableCell>
                                    </asp:TableRow>
                
                                    <asp:TableRow ID="TableRow3" runat="server" BackColor="#CCCCFF">
                                       <asp:TableCell><center><b><asp:TextBox runat="server" id="bodyDetails1" TextMode="multiline" columns="28" style="border:none;background:transparent" Height="200px" Font-Size="Larger" ForeColor="Black" ReadOnly="true"/></b></center></asp:TableCell>
                                       <asp:TableCell><center><b><asp:TextBox runat="server" id="lensDetails1" TextMode="multiline" columns="28" style="border:none;background:transparent" Height="200px" Font-Size="Larger" ForeColor="Black" ReadOnly="true"/></b></center></asp:TableCell>
                                    </asp:TableRow>
        
                                </asp:Table><br />

                                <header class="special">
						           <h3>RECOMMENDATION 2 : RM<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h3>
						        </header><br />
                                <asp:Table ID="Table2" runat="server"  border="2" class="table text-center" 
                                           Width="90%" Height="90%" BorderColor="Black" BorderStyle="Solid" 
                                           CellPadding="2" CellSpacing="2">
       
                                    <asp:TableRow ID="TableRow4" runat="server" BackColor="#CCCCFF">
                                        <asp:TableHeaderCell><center><b>BODY</b></center></asp:TableHeaderCell>
                                        <asp:TableHeaderCell><center><b>LENS</b></center></asp:TableHeaderCell>
                                    </asp:TableRow>

                                    <asp:TableRow ID="TableRow5" runat="server" BackColor="White">
                                        <asp:TableCell><center>
                                                        <asp:Label ID="bodyName2" runat="server" Font-Bold="true"></asp:Label><br />
                                                        <asp:Image ID="bodyImage2"  width = "200px" height="200px" runat="server" CssClass="myIMG"></asp:Image>
                                        </center></asp:TableCell>
                                        <asp:TableCell><center>
                                                        <asp:Label ID="lensName2" runat="server" Font-Bold="true"></asp:Label><br /><br />
                                                        <asp:Image ID="lensImage2"  width = "150px" height="150px" runat="server" CssClass="myIMG"></asp:Image>
                                        </center></asp:TableCell>
                                    </asp:TableRow>
                
                                    <asp:TableRow ID="TableRow6" runat="server" BackColor="#CCCCFF">
                                       <asp:TableCell><center><b><asp:TextBox runat="server" id="bodyDetails2" TextMode="multiline" columns="28" style="border:none;background:transparent" Height="200px" Font-Size="Larger" ForeColor="Black" ReadOnly="true"/></b></center></asp:TableCell>
                                       <asp:TableCell><center><b><asp:TextBox runat="server" id="lensDetails2" TextMode="multiline" columns="28" style="border:none;background:transparent" Height="200px" Font-Size="Larger" ForeColor="Black" ReadOnly="true"/></b></center></asp:TableCell>
                                    </asp:TableRow>
        
                                </asp:Table><br />

                                <header class="special">
						           <h3>RECOMMENDATION 3 : RM<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h3>
						        </header><br />
                                <asp:Table ID="Table3" runat="server"  border="2" class="table text-center" 
                                           Width="90%" Height="90%" BorderColor="Black" BorderStyle="Solid" 
                                           CellPadding="2" CellSpacing="2">
       
                                    <asp:TableRow ID="TableRow7" runat="server" BackColor="#CCCCFF">
                                        <asp:TableHeaderCell><center><b>BODY</b></center></asp:TableHeaderCell>
                                        <asp:TableHeaderCell><center><b>LENS</b></center></asp:TableHeaderCell>
                                    </asp:TableRow>

                                    <asp:TableRow ID="TableRow8" runat="server" BackColor="White">
                                        <asp:TableCell><center>
                                                        <asp:Label ID="bodyName3" runat="server" Font-Bold="true"></asp:Label><br />
                                                        <asp:Image ID="bodyImage3"  width = "200px" height="200px" runat="server" CssClass="myIMG"></asp:Image>
                                        </center></asp:TableCell>
                                        <asp:TableCell><center>
                                                        <asp:Label ID="lensName3" runat="server" Font-Bold="true"></asp:Label><br /><br />
                                                        <asp:Image ID="lensImage3"  width = "150px" height="150px" runat="server" CssClass="myIMG"></asp:Image>
                                        </center></asp:TableCell>
                                    </asp:TableRow>
                
                                    <asp:TableRow ID="TableRow9" runat="server" BackColor="#CCCCFF">
                                       <asp:TableCell><center><b><asp:TextBox runat="server" id="bodyDetails3" TextMode="multiline" columns="28" style="border:none;background:transparent" Height="200px" Font-Size="Larger" ForeColor="Black" ReadOnly="true"/></b></center></asp:TableCell>
                                       <asp:TableCell><center><b><asp:TextBox runat="server" id="lensDetails3" TextMode="multiline" columns="28" style="border:none;background:transparent" Height="200px" Font-Size="Larger" ForeColor="Black" ReadOnly="true"/></b></center></asp:TableCell>
                                    </asp:TableRow>
        
                                </asp:Table>
                                </div>
                                </form>
                                </center>
</body>
</html>