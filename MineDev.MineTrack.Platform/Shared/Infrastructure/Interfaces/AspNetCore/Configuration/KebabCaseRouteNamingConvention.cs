using Microsoft.AspNetCore.Mvc.ApplicationModels;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration.Extensions;

namespace MineDev.MineTrack.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;

/// <summary>
///     Replaces [controller] tokens in route templates with kebab-case.
///     Example: MachineryController → /api/v1/machinery
/// </summary>
public class KebabCaseRouteNamingConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);

        foreach (var selector in controller.Actions.SelectMany(a => a.Selectors))
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);
    }

    private static AttributeRouteModel? ReplaceControllerTemplate(SelectorModel selector, string name)
    {
        return selector.AttributeRouteModel != null
            ? new AttributeRouteModel
            {
                Template = selector.AttributeRouteModel.Template?.Replace("[controller]", name.ToKebabCase())
            }
            : null;
    }
}