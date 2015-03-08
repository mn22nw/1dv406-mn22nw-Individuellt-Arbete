using Repertoar.App_GlobalResourses;
using Repertoar.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Repertoar.Pages.RepertoarPages
{
    public partial class Edit : System.Web.UI.Page
    {   
        #region Service instance object
        private Service _service;
        

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        #endregion
        
 
        public int MID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Om genomförd handling lyckades av klienten och meddelande finns så visas det
            SuccessMessageLiteral.Text = Page.GetTempData("SuccessMessage") as string;
            SuccessMessagePanel.Visible = !String.IsNullOrWhiteSpace(SuccessMessageLiteral.Text);
        }

        public Material MaterialFormView_GetSong([RouteData]int id)
        {
            try
            {
                return Service.GetSongByID(id);
           }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, Strings.Song_Selecting_Error);
                return null;
            }
        }


        public IEnumerable<Kategori> CategoryDropDownList_GetData()
        {
            return Service.GetCategories();
        }

        public IEnumerable<Kompositör> ComposerDropDownList_GetData()
        {
            return Service.GetComposers(true);
        }

        public void MaterialFormView_UpdateSong(Material material)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    var song = Service.GetSongByID(material.MID);
                    if (song == null)
                    {
                        // Hittade inte låten.
                        ModelState.AddModelError(String.Empty, String.Format("Låten med id {0} hittades inte.", material.MID));
                        return;
                    }
                   
                    if (TryUpdateModel(song))
                    {   
                        //Kollar om användaren har valt att lägga till ny kompositör - kollar textfältet
                        TextBox kompText = (TextBox)EditFormView.Row.FindControl("KompNamn");

                        if (!String.IsNullOrWhiteSpace(kompText.Text))
                        {
                            //Om kompNamn är inte är tomt så har användaren valt att lägga till en ny kompositör
                            material.Composer = kompText.Text;
                            material.KompID = 0;
                        }

                        Service.SaveSong(material);
                       
                    }
                    //sätter meddelande till klienten
                    Page.SetTempData("SuccessMessage", Strings.Action_Song_Updated);

                    //Omdirigerar klienten till detaljerad vy över den sparade låten
                    Response.RedirectToRoute("Details", new { id = material.MID });
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, Strings.Song_Updating_Error);
                }
            }
        }
        
    }
}