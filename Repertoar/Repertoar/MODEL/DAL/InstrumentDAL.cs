using Repertoar.App_GlobalResourses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Repertoar.MODEL.DAL
{
    public class InstrumentDAL:DALBase
    {
       
        public IEnumerable<Instrument> GetInstruments()  
        {
            // Skapar ett anslutningsobjekt.
            using (var conn = CreateConnection())
            {
               try
                {   // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("Repertoar_GetInstruments", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Skapar det List-objekt som initialt har plats för 10 referenser till ContactType-objekt.
                    List<Instrument> instruments = new List<Instrument>(10);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en SELECT-sats som kan returnera flera poster varför
                    // ett SqlDataReader-objekt måste ta hand om alla poster. Metoden ExecuteReader skapar ett
                    // SqlDataReader-objekt och returnerar en referens till objektet.
                    using (var reader = cmd.ExecuteReader())
                    {

                        var InstrumentIdIndex = reader.GetOrdinal("InstrumentID"); 
                        var NamnIndex = reader.GetOrdinal("Namn");

                       
                        while (reader.Read())
                        {
                            instruments.Add(new Instrument
                            {
                                InstrumentID = reader.GetInt32(InstrumentIdIndex),
                                Namn = reader.GetString(NamnIndex),
                            });
                        }
                    }

                    // Sätter kapaciteten till antalet element i List-objektet, d.v.s. avallokerar minnesom inte används.
                    instruments.TrimExcess();

                    // Returnerar referensen till List-objektet med referenser med ContactType-objekt.
                    return instruments;
               }
                catch(Exception)
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException(Strings.DataBase_Instrument_Error);
                }
            }
        }

        #region INSERT
        public int InsertInstrument(Instrument instrument)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Repertoar_NewInstrument", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@InstrumentID", SqlDbType.Int, 4).Value = instrument.InstrumentID;
                    cmd.Parameters.Add("@Namn", SqlDbType.VarChar, 60).Value = instrument.Namn;

                    conn.Open();  // ska inte vara öppen mer än vad som behövs, därför läggs den in här senare. 

                    //ExecuteScalar används för att exekvera den lp och få tillgång till primärnyckeln
                    int instrumentID = (int)cmd.ExecuteScalar();
                    return instrumentID;
                }

                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                    //  throw new ApplicationException(Strings.Song_Inserting_Error);
                }
            }
        }
        #endregion
        #region UPDATE
        public void UpdateInstrument(Instrument instrument)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Repertoar_UpdateInstrument", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@InstrumentID", SqlDbType.Int, 4).Value = instrument.InstrumentID;
                    cmd.Parameters.Add("@Namn", SqlDbType.VarChar, 60).Value = instrument.Namn;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }

                catch
                {
                    throw new ApplicationException(Strings.Instrument_Updating_Error);
                    // throw new ApplicationException(ex.Message); // TODO remove this

                }
            }
        }
        #endregion

    }
}