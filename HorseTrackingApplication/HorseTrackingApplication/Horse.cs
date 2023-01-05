using System;
using System.Collections.Generic;
using System.Text;

namespace HorseTrackingApplication
{
    public class Horse
    {
        public int Id;
        public string Name;
        public int Odds;
        public string Res;

        public Horse() { }
        public Horse(string Name, int Odds,string Res, int Id)
        {
            this.Name = Name;
            this.Odds = Odds;
            this.Res = Res;
            this.Id = Id;
        }

        public int GetId()
        {
            return Id;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetOdds()
        {
            return Odds;
        }

        public string GetRes()
        {
            return Res;
        }

        public void SetRes(string res)
        {
            this.Res = res;
        }

    }
}
