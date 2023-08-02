using System.Collections.Generic;
using System.Linq;
using CodeLearn.Core.Services.Interfaces;
using CodeLearn.DataLayer.Context;
using CodeLearn.DataLayer.Entities.Permissions;
using CodeLearn.DataLayer.Entities.User;

namespace CodeLearn.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private CodeLearnContext _context;

        public PermissionService(CodeLearnContext context)
        {
            _context = context;
        }
        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            UpdateRole(role);
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (var roleId in roleIds)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }

            _context.SaveChanges();
        }

        public void EditRolesUser(int userId, List<int> roleIds)
        {
            //Delete All User Roles
            _context.UserRoles.Where(r => r.UserId == userId).ToList().ForEach(r => _context.UserRoles.Remove(r));

            //Add New Roles
            AddRolesToUser(roleIds, userId);
        }

        public List<Permission> GetAllPermission()
        {
            return _context.Permissions.ToList();
        }

        public void AddPermissionToRole(int roleId, List<int> permissions)
        {
            foreach (var p in permissions)
            {
                _context.RolePermissions.Add(new RolePermission()
                {
                    RoleId = roleId,
                    PermissionId = p
                });
            }

            _context.SaveChanges();
        }

        public List<int> PermissionsRole(int roleId)
        {
            return _context.RolePermissions
                .Where(r => r.RoleId == roleId)
                .Select(r => r.PermissionId).ToList();
        }

        public void UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            _context.RolePermissions.Where(p => p.RoleId == roleId)
                .ToList().ForEach(p => _context.RolePermissions.Remove(p));

            AddPermissionToRole(roleId, permissions);
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;

            List<int> userRoles = _context.UserRoles
                .Where(u => u.UserId == userId)
                .Select(u => u.RoleId).ToList();

            if (!userRoles.Any())
                return false;

            List<int> rolesPermission = _context.RolePermissions
                .Where(r => r.PermissionId == permissionId)
                .Select(r => r.RoleId).ToList();

            return rolesPermission.Any(r => userRoles.Contains(r));
        }
    }
}
