using System;
using System.Collections.Generic;

namespace Symulacja_Cyfrowa
{
    public class BloodStore
    {

        private int _bloodLevel;
        public List<Blood> BloodStorageList = new List<Blood>();
        public static readonly Random Rnd = new Random();
        public static int GetRandomNumber() // function lock makes sure, that won't be a problem with generator
        {
            lock (Rnd)
            {
                return Rnd.Next();
            }
        }
        public BloodStore(int bloodLevel)
        {
            _bloodLevel = bloodLevel;
        }
        public int BloodLevel
        {
            get { return _bloodLevel; }
            set
            {
                _bloodLevel = value;
                _bloodLevel = BloodLevel;
            }
        }
        public void FillBlood(Blood[,] scheduler)
        {
            for (var i = 0; i < 20; i++)
            {
                AddToScheduler(scheduler,Blood.AddBlood(BloodStorageList),i); // Add to scheduler blood expDate; Function addBlood gives reference to created object type Blood
            }
        }
        public void AddToScheduler(Blood[,] scheduler,Blood classRef, int index)
        {
                for (var x = 0; x < 10; x++)
                {
                    if (scheduler[x, BloodStorageList[index].ExpDate] != null) continue;
                    scheduler[x, BloodStorageList[index].ExpDate] = classRef; break;
                    // if scheduler [x,i] is empty it means, 
                        // that in that unit of time there's no one task and Exception will be thrown 
                        // if it's not empty the x will increase and locate in the free space of a matrix.
                        // Scheduler contains references to object for each unit of time.
                        // breaks for (x), which gives us information that we put an expDate in the correct unit of time.
                }
        }
        public static void ShowScheduler(Blood[,] scheduler)
        {
            Console.Write("\n\nSCHEDULER: ");
            for (var i = 0; i < 11; i++) // There might be size of the scheduler (defined is 50), but know I will use 11, because 10 is max expDate.
            {
                for (var j = 0; j < 10; j++)
                {
                    if (scheduler[j, i] != null)
                    {
                        Console.Write(scheduler[j, i].ExpDate + "\t");
                    }
                    else { }
                }
                Console.WriteLine();
            }
        }
        public void ShowExpDate()
        {
            foreach (var blood in BloodStorageList)
            {
                Console.Write(blood.ExpDate + " ");
            }
        }
        public void NewDonnor(Blood [,]scheduler) 
        {
            BloodLevel++; // increase bloodLevel in Storage
            var classRef = Blood.AddBlood(BloodStorageList); // Gives reference to Object Blood which is inside of BloodStorage
            AddToScheduler(scheduler, classRef, BloodStorageList.LastIndexOf(classRef)); // Adds expDate into the scheduler 
        }
        public void Transfusion(int x, PatientQueue patientQ, Blood[,] scheduler)
        {
            int counter = 0;
            var p1 = patientQ.RemovePatient();            // Take 1st patient from Queue and get him into var p1 
            Console.Write("\n########## Need " + p1.BloodUnits + " blood units.. ##########");
            for (int i = 0; i < 50; i++)
            {
                if (counter < p1.BloodUnits)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if ((scheduler[j, i] != null) && (counter < p1.BloodUnits))
                        {
                            var blood = scheduler[j, i]; // Take blood from scheduler
                            scheduler[j, i] = null; // Remove blood from scheduler
                            counter++;
                            BloodStorageList.Remove(blood);
                        }

                    }
                }
                else break;
            }
            BloodLevel = BloodLevel - x; // Reduce BloodLevel
            Console.WriteLine("\n########## Current blood level: " + BloodLevel + " ##########");
        }
        public void BloodTransport(int bloodUnits)
        {
            BloodLevel = BloodLevel + bloodUnits;
            // Put Data (with expDate) into the Scheduler 
        }
        public void Utilisation(int bloodUnits)
        {
            BloodLevel = BloodLevel - bloodUnits;
            //Remove Data (with expDate) from Scheduler
            // Check how much blood left, if <= R, make an order on N blood units
        }
        public void NewOrder(int bloodNUnits,Blood [,] scheduler)             // order on N blood units
        {
            const int time_1 = 5; // T1 time
            int timeToWait = 1; // GENERATOR

            //Wait timeToWait units of time and then use AddBlood, and AddToScheduler functions
            /*
            for(int i=0; i<bloodNUnits; i++)
            {
            var classRef = Blood.AddBlood(BloodStorageList, time_1); // Gives reference to Object Blood which is inside of BloodStorage
            AddToScheduler(scheduler, classRef, BloodStorageList.LastIndexOf(classRef)); // Adds expDate into the scheduler 
            */
        }
        public void EmergencyOrder(int bloodQUnits)             // order on Q blood units
        {
            const int time_2 = 4; // T2 time
            int timeToWait = 1; // GENERATOR

            // Wait timeToWait units of time and then use AddBlood, and AddToScheduler functions
            /*
            for(int i=0; i<bloodQUnits; i++)
            {
            var classRef = Blood.AddBlood(BloodStorageList, time_1); // Gives reference to Object Blood which is inside of BloodStorage
            AddToScheduler(scheduler, classRef, BloodStorageList.LastIndexOf(classRef)); // Adds expDate into the scheduler 
            }
            */
        }

    }
}
