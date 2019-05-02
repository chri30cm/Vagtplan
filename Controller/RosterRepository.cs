﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    public static class RosterRepository
    {
        private static List<Roster> rosters = new List<Roster>();
        public static void AddRoster(Roster roster)
        {
            rosters.Add(roster);
        }
        public static List<Roster> GetRosters()
        {
            return rosters;
        }
        public static void RemoveRoster(Roster roster)
        {
			rosters.Remove(roster);
        }


        public static DateTime GetEndDate()
        {
            return rosters[rosters.Count - 1].EndDate;
        }

        public static bool CurrentRosterExist()
        {
            bool exists = false;
            if (rosters.Count > 0)
            {
                exists = true;
            }

            return exists;
        }
    }
}
