namespace FastEndpoint.Contracts.Coordinator.Responses;

public record CoordinatorResponse(
    string FirstName,
    string LastName,
    Guid UserId);