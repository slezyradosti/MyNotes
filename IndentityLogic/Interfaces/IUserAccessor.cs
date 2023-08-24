namespace IndentityLogic.Interfaces
{
    public interface IUserAccessor
    {
        string GetUsername();
        Guid GetUserId();
    }
}
