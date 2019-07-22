using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLeasesDB.Models;

namespace EasyLeasesDB.App_Code
{
    public class PropertiesRepository : IPropertyRepository
    {
        private readonly EasyLeasesDBContext _db;

        public PropertiesRepository(EasyLeasesDBContext db)
        {
            _db = db;
        }

        private bool PropertyExists(long propID)
        {
            return _db.Properties.Any(p => p.PropertiesId == propID);
        }

        public Properties CreateProperty(Properties property)
        {
            _db.Properties.Add(property);
            _db.SaveChanges();

            return property;
        }

        public void DeleteProperty(long id)
        {
            Properties property = GetProperty(id);
            if(property != null)
            {
                _db.Properties.Remove(property);
                _db.SaveChanges();
            }
        }

        public List<Properties> GetAllProperties()
        {
            return _db.Properties.ToList();
        }

        public Properties GetPropertyAddress(String propAddress)
        {
            return _db.Properties.FirstOrDefault(p => p.PropertyAddress == propAddress);
        }

        public Properties GetProperty(long id)
        {
            return _db.Properties.FirstOrDefault(p => p.PropertiesId == id);
        }

        public void UpdateProperty(long id, Properties updatedProperty)
        {
            Properties property = GetProperty(id);
            if (property == null)
                throw new Exception("Property Not Found!");

            //update all fields within the property entity
            property.AvailableDate = updatedProperty.AvailableDate;
            property.PropertyRentAmount = updatedProperty.PropertyRentAmount;
            property.PropertyAddress = updatedProperty.PropertyAddress;
            property.PropertyCity = updatedProperty.PropertyCity;
            property.PropertyState = updatedProperty.PropertyState;
            property.PropertyZip = updatedProperty.PropertyZip;
            property.PropertyFloors = updatedProperty.PropertyFloors;
            property.PropertyBeds = updatedProperty.PropertyBeds;
            property.PropertyBaths = updatedProperty.PropertyBaths;
            property.PropertyLivingSqFt = updatedProperty.PropertyLivingSqFt;
            property.IsLeased = updatedProperty.IsLeased;

            _db.SaveChanges();
        }
    }
}
