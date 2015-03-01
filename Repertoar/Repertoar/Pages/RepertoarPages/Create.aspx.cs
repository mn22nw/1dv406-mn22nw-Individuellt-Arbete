using Repertoar.App_GlobalResourses;
using Repertoar.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Repertoar.Pages.RepertoarPages
{
    public partial class Create : System.Web.UI.Page
    {
        #region Service-objekt
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        #endregion

        public int MID { get; set; }
        public int KaID { get; set; }
        public int KompID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            DropDownList ddlComposer = (DropDownList)SongFormView.Row.FindControl("ddlComposer");
            ddlComposer.Items.Insert(0, new ListItem("-- Välj Kompositör --", string.Empty));
        }


        public void MaterialFormView_InsertSong(Material material)
        {
            string kompNamn = "";
           
            
            //Om KompId är 0 så har användaren inte valt en kompositör från listan och vill därför lägga till en ny kompositör
            if (KompID == 0)
            {   
                TextBox kompText =(TextBox)SongFormView.Row.FindControl("KompNamn");

                if (String.IsNullOrWhiteSpace(kompText.ToString()))
                {
                    //Om kompNamn är tomt så har användaren antingen glömt välja kompositör ur dropdownlistan eller lämnat textfältet tomt
                    SuccessMessageLiteral.Text = "En kompositör måste väjas ur listan eller läggas till";
                    SuccessMessagePanel.Visible = true;
                }
                else
                {
                    kompNamn = kompText.ToString();
                }
 
            }

           
                if (String.IsNullOrWhiteSpace(material.Anteckning))
                {
                    material.Anteckning = "Klicka på redigera för att lägga till en anteckning. ";
                }

                

                material.MID = Service.SaveSong(material, kompNamn);

                Page.SetTempData("SuccessMessage", Strings.Action_Song_Saved);
                Response.RedirectToRoute("Details", new { id = material.MID });
                Context.ApplicationInstance.CompleteRequest();
        }


        public IEnumerable<Material> MaterialListView_GetData()
        {
            return Service.GetSongs();
        }

        public IEnumerable<Kategori> CategoryDropDownList_GetData()
        {
            return Service.GetCategories();
        }

        public IEnumerable<Kompositör> ComposerDropDownList_GetData()
        {
            return Service.GetComposers();
        }


        protected void exitbutton_OnClick(object sender, EventArgs e)
        {
            SuccessMessagePanel.Visible = false;
        }

    }
}