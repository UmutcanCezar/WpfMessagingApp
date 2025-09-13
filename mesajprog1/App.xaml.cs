using mesajprog1.Service;
using System.Configuration;
using System.Data;
using System.Windows;

namespace mesajprog1;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static MessageService MessageService { get; private set; }
    static App()
    {
        MessageService = new MessageService();
        _ = MessageService.InitSignalRAsync();

    }
}

