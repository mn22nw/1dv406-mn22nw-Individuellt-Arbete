using Repertoar.App_GlobalResourses;
using Repertoar.MODEL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Repertoar.Pages.RepertoarPages
{
    public partial class SongDetails : System.Web.UI.Page
    {   
        #region Service-objekt
        private Service _service;
        private Service Service
        {
            // Ett Service-objekt skapas först då det behövs för första 
            // gången (lazy initialization, http://en.wikipedia.org/wiki/Lazy_initialization).
            get { return _service ?? (_service = new Service()); }
        }
        #endregion
        
        public int MID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            SuccessMessageLiteral.Text = Page.GetTempData("SuccessMessage") as string;
            SuccessMessagePanel.Visible = !String.IsNullOrWhiteSpace(SuccessMessageLiteral.Text);
        }

        public Material MaterialListView_GetItem([RouteData]int id)
        {
            try
            {
                return Service.GetSongByID(id);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty,  String.Format(Strings.Song_Not_Found, id));
                return null;
            }
        }
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
                var Composer = Service.GetComposers(true)
                    .Single(co => co.KompID == material2.KompID);

                // ...så att en beskrivning av kategori kan presenteras; ex: Kategori:Not
                label2.Text = String.Format(label2.Text, Composer.Namn);
            }

            var label3 = e.Item.FindControl("InstrumentNameLabel") as Label;
            if (label3 != null)
            {
                // Typomvandlar e.Item.DataItem så att primärnyckelns värde kan hämtas och...
                var material3 = (Material)e.Item.DataItem;

                // ...som sedan kan användas för att hämta ett instrumentobjekt...
                var instrument = Service.GetInstruments()
                    .Single(instr => instr.InstrumentID == material3.InstrumentID);

                // ...så att en beskrivning av instrument kan presenteras; ex: instrument:piano
                label3.Text = String.Format(label3.Text, instrument.Namn);
            }
        }


        public void MaterialtListView_DeleteSong(Material material)
        {
            try
            {
                Service.DeleteSong(material);
                Page.SetTempData("SuccessMessage", Strings.Action_Song_Deleted);
                Response.RedirectToRoute("Default");
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, Strings.Song_Deleting_Error);
            }
        }
    }
}