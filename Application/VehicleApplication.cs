using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AltalizaApi.Models;

namespace AltalizaApi.Application
{
    public class VehicleApplication
    {
        private ApiContext _context;

        public VehicleApplication(ApiContext context)
        {
            _context = context;
        }

        public string InsertVehicle(Vehicle vehicle)
        {
            try {
                if (vehicle != null) {
                    _context.Add(vehicle);
                    _context.SaveChanges();
                    return "Vehicle added successfully";
                } else {
                    return "Vehicle null";
                }
            } catch (Exception) {
                return "Error on adding Vehicle";
            }
        }

        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> lista = new List<Vehicle>();
            try
            {
                lista = _context.Vehicle.Where(x => x.Estoque > 0).ToList();
                return lista;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
