using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Symulacja_Cyfrowa
{
    class Utilisation : Event
    {
        private int _eventTime;
        private string _type;
        public Utilisation(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = Type;
        }
        public override void Execute(List<Event> scheduler,BloodStore bloodStorage, PatientQueue patientQ)
        {
                bloodStorage.BloodLevel = --bloodStorage.BloodLevel; // Changes value of BloodLevel
                Sorted(scheduler);
                bloodStorage.BloodStorageList.RemoveAt(0); // Removes Blood from BloodList
                Console.WriteLine("\n### UTILISATION -> Current blood level: " + bloodStorage.BloodLevel + " ###");
        }
    }
}
