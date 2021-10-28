using System;

namespace ConsoleUI
{
    class Program
    {
        static void show()
        {
            Console.WriteLine("ENTER YOUR CHOISE:/n 1-station /n 2-to drone /n 3-customer /n 4-parcel /n");
            string choise = Console.ReadLine();
            switch (choise) {
                case "1":
                    Console.WriteLine("enter id");
                    Statin s(int(Console.ReadLine()));
                    s.printStation();
                    break;


                case "2":
                    Console.WriteLine("enter id");
                    Drone d(int(Console.ReadLine()));
                    d.printDrone();
                    break;
                case "3":
                    Console.WriteLine("enter id");
                    Customer c(int(Console.ReadLine()));
                    c.printCustomer();
                    break;
                case "4":
                    Console.WriteLine("enter id");
                    Parcel p(int(Console.ReadLine()));
                    p.printParcel();
                    break;
            }

        }


            static void update()
            {

            }
            static void add()
            {
                Console.WriteLine("ENTER YOUR CHOISE:/n 1-station /n 2-to drone /n 3-customer /n 4-parcel /n");
                string choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":

                        Console.WriteLine("enter id,  Name, , Latitude, ChargeSlot");
                        string Id = Console.ReadLine();
                        string Name = Console.ReadLine();
                        string Longitude = Console.ReadLine();
                        string Latitude = Console.ReadLine();
                        string ChargeSlot== Console.ReadLine();
                        Station s(int(Id), Name, double(Longitude), double(Latitude), int(ChargeSlot));
                        addStation(s);
                        break;
                    case "2":
                        Console.WriteLine("enter id,  Model, , MaxWeight, Status,Battery");
                        string Id = Console.ReadLine();
                        string Model = Console.ReadLine();
                        string MaxWeight = Console.ReadLine();
                        string Status = Console.ReadLine();
                        string Battery== Console.ReadLine();
                        Drone d(int(Id), Name, WeightCategories(MaxWeight), DroneStatuses(Status), double(Battery));
                        addDrone(d);
                        break;
                    case "3":
                        Console.WriteLine("enter id,  Name, , Phone, Longitude, Latitude");
                        string Id = Console.ReadLine();
                        string Name = Console.ReadLine();
                        string Phone = Console.ReadLine();
                        string Longitude = Console.ReadLine();
                        string Latitude== Console.ReadLine();
                        Customer c(int(Id), Name, WeightCategories(MaxWeight), DroneStatuses(Status), double(Battery));
                        addDrone(c);
                        break;

                    case "4":
                        Console.WriteLine("enter id,  SenderId,  TargetId, Weight, priority,Requested,scheduled,PickedUp,Delivered,DroneId");
                        string Id = Console.ReadLine();
                        string SenderId = Console.ReadLine();
                        string TargetId = Console.ReadLine();
                        string Weight = Console.ReadLine();
                        string priority = Console.ReadLine();
                        string Requested = Console.ReadLine();
                        string scheduled = Console.ReadLine();
                        string PickedUp = Console.ReadLine();
                        string Delivered = Console.ReadLine();
                        string DroneId = Console.ReadLine();
                        Customer c(int(Id), int(SenderId), int(TargetId), WeightCategories(Weight), Priorities(priority), DateTime(Requested),
                           DateTime(scheduled), DateTime(PickedUp), DateTime(Delivered), int(DroneId));
                        addDrone(c);
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
                        viewStation();
                        break;
                    case "2":
                        viewDrone();
                        break;
                    case "3":
                        viewCustomer();
                        break;
                    case "4":
                        viewParcel();
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

