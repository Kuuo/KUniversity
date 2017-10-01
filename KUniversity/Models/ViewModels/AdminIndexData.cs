using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUniversity.Models.ViewModels
{
    public class AdminIndexData
    {
        public IEnumerable<Campus> Campuses { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}
