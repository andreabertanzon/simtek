using CommunityToolkit.Mvvm.Input;
using MediatR;
using SimtekData.Repository.Abstractions;
using SimtekDomain;
using SimtekDomain.CustomerCQRS;
using SimtekMaui.Utils;

namespace SimtekMaui.ViewModels;

public partial class AddCustomerViewModel:BaseViewModel
{
    private readonly IMediator _mediator;

    public AddCustomerViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public ObservableRangeCollection<Customer> Customers { get; private set; } = new();

    [RelayCommand]
    public async Task LoadCustomers()
    {
        var customers = await _mediator.Send(new GetCustomersQuery());
        customers.Switch(success =>
        {
            Customers.AddRange(success);
        }, error=>
        {
            InError = true;
        });
    }
}