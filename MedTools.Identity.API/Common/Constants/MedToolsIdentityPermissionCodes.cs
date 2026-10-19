namespace MedTools.Identity.API.Common.Constants;

public static class MedToolsIdentityPermissionCodes
{
    public const string CanReadUsers = "identity.users.read";
    public const string CanCreateUsers = "identity.users.create";
    public const string CanUpdateUsers = "identity.users.update";
    public const string CanUpdateUserStatus = "identity.users.status_update";
    public const string CanReadUserRoles = "identity.users.roles_read";
    public const string CanManageUserRoles = "identity.users.roles_manage";
    public const string CanReadUserPermissions = "identity.users.permissions_read";

    public const string CanReadOrganizations = "identity.organizations.read";
    public const string CanCreateOrganizations = "identity.organizations.create";
    public const string CanUpdateOrganizations = "identity.organizations.update";

    public const string CanReadRoles = "identity.roles.read";
    public const string CanCreateRoles = "identity.roles.create";
    public const string CanReadRolePermissions = "identity.roles.read_permissions";
    public const string CanManageRolePermissions = "identity.roles.permissions_manage";

    public const string CanReadPermissions = "identity.permissions.read";
    public const string CanCreatePermissions = "identity.permissions.create";

}