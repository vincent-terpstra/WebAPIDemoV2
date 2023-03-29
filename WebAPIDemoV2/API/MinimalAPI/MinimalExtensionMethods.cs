namespace WebAPIDemoV2.API.MinimalAPI;

public static class MinimalExtensionMethods
{
    public static RouteMapper<T> MapRoutes<T>(this WebApplication app, string route, string tags)
        =>new (app, route, tags);
    
}