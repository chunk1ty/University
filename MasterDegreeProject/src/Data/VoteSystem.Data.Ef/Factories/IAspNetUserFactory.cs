using VoteSystem.Data.Ef.Models;

namespace VoteSystem.Data.Ef.Factories
{
    public interface IAspNetUserFactory
    {
        AspNetUser CreateAuthUser();
    }
}
