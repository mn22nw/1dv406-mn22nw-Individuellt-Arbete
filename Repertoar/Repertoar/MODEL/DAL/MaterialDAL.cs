using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Configuration;
using Repertoar.MODEL;
using Repertoar.MODEL.DAL;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Repertoar.App_GlobalResourses;

namespace Repertoar.MODEL.DAL
{
    public class MaterialDAL : DALBase
    {
        public void DeleteSong(int MID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Repertoar_DeleteSong", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MID", SqlDbType.Int, 4).Value = MID;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }

                catch
                {
                    throw new ApplicationException(Strings.Song_Deleting_Error);
                }
            }
        }

        public Material GetSongById(int MID)
        {
            using (var conn = CreateConnection())  
            {
                try
                {
                    var cmd = new SqlCommand("Repertoar_GetSong", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MID", SqlDbType.Int, 4).Value = MID;

                    conn.Open();  // ska inte vara öppen mer än vad som behövs, därför läggs den in här senare. 

                    using (var reader = cmd.ExecuteReader())
                    {
                        var MIDIndex = reader.GetOrdinal("MID"); // ger tillbaka ett heltal där ContactId finns
                        var KaIdIndex = reader.GetOrdinal("KaID");
                        var KompIndex = reader.GetOrdinal("KompID");
                        var NameIndex = reader.GetOrdinal("Namn");
                        var LevelIndex = reader.GetOrdinal("Svårighetsgrad");
                        var GenreIndex = reader.GetOrdinal("Genre");
                        var StatusIndex = reader.GetOrdinal("StatusSong");
                        var InstrumentIndex = reader.GetOrdinal("Instrument");
                        var DateIndex = reader.GetOrdinal("Datum");
                        var NoteIndex = reader.GetOrdinal("Anteckning");

                        if (reader.Read())
                        {
                            return new Material
                            {
                                MID = reader.GetInt32(MIDIndex),
                                KaID = reader.GetInt32(KaIdIndex),
                                KompID = reader.GetInt32(KompIndex),
                                Namn = reader.GetString(NameIndex),
                                Level = reader.GetByte(LevelIndex),
                                Genre = reader.GetString(GenreIndex),
                                Status = reader.GetString(StatusIndex),
                                Instrument = reader.GetString(InstrumentIndex),
                                Datum = reader.GetDateTime(DateIndex),
                                Anteckning = reader.GetString(NoteIndex)
                            };
                        }
                    }

                    return null;
                }

                catch (Exception )
                {
                    throw new ApplicationException( "Det gick inte att hämta ut låten från databasen");
                }
            }
        }

        public IEnumerable<Material> GetSongs()
        {
            using (var conn = CreateConnection())  // å
            {
                //  try
                //{
                var materials = new List<Material>(100);   // Object som håller ordning på de objekt som ska instansieras

                var cmd = new SqlCommand("Repertoar_GetSongs", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();  // ska inte vara öppen mer än vad som behövs, därför läggs den in här senare. 

                using (var reader = cmd.ExecuteReader())
                {
                    var MIDIndex = reader.GetOrdinal("MID"); // ger tillbaka ett heltal där MID finns
                    var KaIdIndex = reader.GetOrdinal("KaID");
                    var KompIndex = reader.GetOrdinal("KompID");
                    var NameIndex = reader.GetOrdinal("Namn");
                    var LevelIndex = reader.GetOrdinal("Svårighetsgrad");
                    var GenreIndex = reader.GetOrdinal("Genre");
                    var StatusIndex = reader.GetOrdinal("StatusSong");
                    var InstrumentIndex = reader.GetOrdinal("Instrument");
                    var DateIndex = reader.GetOrdinal("Datum");
                    var NoteIndex = reader.GetOrdinal("Anteckning");

                    while (reader.Read()) //så länge metoden read returnerar true finns det data att hämta. 
                    {
                        materials.Add(new Material
                        {

                            MID = reader.GetInt32(MIDIndex),
                            KaID = reader.GetInt32(KaIdIndex),
                            KompID = reader.GetInt32(KompIndex),
                            Namn = reader.GetString(NameIndex),
                            Level = reader.GetByte(LevelIndex),
                            Genre = reader.GetString(GenreIndex),
                            Status = reader.GetString(StatusIndex),
                            Instrument = reader.GetString(InstrumentIndex),
                            Datum = reader.GetDateTime(DateIndex),
                            Anteckning = reader.GetString(NoteIndex)
                        });
                    }
                    materials.TrimExcess(); // krymper till det faktiskta antalet element som är utnyttjat 
                }
                return materials;

                /*  }
                  catch
                  {
                      throw new ApplicationException("Det gick inte att hämta ut kontakterna från databasen");
                  }*/
            }
        }

        public int InsertSong(Material material, string KompNamn)
        {
            using (var conn = CreateConnection())
            {
                //TODO try catch
              /*  try
                {*/
                    var cmd = new SqlCommand("Repertoar_NewSong", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    cmd.Parameters.Add("@KaID", SqlDbType.Int, 4).Value = material.KaID;
                    cmd.Parameters.Add("@KompID", SqlDbType.Int, 4).Value = material.KompID;
                    cmd.Parameters.Add("@Namn", SqlDbType.VarChar, 100).Value = material.Namn;
                    cmd.Parameters.Add("@Svarighetsgrad", SqlDbType.TinyInt).Value = material.Level;
                    cmd.Parameters.Add("@Genre", SqlDbType.VarChar, 20).Value = material.Genre;
                    cmd.Parameters.Add("@StatusSong", SqlDbType.VarChar, 15).Value = material.Status;
                    cmd.Parameters.Add("@Instrument", SqlDbType.VarChar, 25).Value = material.Instrument;
                    //cmd.Parameters.AddWithValue("@Datum",DBNull.Value);
                    cmd.Parameters.Add("@Anteckning", SqlDbType.VarChar, 4000).Value = material.Anteckning;
                    cmd.Parameters.Add("@kompNamn", SqlDbType.VarChar, 60).Value = KompNamn;


                   // cmd.Parameters.Add("@MID", DBNull.Value).Direction = ParameterDirection.Output;
                    conn.Open();  // ska inte vara öppen mer än vad som behövs, därför läggs den in här senare. 

                    //ExecuteNonQuery används för att exekvera den lp. 
                   // cmd.ExecuteNonQuery();

                    
                    int MID = (int)cmd.ExecuteScalar();

                    return MID;
               /* }

                catch
                {
                    throw new ApplicationException(Strings.Song_Inserting_Error);
                }*/
            }
        }

        public void UpdateSong(Material material)
        {
            using (var conn = CreateConnection())  // å
            {
                try
                {
                    var cmd = new SqlCommand("Repertoar_UpdateSong", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MID", SqlDbType.Int, 4).Value = material.MID;
                    cmd.Parameters.Add("@KaID", SqlDbType.Int, 4).Value = material.KaID;
                    cmd.Parameters.Add("@KompID", SqlDbType.Int, 4).Value = material.KompID;
                    cmd.Parameters.Add("@Namn", SqlDbType.VarChar, 30).Value = material.Namn;
                    cmd.Parameters.Add("@Svårighetsgrad", SqlDbType.TinyInt).Value = material.Level;
                    cmd.Parameters.Add("@Genre", SqlDbType.VarChar, 20).Value = material.Genre;
                    cmd.Parameters.Add("@StatusSong", SqlDbType.VarChar, 15).Value = material.Status;
                    cmd.Parameters.Add("@Intrument", SqlDbType.VarChar, 25).Value = material.Instrument;
                    cmd.Parameters.Add("@Datum", SqlDbType.SmallDateTime).Value = material.Datum;
                    cmd.Parameters.Add("@Anteckning", SqlDbType.VarChar, 4000).Value = material.Anteckning;
                    conn.Open();  

                    cmd.ExecuteNonQuery();
                }

                catch
                {
                    throw new ApplicationException(Strings.Song_Inserting_Error_U);
                }
            }
        }
    }
}