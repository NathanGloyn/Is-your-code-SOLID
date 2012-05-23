<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewRecord.aspx.cs" Inherits="BoxInformation.ViewRecord" ValidateRequest="false" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Box Information</title>
    <link href="Style.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Box Information Management</h1>
        <br />
        <a href="search.aspx">Return To Search</a><br />
        <br />
        <h1>Record:</h1>
        <asp:Panel ID="pnlAdminView" runat="server">
            <fieldset>
                <legend>Record Details</legend>
                <ol>
                    <li><asp:Label ID="lblClientNumber" runat="server" Text="Client Number:" AssociatedControlID="txtClientNumber"></asp:Label><asp:TextBox ID="txtClientNumber" runat="server" Width="600px"></asp:TextBox>&nbsp;<asp:Label ID="lblErrorProjNumb" AssociatedControlID="txtClientNumber" runat="server" /></li>
                    <li><asp:Label ID="lblClientName" runat="server" Text="Client Name:" AssociatedControlID="txtClientName"></asp:Label> <asp:TextBox ID="txtClientName" runat="server" Width="600px"></asp:TextBox>&nbsp;<asp:Label ID="lblErrorProjName" AssociatedControlID="txtClientName" runat="server" /></li>
                    <li><asp:Label ID="lblClientLeader" runat="server" Text="Contact:" AssociatedControlID="txtClientLeader"></asp:Label> <asp:TextBox ID="txtClientLeader" runat="server" Width="600px"></asp:TextBox>&nbsp;<asp:Label ID="lblErrorProjLeader" AssociatedControlID="txtClientLeader" runat="server" /></li>
                    <li><asp:Label ID="lblReviewDate" runat="server" Text="Review Date:" AssociatedControlID="txtReviewDate"></asp:Label> <asp:TextBox ID="txtReviewDate" runat="server" Width="600px"></asp:TextBox>&nbsp;<asp:Label ID="lblErrorReviewDate" AssociatedControlID="txtReviewDate" runat="server" /></li>
                    <li><asp:Label ID="lblComments" runat="server" Text="Comments:" AssociatedControlID="ftbComments"></asp:Label><FTB:FreeTextBox id="ftbComments" runat="Server" /></li>
                    <li><asp:Label ID="lblUploadDocument" runat="server" Text="Storage Manifest" AssociatedControlID="fluDocument"></asp:Label><asp:FileUpload ID="fluDocument" runat="server" /></li>
                    <li><asp:Label ID="lblCurrentFileName" runat="server" Text=""></asp:Label> <asp:Literal ID="litFileName" runat="server"></asp:Literal> <asp:Button runat="server" ID="btnDeleteFile1" Text="Delete File" OnClick="btnDeleteFile1_Click" /></li>
                    <li><asp:Label ID="lblUploadDocument2" runat="server" Text="Signed Agreement" AssociatedControlID="fluDocument2"></asp:Label><asp:FileUpload ID="fluDocument2" runat="server" /></li>
                    <li><asp:Label ID="lblCurrentFileName2" runat="server" Text=""></asp:Label> <asp:Literal ID="litFileName2" runat="server"></asp:Literal> <asp:Button runat="server" ID="btnDeleteFile2" Text="Delete File" OnClick="btnDeleteFile2_Click" /></li>
                    <li><asp:Label ID="lblSecure" runat="server" Text="Secure Storage:" AssociatedControlID="cbxSecure"></asp:Label><asp:CheckBox ID="cbxSecure" runat="server" /></li>
                </ol>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlUserView" runat="server">
            <fieldset>
                <legend>Record Details</legend>
                <ol>
                    <li><b>Client Number</b> : <asp:Literal ID="litClientNumber" runat="server"></asp:Literal></li>
                    <li><b>Client Name</b> : <asp:Literal ID="litClientName" runat="server"></asp:Literal></li>
                    <li><b>Contact</b> : <asp:Literal ID="litClientContact" runat="server"></asp:Literal></li>
                    <li><b>Review Date</b> : <asp:Literal ID="litReviewDate" runat="server"></asp:Literal></li>
                    <li><b>Comments</b> :  <asp:Literal ID="litComments" runat="server"></asp:Literal></li>
                    <li><b>Storage Manifest</b> : <asp:Literal ID="litDocumentFilename" runat="server"></asp:Literal></li>
                    <li><b>Signed Agreement</b> : <asp:Literal ID="litDocumentFilename2" runat="server"></asp:Literal></li>
                    <li><b>Secure Storage</b> : <asp:Literal ID="litSecure" runat="server"></asp:Literal></li>
                </ol>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlBoxDetails" runat="server">
        </asp:Panel>
        <asp:Panel ID="pnlAddBoxes" runat="server">
            <fieldset>
                <legend>Add Boxes</legend>
                <ol>
                    <li><asp:Label ID="lblBoxLocation" runat="server" Text="Box Location:" AssociatedControlID="txtBoxLocation"></asp:Label> <asp:TextBox ID="txtBoxLocation" runat="server"></asp:TextBox></li>
                    <li><asp:Label ID="lblNumberOfBoxes" runat="server" Text="Number Of Boxes:" AssociatedControlID="txtNumberOfBoxes"></asp:Label> <asp:TextBox ID="txtNumberOfBoxes" runat="server"></asp:TextBox></li>
                    <li><asp:Button runat="server" ID="btnAddBoxes" Text="Add Boxes" OnClick="AddBoxes_Click" /></li>
                </ol>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlAdminControls" runat="server">
            <fieldset>
                <legend>Form Controls</legend>
                <ol>
                    <li><asp:Button runat="server" ID="btnAddRecord" Text="Add Entry" OnClick="AddRecord_Click" /></li>
                    <li><asp:Button runat="server" ID="btnUpdateRecord" Text="Update Record" OnClick="UpdateRecord_Click" /></li>
                    <li><asp:Button runat="server" ID="btnDeleteRecord" Text="Delete Record" OnClick="DeleteRecord_Click" /></li>
                </ol>
            </fieldset>        
        </asp:Panel>
    </div>
    </form>
</body>
</html>
