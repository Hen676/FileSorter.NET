using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace FileSorter.NET.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TopRow.PointerPressed += MainWindow_PointerPressed;
        }

        private void MainWindow_PointerPressed(object? sender, PointerPressedEventArgs e) => BeginMoveDrag(e);
    }
}