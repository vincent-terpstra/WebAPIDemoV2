using WebAPIDemoV2.Controllers;

namespace WebAPIDemoV2;

public static class ExtensionMethods
{
    public static RouteMapper<T> MapRoutes<T>(this WebApplication app, string route, string tags)
        =>new (app, route, tags);
    
}