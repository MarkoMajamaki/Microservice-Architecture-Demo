using System.Collections;

namespace Order.Application;

public record GetAllOrdersResponse(IEnumerable Orders);