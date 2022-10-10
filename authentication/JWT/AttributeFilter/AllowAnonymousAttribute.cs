namespace JWT.AttributeFilter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}
