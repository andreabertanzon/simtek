using MediatR;
using SimtekMaui.Models;
using SimtekMaui.Models.Exceptions;

namespace SimtekMaui.Utils;

public class NewInterventionStateBuilder
{
    private NewInterventionState? _newInterventionState;

    public void StartTracking()
    {
        _newInterventionState = new NewInterventionState();
    }

    public Result<Unit> AddCustomerData(
        Customer customer)
    {
        if (_newInterventionState is null)
        {
            return Result<Unit>.Failure(new NoDataFoundException("_newInterventionState"));
        }

        _newInterventionState = _newInterventionState with
        {
            Customer = customer
        };

        return Result<Unit>.Success(Unit.Value);
    }

    public Result<Unit> AddSiteData(string name, string address)
    {
        if (_newInterventionState?.Customer is null)
        {
            return Result<Unit>.Failure(new NoDataFoundException("_newInterventionState"));
        }

        _newInterventionState = _newInterventionState with
        {
            Site = new Site(name,address,_newInterventionState.Customer)
        };
        return Result<Unit>.Success(Unit.Value);
    }

    public string GetAddressFromCustomer()
    {
        return _newInterventionState?.Customer is null ? "" : _newInterventionState.Customer.Address;
    }

    public Result<NewInterventionState> Build()
    {
        if (_newInterventionState is null)
        {
            return Result<NewInterventionState>.Failure(new NoDataFoundException("_newInterventionState"));
        }

        var result = _newInterventionState with { };
        _newInterventionState = null;
        return Result<NewInterventionState>.Success(result);
    }
}