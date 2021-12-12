using System;

namespace VoteSystem.Data.Ef.Contracts
{
    public interface IEntityFrameworkTransaction : IDisposable
    {
        void SaveChanges();

        void StartTransactionScope();

        void CommitTransactionScope();
    }
}
