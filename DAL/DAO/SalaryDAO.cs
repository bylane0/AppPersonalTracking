using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class SalaryDAO : EmployeeContext
    {
        public static List<MONTHS> GetMonths()
        {
            return db.MONTHS.ToList();
        }

        public static void AddSalary(SALARY salary)
        {
            try
            {
                db.SALARY.InsertOnSubmit(salary);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<SalaryDetailDTO> GetSalaries()
        {
            List<SalaryDetailDTO> salaryList = new List<SalaryDetailDTO>();

            var list = (from s in db.SALARY
                        join e in db.EMPLOYEE on s.EmployeeID equals e.UserNo
                        join m in db.MONTHS on s.MonthID equals m.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            name = e.Name,
                            surname = e.Surname,
                            EmployeeID = s.EmployeeID,
                            amount = s.Amount,
                            year = s.Year,
                            monthname = m.MonthName,
                            monthID = s.MonthID,
                            salaryID = s.ID,
                            departmentID = e.DepartmentID,
                            positionID = e.PositionID,
                            Aguinaldo = s.Aguinaldo

                        }).OrderBy(x => x.year).ToList();

            foreach (var item in list)
            {
                SalaryDetailDTO dto = new SalaryDetailDTO();
                dto.UserNo = item.UserNo;
                dto.Name = item.name;
                dto.Surname = item.surname;
                dto.EmployeeID = item.EmployeeID;
                dto.SalaryAmount = item.amount;
                dto.SalaryYear = item.year;
                dto.MonthName = item.monthname;
                dto.MonthID = item.monthID;
                dto.SalaryID = item.salaryID;
                dto.DepartmentID = item.departmentID;
                dto.PositionID = item.positionID;
                dto.OldSalary = item.amount;
                dto.Aguinaldo = item.Aguinaldo;
                salaryList.Add(dto);
            }
            return salaryList;
        }

        public static void UpdateSalary(DateTime value1, DateTime value2)
        {
            try
            {
                db.GenerateAguinaldo(value1,value2);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateSalary()
        {
          
        }

        public static void DeleteSalary(int salaryID)
        {
            try
            {
                SALARY sl = db.SALARY.First(x => x.ID == salaryID);
                db.SALARY.DeleteOnSubmit(sl);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateSalary(SALARY salary)
        {
            try
            {
                SALARY sl = db.SALARY.First(x => x.ID == salary.ID);
                sl.Amount = salary.Amount;
                sl.Year = salary.Year;
                sl.MonthID = salary.MonthID;
                db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
