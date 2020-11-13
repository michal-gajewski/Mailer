using Database.Tables;
using System.Collections.Generic;

namespace Domain.Queries
{
    public class GetPendingEmailsQuery
    {
    }

    public interface IGetPendingEmailsQueryHandler
    {
        IEnumerable<Email> Handle(GetPendingEmailsQuery query);
    }
}