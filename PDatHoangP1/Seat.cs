/*
 * Seat.cs
 * This is a class that has properties and methods that belong to a seat. 
 * 
 * Revision history:
 * Hoang Huu Tat Dat, 2017/08/01: Created
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDatHoangP1
{
    class Seat
    {
        private string customerName;
        private bool isAvailable;

        public string CustomerName {get => customerName;set{customerName = CorrectName(value);}}
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }

        public Seat()
        {
            CustomerName = "";
            IsAvailable = true;
        }

        private string CorrectName(string name)
        {
            if (name.Length > 8)
                return name.Remove(8);
            if (name.Length < 8)
            {
                int j = 8 - name.Length;
                for (int i = 0; i < j; i++)
                    name += " ";
            }
            return name;
        }

    }
}
