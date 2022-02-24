using System;
using BO;
using System.Threading;
using BlApi;

namespace BL
{

    class Simulator
    {
        //static System.Windows.Timer myTimer = new System.Windows.Timer();
        const int DELAY = 5000;// השהיה לחצי שניה
        public double speed;//מהירות
        private int idDrone;
        private Action a;
        private Func<bool> stop;



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
                                
                                catch(BO.NotImplementedException)
                                {
                                    bl.SendDroneToStation(idDrone);///שליחת רחפן לטעינה
                                }
                                catch
                                {
                                    throw new SimulatorException("No suitable package was found or the glider does not have a sufficient battery");
                                }
                            }
                            bl.CollectionAParcelByDroen(idDrone);//אוסף חבילה
                            break;
                        case DroneStatuses.Maintenance:
                            a();
                            break;
                        case DroneStatuses.Shipping:
                            bl.DeliveryOfAParcelByDrone(idDrone);//מבצע משלוח
                            break;
                        default:
                            break;
                    }}
                    Thread.Sleep(DELAY);
                    
                   
                

            }


        }


    }
}

