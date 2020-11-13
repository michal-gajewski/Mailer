using Infrastructure.DTOs.Enumerations;

namespace Domain.Queries
{
    public class GetEmailStatusQuery
    {
        public GetEmailStatusQuery(long emailId)
        {
            EmailId = emailId;
        }

        public long EmailId { get; }
    }

    public interface IGetEmailStatusQueryHandler
    {
        EmailStatus Handle(GetEmailStatusQuery query);
    }
}