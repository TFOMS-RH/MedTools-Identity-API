using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Roles
{
    public static class GetRoleByIdHandler
    {
        public static async Task<IResult> HandleAsync(
            Guid id,
            RoleRepository roleRepository,
            CancellationToken cancellationToken)
        {
            var role = await roleRepository.GetRoleByIdAsync(
                roleId: id,
                cancellationToken: cancellationToken);

            return Results.Ok(role);
        }
    }
}
