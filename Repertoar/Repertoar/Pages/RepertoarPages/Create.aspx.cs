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
        }


        public void MaterialFormView_InsertSong(Material material)
        {
                    
            if (KompID == 0)
            {
                SuccessMessagePanel.Visible = true;
                SuccessMessageLiteral.Text = "En kompositör måste väjas";
            }

            if (KompID > 0)
            {

                if (String.IsNullOrWhiteSpace(material.Anteckning))
                {
                    material.Anteckning = "Klicka på redigera för att lägga till en anteckning. ";
                }

                string KompNamn = "Kompnamn";

                material.MID = Service.SaveSong(material, KompNamn);

                Page.SetTempData("SuccessMessage", Strings.Action_Song_Saved);
                Response.RedirectToRoute("Details", new { id = material.MID });
                Context.ApplicationInstance.CompleteRequest();
            }
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



        protected void ddlComposers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //när index ändras ska det skickas somehow till insertitem...
            //  Debug.WriteLine(Composer + " indexch");
            //Composer = ddlComposers.SelectedValue;

        }

        protected void rblKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            //kod här
        }


        protected void exitbutton_OnClick(object sender, EventArgs e)
        {
            SuccessMessagePanel.Visible = false;
        }

        #region Dynamic Methods


        public void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl = (DropDownList)sender;
            if (ddl.SelectedIndex > 0)
            {
                KompID = Convert.ToInt32(ddl.SelectedItem.Value);
            }
        }

        public void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var rbl = (RadioButtonList)sender;
            KaID = Convert.ToInt32(rbl.SelectedItem.Value);
            
        } 
        #endregion
    }
}