using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLeasesDB.Models;

namespace EasyLeasesDB.App_Code
{
    public interface IPropertyRepository
    {
        List<Properties> GetAllProperties();
        
        //if class does not have a foreign key you only include this part 
        Properties GetProperty(long id);
        Properties CreateProperty(Properties property);
        void UpdateProperty(long id, Properties updatedProperty);
        void DeleteProperty(long id);
        //the end of non-foreign key classes
    }
}
