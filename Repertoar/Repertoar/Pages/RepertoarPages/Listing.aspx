<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.Listing" viewstatemode ="disabled" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    
    <span class="titleRepertoar">REPERTOAR</span>
    <asp:HyperLink ID="HyperLink2"  runat="server" Text="Lägg till instrument" NavigateUrl="<%$ RouteUrl:routename=CreateInstrument %>" CssClass="button instrument " />
    <asp:HyperLink ID="HyperLink1"  runat="server" Text="Skapa Ny Låt" NavigateUrl="<%$ RouteUrl:routename=CreateSong %>" CssClass="button newSong" />
        
    <hr />
    <asp:Panel runat="server" ID="SuccessMessagePanel" Visible="false" CssClass="icon-ok">
                <asp:Literal runat="server" ID="SuccessMessageLiteral" />
            <asp:Button ID="Button1" CssClass="exit" runat="server" Text="Stäng" OnClientClick="exitbutton_OnClick" />
        <br />
            </asp:Panel>
    
     <asp:ListView ID="MaterialListView" runat="server" 
            ItemType="Repertoar.MODEL.Material"
            SelectMethod="MaterialListView_GetData"
            DataKeyNames="MID"
             OnItemDataBound="MaterialListView_ItemDataBound"
            >
          <LayoutTemplate>
         <table id="allContactsTable">
                    <tr>
                        <th>
                        </th>
                        <th>Instrument:
                        </th>   
                    </tr>
                <%-- Platshallare för nya rader --%>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </table>
              </LayoutTemplate>
          <%-- Mall för nya rader --%>
                   
            <ItemTemplate>
                    <tr>
                        <td class="Name">
                            <asp:HyperLink ID="NamnLabel" runat="server"  Text="<%# Item.Namn %>" 
                            NavigateUrl='<%# GetRouteUrl("Details", new { id= Item.MID}) %>' CssClass="song" />
                        </td>
                         <td id="instrument">  
                            <asp:Label ID="InstrumentNameLabel" runat="server" Text="{0}" /></span>
                        </td>
                      </tr>
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
