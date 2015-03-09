<%@ Page Title="Create" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="InstrumentCRUD.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.InstrumentCRUD " ViewStateMode="Disabled" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
 
     <asp:Panel runat="server" ID="SuccessMessagePanel" Visible="false" CssClass="icon-ok">
                <asp:Literal runat="server" ID="SuccessMessageLiteral" />
            <asp:Button ID="Button1" CssClass="exit" runat="server" Text="Stäng" OnClientClick="exitbutton_OnClick" />
            </asp:Panel>

 <%-- LÄGG TILL NYTT INSTRUMENT --%>   
   
     <h1>Lägg till nytt instrument</h1> <hr /> <br /> 
             
<asp:FormView ID="InstrumentFormView" runat="server"
    ItemType="Repertoar.MODEL.Instrument"
    DefaultMode="Insert"
    InsertMethod="InstrumentFormView_InsertInstrument"
    RenderOuterTable="false" 
    ViewStateMode="Enabled">

    <InsertItemTemplate>
            <div class="sectionInstrument" >
                <div class="left">
                    <span class="title">Namn:</span>
                        <asp:TextBox ID="NewName" runat="server" Text='<%# BindItem.Namn %>'  MaxLength="60" Width="350px" />  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="newInstrument"
                            ErrorMessage="Ett instrumentnamn måste anges." ControlToValidate="NewName" Display="Dynamic" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
               </div>
    <div class="bottom">
      <asp:Button ID="SaveButton" runat="server" Text="Lägg till" CommandName="Insert" ValidationGroup="newInstrument" CssClass="button lessMargin"/>
        </div>
        <br />

       </InsertItemTemplate>
      </asp:FormView>
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Följande fel inträffade:"  ValidationGroup="newInstrument"
            CssClass="validation-summary-errors"/>
     <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="Följande fel inträffade"
                        CssClass="validation-summary-errors" ValidationGroup="oldInstrument" ShowModelStateErrors="False" />
    <br />
     <%-- REDIGERA BEFINTLIGA INSTRUMENT --%>
    <h1>Redigera eller ta bort befintliga instrument</h1> <hr /> <br />

       <%-- ListView som presenterar alla instrument som kan redigeras. --%>
            <asp:ListView ID="InstrumentListView" runat="server"
                ItemType="Repertoar.MODEL.Instrument"
                DataKeyNames="InstrumentID"
                SelectMethod="InstrumentListView_GetData"
                UpdateMethod="InstrumentListView_UpdateInstrument"
                DeleteMethod="InstrumentListView_DeleteInstrument"
               >
                <LayoutTemplate>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <p>
                        Du har inga instrument ännu/ instrument saknas.
                    </p>
                </EmptyDataTemplate>

               <ItemTemplate>
               <%-- Namn --%>
                <div class="section" >
                    <div class="left">
                        <span class="title">Namn:</span>
                            <asp:TextBox ID="Namn" runat="server" Text='<%# BindItem.Namn %>'  MaxLength="60" Width="350px" />  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="oldInstrument"
                                ErrorMessage="Ett låtnamn måste anges." ControlToValidate="Namn" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                    <div class="sRight">
                     <%-- Redigera, delete + tillbakaknapp. --%>
                     <asp:LinkButton ID="LinkButton1"  runat="server" CommandName="Update" Text="Uppdatera"  CssClass="button lessMargin" ValidationGroup="oldInstrument" />  
                       
                     <asp:LinkButton ID="LinkButton2"  runat="server" CommandName="Delete" Text="Radera"  CssClass="button lessMargin" ValidationGroup="oldInstrument"
                                     OnClientClick='<%# String.Format("return confirm(\" VARNING: ALLA låtar som har detta instrumentet kommer också att försvinna. - Är du fortfarande säker på att du vill ta bort instrumentet {0}?\")", Item.Namn) %>'/>  
                    </div>
                    </div>
                        <br />

                     </ItemTemplate>                 
            </asp:ListView>
    <asp:HyperLink ID="HyperLink2" runat="server" Text="Tillbaka till listan" 
                                   NavigateUrl="<%$ RouteUrl:routename=Default %>" CssClass="buttonBack" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
