using SMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Business.TeacherServices
{
    public class CreateTeacherDTO
    {
        public string Qualification { get; set; }
        public DateTime StartDate { get; set; }
        public int CreatedByUserID { get; set; }

        internal Teacher ToEntity()
        {
            return new Teacher
            {
                Qualification = this.Qualification,
                StartDate = this.StartDate,
                CreatedByUserID = this.CreatedByUserID,
                
                ExitDate = null,
                TeacherStatus = Teacher.enTeacherStatus.Active
            };
        }
    }

    public class UpdateTeacherDTO
    {
        public int TeacherID { get; set; }
        public string Qualification { get; set; }
        public DateTime? ExitDate { get; set; }
        public Teacher.enTeacherStatus TeacherStatus { get; set; }

        internal void UpdateEntity(Teacher teacher)
        {
            teacher.Qualification = this.Qualification;
            teacher.ExitDate = this.ExitDate;
            teacher.TeacherStatus = this.TeacherStatus;
        }
    }
}
