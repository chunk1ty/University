using System.Transactions;
using VoteSystem.Data.Contracts;
using VoteSystem.Data.Ef.Contracts;

namespace VoteSystem.Data.Ef.Transaction
{
    public class EntityFrameworkTransaction : IEntityFrameworkTransaction
    {
        private readonly IVoteSystemEfDbContextSaveChanges _voteSystemDbContext;

        private TransactionScope _scope;

        public EntityFrameworkTransaction(IVoteSystemEfDbContextSaveChanges voteSystemDbContext)
        {
            _voteSystemDbContext = voteSystemDbContext;
        }

        public void SaveChanges()
        {
            _voteSystemDbContext.SaveChanges();
        }

        public void StartTransactionScope()
        {
            _scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
        }

        public void CommitTransactionScope()
        {
            if (_scope != null)
            {
                _scope.Complete();
            }
        }

        public void Dispose()
        {
            if (_scope != null)
            {
                _scope.Dispose();
                _scope = null;
            }
        }
    }
}
