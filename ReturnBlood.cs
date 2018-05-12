using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlagsAttribute = System.FlagsAttribute;

namespace Symulacja_Cyfrowa
{
    class ReturnBlood : Event
    {
        private int _eventTime;
        private string _type;
        private int _timeGenerator;

        public ReturnBlood(int eventTime, string type, int timeGenerator) : base(eventTime, type)
        {
            _eventTime = timeGenerator + eventTime;
            _type = type;
            _timeGenerator = timeGenerator;
        }

        public override void Execute(List<Event> scheduler, BloodStore bloodStorage, PatientQueue patientQ)
        {
            SortedByType(scheduler); // Sorting scheduler by Type aUT will be first
            while (true)
            {
                if ((_timeGenerator == SystemTime) && (scheduler[0].Type == "aUT") && (scheduler[0].BloodFromEmergency == true))
                {
                    bloodStorage.BloodLevel = --bloodStorage.BloodLevel; // Changes value of BloodLevel
                    bloodStorage.BloodStorageList.RemoveAt(0); // Removes Blood from BloodList
                    Console.WriteLine("\n### ReturnBlood -> Current blood level: " + bloodStorage.BloodLevel + " ###");
                }
                else
                {
                    break;
                }
            }
            Sorted(scheduler); // Sorting scheduler by expDate.
        }

    }
}
