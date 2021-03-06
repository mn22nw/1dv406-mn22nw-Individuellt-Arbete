﻿using System;
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
        #region INSERT
        public int InsertSong(Material material)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Repertoar_NewSong", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@KaID", SqlDbType.Int, 4).Value = material.KaID;
                    cmd.Parameters.Add("@KompID", SqlDbType.Int, 4).Value = material.KompID;
                    cmd.Parameters.Add("@Namn", SqlDbType.VarChar, 100).Value = material.Namn;
                    cmd.Parameters.Add("@Svarighetsgrad", SqlDbType.TinyInt).Value = material.Level;
                    cmd.Parameters.Add("@Genre", SqlDbType.VarChar, 20).Value = material.Genre;
                    cmd.Parameters.Add("@StatusSong", SqlDbType.VarChar, 15).Value = material.Status;
                    cmd.Parameters.Add("@InstrumentID", SqlDbType.Int, 4).Value = material.InstrumentID;

                    if (material.Anteckning == null)
                    {
                        material.Anteckning = "";

                    }

                    cmd.Parameters.Add("@Anteckning", SqlDbType.VarChar, 4000).Value = material.Anteckning;

                    if (material.Composer == null)
                    {
                        material.Composer = "";
                    }

                    cmd.Parameters.Add("@kompNamn", SqlDbType.VarChar, 60).Value = material.Composer;

                    conn.Open();  // ska inte vara öppen mer än vad som behövs, därför läggs den in här senare. 

                    //ExecuteScalar används för att exekvera den lp och få tillgång till primärnyckeln
                    int MID = (int)cmd.ExecuteScalar();
                    return MID;
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
        public void UpdateSong(Material material)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Repertoar_UpdateSong", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MID", SqlDbType.Int, 4).Value = material.MID;
                    cmd.Parameters.Add("@KaID", SqlDbType.Int, 4).Value = material.KaID;
                    cmd.Parameters.Add("@KompID", SqlDbType.Int, 4).Value = material.KompID;
                    cmd.Parameters.Add("@Namn", SqlDbType.VarChar, 30).Value = material.Namn;
                    cmd.Parameters.Add("@Svarighetsgrad", SqlDbType.TinyInt).Value = material.Level;
                    cmd.Parameters.Add("@Genre", SqlDbType.VarChar, 20).Value = material.Genre;
                    cmd.Parameters.Add("@StatusSong", SqlDbType.VarChar, 15).Value = material.Status;
                    cmd.Parameters.Add("@InstrumentID", SqlDbType.Int, 4).Value = material.InstrumentID;

                    if (material.Anteckning == null)
                    {
                        material.Anteckning = "";

                    }
                    cmd.Parameters.Add("@Anteckning", SqlDbType.VarChar, 4000).Value = material.Anteckning;

                    if (material.Composer == null)
                    {
                        cmd.Parameters.Add("@kompNamn", SqlDbType.VarChar, 60).Value = "";
                    }
                    else
                    {
                        cmd.Parameters.Add("@kompNamn", SqlDbType.VarChar, 60).Value = material.Composer;
                    }
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }

                catch
                {
                    throw new ApplicationException(Strings.Song_Inserting_Error_U);
                   // throw new ApplicationException(ex.Message); // TODO remove this
                    
                }
            }
        }
        #endregion
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
                        var MIDIndex = reader.GetOrdinal("MID"); // ger tillbaka ett heltal där MID finns
                        var KaIdIndex = reader.GetOrdinal("KaID");
                        var KompIndex = reader.GetOrdinal("KompID");
                        var NameIndex = reader.GetOrdinal("Namn");
                        var LevelIndex = reader.GetOrdinal("Svårighetsgrad");
                        var GenreIndex = reader.GetOrdinal("Genre");
                        var StatusIndex = reader.GetOrdinal("StatusSong");
                        var InstrumentIDIndex = reader.GetOrdinal("InstrumentID");
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
                                InstrumentID = reader.GetInt32(InstrumentIDIndex),
                                Datum = reader.GetDateTime(DateIndex),
                                Anteckning = reader.GetString(NoteIndex)
                            };
                        }
                    }

                    return null;
                }

                catch (Exception )
                {
                    throw new ApplicationException(Strings.Database_GetSong_Error);
                }
            }
        }

        public IEnumerable<Material> GetSongs()
        {
            using (var conn = CreateConnection())  
            {
              try
                {
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
                    var InstrumentIDIndex = reader.GetOrdinal("InstrumentID");
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
                            InstrumentID = reader.GetInt32(InstrumentIDIndex),
                            Datum = reader.GetDateTime(DateIndex),
                            Anteckning = reader.GetString(NoteIndex)
                        });
                    }
                    materials.TrimExcess(); // krymper till det faktiskta antalet element som är utnyttjat 
                }
                return materials;

                }
                  catch
                  {
                      throw new ApplicationException(Strings.Database_GetSongs_Error);
                  }
            }
        }
    }
}