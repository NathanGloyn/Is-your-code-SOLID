<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="BoxInformation.Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Box Information - Search</title>
    <link href="Style.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            Box Information Search</h1>
        <br />
        <asp:Literal ID="litAddRecord" runat="server" Text="<a href='viewrecord.aspx'>Add New Record</a><br />"> </asp:Literal>
        <br />
        <h1>
            Search:</h1>
        <fieldset>
            <legend>Search Details :</legend>
            <ol>
                <li>
                    <asp:Label ID="lblClientNumber" runat="server" Text="Client Number :" AssociatedControlID="txtClientNumber"> </asp:Label><asp:TextBox
                        ID="txtClientNumber" runat="server" Width="600px"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="lblClientName" runat="server" Text="Client Name :" AssociatedControlID="txtClientName"> </asp:Label><asp:TextBox
                        ID="txtClientName" runat="server" Width="600px"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="lblClientContact" runat="server" Text="Contact :" AssociatedControlID="txtClientContact"> </asp:Label><asp:TextBox
                        ID="txtClientContact" runat="server" Width="600px"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="lblLocation" runat="server" Text="Location:" AssociatedControlID="ddlLocation"></asp:Label>
                    <asp:DropDownList ID="ddlLocation" runat="server">
                        <asp:ListItem Selected="True" Value="0">Select Location</asp:ListItem>
                        <asp:ListItem Value="1">Bath</asp:ListItem>
                        <asp:ListItem Value="2">Leeds</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="lblReviewDate" runat="server" Text="Review Date:" AssociatedControlID="txtReviewDate"></asp:Label>
                    <asp:TextBox ID="txtReviewDate" runat="server" Width="600px"></asp:TextBox></li>
                <li>
                    <asp:Label ID="lblComments" runat="server" Text="Comments:" AssociatedControlID="txtComments"></asp:Label>
                    <asp:TextBox ID="txtComments" runat="server" Rows="5" TextMode="MultiLine" Width="600px"></asp:TextBox></li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Search Results :</legend>
            <ol>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="Search_Click" /><br />
                <asp:DataList ID="dtlSearchResults" runat="server">
                    <ItemTemplate>
                        <li><a href="ViewRecord.aspx?ID=<%# ((System.Data.DataRowView)Container.DataItem)["ID"] %>">
                            Client Number: <%# ((System.Data.DataRowView)Container.DataItem)["ClientNumber"] %>,
                            Client: <%# ((System.Data.DataRowView)Container.DataItem)["ClientName"] %>,
                            Contact : <%# ((System.Data.DataRowView)Container.DataItem)["ClientLeader"] %>,
                            Secure Store : <%# ((System.Data.DataRowView)Container.DataItem)["SecureStorage"] %></a> </li>
                    </ItemTemplate>
                </asp:DataList>
            </ol>
        </fieldset>
    </div>
    </form>
</body>
</html>
