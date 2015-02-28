<%@ Page Title="Create" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.Create" ViewStateMode="Enabled" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   <h3>Lägg till ny låt</h3>
             <asp:Panel runat="server" ID="SuccessMessagePanel" Visible="false" CssClass="icon-ok">
                <asp:Literal runat="server" ID="SuccessMessageLiteral" />
            <asp:Button ID="Button1" CssClass="exit" runat="server" Text="Stäng" OnClientClick="exitbutton_OnClick" />
            </asp:Panel>
         
<asp:FormView ID="SongFormView" runat="server"
    ItemType="Repertoar.MODEL.Material"
    DefaultMode="Insert"
    InsertMethod="MaterialFormView_InsertSong"
    RenderOuterTable="false" 
    ViewStateMode="Enabled">

    <InsertItemTemplate>
            <%-- Namn --%>
            <div class="editor-label">
                <label for="Namn">Namn</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="Namn" runat="server" Text='<%# BindItem.Namn %>'  MaxLength="50" />  
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Ett låtnamn måste anges." ControlToValidate="Namn" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div> <br />

            <%-- Kompositör --%>
            <div class="editor-label">
                <label for="ddlComposer">Kompositör</label>
            </div>
              <asp:DropDownList ID="ddlComposer" runat="server"
                ItemType="Repertoar.MODEL.Kompositör"
                SelectMethod="ComposerDropDownList_GetData"
                DataTextField="Namn"
                DataValueField="KompID"
                SelectedValue='<%# BindItem.KompID %>'
                CssClass="DropDown">
            </asp:DropDownList>
            <br />

            <%-- Instrument --%>        
            <label for="ddlInstruments">Instrument</label>
               <asp:DropDownList ID="ddlInstruments" runat="server" SelectedValue='<%# BindItem.Instrument %>'
                CssClass="DropDown"> 
                      <asp:ListItem Text="Bas" Value="Bas"></asp:ListItem> 
                      <asp:ListItem Text="Fiol" Value="Fiol"></asp:ListItem>
                      <asp:ListItem Text="Flöjt" Value="Flöjt"></asp:ListItem>
                      <asp:ListItem Text="Gitarr" Value="Gitarr"></asp:ListItem>
                      <asp:ListItem Text="Klarinett" Value="Klarinett"></asp:ListItem>
                      <asp:ListItem Text="Oboe" Value="Oboe"></asp:ListItem>
                      <asp:ListItem Text="Piano" Value="Piano" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="Saxofon" Value="Saxofon"></asp:ListItem>  
                      <asp:ListItem Text="Trumpet" Value="Trumpet"></asp:ListItem>
                      <asp:ListItem Text="Trombon" Value="Trombon"></asp:ListItem>  
                      <asp:ListItem Text="Trummor" Value="Trummor"></asp:ListItem> 
                      <asp:ListItem Text="Tuba" Value="Tuba"></asp:ListItem>  
                      <asp:ListItem Text="Valthorn" Value="Valthorn"></asp:ListItem>
                      </asp:DropDownList>  <br /><br />

           <%-- Kategori --%>  
          <div class="editor-label">
            <label for="PlaceHolder2">Kategori</label>
            </div>
         <asp:DropDownList ID="ddlCategory" runat="server"
                ItemType="Repertoar.MODEL.Kategori"
                SelectMethod="CategoryDropDownList_GetData"
                DataTextField="Namn"
                DataValueField="KaID"
                SelectedValue='<%# BindItem.KaID %>'
                CssClass="DropDown">
            </asp:DropDownList> <br />


           <%-- Status --%>  
            <div class="editor-label">
               <label for="rblStatus">Status</label>
               <asp:radiobuttonlist ID="rblStatus" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# BindItem.Status %>' >  
                      <asp:ListItem Text="Vill lära mig" Value="Vill lära mig"></asp:ListItem> 
                      <asp:ListItem Text="Påbörjad" Value="Påbörjad" Selected="True"></asp:ListItem>   
                      <asp:ListItem Text="Klar" Value="Klar"></asp:ListItem>  
                      </asp:radiobuttonlist>  
            </div><br />

            <%-- Genre --%> 
            <label for="ddlGenre">Genre</label>
            <asp:DropDownList ID="ddlGenre" runat="server" SelectedValue='<%# BindItem.Genre %>'>  
                  <asp:ListItem Text="Blues" Value="Blues"></asp:ListItem>
                  <asp:ListItem Text="Country" Value="Country"></asp:ListItem>
                  <asp:ListItem Text="Funk" Value="Funk"></asp:ListItem>
                  <asp:ListItem Text="Gospel" Value="Gospel"></asp:ListItem>
                  <asp:ListItem Text="Indie-pop" Value="Indie-pop"></asp:ListItem>
                  <asp:ListItem Text="Jazz" Value="Jazz"></asp:ListItem> 
                  <asp:ListItem Text="Klassisk" Value="Klassisk" Selected="True"></asp:ListItem>   
                  <asp:ListItem Text="Metal" Value="Metal"></asp:ListItem>
                  <asp:ListItem Text="Pop" Value="Pop"></asp:ListItem>
                  <asp:ListItem Text="Rock" Value="Rock"></asp:ListItem>
                  <asp:ListItem Text="Övrigt" Value="Övrigt"></asp:ListItem> 
                  </asp:DropDownList>  
            <br /><br />
            
            <%-- Svårighetsgrad --%> 
            <label for="ddlLevel">Svårighetsgrad</label>
             <asp:DropDownList ID="ddlLevel" runat="server" SelectedValue='<%# BindItem.Level %>' >  
                  <asp:ListItem Text="1" Value="1"></asp:ListItem>
                  <asp:ListItem Text="2" Value="2"></asp:ListItem>
                  <asp:ListItem Text="3" Value="3"></asp:ListItem>
                  <asp:ListItem Text="4" Value="4" Selected="True"></asp:ListItem>
                  <asp:ListItem Text="5" Value="5"></asp:ListItem>
                  <asp:ListItem Text="6" Value="6"></asp:ListItem> 
                  <asp:ListItem Text="7" Value="7"></asp:ListItem>   
                  <asp:ListItem Text="8" Value="8"></asp:ListItem>
                  <asp:ListItem Text="9" Value="9"></asp:ListItem>
                  <asp:ListItem Text="10" Value="10"></asp:ListItem>
                  </asp:DropDownList> <br /><br />

            <%-- Anteckningar --%> 
             <div class="editor-field">
                <asp:TextBox ID="TextBox2" runat="server" MaxLength="1500" Text='<%# BindItem.Anteckning %>' TextMode="MultiLine" ></asp:TextBox>
            </div>    

        <asp:Button ID="SaveButton" runat="server" Text="Lägg till" CommandName="Insert" CssClass="button"/>
       </InsertItemTemplate>
      </asp:FormView>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
