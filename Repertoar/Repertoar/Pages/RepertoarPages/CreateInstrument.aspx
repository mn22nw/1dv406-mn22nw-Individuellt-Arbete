<%@ Page Title="Create" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="CreateInstrument.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.CreateInstrument " ViewStateMode="Disabled" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   <h1>Lägg till nytt instrument</h1>
   <hr />
             <asp:Panel runat="server" ID="SuccessMessagePanel" Visible="false" CssClass="icon-ok">
                <asp:Literal runat="server" ID="SuccessMessageLiteral" />
            <asp:Button ID="Button1" CssClass="exit" runat="server" Text="Stäng" OnClientClick="exitbutton_OnClick" />
            </asp:Panel>
  
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Följande fel inträffade:" 
            CssClass="validation-summary-errors"/>
         
<asp:FormView ID="InstrumentFormView" runat="server"
    ItemType="Repertoar.MODEL.Instrument"
    DefaultMode="Insert"
    InsertMethod="InstrumentFormView_InsertInstrument"
    RenderOuterTable="false" 
    ViewStateMode="Enabled">

    <InsertItemTemplate>
        <br />
            <%-- Instrument-namn --%>
            <div class="section smaller" >
                <div class="left">
                    <span class="title">Namn:</span>
                        <asp:TextBox ID="Namn" runat="server" Text='<%# BindItem.Namn %>'  MaxLength="60" Width="350px" />  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="Ett instrumentnamn måste anges." ControlToValidate="Namn" Display="Dynamic" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
                </div>

      <asp:Button ID="SaveButton" runat="server" Text="Lägg till" CommandName="Insert" CssClass="button"/>
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" Text="Tillbaka" 
                           NavigateUrl="<%$ RouteUrl:routename=Default %>" CssClass="buttonBack" />
       </InsertItemTemplate>
      
      </asp:FormView>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
