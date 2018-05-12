using System;
using System.Collections.Generic;

namespace Symulacja_Cyfrowa
{
    public class Blood
    {
        private int _expDate;
        public Blood(int experienceDate)  
                                                      
        {
            _expDate = experienceDate;
        }
        public static void AddBlood(List<Blood> bloodStorageList) // returns reference to object Blood
        {
            var newBlood = new Blood(Event.Rnd.Next(1, 11)); // inside Rnd Next there's Expiration Date
            bloodStorageList.Add(newBlood); // First blood fill into the storage
        }
        public static void AddBlood(List<Blood> bloodStorageList, int time) // Adds blood with expDate dependent on type of Supply
        {
            var newBlood = new Blood(time);
            bloodStorageList.Add(newBlood); // Add blood to the storage
        }
        public int ExpDate
        {
            get { return _expDate; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                _expDate = value;
                ExpDate = _expDate;
            }
        }
    }
}
