using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulacja_Cyfrowa
{
    class NewDonnor : Event
    {
        private int _eventTime;
        private string _type;
        public NewDonnor(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = type;
        }
        public override void Execute(List<Event> scheduler, BloodStore bloodStorage, PatientQueue patientQ)
        {
                Type = "ND";
                int time = Rnd.Next(11,21); // Gets this from generator
                time = SystemTime + time;
                Console.WriteLine("\n### NEW DONNOR ###");
                Blood.AddBlood(bloodStorage.BloodStorageList,time); // Adds Blood to the Storage
                BloodStore.Sorted(bloodStorage.BloodStorageList);
                AddToScheduler(scheduler, new BloodSupply(SystemTime+time,"TR",1,false)); // Adds Transport with Blood which will arrive 
                AddToScheduler(scheduler, new NewDonnor(SystemTime+time,"ND")); // Planning new Donnor Event  
                Sorted(scheduler);
        }
    }
}
