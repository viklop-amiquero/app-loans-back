using HRA.Application.Common.Interfaces;

namespace HRA.Infrastructure.Persistence.Services
{
    public class DateTimeService : IDateTime
    {
        DateTime IDateTime.Today => DateTime.Today;
        DateTime IDateTime.Now => DateTime.Now;
    }
}
