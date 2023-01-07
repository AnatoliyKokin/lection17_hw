namespace FileExplorerApp.Extensions;

public static class MaxExtension
{
    public static T GetMax<T>(this IEnumerable<T> e, Func<T, float> getParameter) where T : class
    {
        T? maxResult = e.MaxBy(v => getParameter(v));
        if (maxResult != null) return maxResult;
        throw new NullReferenceException();
    }

}