<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="IMSLookUp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IMS Cell Phone Lookup</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtCellPhone" runat="server" Font-Bold="True" Font-Names="Arial"
            Font-Size="Large" MaxLength="10" Style="z-index: 100; left: 109px; position: absolute;
            top: 109px" TabIndex="1" Width="188px" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:Label ID="lblTitle" runat="server" Font-Names="Arial" Font-Size="XX-Large" Style="z-index: 101;
            left: 14px; position: absolute; top: 19px" Text="IMS Cell Phone Lookup"></asp:Label>
        <img src="CCDLogoSmall.gif" style="z-index: 109; left: 973px; position: absolute;
            top: 3px" />
        <hr style="z-index: 110; left: 8px; position: absolute; top: 56px; width: 1007px;" />
        &nbsp;
        <asp:Label ID="lblCell" runat="server" Font-Bold="True" Font-Names="Arial" Style="z-index: 102;
            left: 11px; position: absolute; top: 116px" Text="Cell Phone:"></asp:Label>
        <asp:Button ID="btnSearch" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
            Style="z-index: 103; left: 310px; position: absolute; top: 108px" TabIndex="2"
            Text="Search" Width="80px" />
        <asp:GridView ID="grdResults" runat="server" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"
            Style="z-index: 104; left: 10px; position: absolute; top: 197px" Width="312px" Font-Bold="True" Font-Names="Arial">
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <Columns>
                <asp:BoundField>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    
    </div>
        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="Red"
            Style="z-index: 105; left: 664px; position: absolute; top: 115px"></asp:Label>
        <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="Blue"
            Style="z-index: 106; left: 11px; position: absolute; top: 164px"></asp:Label>
        <asp:RequiredFieldValidator ID="rfvCellPhone" runat="server" ControlToValidate="txtCellPhone"
            ErrorMessage="Cell Phone is required." Font-Bold="True" Font-Names="Arial" Style="z-index: 107;
            left: 410px; position: absolute; top: 116px"></asp:RequiredFieldValidator>
        <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" Style="z-index: 108; left: 985px;
            position: absolute; top: 17px" Width="1px"></asp:TextBox>
        <div style="z-index: 99; left: 4px; width: 395px; position: absolute; top: 98px; height: 43px; border-right: thin outset; border-top: thin outset; border-left: thin outset; border-bottom: thin outset;">
        </div>
    </form>
</body>
</html>
