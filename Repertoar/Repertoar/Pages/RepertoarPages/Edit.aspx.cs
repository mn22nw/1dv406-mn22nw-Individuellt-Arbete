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
        
        string Instrument;
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
            return Service.GetComposers();
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
                        //Ifall användaren har valt att lägga till ny kompositör måste kompID sättas till 0
                       
                        //Måste hitta controllen för att säkerhetsställa att användaren inte lämnat denna tom.
                        TextBox kompText = (TextBox)EditFormView.Row.FindControl("KompNamn");

                        if (String.IsNullOrWhiteSpace(kompText.Text))
                        {
                            //Om kompNamn är tomt så har användaren antingen glömt välja kompositör ur dropdownlistan eller lämnat textfältet tomt
                            ModelState.AddModelError(String.Empty, Strings.Song_Validation_Composer);
                        }
                        else
                        {
                            //Sätter composer till det nya namn som användaren precis la till. 
                            material.Composer = kompText.Text;

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


/*
        protected void MaterialListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var label = e.Item.FindControl("KategoryNameLabel") as Label;
            if (label != null)
            {
                // Typomvandlar e.Item.DataItem så att primärnyckelns värde kan hämtas och...
                var material = (Material)e.Item.DataItem;

                // ...som sedan kan användas för att hämta ett ("cachat") kategoriobjekt...
                var Kategori = Service.GetCategories()
                    .Single(ka => ka.KaID == material.KaID);

                // ...så att en beskrivning av kategori kan presenteras; ex: Kategori:Not
                label.Text = String.Format(label.Text, Kategori.Namn);
            }

            var label2 = e.Item.FindControl("ComposerNameLabel") as Label;
            if (label2 != null)
            {
                // Typomvandlar e.Item.DataItem så att primärnyckelns värde kan hämtas och...
                var material2 = (Material)e.Item.DataItem;

                // ...som sedan kan användas för att hämta ett ("cachat") kategoriobjekt...
                var Composer = Service.GetComposers()
                    .Single(co => co.KompID == material2.KompID);

                // ...så att en beskrivning av kategori kan presenteras; ex: Kategori:Not
                label2.Text = String.Format(label2.Text, Composer.Namn);
            }
        } */
        
    }
}