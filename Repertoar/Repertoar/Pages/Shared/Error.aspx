<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Repertoar.Pages.Shared.Error" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
      <link href="~/Content/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
     <div id="page">
        <div id="AllContent">
    <form id="form1" runat="server">
    <div>
    <h1>Serverfel</h1>
    <p>Ett fel inträffade då förfrågan behandlades.</p>
    <asp:HyperLink ID="HyperLink1"  runat="server" Text="Tillbaka till startsidan" NavigateUrl="<%$ RouteUrl:routename=SongListing %>" 
        CssClass="nyKund" />
        <br />
    </div>
    </form>
    </div>
    </div>
</body>
</html>