using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Repertoar.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("CreateSong", "Song/new", "~/Pages/RepertoarPages/Create.aspx");
            routes.MapPageRoute("SongListing", "Songs/list", "~/Pages/RepertoarPages/Listing.aspx");
            routes.MapPageRoute("EditSong", "Song/{id}/redigera", "~/Pages/RepertoarPages/Edit.aspx");
            routes.MapPageRoute("DeleteSong", "Song/{id}/radera", "~/Pages/RepertoarPages/Delete.aspx");
            routes.MapPageRoute("Details", "Song/{id}/detaljer", "~/Pages/RepertoarPages/SongDetails.aspx");

            routes.MapPageRoute("Error", "serverfel", "~/Pages/Shared/Error.aspx");
            routes.MapPageRoute("Default", "", "~/Pages/RepertoarPages/Listing.aspx");
        }
    }
}