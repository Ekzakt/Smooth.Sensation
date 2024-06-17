using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Smooth.Flaunt.Components;

public partial class SmoothButton : SmoothComponentBase, INotifyPropertyChanged
{
    [Parameter]
    public EventCallback OnButtonClickCallback { get; set; }


    private string _caption = string.Empty;

    [Parameter]
    public string Caption
    {
        get => _caption;
        set
        {
            if (_caption != value)
            {
                _caption = value;
                NotifyPropertyChanged(nameof(Caption));
            }
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;


    private string GetCaption() => Caption;


    private async Task OnButtonClick()
    {
        var oldText = Caption;

        Caption = "Loading...";

        await OnButtonClickCallback.InvokeAsync();

        Caption = oldText;

    }

    private void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        StateHasChanged();
    }
}
