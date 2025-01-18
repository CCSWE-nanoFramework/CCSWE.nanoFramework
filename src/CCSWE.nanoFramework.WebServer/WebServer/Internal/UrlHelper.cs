namespace CCSWE.nanoFramework.WebServer.Internal
{
    internal static class UrlHelper
    {
        public static string CombinePathSegments(string segmentA, string segmentB)
        {
            if (string.IsNullOrEmpty(segmentA))
            {
                return segmentB;
            }

            if (string.IsNullOrEmpty(segmentB))
            {
                return segmentA;
            }

            return $"{segmentA}{(segmentA.EndsWith("/") ? string.Empty : "/")}{(segmentB.StartsWith("/") ? segmentB.Substring(1) : segmentB)}";
        }
    }
}
