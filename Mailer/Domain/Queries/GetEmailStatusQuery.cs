using Infrastructure.DTOs.Enumerations;

namespace Domain.Queries
{
    public class GetEmailStatusQuery
    {
        public GetEmailStatusQuery(int emailId)
        {
            EmailId = emailId;
        }

        public int EmailId { get; }
    }

    public interface IGetEmailStatusQueryHandler
    {
        EmailStatus Handle(GetEmailStatusQuery query);
    }
}