<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DSLR Recommender</title>
		<meta charset="utf-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<link rel="stylesheet" href="assets/css/main.css" />
        <link id="Link1" runat="server" rel="shortcut icon" href="images/mix.ico" type="image/x-icon" />
        <link id="Link2" runat="server" rel="icon" href="images/mix.ico" type="image/ico" />

      <%-- Validation for checkbox --%>
        <script type = "text/javascript">
            function ValidateCheckBox(sender, args) {
                var checkBoxList = document.getElementById("<%=cboxListPhoto.ClientID %>");
                var checkboxes = checkBoxList.getElementsByTagName("input");
                var isValid = false;
                for (var i = 0; i < checkboxes.length; i++) {
                    if (checkboxes[i].checked) {
                        isValid = true;
                        break;
                    }
                }
                args.IsValid = isValid;
            }
        </script>

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

					<!-- Banner -->
						<section id="banner">
							<div class="inner">
								<h1>DSLR Camera & Lens Recommender</h1>
								<p>A recommendation website to help you choose your needed DSLR camera and lens</p>
								<ul class="actions">
									<li><a href="#Select" class="button alt scrolly big">Continue</a></li>
								</ul>
							</div>
						</section>

					<!-- Selection form -->
                    <section id="Select">
                    <div class="gallery">
                        <header class="special">
						    <h2>Make Your Selection</h2>
						</header>
                        <div class="gallery">
                        <form id="form1" runat="server">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <div class="page-wrap">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <div class="modal">
                                    <div class="center">
                                        <img alt=" " src="images/759.gif" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Height="900px" Width="100%" CssClass="divModalBackground" Visible="true">
                                <ContentTemplate>
								    <h5><label for="txtBudget">Enter your budget</label></h5>
                                    <asp:TextBox ID="txtBudget" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFValidator1" runat="server" 
                                                                ControlToValidate="txtBudget" 
                                                                InitialValue="" 
                                                                ErrorMessage="Please enter your budget" 
                                                                Display="None"
                                                                ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                          Operator="DataTypeCheck" Type="Double" 
                                                          ControlToValidate="txtBudget" 
                                                          ErrorMessage="Budget must be in number"
                                                          Display="None" 
                                                          ForeColor="Red">
                                    </asp:CompareValidator>
                                   <br /><br />
								    <h5><label for="cboxListPhoto">Choose your type of photography selection</label></h5>
                                    <center>
								    <asp:CheckBoxList ID="cboxListPhoto" 
                                                      AutoPostBack="True"
                                                      CellPadding="5"
                                                      CellSpacing="5"
                                                      RepeatDirection="Horizontal"
                                                      RepeatColumns="6"
                                                      RepeatLayout="Table"
                                                      TextAlign="Right"
                                                      runat="server">

                                        <%--<asp:ListItem>Abstract</asp:ListItem>
                                        <asp:ListItem>Architecture</asp:ListItem>
                                        <asp:ListItem>Fashion</asp:ListItem>
                                        <asp:ListItem>Food</asp:ListItem>
                                        <asp:ListItem>Landscape</asp:ListItem>
                                        <asp:ListItem>Macro/Micro</asp:ListItem>
                                        <asp:ListItem>Nature</asp:ListItem>
                                        <asp:ListItem>Portrait</asp:ListItem>
                                        <asp:ListItem>Sport</asp:ListItem>
                                        <asp:ListItem>Street</asp:ListItem>
                                        <asp:ListItem>Studio</asp:ListItem>
                                        <asp:ListItem>Wedding/Event</asp:ListItem>--%>

                                    </asp:CheckBoxList>
                                    <asp:CustomValidator ID="CV1" runat="server" 
                                                         ErrorMessage="Please choose type of photography"
                                                         Display="None" 
                                                         ForeColor="Red"
                                                         ClientValidationFunction = "ValidateCheckBox">
                                    </asp:CustomValidator>
                                    </center>
							       
                                    <h5><label for="dropBrand">Your preferred brand</label></h5>
								    <asp:DropDownList ID="dropBrand" runat="server">

                                        <asp:ListItem Selected="True">--- Select brand ---</asp:ListItem>
                                        <asp:ListItem>Canon</asp:ListItem>
                                        <asp:ListItem>Nikon</asp:ListItem>
                                        <asp:ListItem>Sony</asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                                ControlToValidate="dropBrand" 
                                                                InitialValue="--- Select brand ---" 
                                                                ErrorMessage="Please choose brand"
                                                                Display="None" 
                                                                ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                   <br />
								    <h5><label for="dropLocation">Your preferred location</label></h5>
								    <asp:DropDownList ID="dropLocation" runat="server">

                                        <asp:ListItem Selected="True">--- Select location ---</asp:ListItem>
                                        <asp:ListItem>Alor Gajah</asp:ListItem>
                                        <asp:ListItem>Jasin</asp:ListItem>
                                        <asp:ListItem>Melaka Tengah</asp:ListItem>
                                        <asp:ListItem>Online</asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFValidator2" runat="server" 
                                                                ControlToValidate="dropLocation" 
                                                                InitialValue="--- Select location ---" 
                                                                ErrorMessage="Please choose location"
                                                                Display="None" 
                                                                ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                   <br />
                                   <asp:ValidationSummary runat="server" ShowMessageBox="true" ShowSummary="false" />
                                   <center><br />
							        <ul class="actions">
                                        <li><asp:Button ID="button2" runat="server" Text="RESET" onclick="reset_onClick" Font-Bold="True" /></li>
								        <li><asp:Button ID="Button1" runat="server" Text="SUBMIT" onclick="recommend_onClick" Font-Bold="True" /></li>
							        </ul>
                                    </center>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </form>
                        </div>
                    </div>
                    </section>
    </div>
</body>
</html>
