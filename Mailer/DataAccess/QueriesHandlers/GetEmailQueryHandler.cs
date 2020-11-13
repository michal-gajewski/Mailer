using Database;
using Database.Tables;
using Domain.Queries;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.QueriesHandlers
{
    public class GetEmailQueryHandler : IGetEmailQueryHandler
    {
        private readonly MailerDbContext _mailerDbContext;

        public GetEmailQueryHandler(MailerDbContext mailerDbContext)
        {
            _mailerDbContext = mailerDbContext;
        }

        public Email Handle(GetEmailQuery query)
        {
            return _mailerDbContext.Emails.Include(e => e.Recipients).FirstOrDefault(e => e.Id == query.Id);
        }
    }
}