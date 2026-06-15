using Microsoft.AspNetCore.Mvc.ApplicationModels;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration.Extensions;

namespace MineDev.MineTrack.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;

/// <summary>
///     Kebab case route naming convention for ASP.NET Core controllers.
///     Converts PascalCase controller names to kebab-case URL segments.
///     Example: RentalRequestsController → /api/v1/rental-requests
/// </summary>
public class KebabCaseRouteNamingConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
        {
            if (selector.AttributeRouteModel != null)
            {
                selector.AttributeRouteModel.Template =
                    selector.AttributeRouteModel.Template?
                        .Replace("[controller]", controller.ControllerName.ToKebabCase());
            }
        }
    }
}