namespace PocketIS.Infrastucture.Validation
{
    /// <summary>
    /// In order to disable silly Code Analysis warnings
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}
