using Microsoft.AspNetCore.Mvc.ApplicationModels;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration.Extensions;

namespace MineDev.MineTrack.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;

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
