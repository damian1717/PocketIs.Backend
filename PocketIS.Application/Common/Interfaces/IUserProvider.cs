namespace PocketIS.Application.Common.Interfaces
{
    public interface IUserProvider
    {
        Guid? GetUserId();
        Guid? GetCompanyId();
    }
}
