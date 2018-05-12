using System;
using System.Collections.Generic;

namespace Symulacja_Cyfrowa
{
    class BloodSupply : Event
    {
        private int _eventTime;
        private string _type;
        private int _bUnits;
        private bool _flagEmergency;
        public BloodSupply(int eventTime, string type, int bUnits,bool flagEmergency) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = Type;
            _bUnits = bUnits; // Blood units N = 20 / Q = 11
            _flagEmergency = flagEmergency;
        }

        public int BUnits
        {
            get { return _bUnits; }
            set { _bUnits = BUnits; }
        }
        public override void Execute(List<Event> scheduler, BloodStore bloodStorage, PatientQueue patientQ)
        {
            _type = "BT";
            int time = BUnits == 1 ? 500 : 300; // lambda statemenet N and Q are connect with T1 and T2
            for(int i=0; i<BUnits; i++)
            {
            bloodStorage.BloodLevel++;
            bloodStorage.BloodStorageList.Add(new Blood(time));
            Utilisation temp = new Utilisation(SystemTime + time, "aUT");
            AddToScheduler(scheduler, temp); // Adds time of utilisation of Blood
                if (_flagEmergency == true) // Enables to return exactly right units of blood from Emergency
                {
                    temp.BloodFromEmergency = true;
                }
            }
            BloodStore.Sorted(bloodStorage.BloodStorageList);
            Sorted(scheduler);
            if(time == 300) { BloodStore.OrderFlag = false; } // Setting right flags (makes NewOrder available)
            else{ BloodStore.EmergencyFlag = false;  } // Makes EmergencyOrder available
        }
    }
}
