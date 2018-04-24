using System;
using System.Collections.Generic;

namespace Symulacja_Cyfrowa
{
    public class Blood
    {
        private int _expDate;
        public Blood(int experienceDate)  // Flag active is used to supplying the storage.
                                                      // It has to change on true, when blood is able to use.
        {
            _expDate = experienceDate;
        }
        public static Blood AddBlood(List<Blood> bloodStorageList) // returns reference to object Blood
        {
            var newBlood = new Blood(BloodStore.Rnd.Next(1, 11));
            bloodStorageList.Add(newBlood); // First blood fill into the storage
            var classRef = newBlood; // reference to newBlood
            return classRef;
        }
        public static Blood AddBlood(List<Blood> bloodStorageList, int time) // Adds blood with expDate dependent on type of Supply
        {
            var newBlood = new Blood(time);
            bloodStorageList.Add(newBlood); // Add blood to the storage
            var classRef = newBlood; // reference to newBlood
            return classRef;
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
