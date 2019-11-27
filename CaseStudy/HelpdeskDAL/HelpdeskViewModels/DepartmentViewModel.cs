using HelpdeskDAL;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
namespace HelpdeskViewModels
{
    public class DepartmentViewModel
    {

        private DepartmentModel _model;

        public int Id { get; set; }

        public string DepartmentName { get; set; }

        // Constructor
        public DepartmentViewModel()
        {
            _model = new DepartmentModel();
        }

        // Gets all the departments that matches the existing departmentId
        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> allVms = new List<DepartmentViewModel>();
            try
            {
                // Get the Departments
                List<Departments> allDepts = _model.GetAll();

                // For each elements in allDepts, add them to alllVms
                foreach (Departments dept in allDepts)
                {
                    DepartmentViewModel vm = new DepartmentViewModel();
                    vm.Id = dept.Id;
                    vm.DepartmentName = dept.DepartmentName;

                    allVms.Add(vm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            } // try/catch

            return allVms;
        }
    }
}
