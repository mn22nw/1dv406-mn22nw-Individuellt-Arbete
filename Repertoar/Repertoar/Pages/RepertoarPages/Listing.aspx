<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.Listing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    
   <h1>REPERTOAR</h1>

    <asp:Panel runat="server" ID="SuccessMessagePanel" Visible="false" CssClass="icon-ok">
                <asp:Literal runat="server" ID="SuccessMessageLiteral" />
            <asp:Button ID="Button1" CssClass="exit" runat="server" Text="Stäng" OnClientClick="exitbutton_OnClick" />
            </asp:Panel>
        <asp:HyperLink ID="HyperLink1"  runat="server" Text="Skapa Ny Låt" NavigateUrl="<%$ RouteUrl:routename=CreateSong %>" CssClass="button right" />

     <asp:ListView ID="MaterialListView" runat="server" 
            ItemType="Repertoar.MODEL.Material"
            SelectMethod="MaterialListView_GetData"
            DeleteMethod="MaterialListView_DeleteSong"
            DataKeyNames="MID"
            >
          <LayoutTemplate>
         <table class="HeaderTb">
                    <tr>
                        <th class="firstName">Instrument:
                        </th>   
                    </tr>
                <%-- Platshallare för nya rader --%>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </table>
              </LayoutTemplate>

            <ItemTemplate>
                 <%-- Mall för nya rader --%>
                    <table id="allContactsTable" >
                    <tr>
                         <th id="instrument">
                            <asp:Label ID="InstrumentLabel" runat="server" Text="<%# Item.Instrument %> " />
                        </th>
                        <th class="Name">
                            <asp:HyperLink ID="NamnLabel" runat="server"  Text="<%# Item.Namn %>" 
                            NavigateUrl='<%# GetRouteUrl("Details", new { id= Item.MID}) %>' CssClass="song" />
                        </th>
                        
                         <th> 
                            <asp:HyperLink ID="HyperLink2" runat="server" Text="Visa" 
                                NavigateUrl='<%# GetRouteUrl("Details", new { id= Item.MID}) %>' CssClass="button smaller" />
                        </th>
                        <hr />
                        </table>
            </ItemTemplate>        
            <EmptyDataTemplate>
                <%-- Om uppgifter saknas --%>
                <table class="grid">
                    <tr>
                        <td>
                            Uppgifter saknas.
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
 </asp:ListView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
