namespace VoteSystem.Clients.MVC.Infrastructure.Mapping.Contracts
{
    /// <summary>
    /// Map from view model to db model. Must be use only in view models.
    /// </summary>
    /// <typeparam name="T">Db model</typeparam>
    public interface IMapFrom<T>
        where T : class
    {
    }
}
