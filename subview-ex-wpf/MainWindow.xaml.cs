using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace subview_ex_wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IViewFor<MainViewModel>
	{
		public MainWindow()
		{
			InitializeComponent();

			ViewModel = new MainViewModel();

			this.WhenAnyValue(v => v.ViewModel.SubViewModel).BindTo(TheTextBox, tb => tb.DataContext);
			this.WhenAnyObservable(v => v.ViewModel.SubViewModel.DummyCommand).Do(s => Console.WriteLine(s)).BindTo(CommandResultTextBlock, tb => tb.Text);

			TheTextBox.DataContext = ViewModel.SubViewModel;
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (MainViewModel) value;
		}

		public MainViewModel ViewModel { get; set; }
	}

	public class MainViewModel : ReactiveObject
	{
		[Reactive]
		public SubViewModel SubViewModel { get; set; } = new SubViewModel();
	}
}
