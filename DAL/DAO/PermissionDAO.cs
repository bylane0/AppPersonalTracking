using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PermissionDAO : EmployeeContext
    {
        public static void AddPermission(PERMISSION permission)
        {
            try
            {
                db.PERMISSION.InsertOnSubmit(permission);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<PERMISSIONSTATE> GetStates()
        {
            return db.PERMISSIONSTATE.ToList();
        }

        public static List<PermissionDetailDTO> GetPermissions()
        {
            List<PermissionDetailDTO> permissions = new List<PermissionDetailDTO>();
            var list = (from p in db.PERMISSION
                        join s in db.PERMISSIONSTATE on p.PermissionState equals s.ID
                        join e in db.EMPLOYEE on p.EmployeeID equals e.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.Surname,
                            StateName = s.StateName,
                            StateID = p.PermissionState,
                            StartDate = p.PermissionStartDate,
                            EndDate = p.PermissionEndDate,
                            EmployeeID = p.EmployeeID,
                            PermissionID = p.ID,
                            Explanation = p.PermissionExplain,
                            DayAmount = p.PermissionDay,
                            DepartmentID = e.DepartmentID,
                            PositionID = e.PositionID
                        }).OrderBy(x => x.StartDate).ToList();

            foreach (var item in list)
            {
                PermissionDetailDTO dto = new PermissionDetailDTO();
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.EmployeeID = item.EmployeeID;
                dto.PermissionDayAmount = item.DayAmount;
                dto.StartDate = item.StartDate;
                dto.EndDate = item.EndDate;
                dto.DepartmentID = item.DepartmentID;
                dto.PositionID = item.PositionID;
                dto.State = item.StateID;
                dto.StateName = item.StateName;
                dto.Explanation = item.Explanation;
                dto.PermissionID = item.PermissionID;
                permissions.Add(dto);
              
            }
            return permissions;
        }

        public static void DeletePermission(int permissionID)
        {
            try
            {
                PERMISSION pr = db.PERMISSION.First(x => x.ID == permissionID);
                db.PERMISSION.DeleteOnSubmit(pr);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdatePermission(int permissionID, int approved)
        {
            try
            {
                PERMISSION pr = db.PERMISSION.First(x => x.ID == permissionID);
                pr.PermissionState = approved;
                db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdatePermission(PERMISSION permission)
        {
            try
            {
                PERMISSION pr = db.PERMISSION.First(x => x.ID == permission.ID);
                pr.PermissionStartDate = permission.PermissionStartDate;
                pr.PermissionEndDate = permission.PermissionEndDate;
                pr.PermissionExplain = permission.PermissionExplain;
                pr.PermissionDay = permission.PermissionDay;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
