<%@ Page Title="Create" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="CreateSong.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.CreateSong" ViewStateMode="Disabled" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   <h1>Lägg till ny låt</h1>
   <hr />
             <asp:Panel runat="server" ID="SuccessMessagePanel" Visible="false" CssClass="icon-ok">
                <asp:Literal runat="server" ID="SuccessMessageLiteral" />
            <asp:Button ID="Button1" CssClass="exit" runat="server" Text="Stäng" OnClientClick="exitbutton_OnClick" />
            </asp:Panel>
  
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Följande fel inträffade:" 
            CssClass="validation-summary-errors"/>
         
<asp:FormView ID="SongFormView" runat="server"
    ItemType="Repertoar.MODEL.Material"
    DefaultMode="Insert"
    InsertMethod="MaterialFormView_InsertSong"
    RenderOuterTable="false" 
    ViewStateMode="Enabled">

    <InsertItemTemplate>
        <br />
            <%-- Namn --%>
            <div class="section smaller" >
                <div class="left">
                    <span class="title">Namn:</span>
                        <asp:TextBox ID="Namn" runat="server" Text='<%# BindItem.Namn %>'  MaxLength="100" Width="350px" />  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="Ett låtnamn måste anges." ControlToValidate="Namn" Display="Dynamic" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
                </div>

             <%-- Instrument --%>   
             <div class="sRight">  
                   <span class="title">Instrument:</span>
                 <asp:DropDownList ID="ddlInstruments" runat="server"
                        ItemType="Repertoar.MODEL.Instrument"
                        SelectMethod="InstrumentList_GetData"
                        DataTextField="Namn"
                        DataValueField="InstrumentID"
                        SelectedValue='<%# BindItem.InstrumentID %>'
                        CssClass="dropDown">
                    </asp:DropDownList> <br />
                  </div>
                </div>
         <%-- Status --%>  
          <div class="section smaller"> 
              <div class="left">
               <span class="title">Status:</span>
               <asp:radiobuttonlist ID="rblStatus" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# BindItem.Status %>' >  
                      <asp:ListItem Text="Vill lära mig" Value="Vill lära mig"></asp:ListItem> 
                      <asp:ListItem Text="Påbörjad" Value="Påbörjad" Selected="True"></asp:ListItem>   
                      <asp:ListItem Text="Klar" Value="Klar"></asp:ListItem>  
                      </asp:radiobuttonlist>  
            </div>
               <%-- Kategori --%>  
               <div class="sRight"> 
                <span class="title">Kategori:</span>
                 <asp:DropDownList ID="ddlCategory" runat="server"
                        ItemType="Repertoar.MODEL.Kategori"
                        SelectMethod="CategoryList_GetData"
                        DataTextField="Namn"
                        DataValueField="KaID"
                        SelectedValue='<%# BindItem.KaID %>'
                        CssClass="dropDown">
                    </asp:DropDownList> <br />
                </div>
            </div>
          <%-- Kompositör --%>
            <div class="section"> 
            <h3>Kompositör</h3>
            <div class="left">
                          <asp:DropDownList ID="ddlComposer" runat="server"
                            ItemType="Repertoar.MODEL.Kompositör"
                            SelectMethod="ComposerList_GetData"
                            DataTextField="Namn"
                            DataValueField="KompID"
                            SelectedValue='<%# BindItem.KompID %>'
                            CssClass="dropDown">
                        </asp:DropDownList>
                    </div>
              
                    <%-- Lägg till ny kompositör --%>
                    <div class="sRight">
                          <span class="newName">eller lägg till ny kompositör:</span> 
                          <asp:TextBox ID="KompNamn" runat="server" Text=""  MaxLength="60" />   
                    </div>
            </div>
           
         <div class="section smaller"> 
              <%-- Genre --%> 
             <div class="left">
                <span class="title">Genre:</span>
                <asp:DropDownList ID="ddlGenre" runat="server" SelectedValue='<%# BindItem.Genre %>'  CssClass="dropDown" >  
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
              </div>  
            
                <%-- Svårighetsgrad --%> 
                <div class="sRight"> 
                <span class="title">Svårighetsgrad:</span>
                 <asp:DropDownList ID="ddlLevel" runat="server" SelectedValue='<%# BindItem.Level %>'  CssClass="dropDown" >  
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
                </div>
            </div>
            <%-- Anteckningar --%> 
            <div class="section"> 
            <h2>Anteckningar</h2>
             <asp:TextBox ID="TextBox2" runat="server" MaxLength="1500" Text='<%# BindItem.Anteckning %>' TextMode="MultiLine" ></asp:TextBox>
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
