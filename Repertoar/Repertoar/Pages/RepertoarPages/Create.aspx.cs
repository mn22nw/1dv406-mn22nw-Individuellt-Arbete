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
using System.ComponentModel.DataAnnotations;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            //Lägger till ett första 'ovalt' alternativ för dropdownlist (kompositör som genereras från databasen)
            DropDownList ddlComposer = (DropDownList)SongFormView.Row.FindControl("ddlComposer");
            ddlComposer.Items.Insert(0, new ListItem("-- Välj Kompositör --", "0"));

            //Om genomförd handling lyckades av klienten och meddelande finns så visas det
             SuccessMessageLiteral.Text = Page.GetTempData("SuccessMessage") as string;
             SuccessMessagePanel.Visible = !String.IsNullOrWhiteSpace(SuccessMessageLiteral.Text);

        }


        public void MaterialFormView_InsertSong(Material material)
        {
            if (ModelState.IsValid)
            {
                try
                {   
                    //*HANTERAR VALIDERING AV KOMPOSITÖR DROPDOWNLIST + inputfält för kompositör*//
                    //Om KompId är 0 så har användaren inte valt en kompositör från listan och vill därför lägga till en ny kompositör
                    if (material.KompID == 0)
                    {
                        //Måste hitta controllen för att säkerhetsställa att användaren inte lämnat denna tom.
                        TextBox kompText = (TextBox)SongFormView.Row.FindControl("KompNamn");

                        if (String.IsNullOrWhiteSpace(kompText.Text))
                        {
                            //Om kompNamn är tomt så har användaren antingen glömt välja kompositör ur dropdownlistan eller lämnat textfältet tomt
                            ModelState.AddModelError(string.Empty, Strings.Song_Validation_Composer);
                            return;
                        }
                        else
                        {
                            //Sätter composer till det nya namn som användaren precis la till. 
                            material.Composer = kompText.Text;
                        }
                    }


                    //*SPARAR DEN NYA LÅTEN  - IFALL ALLT ÄR VALIDERAT OCH OKEJ*//
                    material.MID = Service.SaveSong(material);

                    //sätter meddelande till klienten
                    Page.SetTempData("SuccessMessage", Strings.Action_Song_Saved);

                    //Omdirigerar klienten till detaljerad vy över den sparade låten
                    Response.RedirectToRoute("Details", new { id = material.MID });
                    Context.ApplicationInstance.CompleteRequest();
                }

                catch (Exception ex)
                {   
                    //sätter felmeddelande till klienten
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                
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

        protected void exitbutton_OnClick(object sender, EventArgs e)
        {
            SuccessMessagePanel.Visible = false;
        }

    }
}