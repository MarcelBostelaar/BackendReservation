using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ReserveerBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend
{
    public static class Authorization
    {
        public const string AdminOrHigher = "3";
        public const string ServiceOrHigher = "2, 3";
        public const string TeacherOrHigher = "1, 2, 3";
        public const string StudentOrHigher = "0, 1, 2, 3";

        public const string StudentString = "Student";
        public const string TeacherString = "Teacher";
        public const string ServiceDeskString = "ServiceDesk";
        public const string AdminString = "Admin";

        public static Role? FromString(string role)
        {
            switch (role)
            {
                case StudentString: return Role.Student;
                case TeacherString: return Role.Teacher;
                case ServiceDeskString: return Role.ServiceDesk;
                case AdminString: return Role.Admin;
                default: return null;
            }
        }

        public static String FromRole(Role role)
        {
            switch (role)
            {
                case Role.Student: return StudentString;
                case Role.Teacher: return TeacherString;
                case Role.ServiceDesk: return ServiceDeskString;
                case Role.Admin: return AdminString;
                default: throw new NotImplementedException();
            }
        }
    }

    public enum Role
    {
        Student = 0,
        Teacher = 1,
        ServiceDesk = 2,
        Admin = 3
    }
}
