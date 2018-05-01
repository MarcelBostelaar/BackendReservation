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
        public const string StudentUser = "StudentUser";
        public const string TeacherUser = "TeacherUser";
        public const string ServiceDeskUser = "ServiceDeskUser";
        public const string AdminUser = "AdminUser";

        public static Action<AuthorizationOptions> AddUserAuthenticationPolicies()
        {
            Action<AuthorizationPolicyBuilder> userfields = policy =>
            {
                policy.RequireClaim(User._Email)
                .RequireClaim(User._EmailNotification)
                .RequireClaim(User._ID)
                .RequireClaim(User._Role);
            };

            return options =>
            {
                options.AddPolicy(AdminUser, policy =>
                {
                    userfields(policy);
                    policy.Requirements.Add(new MinimumRole(Role.Admin));
                });
                options.AddPolicy(StudentUser, policy =>
                {
                    userfields(policy);
                    policy.Requirements.Add(new MinimumRole(Role.Student));
                });
                options.AddPolicy(TeacherUser, policy =>
                {
                    userfields(policy);
                    policy.Requirements.Add(new MinimumRole(Role.Teacher));
                });
                options.AddPolicy(ServiceDeskUser, policy =>
                {
                    userfields(policy);
                    policy.Requirements.Add(new MinimumRole(Role.ServiceDesk));
                });
            };
        }
    }

    public class MinimumRoleHandler : AuthorizationHandler<MinimumRole>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRole requirement)
        {
            try
            {
                var role = (Role)(int.Parse(context.User.FindFirst(User._Role).Value));

                if (requirement.check(role))
                {
                    context.Succeed(requirement);
                }
            }
            catch(Exception e){

            }
            return Task.CompletedTask;
        }
    }

    public class MinimumRole : IAuthorizationRequirement
    {
        public Func<Role, bool> check { get; private set; }
        public MinimumRole(Role role)
        {
            switch (role)
            {
                case Role.Teacher:
                    check = TeacherOrMore;
                    break;
                case Role.Student:
                    check = StudentOrMore;
                    break;
                case Role.Admin:
                    check = AdminOrMore;
                    break;
                case Role.ServiceDesk:
                    check = SDOrMore;
                    break;
            }
        }

        static private bool AdminOrMore(Role role)
        {
            return role == Role.Admin;
        }
        static private bool SDOrMore(Role role)
        {
            return role == Role.ServiceDesk || AdminOrMore(role);
        }
        static private bool TeacherOrMore(Role role)
        {
            return role == Role.Teacher || SDOrMore(role);
        }
        static private bool StudentOrMore(Role role)
        {
            return true;
        }
    }
}
