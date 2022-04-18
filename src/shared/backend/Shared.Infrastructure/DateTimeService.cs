
using Shared.Application;

namespace Shared.Infrastructure;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}