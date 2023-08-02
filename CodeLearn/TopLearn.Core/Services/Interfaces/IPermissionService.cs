using System.Collections.Generic;
using CodeLearn.DataLayer.Entities.Permissions;
using CodeLearn.DataLayer.Entities.User;

namespace CodeLearn.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Roles

        List<Role> GetRoles();
        int AddRole(Role role);
        Role GetRoleById(int roleId);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        void AddRolesToUser(List<int> roleIds, int userId);
        void EditRolesUser(int userId, List<int> roleIds);

        #endregion

        #region Permission

        List<Permission> GetAllPermission();
        void AddPermissionToRole(int roleId, List<int> permissions);
        List<int> PermissionsRole(int roleId);
        void UpdatePermissionsRole(int roleId, List<int> permissions);
        bool CheckPermission(int permissionId, string userName);

        #endregion

    }
}
