<%@ Page Title="Create" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Create2.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.Create" ViewStateMode="Enabled" %>
<%@ Register Assembly="Repertoar"  TagName="CustomLabelControl" TagPrefix="aspNewControls" Namespace="NewControls"%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
     <h3>Lägg till ny låt</h3>
    
            <div class="editor-label">
                <label for="Name">Namn</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="Name" runat="server" MaxLength="50" Text=""></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Ett förnamn måste anges." ControlToValidate="Name" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
             <br />
     
        <div class="editor-label">
                <label for="ddlComposers">Kompositör</label>
        </div>
    <aspNewControls:NewDropDownList ID="ddlComposers" runat="server"DataTextField = "Namn" DataValueField = "KompID">
                                                </aspNewControls:NewDropDownList>
      <br />
      <br />
         <label for="ddlInstruments">Instrument</label>
               <asp:DropDownList ID="ddlInstruments" runat="server" >  
                      <asp:ListItem Text="Bas" Value="Bas"></asp:ListItem> 
                      <asp:ListItem Text="Fiol" Value="Fiol"></asp:ListItem>
                      <asp:ListItem Text="Flöjt" Value="Flöjt"></asp:ListItem>
                      <asp:ListItem Text="Gitarr" Value="Gitarr"></asp:ListItem>
                      <asp:ListItem Text="Klarinett" Value="Klarinett"></asp:ListItem>
                      <asp:ListItem Text="Oboe" Value="Oboe"></asp:ListItem>
                      <asp:ListItem Text="Piano" Value="Piano"></asp:ListItem>
                      <asp:ListItem Text="Saxofon" Value="Saxofon"></asp:ListItem>  
                      <asp:ListItem Text="Trumpet" Value="Trumpet"></asp:ListItem>
                      <asp:ListItem Text="Trombon" Value="Trombon"></asp:ListItem>  
                      <asp:ListItem Text="Trummor" Value="Trummor"></asp:ListItem> 
                      <asp:ListItem Text="Tuba" Value="Tuba"></asp:ListItem>  
                      <asp:ListItem Text="Valthorn" Value="Valthorn"></asp:ListItem>
                      </asp:DropDownList>  
        <br />
        <br />
     <div class="editor-label">
            <label for="rblKategori">Kategori</label>
      </div>
    
      <asp:radiobuttonlist ID="rblKategori" runat="server" DataTextField = "Namn" DataValueField = "KaID" ></asp:radiobuttonlist>
        <br />
        <div class="editor-label">
           <label for="rblStatus">Status</label>
           <asp:radiobuttonlist ID="rblStatus" runat="server" RepeatDirection="Horizontal" >  
                  <asp:ListItem Text="Vill lära mig" Value="Vill lära mig"></asp:ListItem> 
                  <asp:ListItem Text="Påbörjad" Value="Påbörjad"></asp:ListItem>   
                  <asp:ListItem Text="Klar" Value="Klar"></asp:ListItem>  
                  </asp:radiobuttonlist>  
        </div>
        <br />
    <label for="ddlGenre">Genre</label>
           <asp:DropDownList ID="ddlGenre" runat="server" >  
                  <asp:ListItem Text="Blues" Value="Blues"></asp:ListItem>
                  <asp:ListItem Text="Country" Value="Country"></asp:ListItem>
                  <asp:ListItem Text="Funk" Value="Funk"></asp:ListItem>
                  <asp:ListItem Text="Gospel" Value="Gospel"></asp:ListItem>
                  <asp:ListItem Text="Indie-pop" Value="Indie-pop"></asp:ListItem>
                  <asp:ListItem Text="Jazz" Value="Jazz"></asp:ListItem> 
                  <asp:ListItem Text="Klassisk" Value="Klassisk"></asp:ListItem>   
                  <asp:ListItem Text="Metal" Value="Metal"></asp:ListItem>
                  <asp:ListItem Text="Pop" Value="Pop"></asp:ListItem>
                  <asp:ListItem Text="Rock" Value="Rock"></asp:ListItem>
                  <asp:ListItem Text="Övrigt" Value="Övrigt"></asp:ListItem> 
                  </asp:DropDownList>  
        <br />
        <br />
    <label for="ddlLevel">Svårighetsgrad</label>
           <asp:DropDownList ID="ddlLevel" runat="server" >  
                  <asp:ListItem Text="1" Value="1"></asp:ListItem>
                  <asp:ListItem Text="2" Value="2"></asp:ListItem>
                  <asp:ListItem Text="3" Value="3"></asp:ListItem>
                  <asp:ListItem Text="4" Value="4"></asp:ListItem>
                  <asp:ListItem Text="5" Value="5"></asp:ListItem>
                  <asp:ListItem Text="6" Value="6"></asp:ListItem> 
                  <asp:ListItem Text="7" Value="7"></asp:ListItem>   
                  <asp:ListItem Text="8" Value="8"></asp:ListItem>
                  <asp:ListItem Text="9" Value="9"></asp:ListItem>
                  <asp:ListItem Text="10" Value="10"></asp:ListItem>
                  </asp:DropDownList>  
        <br />
        <br />
        <label for="Anteckningar">Anteckningar</label>
        <asp:TextBox ID="Anteckningar" runat="server" TextMode="MultiLine"></asp:TextBox>
        <asp:Button ID="SaveButton" runat="server" Text="Lägg till" OnClick="MaterialListView_InsertItem" CssClass="button"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
