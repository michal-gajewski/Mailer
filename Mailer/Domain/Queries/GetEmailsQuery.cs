using Database.Tables;
using System.Collections.Generic;

namespace Domain.Queries
{
    public class GetEmailsQuery
    {
    }

    public interface IGetEmailsQueryHandler
    {
        IEnumerable<Email> Handle(GetEmailsQuery query);
    }
}