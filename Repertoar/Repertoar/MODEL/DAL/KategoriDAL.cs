using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Repertoar.MODEL.DAL
{
    public class KategoriDAL:DALBase
    {
       
        public IEnumerable<Kategori> GetCategories()  
        {
            // Skapar ett anslutningsobjekt.
            using (var conn = CreateConnection())
            {
               // try
              //  {   // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("Repertoar_GetKategories", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Skapar det List-objekt som initialt har plats för 10 referenser till ContactType-objekt.
                    List<Kategori> kategories = new List<Kategori>(10);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en SELECT-sats som kan returnera flera poster varför
                    // ett SqlDataReader-objekt måste ta hand om alla poster. Metoden ExecuteReader skapar ett
                    // SqlDataReader-objekt och returnerar en referens till objektet.
                    using (var reader = cmd.ExecuteReader())
                    {

                        var KaIdIndex = reader.GetOrdinal("KaID"); 
                        var NamnIndex = reader.GetOrdinal("Namn");

                       
                        while (reader.Read())
                        {
                            kategories.Add(new Kategori
                            {
                                KaID = reader.GetInt32(KaIdIndex),
                                Namn = reader.GetString(NamnIndex),
                            });
                        }
                    }

                    // Sätter kapaciteten till antalet element i List-objektet, d.v.s. avallokerar minnesom inte används.
                    kategories.TrimExcess();

                    // Returnerar referensen till List-objektet med referenser med ContactType-objekt.
                    return kategories;
              /*  }
                catch(Exception)
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("Det gick inte att hämta ut kategori från databasen");
                }*/
            }
        }
    }
}