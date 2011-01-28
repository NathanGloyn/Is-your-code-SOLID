<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="BoxInformation.Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Box Information - Search</title>
    <link href="css/Style.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Box Information Search</h1>
        <br />
        <asp:HyperLink id="lnkAddNew" runat="server" NavigateUrl="viewrecord.aspx">Add New Record</asp:HyperLink>
        <br />
        <h1>Search:</h1>
        <fieldset>
            <ol>
                <li>
                    <asp:Label ID="lblClientNumber" runat="server" Text="Client Number :" AssociatedControlID="txtClientNumber"> </asp:Label><asp:TextBox
                        ID="txtClientNumber" runat="server"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="lblClientName" runat="server" Text="Client Name :" AssociatedControlID="txtClientName"> </asp:Label><asp:TextBox
                        ID="txtClientName" runat="server" ></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="lblClientContact" runat="server" Text="Contact :" AssociatedControlID="txtClientContact"> </asp:Label><asp:TextBox
                        ID="txtClientContact" runat="server" ></asp:TextBox>
                </li>
            </ol>
        </fieldset>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="Search_Click" />
        <br />
        <br />
        <fieldset>
            <legend>Search Results :</legend>
            <ol>
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
