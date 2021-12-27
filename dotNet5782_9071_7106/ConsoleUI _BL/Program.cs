﻿using IBL.BO;
using System;
using System.Collections;

namespace ConsoleUI__BL
{
    class Program
    {

        static IBL.BL bl = new();
        static void add()
        {
            Console.WriteLine("ENTER YOUR CHOISE:/n 1-station /n 2-to drone /n 3-customer /n 4-parcel /n");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":

                    Console.WriteLine("enter id");
                    int Id = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter Name");
                    string Name = Console.ReadLine();
                    Console.WriteLine("enter Location");
                    double Longitude = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter Latitude");
                    double Latitude = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter ChargeSlot");
                    int ChargeSlot = int.Parse(Console.ReadLine());
                    Location location = new Location(Latitude, Latitude);

                    try
                    {
                        bl.addStation(Id, Name, location, ChargeSlot);
                    }

                    catch (IBL.BO.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
                case "2":
                    Console.WriteLine("enter Id");
                    Id = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter Model");
                    string Model = Console.ReadLine();
                    Console.WriteLine("enter MaxWeight: 1-Easy /n 2-Intermediate /n 3-Liver");
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
                    Console.WriteLine("enter Id station");
                    int idStation = int.Parse(Console.ReadLine());
                    try
                    {
                        bl.addDrone(Id, Model, MaxWeight, idStation);
                    }

                    catch (IBL.BO.Exception e)/// לבדוק!!!
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
                    try
                    {
                        bl.addCustomer(Id, Name, Phone, Longitude, Latitude);
                    }

                    catch (IBL.BO.Exception e)
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
                    Console.WriteLine("enter weight: 1-Easy /n 2-Intermediate /n 3-Liver");
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
                    Console.WriteLine("enter priority: 1-Regular /n 2-Fast /n 3-Emergency");
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
                    try
                    {
                        bl.addParcel(SenderId, TargetId, Weight, priority);
                    }
                    catch (IBL.BO.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

            }
        }
        static void update()
        {
            Console.WriteLine("ENTER YOUR CHOISE:/n 1-update name Drone /n 2-Update Station Detail /n 3-drone to  charging /n 4-Send Drone To Station /n 5-free Drone /n 6-update parcel to drone");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":

                    Console.WriteLine("enter idDrone");
                    int idDrone1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter Model");
                    String Model = Console.ReadLine();
                    try
                    {
                        bl.UpdateNameDrone(idDrone1, Model);
                    }

                    catch (IBL.BO.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
                case "2":

                    Console.WriteLine("enter idStation");
                    int IdDrone = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter Name Station");
                    String Name = Console.ReadLine();
                    Console.WriteLine("enter available chargeSlots");
                    int ChargeSlots = int.Parse(Console.ReadLine());
                    try
                    {
                        bl.UpdateStationDetails(IdDrone, Name, ChargeSlots);
                    }

                    catch (IBL.BO.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }



                    break;
                case "3":
                    Console.WriteLine("enter idCustomer");
                    int idCustomer = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter Name");
                    String name = Console.ReadLine();
                    Console.WriteLine("enter Phone");
                    String phone = Console.ReadLine();
                    try
                    {
                        bl.UpdateCustomerDetails(idCustomer, name, phone);
                    }

                    catch (IBL.BO.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
                case "4":
                    Console.WriteLine("enter idDrone");
                    idDrone1 = int.Parse(Console.ReadLine());
                    try
                    {
                        bl.sendDroneToStation(idDrone1);
                    }
                    catch (IBL.BO.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;

                case "5":
                    Console.WriteLine("enter id Drone");
                    int idDrone2 = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter time in charging");
                    double time = double.Parse(Console.ReadLine());
                    try
                    {
                        bl.freeDrone(idDrone2, time);
                    }
                    catch (IBL.BO.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
                case "6":
                    Console.WriteLine("enter id Drone");
                    int idDrone = int.Parse(Console.ReadLine());
                    try
                    {
                        bl.updateParcelToDrone(idDrone);
                    }
                    catch (IBL.BO.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
            }
        }
        static void getDetails()
        {
            Console.WriteLine("ENTER YOUR CHOISE:/n 1-station /n 2-to drone /n 3-customer /n 4-parcel /n");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    Console.WriteLine("enter id station");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.Write(bl.printStation(id));
                    }
                    catch (IBL.BO.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case "2":
                    Console.WriteLine("enter id drone");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.Write(bl.printDrone(id));
                    }
                    catch (IBL.BO.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case "3":
                    Console.WriteLine("enter id customer");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.Write(bl.printCustomer(id));
                    }
                    catch (IBL.BO.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
                case "4":
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.Write(bl.printParcel(id));
                    }
                    catch (IBL.BO.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
            }

        }
        static void getList()
        {
            Console.WriteLine("ENTER YOUR CHOISE:/n 1-station /n 2-to drone /n 3-customer /n 4-parcel /n 5-parcel without drone/ 6-station without charge");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    {
                        IEnumerable f1 = bl.viewStation();//לשנות אח''כ
                        foreach (var item in f1)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case "2":
                    {
                        IEnumerable f2 = bl.viewDrone();
                        foreach (var item in f2)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case "3":
                    {
                        IEnumerable f3 = bl.viewCustomer();
                        foreach (var item in f3)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case "4":
                    {
                        IEnumerable f4 = bl.viewParcel();
                        foreach (var item in f4)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case "5":
                    {
                        IEnumerable f5 = bl.ParcelNoDrone();
                        foreach (var item in f5)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;

                case "6":
                    {
                        IEnumerable f5 = bl.StationNoCharge();
                        foreach (var item in f5)
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
                    getDetails();
                    break;
                case "4":
                    getList();
                    break;
                case "5":
                    Console.WriteLine("good bye");
                    break;



            }
        }
    }
}