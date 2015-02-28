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
        public string Composer { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            CustomLabelControl myControl = new CustomLabelControl();

            Page.Controls.Add(myControl);

            myControl.LabelText = "Will this work";


            if (IsPostBack)
            {
                ddlComposers.SelectedValue = Request.Form[ddlComposers.UniqueID];
                Debug.WriteLine("mimii " + ddlComposers.SelectedValue);
            }

          if (!IsPostBack)
            {
                BindList();
            }
        
        }
        #region Bindlist för Radiobuttonlists och DropDownLists
        private void BindList()
        { //TODO try catch
            rblKategori.DataSource = Service.GetKategories();
           // rblKategori.AppendDataBoundItems = true;
            rblKategori.DataBind();
            
            ListItem liKategori = new ListItem("Välj Kategori", "-1");
            rblKategori.Items.Insert(0, liKategori);

            #region ---
            /* if (rblKategori.SelectedIndex == -1) // -1 är om inget är valt
            {
                rblKategori.SelectedIndex = 0; 
            }

            if (rblStatus.SelectedIndex == -1) 
            {
                rblStatus.SelectedIndex = 0; 
            }*/
            #endregion 
            ddlComposers.DataSource = Service.GetComposers();
            ddlComposers.DataBind();

            ListItem liComposers = new ListItem("Välj Kompositör", "-1");
            ddlComposers.Items.Insert(0, liComposers);
            ddlInstruments.SelectedValue = "Piano";
            ddlGenre.SelectedValue = "Klassisk";
        }
        #endregion

        public IEnumerable<Material> MaterialListView_GetData()
        {
            return Service.GetSongs();
        }

        public IEnumerable<Kategori> KategoriDropDownList_GetData()
        {
            return Service.GetKategories();
        }
        //TODO kolla om denna behövs
        #region KAN BEHÖVAS?  
        /*  protected void MaterialListView_ItemDataBound(object sender, ListViewItemEventArgs e)
         {
             var label = e.Item.FindControl("KategoryNameLabel") as Label;
             if (label != null)
             {
                 // Typomvandlar e.Item.DataItem så att primärnyckelns värde kan hämtas och...
                 var material = (Material)e.Item.DataItem;

                 // ...som sedan kan användas för att hämta ett ("cachat") kategoriobjekt...
                 var Kategori = Service.GetKategories()
                     .Single(ka => ka.KaID == material.KaID);

                 // ...så att en beskrivning av kategori kan presenteras; ex: Kategori:Not
                 label.Text = String.Format(label.Text, Kategori.Namn);
             }

             var label2 = e.Item.FindControl("ComposerNameLabel") as Label;
             if (label2 != null)
             {
                 var material2 = (Material)e.Item.DataItem;

                 var Composer = Service.GetComposers()
                     .Single(co => co.KompID == material2.KompID);
 
                 label2.Text = String.Format(label2.Text, Composer.Namn);
             }
         }*/
        #endregion

        public void MaterialListView_InsertItem(object sender, EventArgs e)
        { 
             if (Page.ModelState.IsValid)
             {  
                 Material material = new Material();
                
                 //TODO - Validation if not null! 
               //  try
               //  {  // Anger värden från listorna
                        Debug.WriteLine(Composer + " insertt");
                     string KompNamn = ddlComposers.SelectedItem.Value;
                     
                     material.MID = 0;
                     material.KaID = Convert.ToInt32(rblKategori.SelectedItem.Value);
                     Debug.WriteLine(material.KaID);
                     material.Status = rblStatus.SelectedItem.Value;
                     material.Namn = Name.Text;
                     material.Genre = ddlGenre.SelectedItem.Value;
                     material.Level = Convert.ToInt32(ddlLevel.SelectedItem.Value);
                     material.Instrument = ddlInstruments.SelectedItem.Value; 
                     material.Anteckning = Anteckningar.Text;
                     
                     Service.SaveSong(material, KompNamn);
                     Debug.WriteLine("intehiti!");
                     Page.SetTempData("SuccessMessage", Strings.Action_Song_Saved);
                     Response.RedirectToRoute("Default");
                     Context.ApplicationInstance.CompleteRequest();
                /* }
                 catch (Exception)
                 {
                     Page.ModelState.AddModelError(String.Empty, Strings.Song_Inserting_Error);
                 }*/
             }
         }

        protected void ddlComposers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //när index ändras ska det skickas somehow till insertitem...
            Debug.WriteLine(Composer + " indexch");
            Composer = ddlComposers.SelectedValue;
            
        }

        protected void rblKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            //kod här
        }

     /*   protected void ListView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            DropDownList DropDownList1 = ListView1.FindControl("ddlComposers") as DropDownList;
            //save ddl index to viewstate.
            ViewState["ddlindex"] = DropDownList1.SelectedIndex;
            ListView1.DataBind();

        }

        

      /*  protected void DetailsView1_DataBound(object sender, EventArgs e)
        {
            
                DropDownList DropDownList1 = ListView1.FindControl("DropDownList1") as DropDownList;
                DropDownList1.DataSource = Service.GetComposers();
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "KompID";
                DropDownList1.DataBind();
                //when you first load detailsview,the viewstate["ddlindex"] is null,when you insert item to detailsview,
                //rebind your it,it will fire the databound event again.
                if (ViewState["ddlindex"] != null)
                {
                    DropDownList1.SelectedIndex = (int)ViewState["ddlindex"];
                }

            }*/

        }

    }
