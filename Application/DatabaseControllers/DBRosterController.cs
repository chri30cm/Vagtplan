﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Domain.Models;
using System.Data;
using System.IO;
using Application.Repositories;

namespace Application.DatabaseControllers
{
    public static class DBRosterController
    {
        public static List<Roster> GetRosters()
        {
            List<Roster> rosterList = new List<Roster>();
            string rosterID = null;
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            string shop = null;

            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                string query = "SELECT * FROM Roster";
                SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rosterID = reader["RosterID"].ToString();
                        int.TryParse(rosterID, out int rosterIDint);

                        startDate = (DateTime)reader["StartDate"];

                        endDate = (DateTime)reader["EndDate"];

                        shop = reader["Shop"].ToString();
                        Shop newShop;
                        if (shop == "kongensgade")
                        {
                            newShop = Shop.kongensgade;
                        }
                        else
                        {
                            newShop = Shop.skibhusvej;
                        }

                        Roster addRoster = new Roster(startDate, endDate, newShop);
                        RosterRepository.AddRoster(addRoster);
                        rosterList.Add(addRoster);
                    }
                }
                DBConnection.Close();
            }
            return rosterList;
        }

        public static void CreateRoster(Roster roster)
        {
            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                string query = "Create_Roster";
                var cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StartDate_IN", roster.StartDate));
                cmd.Parameters.Add(new SqlParameter("@EndDate_IN", roster.EndDate));
                cmd.Parameters.Add(new SqlParameter("@Shop_IN", roster.Shop.ToString()));
                cmd.ExecuteReader();
                DBConnection.Close();
            }
        }
    }
}