using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CRUD.Db.Context;
using CRUD.Models;

namespace CRUD.Db.Data
{
    public class EmployeeData
    {
        CrudContext context = new CrudContext();

        public EmployeeData()
        {
            
        }

        public EmployeeData(CrudContext context)
        {
            
        }

        public Employee Get(long id)
        {
            Employee employee = context.Employees.Single(x => x.Id == id);
            return employee;
        }

        public List<Employee> GetAll()
        {
            var employees = context.Employees.ToList();
            return employees;
        }

        public string Create(Employee employee)
        {
            try
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Update(Employee employee)
        {
            try
            {
                Employee emp = context.Employees.Single(e => e.Id == employee.Id);
                emp.Name = employee.Name;
                emp.Address = employee.Address;
                emp.Email = employee.Email;
                emp.Contact = employee.Contact;
                context.SaveChanges();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Delete(Employee employee)
        {
            try
            {
                Employee emp = context.Employees.Single(e => e.Id == employee.Id);
                context.Employees.Remove(emp);
                context.SaveChanges();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}