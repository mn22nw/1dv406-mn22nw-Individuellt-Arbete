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
            if (Session["SuccessUpdate"] as bool? == true)
            {
                MessagePlaceHolder.Visible = true;
                Session.Remove("Success");
            }

            if (!IsPostBack)
            {
               BindList();
            }
            // http://www.dotnetfox.com/articles/how-to-bind-data-to-radiobuttonlist-control-in-Asp-Net-using-C-Sharp-1048.aspx#sthash.yt1EPaNZ.dpuf

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

        public void MaterialFormView_UpdateSong(int MID)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var material = Service.GetSongByID(MID);
                    if (material == null)
                    {
                        // Hittade inte låten.
                        ModelState.AddModelError(String.Empty,
                            String.Format("Låten med id {0} hittades inte.", MID));
                        return;
                    }

                    if (TryUpdateModel(material))
                    {
                       // Service.SaveSong(material, "TODO");

                        Page.SetTempData("SuccessMessage", Strings.Action_Song_Updated);
                        Response.RedirectToRoute("SongListing");
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, Strings.Song_Updating_Error);
                }
            }
        }

        public void MaterialListView_InsertItem(Material material)
        {
            if (Page.ModelState.IsValid)
            {
                try
                {   //TODO
                    material.MID = MID;

                    // Service.SaveSong(material, "todo", "gojo");
                    // Spara (rätt)meddelande och dirigera om klienten till lista med låtar.
                    // (Meddelandet sparas i en "temporär" sessionsvariabel som kapslas 
                    // in av en "extension method" i App_Infrastructure/PageExtensions.)
                    // Del av designmönstret Post-Redirect-Get (PRG, http://en.wikipedia.org/wiki/Post/Redirect/Get).
                    Page.SetTempData("SuccessMessage", Strings.Action_Song_Saved);
                    Response.RedirectToRoute("SongDetails", new { id = MID });
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    Page.ModelState.AddModelError(String.Empty, Strings.Song_Inserting_Error);
                }
            }
        }






        private void BindList()
        {   

          List<string> myinstruments = new List<string>();

          myinstruments.Add("Piano");
          myinstruments.Add("Gitarr");
          myinstruments.Sort();

          
          //rb.DataSource = myinstruments;
         // rb.DataBind();
         // rb.Items.FindByValue("Piano").Selected = true; //http://stackoverflow.com/questions/5662113/set-radiobuttonlist-selected-from-codebehind

           /* DataSet ds = new DataSet();
           
            return Service.GetInstrument(id);
            SqlDataAdapter adp = new SqlDataAdapter(cmdstr, CreateConnection());
            adp.Fill(ds);
            rblInstrument.DataSource = ds;
            rblInstrument.DataTextField = "Intrument";
            rblInstrument.DataValueField = "id";
            rblInstrument.DataBind();*/
        }

        protected void MaterialListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var label = e.Item.FindControl("ContactTypeNameLabel") as Label;
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
        }
        
    }
}