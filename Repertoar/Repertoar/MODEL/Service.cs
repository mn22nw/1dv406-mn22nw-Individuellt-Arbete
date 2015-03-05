﻿using Repertoar.App_GlobalResourses;
using Repertoar.MODEL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Repertoar.MODEL
{
    public class Service  // kommunicerar med dataatkomstlagret som man kan instansiera i presentationslagret
    {
        #region Fält

        private MaterialDAL _materialDAL;
        private KategoriDAL _kategoriDAL;
        private ComposerDAL _composerDAL;

        #endregion

        #region Egenskaper

        private MaterialDAL MaterialDAL
        {
            // Ett materialDAL-objekt skapas först då det behövs för första 
            // gången (lazy initialization, http://en.wikipedia.org/wiki/Lazy_initialization).
            get { return _materialDAL ?? (_materialDAL = new MaterialDAL()); }
        }

        private KategoriDAL KategoriDAL
        {
            get { return _kategoriDAL ?? (_kategoriDAL = new KategoriDAL()); }
        }

        private ComposerDAL ComposerDAL
        {
            get { return _composerDAL ?? (_composerDAL = new ComposerDAL()); }
        }

        #endregion

        #region Material CRUD-metoder
        public int SaveSong(Material material)
        {   
            try
            {
                int MID;
                ICollection<ValidationResult> validationResults;

                //Kollar att objektet verkligen går igenom validering och kastar undatag ifall något är fel
                if (!material.Validate(out validationResults))
                {
                    var ex = new ValidationException("Objektet klarade inte valideringen.");
                    ex.Data.Add("ValidationResults", validationResults);
                    throw ex;
                }

                if (material.MID == 0) // Ny post om MID är 0!
                {
                  //Lägger till en ny sång
                   MID =  MaterialDAL.InsertSong(material);
                   return MID;
                    
                }
                else 
                {  
                   //Updaterar befintlig sång
                   MaterialDAL.UpdateSong(material);
                   return material.MID;
                   
                }

             }
            
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message); //TODO ändra detta!
            }
        }
        public void DeleteSong(int MID)
        {
            MaterialDAL.DeleteSong(MID);
        }

        public Material GetSongByID(int MID)
        {
            return MaterialDAL.GetSongById(MID);
        }

        public IEnumerable<Material> GetSongs()
        {
            return MaterialDAL.GetSongs();

        }
        #endregion
        #region Kategori (C)R(UD)-metoder

        /// <summary>
        /// Hämtar alla kategorier.
        /// </summary>
        /// <returns>Ett List-objekt innehållande referenser till Kategori-objekt.</returns>
        public IEnumerable<Kategori> GetCategories(bool refresh = false)
        {
            // Försöker hämta lista med kategorier från cachen.
            var categories = HttpContext.Current.Cache["Category"] as IEnumerable<Kategori>;

            // Om det inte finns det en lista med kategorier...
            if (categories == null || refresh)
            {
                // ...hämtar då lista med kontakttyper...
                categories = KategoriDAL.GetCategories();

                // ...och cachar dessa. List-objektet, inklusive alla kategori-objekt, kommer att cachas 
                // under 10 minuter, varefter de automatiskt avallokeras från webbserverns primärminne.
                HttpContext.Current.Cache.Insert("Category", categories, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }

            // Returnerar listan med kategorier.
            return categories;
        }

        #endregion

        #region Kompositör (C)R(UD)-metoder

        public IEnumerable<Kompositör> GetComposers(bool refresh = false)
        {
            var composers = HttpContext.Current.Cache["Composer"] as IEnumerable<Kompositör>;

            // Om det inte finns det en lista med kategorier...
            if (composers == null || refresh)
            {
                // ...hämtar då lista med kontakttyper...
                composers = ComposerDAL.GetComposers();

                // ...och cachar dessa. List-objektet, inklusive alla kategori-objekt, kommer att cachas 
                // under 10 minuter, varefter de automatiskt avallokeras från webbserverns primärminne.
                HttpContext.Current.Cache.Insert("Composer", composers, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }

            // Returnerar listan med kategorier.
            return composers;
        }

        #endregion
    }
}