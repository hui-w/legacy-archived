<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Date Diff</title>

    <script type="text/javascript" src="calendarDateInput.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="datediff.css" />
</head>
<body>
    <form id="frmMain" action="Default.aspx">
        <div>
            <div class="row">
                <label>
                    Date <span class="u">F</span>rom:</label>
                <asp:Literal ID="litDateFrom" runat="server" EnableViewState="false"></asp:Literal>
            </div>
            <div class="row">
                <label>
                    Add <span class="u">D</span>ay(s):</label>
                <asp:Literal ID="litDays" runat="server" EnableViewState="false"></asp:Literal>
            </div>
            <div class="row">
                <label>
                    Date <span class="u">T</span>o:</label>
                <asp:Literal ID="litDateTo" runat="server" EnableViewState="false"></asp:Literal>
            </div>
            <div class="row">
                <label>
                    &nbsp;</label>
                <input type="submit" value="Calculate" class="button" />
            </div>
            <asp:Literal ID="litOutput" runat="server" EnableViewState="false"></asp:Literal>
        </div>
        <div class="copyright">
            DateDiff WebApp, Copyright &copy 2002-<asp:Literal ID="litCopyYear" runat="server" EnableViewState="false"></asp:Literal> QLike.com
        </div>
    </form>
</body>
</html>
