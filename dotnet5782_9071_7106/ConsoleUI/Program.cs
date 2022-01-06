using System;

using System.Collections;
using IDAL.DO;
namespace ConsoleUI
{
    class Program
    {
        static DalObject.DalObject dd = new();




        static void add()
        {
            Console.WriteLine("ENTER YOUR CHOISE:\n 1-station \n 2-to drone \n 3-customer \n 4-parcel \n");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":

                    Console.WriteLine("enter id");
                    int Id = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter Name");
                    string Name = Console.ReadLine();
                    Console.WriteLine("enter Longitude");
                    double Longitude = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter Latitude");
                    double Latitude = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter ChargeSlot");
                    int ChargeSlot = int.Parse(Console.ReadLine());
                    Station s = new Station(Id, Name, Longitude, Latitude, ChargeSlot);
                    try
                    {
                        dd.AddStation(s);
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
                case "2":
                    Console.WriteLine("enter Id");
                    Id = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter Model");
                    string Model = Console.ReadLine();
                    Console.WriteLine("enter MaxWeight: 1-Easy \n 2-Intermediate \n 3-Liver");
                    string choiseMaxWeight = Console.ReadLine();
                    WeightCategories MaxWeight = WeightCategories.Easy;
                    switch (choiseMaxWeight)
                    {
                        case "1":
                            MaxWeight = WeightCategories.Easy;
                            break;
                        case "2":
                            MaxWeight = WeightCategories.Intermediate;
                            break;
                        case "3":
                            MaxWeight = WeightCategories.Liver;
                            break;
                    }
                    Console.WriteLine("enter Status: 1-Vacant \n 2-Maintenance \n 3-Shipping");
                    string choiseDroneStatuses = Console.ReadLine();
                    Console.WriteLine("enter Battery");
                    double Battery = double.Parse(Console.ReadLine());
                    Drone d = new Drone(Id, Model, MaxWeight);
                    try
                    {
                        dd.AddDrone(d);
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }


                    break;
                case "3":
                    Console.WriteLine("enter id");
                    Id = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter Name");
                    Name = Console.ReadLine();
                    Console.WriteLine("enter Phone");
                    string Phone = Console.ReadLine();
                    Console.WriteLine("enter Longitude");
                    Longitude = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter Latitude");
                    Latitude = double.Parse(Console.ReadLine());
                    Customer c = new Customer(Id, Name, Phone, Longitude, Latitude);
                    try
                    {
                        dd.AddCustomer(c);
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;

                case "4":
                    Console.WriteLine("enter id");
                    Id = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter SenderId");
                    int SenderId = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter TargetId");
                    int TargetId = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter weight: 1-Easy \n 2-Intermediate \n 3-Liver");
                    string choiseWeight = Console.ReadLine();
                    WeightCategories Weight = WeightCategories.Easy;
                    switch (choiseWeight)
                    {
                        case "1":
                            Weight = WeightCategories.Easy;
                            break;
                        case "2":
                            Weight = WeightCategories.Intermediate;
                            break;
                        case "3":
                            Weight = WeightCategories.Liver;
                            break;
                    }
                    Console.WriteLine("enter priority: 1-Regular \n 2-Fast \n 3-Emergency");
                    string choisePriority = Console.ReadLine();
                    Priorities priority = Priorities.Regular;
                    switch (choisePriority)
                    {
                        case "1":
                            priority = Priorities.Regular;
                            break;
                        case "2":
                            priority = Priorities.Fast;
                            break;
                        case "3":
                            priority = Priorities.Emergency;
                            break;
                    }
                    Console.WriteLine("enter Requested");
                    DateTime Requested = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("enter scheduled");
                    DateTime scheduled = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("enter PickedUp");
                    DateTime PickedUp = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("enter Delivered");
                    DateTime Delivered = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("enter DroneId");//הדברים לא מסודרים ע''פ הסדר שבהנחיות
                    int DroneId = int.Parse(Console.ReadLine());
                    Parcel p = new Parcel(SenderId, TargetId, Weight, priority, Requested,
                       scheduled, PickedUp, Delivered, DroneId);
                    dd.AddParcel(p);
                    break;

            }
        }
        static void update()
        {
            Console.WriteLine("ENTER YOUR CHOISE:\n 1-stationToDrone \n 2-pickParcelToDrone \n 3-parcelToCustomer \n 4-SendDroneToStation \n 5-FreeDrone");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":

                    Console.WriteLine("enter idDrone");
                    int idDrone1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter idParcel");
                    int idParcel1 = int.Parse(Console.ReadLine());
                      try
                    {
                         dd.UpdateParcelToDrone(idDrone1, idParcel1);
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
                case "2":

                    Console.WriteLine("enter idDrone");
                    int idDrone = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter DateTime");
                    DateTime pickUpDate = DateTime.Parse(Console.ReadLine());
                      try
                    {
                         dd.pickedUpD(idDrone, pickUpDate);
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    

                    break;
                case "3":
                    Console.WriteLine("enter idCustomer");
                    int idCustomer = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter idParcel");
                    int idParcel = int.Parse(Console.ReadLine());
                       try
                    {
                        dd.targetId(idCustomer, idParcel);
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                    break;
                case "4":
                    Console.WriteLine("enter idDrone");
                    idDrone1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter idStation");
                    int idStation = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter DroneStatuses: 1-Vacant \n 2-Maintenance \n 3-Shipping");
                    string choiseDroneStatuses = Console.ReadLine();
                    dd.SendDroneToStation(idDrone1, idStation);
                    break;

                case "5":
                    Console.WriteLine("enter idDrone");
                    int idDrone2 = int.Parse(Console.ReadLine());
                    dd.FreeDrone(idDrone2);
                    break;

            }
        }
        static void show()
        {
            Console.WriteLine("ENTER YOUR CHOISE:\n 1-station \n 2-to drone \n 3-customer \n 4-parcel \n");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    Console.WriteLine("enter id");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.Write(dd.GetStation(id));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case "2":
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.Write(dd.GetDrone(id));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case "3":
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.Write(dd.GetCustomer(id));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
                case "4":
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.Write(dd.GetParcel(id));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
            }

        }
        static void showList()
        {
            Console.WriteLine("ENTER YOUR CHOISE:\n 1-station \n 2-to drone \n 3-customer \n 4-parcel \n 5-parcel without drone/ 6-station without charge");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    {
                        IEnumerable f1 = dd.GetListStation();//לשנות אח''כ
                        foreach (var item in f1)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case "2":
                    {
                        IEnumerable f2 = dd.GetListDrone();
                        foreach (var item in f2)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case "3":
                    {
                        IEnumerable f3 = dd.GetListCustomer();
                        foreach (var item in f3)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case "4":
                    {
                        IEnumerable f4 = dd.GetListParcel();
                        foreach (var item in f4)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case "5":
                    {
                        IEnumerable f5 = dd.ParcelNoDrone();
                        foreach (var item in f5)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;

                case "6":
                    {
                        IEnumerable f5 = dd.StationNoCharge();
                        foreach (var item in f5)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;

            }

            static void Main(string[] args)
            {

                Console.WriteLine("WELCOME TO OUR SYSTEM");
                Console.WriteLine("ENTER YOUR CHOISE:\n 1-TO ADD \n 2-to update \n 3-to show \n 4-to show list \n 5-exit \n");
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
}
