using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Controls;
using ReactiveUI;

namespace subview_ex_wpf
{
	/// <summary>
	/// Interaction logic for SubView.xaml
	/// </summary>
	public partial class SubView : UserControl, IViewFor<SubViewModel>
	{
		public SubView()
		{
			InitializeComponent();

			var serialDisposable = new SerialDisposable();

			this.WhenAnyValue(v => v.DataContext)
				.Where(dc =>  dc != null)
				.Subscribe(dc =>
				{
					ViewModel = (SubViewModel) dc;
					// create command on set VM/DataContext
					// and by assigning to serialDisposable, dispose of the possible earlier command on the previous DataContext
					serialDisposable.Disposable = this.BindCommand(ViewModel, vm => vm.DummyCommand, v => v.TheButton);
				});
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (SubViewModel) value;
		}

		public SubViewModel ViewModel { get; set; }
	}

	public class SubViewModel : ReactiveObject
	{
		public ReactiveCommand<Unit, string> DummyCommand { get; set; }

		public SubViewModel()
		{
			DummyCommand = ReactiveCommand.Create<Unit, string>(unit => $"From the sub vm {DateTime.Now:O}");
		}
	}

}
