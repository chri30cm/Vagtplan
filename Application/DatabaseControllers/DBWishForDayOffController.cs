﻿using Application.Repositories;
using Domain.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Application.DatabaseControllers
{
    public static class DBWishForDayOffController
    {
        public static void LoadWishForDayOffs()
        {
            DateTime day;
            int wishForDayOffID;
            int employeeID;
            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                string query = "select * from WishForDayOff";
                var cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.Text;              
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        wishForDayOffID = (int)reader["WishForDayOffID"];
                        employeeID = (int)reader["EmployeeID"];
                        day = (DateTime)reader["Date"];
                        WishForDayOff newWish = new WishForDayOff(wishForDayOffID, employeeID, day);
                        WishForDayOffRepository.AddWishForDayOff(newWish);
                    }
                }
                DBConnection.Close();
            }
        }

        public static void CreateWishForDayOff(WishForDayOff wish)
        {
            string query = "Create_WishForDayOff";
            DBConnection.DatabaseName = "CANE";
            if (!WishForDayOffRepository.WishForDayOffExist(wish))
            {
                if (DBConnection.IsConnected())
                {
                    var cmd = new SqlCommand(query, DBConnection.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@EmployeeID_IN", wish.EmployeeID));
                    cmd.Parameters.Add(new SqlParameter("@Date_IN", wish.Date));
                    cmd.ExecuteReader();
                    DBConnection.Close();
                }
                WishForDayOffRepository.AddWishForDayOff(wish);
            }
        }
    }
}
