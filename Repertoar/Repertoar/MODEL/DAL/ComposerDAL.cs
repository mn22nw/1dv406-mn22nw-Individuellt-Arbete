﻿using Repertoar.App_GlobalResourses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Repertoar.MODEL.DAL
{
    public class ComposerDAL:DALBase
    {
        public IEnumerable<Kompositör> GetComposers()  
        {
            // Skapar ett anslutningsobjekt.
            using (var conn = CreateConnection())
            {
               try
                {   // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("Repertoar_GetComposers", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Skapar det List-objekt som initialt har plats för 10 referenser till ContactType-objekt.
                    List<Kompositör> composers = new List<Kompositör>(10);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en SELECT-sats som kan returnera flera poster varför
                    // ett SqlDataReader-objekt måste ta hand om alla poster. Metoden ExecuteReader skapar ett
                    // SqlDataReader-objekt och returnerar en referens till objektet.
                    using (var reader = cmd.ExecuteReader())
                    {

                        var KompIDIndex = reader.GetOrdinal("KompID"); 
                        var NamnIndex = reader.GetOrdinal("Namn");

                       
                        while (reader.Read())
                        {
                            composers.Add(new Kompositör
                            {
                                KompID = reader.GetInt32(KompIDIndex),
                                Namn = reader.GetString(NamnIndex),
                            });
                        }
                    }

                    // Sätter kapaciteten till antalet element i List-objektet, d.v.s. avallokerar minnesom inte används.
                    composers.TrimExcess();

                    // Returnerar referensen till List-objektet med referenser med ContactType-objekt.
                    return composers;
               }
                catch(Exception)
                {
                    // Kastar ett eget undantag om det går fel
                    throw new ApplicationException(Strings.DataBase_Composer_Error);
                }
            }
        }
        public void DeleteComposer(int KompID)
        {
            
            using (var conn = CreateConnection())
            {
                try
                {

                    var cmd = new SqlCommand("Repertoar_DeleteComposer", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@KompID", SqlDbType.Int, 4).Value = KompID;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }

                catch
                {
                    throw new ApplicationException(Strings.Song_Deleting_Error);
                }
            }
        }
    }
}
   