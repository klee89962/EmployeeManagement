﻿using System;
using System.Collections.Generic;

namespace HelpdeskDAL
{
    // Department class with its attributes
    public partial class Departments : HelpdeskEntity
    {
        public Departments()
        {
            Employees = new HashSet<Employees>();
        }

        public string DepartmentName { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
