using Database;
using Domain.Enumerations;
using Domain.Queries;
using System.Linq;

namespace DataAccess.QueriesHandlers
{
    public class GetEmailStatusQueryHandler : IGetEmailStatusQueryHandler
    {
        private readonly MailerDbContext _mailerDbContext;

        public GetEmailStatusQueryHandler(MailerDbContext mailerDbContext)
        {
            _mailerDbContext = mailerDbContext;
        }

        public EmailStatus Handle(GetEmailStatusQuery query)
        {
            return _mailerDbContext.Emails.FirstOrDefault(e => e.Id == query.EmailId).Status;
        }
    }
}