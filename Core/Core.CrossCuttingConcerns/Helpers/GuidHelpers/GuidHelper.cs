
namespace Core.CrossCuttingConcerns.Helpers.GuidHelpers
{
    public static class GuidHelper
    {
        public static bool NotNullOrEmpty(Guid? g)
        {
            return g != null && g != Guid.Empty;
        }
        public static bool NotNullOrEmpty(Guid g)
        {
            return g != Guid.Empty;
        }
    }
}
