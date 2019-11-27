using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace HelpdeskDAL
{
    public class EmployeeModel
    {
        IRepository<Employees> repository;

        public EmployeeModel()
        {
            repository = new HelpdeskRepository<Employees>();
        }
        // Returns Employee with the matching email
        public Employees GetByEmail(string email)
        {
            // Declare employee list to store result of query
            List<Employees> selectedEmployees = null;

            try
            {
                selectedEmployees = repository.GetByExpression(stu => stu.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            } // try/catch
            // return first of the results
            return selectedEmployees.FirstOrDefault();
        }
        // Return employee with matching id
        public Employees GetById(int id)
        {
            // Decalre employee to store the result
            Employees selectedEmployee = null;

            try
            {
                // Declare HelpdeskContext object for query call
                HelpdeskContext _db = new HelpdeskContext();
                selectedEmployee = _db.Employees.FirstOrDefault(emp => emp.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            } // try/catch

            // return the employee
            return selectedEmployee;
        }
        // Return lists of employees
        public List<Employees> GetAll()
        {
            // declare lists of employees
            List<Employees> allEmployees = new List<Employees>();

            try
            {
                // Declare HelpdeskContext object for function call
                HelpdeskContext _db = new HelpdeskContext();
                allEmployees = _db.Employees.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            } // try/catch

            // Return lists of employees
            return allEmployees;
        }
        // Returns the Id of added employee
        public int Add(Employees newEmployee)
        {
            try
            {
                // Declare HelpdeskContext object for function call
                HelpdeskContext _db = new HelpdeskContext();
                _db.Employees.Add(newEmployee);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            } // try/catch

            // Returns Id
            return newEmployee.Id;
        }
        // Returns Enum value after the update of employee information
        public UpdateStatus Update(Employees updatedEmployee)
        {
            // Decalre UpdateStatus value
            UpdateStatus operationStatus = UpdateStatus.Failed;
            try
            {
                operationStatus = repository.Update(updatedEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            } // try/catch

            // Returns status
            return operationStatus;
        }
        // Return number of deleted employees, -1 if fail
        public int Delete(int id)
        {
            int employeesDeleted = -1;
            try
            {
                HelpdeskContext _db = new HelpdeskContext();
                Employees selectedEmployee = _db.Employees.FirstOrDefault(emp => emp.Id == id);
                _db.Employees.Remove(selectedEmployee);
                employeesDeleted = _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            } // try/catch

            return employeesDeleted;
        }
    }
}
