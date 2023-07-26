using SimtekMaui.Models;
using SimtekMaui.Utils;
using SimtekMaui.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using The49.Maui.BottomSheet;

namespace SimtekMaui.PageUtils;

public class ListAction
{
    public string Title { get; set; }
    public ICommand Command { get; set; }
}

public partial class BottomCustomerInfo : BottomSheet
{
    public ObservableCollection<ListAction> Actions { get; set; } = new();
    
    public BottomCustomerInfo(AddCustomerViewModel viewModel)
    {
        InitializeComponent();
        viewModel.Customers.Select(x => new ListAction()
        {
            Command = new Command(() =>
            {
                viewModel.NewCustomer = x;
                DismissAsync();
            }),
            Title = x.FullName
        }).ToList().ForEach(x =>
        {
            Actions.Add(x);
        });
    }

    void Resize()
    {
        divider.HeightRequest = 32;
    }

    public VisualElement Divider => divider;

    public void SetExtraContent(View view)
    {
        extra.Content = view;
    }
}