using System;
using DalObject;
using System.Collections;
using IDAL.DO;
namespace ConsoleUI
{
    class Program
    {
        static DalObject.DalObject dd = new();




        static void add()
        {
            Console.WriteLine("ENTER YOUR CHOISE:/n 1-station /n 2-to drone /n 3-customer /n 4-parcel /n");
            string choise = Console.ReadLine();
              switch (choise)
              {
                  case "1":

                      Console.WriteLine("enter id,  Name,Longitude, Latitude, ChargeSlot");
                      int Id = int.Parse(Console.ReadLine());
                      string Name = Console.ReadLine();
                      double Longitude = double.Parse(Console.ReadLine());
                      double Latitude = double.Parse(Console.ReadLine());
                      int ChargeSlot= int.Parse(Console.ReadLine());
                      Station s=new Station(Id, Name, Longitude, Latitude, ChargeSlot);
                      dd.addStation(s);
                      Console.WriteLine("very goood");
                      break;
                  case "2":
                      Console.WriteLine("enter id,  Model, , MaxWeight, Status,Battery");
                      Id = int.Parse(Console.ReadLine());
                      string Model = Console.ReadLine();
                      WeightCategories MaxWeight = WeightCategories.Parse( Console.ReadLine());
                      DroneStatuses Status = DroneStatuses.Parse(Console.ReadLine());
                      double Battery= double.Parse( Console.ReadLine());
                      Drone d=new Drone(Id, Model, MaxWeight,Status, Battery);
                      dd.addDrone(d);
                      break;
                  case "3":
                      Console.WriteLine("enter id,  Name, , Phone, Longitude, Latitude");
                      Id = int.Parse(Console.ReadLine());
                      Name = Console.ReadLine();
                      string Phone = Console.ReadLine();
                      Longitude = double.Parse(Console.ReadLine());
                      Latitude = double.Parse(Console.ReadLine());
                      Customer c=new Customer(Id, Name, Phone, Longitude, Latitude);
                      dd.addCustomer(c);
                      break;

                  case "4":
                      Console.WriteLine("enter id,  SenderId,  TargetId, Weight, priority,Requested,scheduled,PickedUp,Delivered,DroneId");
                      Id = int.Parse(Console.ReadLine());
                      int SenderId = int.Parse(Console.ReadLine());
                      int TargetId = int.Parse(Console.ReadLine());
                      WeightCategories Weight = WeightCategories.Parse(Console.ReadLine());
                      Priorities priority = Priorities.Parse(Console.ReadLine());
                      DateTime Requested = DateTime.Parse( Console.ReadLine());
                      DateTime scheduled = DateTime.Parse(Console.ReadLine());
                      DateTime PickedUp = DateTime.Parse(Console.ReadLine());
                      DateTime Delivered = DateTime.Parse(Console.ReadLine());
                      int DroneId = int.Parse(Console.ReadLine());
                      Parcel p=new Parcel(Id, SenderId, TargetId,Weight, priority,Requested,
                         scheduled, PickedUp, Delivered,DroneId);
                      dd.addParcel(p);
                      break;

              }
        }
        static void update()
        {
            Console.WriteLine("ENTER YOUR CHOISE:/n 1-stationToDrone /n 2-pickParcelToDrone /n 3-parcelToCustomer /n 4-SendDroneToStation /n");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    {
                        Console.WriteLine("enter idDrone & idParcel/n");
                        int idDrone = int.Parse(Console.ReadLine());
                        int idParcel = int.Parse(Console.ReadLine());
                        dd.updateParcelToDrone(idDrone,idParcel);
                   }
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
            }
        }
        static void show()
        {
            Console.WriteLine("ENTER YOUR CHOISE:/n 1-station /n 2-to drone /n 3-customer /n 4-parcel /n");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    Console.WriteLine("enter id");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write(dd.printStation(id));
                    break;
                case "2":
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine());
                    Console.Write(dd.printDrone(id));
                    break;
                case "3":
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine());
                    Console.Write(dd.printCustomer(id));
                    break;
                case "4":
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine());
                    Console.Write(dd.printParcel(id));
                    break;
            }

        }
        static void showList()
        {
            Console.WriteLine("ENTER YOUR CHOISE:/n 1-station /n 2-to drone /n 3-customer /n 4-parcel /n");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    {
                        IEnumerable f1 = dd.viewStation();
                        foreach (var item in f1)
                        {
                            Console.WriteLine(item);
                        }
                    }
                        break;
                    case "2":
                    {
                        IEnumerable f2 = dd.viewDrone();
                        foreach (var item in f2)
                        {
                            Console.WriteLine(item);
                        }
                    }
                        break;
                    case "3":
                    {
                        IEnumerable f3 = dd.viewCustomer();
                        foreach (var item in f3)
                        {
                            Console.WriteLine(item);
                        }
                    }
                        break;
                    case "4":
                    {
                        IEnumerable f4 = dd.viewParcel();
                        foreach (var item in f4)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                }
            }
        
            static void Main(string[] args)
{

    Console.WriteLine("WELCOME TO OUR SYSTEM");
    Console.WriteLine("ENTER YOUR CHOISE:/n 1-TO ADD /n 2-to update /n 3-to show /n 4-to show list /n 5-exit /n");
    string choise = Console.ReadLine();
    switch (choise)
    {
        case "1":
            add();
            break;
        case "2":
            update();
            break;
        case "3":
            show();
            break;
        case "4":
            showList();
            break;
        case "5":
            Console.WriteLine("good bye");
            break;



    }



}
        } 
    }

