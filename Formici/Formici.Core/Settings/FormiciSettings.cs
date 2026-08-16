using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Formici.Core.Settings;

/// <summary>
/// Represents general game settings including display and language preferences.
/// Implements <see cref="INotifyPropertyChanged"/> for data binding and UI updates.
/// </summary>
public class FormiciSettings : INotifyPropertyChanged
{
    private bool fullScreen;
    private int language = 2; // Default to English

    /// <summary>
    /// Gets or sets whether the game is in full-screen mode.
    /// </summary>
    public bool FullScreen
    {
        get => fullScreen;
        set
        {
            if (fullScreen != value)
            {
                fullScreen = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the language setting for the game.
    /// </summary>
    public int Language
    {
        get => language;
        set
        {
            if (language != value)
            {
                language = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}