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
                DatabaseError databaseError => Results.StatusCode(500),
                NotFoundError notFoundError => Results.NotFound(),
                RequestInvalidError requestInvalidError => Results.BadRequest(),
                _ => throw new ArgumentOutOfRangeException()
            }
        );
        return output;
    }
}