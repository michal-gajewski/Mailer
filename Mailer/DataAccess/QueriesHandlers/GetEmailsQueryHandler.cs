using Database;
using Database.Tables;
using Domain.Queries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccess.QueriesHandlers
{
    public class GetEmailsQueryHandler : IGetEmailsQueryHandler
    {
        private readonly MailerDbContext _mailerDbContext;

        public GetEmailsQueryHandler(MailerDbContext mailerDbContext)
        {
            _mailerDbContext = mailerDbContext;
        }

        public IEnumerable<Email> Handle(GetEmailsQuery query)
        {
            return _mailerDbContext.Emails.Include(e => e.Recipients);
        }
    }
}