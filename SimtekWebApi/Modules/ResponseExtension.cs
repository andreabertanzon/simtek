using OneOf;
using SimtekDomain.Errors;

namespace SimtekWebApi.Modules;

public static class ResponseExtension
{
    public static IResult ToHttpResult<TResult>(this OneOf<TResult, SimtekError> result)
    {
        var output= result.Match(
            value => Results.Ok(value),
            error => error.Error switch
            {
                DatabaseError databaseError => Results.Problem(statusCode:500,detail:databaseError.Err.Message),
                NotFoundError notFoundError => Results.NotFound($"{notFoundError.PropertyName}, not found"),
                RequestInvalidError requestInvalidError => Results.BadRequest(),
                _ => throw new ArgumentOutOfRangeException()
            }
        );
        return output;
    }
}