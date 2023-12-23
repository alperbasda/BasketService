
namespace Core.CrossCuttingConcerns.Exceptions.Types;

public class AuthorizationException : Exception
{
    public string RedirectUrl { get; set; }

    public AuthorizationException()
    {

    }

    public AuthorizationException(string redirect,string? message) : base(message) { this.RedirectUrl = redirect; }

    public AuthorizationException(string redirect,string? message, Exception? innerException) : base(message, innerException) { this.RedirectUrl = redirect; }
}