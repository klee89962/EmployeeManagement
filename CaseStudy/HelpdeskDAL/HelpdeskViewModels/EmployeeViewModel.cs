using System;
using HelpdeskDAL;
using System.Reflection;
using System.Collections.Generic;

namespace HelpdeskViewModels
{
    public class EmployeeViewModel 
    {
        // Employee View Model Attributes
        private EmployeeModel _model;

        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public string Timer { get; set; }
        public int DepartmentId { get; set; }
        public int Id { get; set; }
        public bool IsTech { get; set; }
        public string StaffPicture64 { get; set; }

        public EmployeeViewModel()
        {
            _model = new EmployeeModel();
        }

        // Get Employee by Email
        public void GetByEmail()
        {
            try
            {
                // Declare employee and get all the information
                Employees emp = _model.GetByEmail(Email);
                Title = emp.Title;
                Firstname = emp.FirstName;
                Lastname = emp.LastName;
                Phoneno = emp.PhoneNo;
                Email = emp.Email;
                Id = emp.Id;
                DepartmentId = emp.DepartmentId;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException nex)
            {
                Lastname = "not found";
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            } // try/catch
        }
        // Get Employee by Id
        public void GetById()
        {
            try
            {
                // Declare employee and get all the information
                Employees emp = _model.GetById(Id);
                Title = emp.Title;
                Firstname = emp.FirstName;
                Lastname = emp.LastName;
                Phoneno = emp.PhoneNo;
                Email = emp.Email;
                Id = emp.Id;
                DepartmentId = emp.DepartmentId;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException nex)
            {
                Lastname = "not found";
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            } // try/catch
        }
        // Get list of EmployeeViewModel
        public List<EmployeeViewModel> GetAll()
        {
            // Declare List of EmployeeViewModel to store the elements
            List<EmployeeViewModel> allVms = new List<EmployeeViewModel>();
            try
            {
                // Get the list
                List<Employees> allEmployees = _model.GetAll();
                // Store every values from allEmployees to allVms
                foreach (Employees emp in allEmployees)
                {
                    EmployeeViewModel empVm = new EmployeeViewModel();
                    empVm.Title = emp.Title;
                    empVm.Firstname = emp.FirstName;
                    empVm.Lastname = emp.LastName;
                    empVm.Phoneno = emp.PhoneNo;
                    empVm.Email = emp.Email;
                    empVm.Id = emp.Id;
                    empVm.DepartmentId = emp.DepartmentId;
                    empVm.Timer = Timer = Convert.ToBase64String(emp.Timer);
                    allVms.Add(empVm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }// try/catch

            // Return List
            return allVms;
        }
        // Add an employee to database by called Add of Employee Model
        public void Add()
        {
            Id = -1;
            try
            {
                Employees emp = new Employees();
                emp.Title = Title;
                emp.FirstName = Firstname;
                emp.LastName = Lastname;
                emp.PhoneNo = Phoneno;
                emp.Email = Email;
                emp.DepartmentId = DepartmentId;
                Id = _model.Add(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            } // try/catch
        }
        // Returns Enum value after the update of template
        public int Update()
        {
            // Decalre UpdateStatus value
            UpdateStatus employeesUpdated = UpdateStatus.Failed;
            try
            {
                // Assign values to employee object and update it
                Employees emp = new Employees();
                emp.Title = Title;
                emp.FirstName = Firstname;
                emp.LastName = Lastname;
                emp.PhoneNo = Phoneno;
                emp.Email = Email;
                emp.Id = Id;
                emp.DepartmentId = DepartmentId;
                if (StaffPicture64 != null)
                {
                    emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }
                emp.Timer = Convert.FromBase64String(Timer);
                employeesUpdated = _model.Update(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            } // try/catch

            // Return update status
            return Convert.ToInt16(employeesUpdated);
        }
        public int Delete()
        {
            int employeesDeleted = -1;

            try
            {
                // call delete that matches the id
                employeesDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            } // try/catch
            // return delete status
            return employeesDeleted;
        }
    }
}
