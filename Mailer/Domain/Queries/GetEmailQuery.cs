using Database.Tables;

namespace Domain.Queries
{
    public class GetEmailQuery
    {
        public GetEmailQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }

    public interface IGetEmailQueryHandler
    {
        Email Handle(GetEmailQuery query);
    }
}