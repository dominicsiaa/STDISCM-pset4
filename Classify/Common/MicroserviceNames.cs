namespace Classify.Common
{
    public enum MicroserviceNames
    {
        AuthenticationAPI,
        GradesAPI,
        EnrollmentAPI
    }

    public static class MicroserviceNamesExtensions
    {
        public static string GetName(this MicroserviceNames client)
        {
            return client.ToString();
        }
    }
}
