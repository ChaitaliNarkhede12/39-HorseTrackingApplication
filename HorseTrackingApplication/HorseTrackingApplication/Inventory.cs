using System;
using System.Collections.Generic;
using System.Text;

namespace HorseTrackingApplication
{
    public class Inventory
    {
        public int Name = 0;
        public int Stock = 0;

        public Inventory() { }
        public Inventory(int Name)
        {
            this.Name = Name;
            this.Stock = 10;
        }

        public int GetName()
        {
            return Name;
        }

        public int GetStock()
        {
            return Stock;
        }

        public void SetStock(int stock)
        {
            this.Stock = stock;
        }

    }
}
