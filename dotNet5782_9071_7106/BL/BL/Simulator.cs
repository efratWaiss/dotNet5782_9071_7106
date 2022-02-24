using System;
using BO;
using System.Threading;
using BlApi;

namespace BL
{

    class Simulator
    {
        //static System.Windows.Timer myTimer = new System.Windows.Timer();
        const int DELAY = 500;// השהיה לחצי שניה
        public double speed;//מהירות
        private int idDrone;
        private Action a;
        private Func<bool> stop;


        //סימולטור אפרת וייס & אילת ויץ
        public Simulator(IBL bl, int idDrone, Action a, Func<bool> stop)
        {
            //double battery;
            while (!stop())
            {
                lock (bl)
                {
                    Drone drone = bl.GetDrone(idDrone);
                    switch (drone.Status)
                    {
                        case DroneStatuses.Vacant:
                            {
                                try
                                {
                                    bl.UpdateParcelToDrone(idDrone);
                                }

                                catch (BO.NotImplementedException)
                                {
                                   
                                    bl.SendDroneToStation(idDrone);///שליחת רחפן לטעינה

                                }
                                catch
                                {
                                    throw new SimulatorException("No suitable package was found or the glider does not have a sufficient battery");
                                }
                            }
                            try
                            {
                                bl.CollectionAParcelByDroen(idDrone);//אוסף חבילה
                            }
                            catch (BO.NotImplementedException)
                           
                            {
                                bl.UpdateParcelToDrone(idDrone);
                            }

                            break;
                        case DroneStatuses.Maintenance:
                            Random rn = new Random();
                            bl.FreeDrone(idDrone, rn.Next(1, 35));
                            //a();
                            break;
                        case DroneStatuses.Shipping:
                            try
                            {
                                bl.DeliveryOfAParcelByDrone(idDrone);//מבצע משלוח
                            }
                            catch (BO.NotImplementedException)
                            {
                                bl.CollectionAParcelByDroen(idDrone);
                            }
                            break;
                        default:
                            break;
                    }
                }
                Thread.Sleep(DELAY);
            }
        }
    }
}

