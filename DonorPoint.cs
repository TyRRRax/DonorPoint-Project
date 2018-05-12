using System;
using System.Collections.Generic;

namespace Symulacja_Cyfrowa
{
    public class DonorPoint : Event
    {
        private static void Main()
        {

            bool flag = true;
            bool flagAcc = true;
            while (flag)
            {
                Console.WriteLine("\n-----------------------------");
                // ####### BEGINING INITIALIZATION OF THE SYSTEM #######
                List<Event> scheduler = new List<Event>(); // Creates scheduler which is list of Events
                var bloodStorage = new BloodStore(0); // object contains bloodStorage; 0 is value how much blood units is inside
                var patientQ = new PatientQueue(); // object contains patientQueue
                bloodStorage.Begin(scheduler, bloodStorage, patientQ);
                // ####### END INITIALIZATION OF THE SYSTEM #######   
                Console.Write("\n\nMENU:\n1.Tryb ciagly\n2.Tryb krokowy\n3.Koniec symulacji\n-----------\n");
                const int r = 15;
                int loopTime = 100;
                Console.Write("Wybor: ");
                int loop = Convert.ToInt32(Console.ReadLine());
                switch (loop)
                {
                    case 1: // Constant Loop
                        {
                        while(loopTime != 0)
                            {
                                while (flagAcc)
                                {
                                    Console.WriteLine("\n\n@@@ " + loopTime + " @@@\n");
                                    if (bloodStorage.BloodLevel <= r) { BloodStore.NewOrder(scheduler);} // Make a NewOrder
                                    if(patientQ.BloodNeeded >= bloodStorage.BloodLevel) { BloodStore.EmergencyOrder(scheduler); } // Make a EmergencyOrder
                                    // Make Transfusion
                                    if ((patientQ.BloodNeeded != 0) && 
                                    (bloodStorage.BloodLevel > patientQ.BloodNeededFirst()))
                                    {
                                    bloodStorage.Transfusion(patientQ, scheduler);
                                    Console.WriteLine("\n### MAKE TRANSFUSION ###\n### PATIENT QUEUE ###");
                                    patientQ.ShowQueue();
                                    Console.WriteLine("### CURRENT SCHEDULER ###");
                                    ShowScheduler(scheduler);
                                    }
                                    else
                                    {
                                        flagAcc = false; 
                                    }
                                }

                                flagAcc = true;
                                if (SystemTime == scheduler[0].EventTime) 
                                {
                                    Event temp = scheduler[0]; // pull out object from list
                                    temp.Execute(scheduler, bloodStorage, patientQ); // Execute function
                                    scheduler.RemoveAt(0); // Remove object from list
                                    Console.WriteLine("\n### REMOVE OBJECT ###");
                                    ShowScheduler(scheduler);
                                }
                                else
                                {
                                    Console.WriteLine("\nNEW OBJECT -> SYS:" + SystemTime + " EVENT: "+scheduler[0].EventTime);
                                    SystemTime = scheduler[0].EventTime;
                                    Console.Write("System Time after Actualisation: " + SystemTime + "\n");
                                    loopTime--;
                                    ShowScheduler(scheduler);
                                }
                            }
                        break;
                        }
                    case 2: // Stepping Loop
                        {
                            while (loopTime != 0)
                            {
                                while (flagAcc)
                                {
                                    Console.WriteLine("\n\n" + loopTime + " @@@@@@@@@@@@\n");
                                    if (bloodStorage.BloodLevel <= r) { BloodStore.NewOrder(scheduler); } // Make a NewOrder
                                    if (patientQ.BloodNeeded >= bloodStorage.BloodLevel) { BloodStore.EmergencyOrder(scheduler); } // Make a EmergencyOrder
                                    // Make Transfusion
                                    if ((patientQ.BloodNeeded != 0) &&
                                    (bloodStorage.BloodLevel > patientQ.BloodNeededFirst()))
                                    {
                                        bloodStorage.Transfusion(patientQ, scheduler);
                                        Console.WriteLine("\n### MAKE TRANSFUSION ###\n### PATIENT QUEUE ###");
                                        patientQ.ShowQueue();
                                        Console.WriteLine("### CURRENT SCHEDULER ###");
                                        ShowScheduler(scheduler);
                                    }
                                    else
                                    {
                                        flagAcc = false;
                                    }
                                }
                                flagAcc = true;
                                if (SystemTime == scheduler[0].EventTime)
                                {
                                    Event temp = scheduler[0]; // pull out object from list
                                    temp.Execute(scheduler, bloodStorage, patientQ); // Execute function
                                    scheduler.RemoveAt(0); // Remove object from list
                                    Console.WriteLine("\n### REMOVE OBJECT ###");
                                    ShowScheduler(scheduler);
                                }
                                else
                                {
                                    Console.WriteLine("\nNEW OBJECT -> SYS:" + SystemTime + " EVENT: " + scheduler[0].EventTime);
                                    SystemTime = scheduler[0].EventTime;
                                    Console.Write("System Time after Actualisation: " + SystemTime + "\n");
                                    loopTime--;
                                    ShowScheduler(scheduler);
                                }
                                Console.WriteLine("\n\nCLICK ENTER TO CONTINUE\n");
                                Console.ReadLine(); // stepping loop
                            }
                            break;
                    }
                    case 3:
                    {
                        flag = false;
                        Console.WriteLine("Zakonczyles prace programu. Dziekujemy za symulacje.");
                        break;
                    }
                    default: 
                    {  
                        Console.WriteLine("Ups.. Podales nieznany numer. Sprobuj ponownie");
                        break;
                    }
                }
            }
        }
    }
}
