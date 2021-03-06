using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class EmployeeDAO : EmployeeContext
    {
        public static void AddEmployee(EMPLOYEE employee)
        {
            try
            {
                db.EMPLOYEE.InsertOnSubmit(employee);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<EmployeeDetailDTO> GetEmployees()
        {
            List<EmployeeDetailDTO> employeeList = new List<EmployeeDetailDTO>();
            var list = (from e in db.EMPLOYEE
                        join d in db.DEPARTMENT on e.DepartmentID equals d.ID
                        join p in db.POSITION on e.PositionID equals p.ID
                        select new
                        {
                            UserNo=e.UserNo,
                            Name=e.Name,
                            Surname=e.Surname,
                            EmployeeID=e.ID,
                            Password=e.Password,
                            DepartmentName=d.DepartmentName,
                            PositionName=p.PositionName,
                            DepartmentID=e.DepartmentID,
                            PositionID=e.PositionID,
                            isAdmin=e.isAdmin,
                            Salary=e.Salary,
                            ImagePath=e.ImagePath,
                            birthDay=e.BirthDay,
                            Adress=e.Adress,
                            Email = e.Email,
                            PhoneNumber = e.PhoneNumber,
                            Admission = e.Admission

                        }).OrderBy(x => x.UserNo).ToList();
                foreach(var item in list)
            {
                EmployeeDetailDTO dto = new EmployeeDetailDTO
                {
                    UserNo = item.UserNo,
                    Name = item.Name,
                    Surname = item.Surname,
                    Adress = item.Adress,
                    Salary = item.Salary,
                    ImagePath = item.ImagePath,
                    EmployeeID = item.EmployeeID,
                    PositionName = item.PositionName,
                    DepartmentName = item.DepartmentName,
                    DepartmentID = item.DepartmentID,
                    PositionID = item.PositionID,
                    Password = item.Password,
                    isAdmin = item.isAdmin,
                    BirthDay = item.birthDay,
                    Email = item.Email,
                    PhoneNumber=item.PhoneNumber,
                    Admission=item.Admission
                };
                employeeList.Add(dto);

            }
            return employeeList;
        }

        public static void DeleteEmployee(int employeeID)
        {
            try
            {
                EMPLOYEE emp = db.EMPLOYEE.First(x => x.ID == employeeID);
                db.EMPLOYEE.DeleteOnSubmit(emp);
                db.SubmitChanges();

                //USANDO TRIGGERS EN SQL NO ES NECESARIO LO DE ABAJO.



                // List<TASK> tasks = db.TASK.Where(x => x.EmployeeID == employeeID).ToList();
               // db.TASK.DeleteAllOnSubmit(tasks);
                //db.SubmitChanges();
               // List<SALARY> salaries = db.SALARY.Where(x => x.EmployeeID == employeeID).ToList();
               // db.SALARY.DeleteAllOnSubmit(salaries);
                // db.SubmitChanges();
               // List<PERMISSION> permissions = db.PERMISSION.Where(x => x.EmployeeID == employeeID).ToList();
               // db.PERMISSION.DeleteAllOnSubmit(permissions);
               // db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmployee(POSITION position)
        {
            List<EMPLOYEE> list = db.EMPLOYEE.Where(x => x.PositionID == position.ID).ToList();
            foreach (var item in list)
            {
                item.DepartmentID = position.DepartmentID;
            }
            db.SubmitChanges();
            
        }

        public static void UpdateEmployee(EMPLOYEE employee)
        {
            try
            {
                EMPLOYEE emp = db.EMPLOYEE.First(x => x.ID == employee.ID);
                emp.UserNo = employee.UserNo;
                emp.Name = employee.Name;
                emp.Surname = employee.Surname;
                emp.Adress = employee.Adress;
                emp.Salary = employee.Salary;
                emp.ImagePath = employee.ImagePath;
                emp.Password = employee.Password;
                emp.isAdmin = employee.isAdmin;
                emp.BirthDay = employee.BirthDay;
                emp.DepartmentID = employee.DepartmentID;
                emp.PositionID = employee.PositionID;
                emp.Email = employee.Email;
                emp.PhoneNumber = employee.PhoneNumber;
                emp.Admission = employee.Admission;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmployee(int employeeID, int amount)
        {
            try
            {
                EMPLOYEE employee = db.EMPLOYEE.First(x => x.UserNo == employeeID);
                employee.Salary = amount;
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<EMPLOYEE> GetEmployees(int v, string text)
        {
            try
            {
                List<EMPLOYEE> list = db.EMPLOYEE.Where(x => x.UserNo == v && x.Password == text).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<EMPLOYEE> GetUsers(int v)
        {
            return db.EMPLOYEE.Where(x=> x.UserNo == v).ToList();
        }
    }
}
