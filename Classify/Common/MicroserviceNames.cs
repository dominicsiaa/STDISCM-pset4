namespace Classify.Common
{
    public enum MicroserviceNames
    {
        AuthenticationAPI,
        GradesAPI,
        EnrollmentAPI,
        RateProfAPI
    }

    public static class MicroserviceNamesExtensions
    {
        public static string GetName(this MicroserviceNames client)
        {
            return client.ToString();
        }
    }
}
