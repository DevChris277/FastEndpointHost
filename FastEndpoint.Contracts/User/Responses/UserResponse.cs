namespace FastEndpoint.Contracts.User.Responses;

public record UserResponse(Guid UserId, string FirstName, string LastName,string Role, string Email);
