using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDal
{
    public interface IDal
    {

        public void addStation(Station s);
        public void addDrone(Drone d);
        public void addCustomer(Customer c);

        public int addParcel(Parcel p);
        public Station printStation(int id);
        public Drone printDrone(int id);
        public Customer printCustomer(int id);
        public Parcel printParcel(int id);
        public DroneCharge printDroneCharge(int idDrone);
        public IEnumerable<DroneCharge> GetDroneCharges();
        public IEnumerable<Station> GetListStation();
        public IEnumerable<Drone> getDrone();
        public IEnumerable<Customer> getCustomer();
        public IEnumerable<Parcel> getParcel();
        public IEnumerable<Parcel> ParcelNoDrone();
        public IEnumerable<Station> StationNoCharge();
        public void updateParcelToDrone(int idDrone, int idParcel);
        public void pickedUpD(int idDrone, DateTime d);
        public void targetId(int idCustomer, int idParcel);
        public void sendDroneToStation(int idDrone, int idStation);
        public void freeDrone(int idDrone);
        public double[] powerConsumpitionByDrone();
        public void UpdateParcel(Parcel p);
        public void removeFromDroneCharges(int idDrone, int idStation);
    }
}
