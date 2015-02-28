<%@ Page Title="Redigera" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Följande fel inträffade:" 
            CssClass="validation-summary-errors"/>
        <asp:PlaceHolder ID="MessagePlaceHolder" runat="server" Visible="false">
            
        </asp:PlaceHolder>
     <h1>Instrument</h1>

     <asp:FormView ID="FormView1" runat="server" 
            ItemType="Repertoar.MODEL.Material"
            DefaultMode="Edit"
            RenderOuterTable="false" 
            SelectMethod="MaterialFormView_GetSong"
            UpdateMethod="MaterialFormView_UpdateSong"
            DataKeyNames="MID"
                >
         <EditItemTemplate>
        <div class="editor-label">
            <label for="Name">Namn</label>
        </div>
 
        <div class="editor-field">
            <asp:TextBox ID="Name" runat="server" MaxLength="50" Text="<%# BindItem.Namn %>"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Ett förnamn måste anges." ControlToValidate="Name" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
    
               <asp:DropDownList ID="DropDownList1" runat="server"
                    ItemType="Repertoar.MODEL.Kategori"
                    DataKeyNames="KaID"
                    SelectMethod="CategoryDropDownList_GetData"
                    DataTextField="Namn"
                    DataValueField="KaID"
                    SelectedValue='<%# BindItem.KaID %>' />
                <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# BindItem.Namn %>' MaxLength="50" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Error"
                    ControlToValidate="ValueTextBox" CssClass="field-validation-error" Display="Dynamic">*</asp:RequiredFieldValidator>

            </EditItemTemplate>
           

        </asp:FormView>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
