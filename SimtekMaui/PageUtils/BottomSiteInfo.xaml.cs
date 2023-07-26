using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimtekMaui.ViewModels;
using The49.Maui.BottomSheet;

namespace SimtekMaui.PageUtils;

public partial class BottomSiteInfo : BottomSheet
{
    public ObservableCollection<ListAction> Actions { get; set; } = new();

    public BottomSiteInfo(AddSiteViewModel viewModel)
    {
        InitializeComponent();
        viewModel.Sites.Select(x => new ListAction()
        {
            Command = new Command(() =>
            {
                viewModel.Address = x.Address;
                viewModel.Name = x.Name;
                
                DismissAsync();
            }),
            Title = x.Name
        }).ToList().ForEach(x => { Actions.Add(x); });
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