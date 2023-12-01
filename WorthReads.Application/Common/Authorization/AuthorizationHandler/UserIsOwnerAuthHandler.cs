using Domain.PdfContainer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;
using WorthReads.Domain.Users.ValueObjects;

namespace Application.Common.Authorization.AuthorizationHandler;

public class ContactOwnerOrManagerAuthHandler : AuthorizationHandler<OperationAuthorizationRequirement, PdfContainer>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, PdfContainer resource)
    {
        if (context.User == null) { return Task.CompletedTask; }

        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Task.CompletedTask;
        }

        var userId = UserId.Create(Guid.Parse(userIdClaim.Value));

        if (userIdClaim == null)
        {
            return Task.CompletedTask;
        }

        if (requirement.Name != Constants.AddPdfOperationName
                && requirement.Name != Constants.GenerateContainerPdf)
        {
            return Task.CompletedTask;
        }

        if (resource.OwnerId == userId) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
