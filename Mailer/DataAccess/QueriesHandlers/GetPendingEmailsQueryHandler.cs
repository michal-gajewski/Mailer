using Database;
using Database.Tables;
using Domain.Queries;
using Infrastructure.DTOs.Enumerations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.QueriesHandlers
{
    public class GetPendingEmailsQueryHandler : IGetPendingEmailsQueryHandler
    {
        private readonly MailerDbContext _mailerDbContext;

        public GetPendingEmailsQueryHandler(MailerDbContext mailerDbContext)
        {
            _mailerDbContext = mailerDbContext;
        }

        public IEnumerable<Email> Handle(GetPendingEmailsQuery query)
        {
            return _mailerDbContext.Emails.Include(e => e.Recipients).Where(e => e.Status == EmailStatus.PENDING);
        }
    }
}