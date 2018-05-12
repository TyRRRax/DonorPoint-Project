using System;
using System.Collections.Generic;

namespace Symulacja_Cyfrowa
{
    public class BloodStore : Event
    {
        public List<Blood> BloodStorageList = new List<Blood>();
        public static bool OrderFlag = false;
        public static bool EmergencyFlag = false;
        
        public static int GetRandomNumber() // function lock makes sure, that won't be a problem with generator
        {
            lock (Rnd)
            {
                return Rnd.Next();
            }
        }
        public BloodStore(int bloodLevel)
        {
            BloodLevel = bloodLevel;
        }
        public int BloodLevel { get; set; }

        public static void Sorted(List<Blood> bloodStorageList) // Sorting Storage
        {
            bloodStorageList.Sort((x, y) => x.ExpDate.CompareTo(y.ExpDate));
        }
        public void FillBlood(List<Event> scheduler)
        {
            for (int i = 0; i < 5; i++)
            {
                int time = Rnd.Next(1, 11); // Time from generator
                BloodStorageList.Add(new Blood(time));
                AddToScheduler(scheduler,new Utilisation(time+SystemTime,"aUT")); // Add to scheduler utilisation Event;
                BloodLevel++;
            }
            Sorted(BloodStorageList);
            Sorted(scheduler);
        } // Filling Blood for the first Time
        public void ShowExpDate()
        {
            foreach (var blood in BloodStorageList)
            {
                Console.Write(blood.ExpDate + " ");
            }
        }
        public void Transfusion(PatientQueue patientQ, List<Event> scheduler)
        {
            Console.Write("\n### TRANSFUSION ###\nBefore Transfusion: " + BloodLevel);
            var p1 = patientQ.RemovePatient();            // Take 1st patient from Queue and get him into var p1 
            Console.Write("\tNeed " + p1.BloodUnits + " blood units..");
            // Remodeling of scheduler, making UT units first in scheduler
            SortedByType(scheduler);
            for (int i = 0; i < p1.BloodUnits; i++)
            {
                BloodStorageList.RemoveAt(0); // Removes blood Unit from the System
                scheduler.RemoveAt(0); // Removes information about utilisation from Scheduler
            }
            BloodLevel = BloodLevel - p1.BloodUnits; // Reduce BloodLevel
            Console.WriteLine("\tAfter Transfusion: " + BloodLevel);
            Sorted(scheduler);
        }
        public static void NewOrder(List<Event> scheduler) // order on N blood units
        {
            if (OrderFlag == false)
            {
            Console.WriteLine("\n### NEW ORDER ###");
            int timeG = 15; // Time from generator (Time of transport the New Order)
            const int nUnits = 20;
            BloodSupply temp = new BloodSupply(timeG, "BS", nUnits, false);
            AddToScheduler(scheduler, temp);
            Sorted(scheduler);
            OrderFlag = true;
            }
        }
        public static void EmergencyOrder(List<Event>scheduler)             // order on Q blood units
        {
            if(EmergencyFlag == false)
            {
            Console.WriteLine("\n### EMERGENCY ORDER ###");
            const int timeG = 10; //  Time from generator(Time of transport the Emergency Order)
            const int timeG1 = 50; // Time from generator(Time of restoring emergency order)
            const int qUnits = 11;
            BloodSupply temp = new BloodSupply(timeG,"BS",qUnits,true);
            AddToScheduler(scheduler, temp);
            ReturnBlood rest = new ReturnBlood(timeG1,"RB",timeG); // timeG is being used to track right units of blood to destroy
            AddToScheduler(scheduler,rest);
            Sorted(scheduler);
            EmergencyFlag = true;
            }
        }
        public void Begin(List<Event> scheduler, BloodStore bloodStorage, PatientQueue patientQ)
        {
            SystemTime = 0;
            bloodStorage.FillBlood(scheduler); // initializes Blood into Storage and adds an ExpDate into scheduler
            Console.WriteLine("\nPo sortowaniu: ");
            bloodStorage.ShowExpDate();
            NewDonnor donator = new NewDonnor(5,"ND");
            NewPatient patient = new NewPatient(5,"NP");
            for (int i = 0; i < 5; i++) donator.Execute(scheduler, bloodStorage, patientQ); // Gives new 5 Donnors
            Sorted(scheduler);
            Console.WriteLine("\nSCHEDULER: ");
            ShowScheduler(scheduler);
            for (int i = 0; i < 5; i++) patient.Execute(scheduler, bloodStorage,patientQ); // creates 5 patients to the Queue
            patientQ.ShowQueue();
        }
    }
}
