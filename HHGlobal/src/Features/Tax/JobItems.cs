namespace HHGlobal.Features.Tax;

public record IncomingJob(bool ExtraMargin, List<IncomingItem> Items);
public record IncomingItem(string Name, decimal Price, bool Exempt);



public record ResponseJob(decimal Total, IEnumerable<ResponseItem> Items);
public record ResponseItem(string Name, decimal Price);