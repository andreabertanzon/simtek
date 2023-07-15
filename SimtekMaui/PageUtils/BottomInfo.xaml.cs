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

public partial class BottomInfo : BottomSheet
{
    public ObservableCollection<ListAction> Actions => new()
    {
        new ListAction
        {
            Title = "Share",
            Command = new Command(() => { }),
        },
        new ListAction
        {
            Title = "Copy",
            Command = new Command(() => { }),
        },
        new ListAction
        {
            Title = "Open in browser",
            Command = new Command(() => { }),
        },
        new ListAction
        {
            Title = "Resize",
        },
        new ListAction
        {
            Title = "Dismiss",
            Command = new Command(() => DismissAsync()),
        }
    };

    
    
    public BottomInfo()
    {
        InitializeComponent();
    }
}