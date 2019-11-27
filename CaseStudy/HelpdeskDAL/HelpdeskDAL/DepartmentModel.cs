using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HelpdeskDAL
{
    public class DepartmentModel
    {
        IRepository<Departments> repo;

        public DepartmentModel()
        {
            repo = new HelpdeskRepository<Departments>();
        }
        // Returns the list of Departments being passed to controller
        public List<Departments> GetAll()
        {
            try
            {
                // Call GetAll()
                return repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex; //bubble up
            } // try/catch
        }

    }
}
