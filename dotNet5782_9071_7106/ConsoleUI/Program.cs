using System;
using DalObject;
using IDAL.DO;
namespace ConsoleUI
{
    class Program
    {
      static DalObject.DalObject dd;
        


            
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
                    //    Station s = new Station(id);
                    //  s.printStation();
                    Console.Write( dd.printStation(id).ToString());
                    break;
                 case "2":
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine());
                   /* Drone d(int(Console.ReadLine()));
                    d.printDrone();*/
                    dd.printDrone(id);
                    break;
                case "3":
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine());
                    /*Customer c(int(Console.ReadLine()));
                    c.printCustomer();*/
                    dd.printCustomer(id);
                    break;
                case "4":
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine());
                    /*Parcel p(int(Console.ReadLine()));
                    p.printParcel();*/
                    dd.printParcel(id);
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
                        dd.viewStation();
                        break;
                    case "2":
                        dd.viewDrone();
                        break;
                    case "3":
                        dd.viewCustomer();
                        break;
                    case "4":
                        dd.viewParcel();
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

