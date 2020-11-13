using Database;
using Domain.Queries;
using Infrastructure.DTOs.Enumerations;
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