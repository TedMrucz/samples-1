using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using GridSyncEntity;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Qactive;

namespace GridSyncClient
{
	public class ClientViewModel : BindableBase
	{
		private readonly Dispatcher dispatcher;
		private readonly TaskScheduler taskScheduler;
		private IDisposable clientDisposable;

		public DelegateCommand ControlLoaded { get; private set; }
		public DelegateCommand ControlClosing { get; private set; }
		public DelegateCommand UpdateAsyncAwaitCommand { get; private set; }
		public DelegateCommand UpdateAsyncTplCommand { get; private set; }
		public DelegateCommand UpdateAsyncDispCommand { get; private set; }
		public DelegateCommand UpdateAsyncThreadCommand { get; private set; }
		public DelegateCommand UpdateAsyncThreadCommand2 { get; private set; }

		private ObservableCollection<Person> items = new ObservableCollection<Person>();
		public ObservableCollection<Person> Items
		{
			get { return this.items; }
			set { SetProperty(ref this.items, value); }
		}

		private string message;
		public string Message
		{
			get { return this.message; }
			private set { SetProperty(ref this.message, value); }
		}

		public ClientViewModel(Dispatcher dispatcher)
		{
			this.dispatcher = dispatcher;
			this.taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

			this.ControlLoaded = new DelegateCommand(OnControlLoaded);
			this.ControlClosing = new DelegateCommand(OnControlClosing);

			this.UpdateAsyncAwaitCommand = new DelegateCommand(OnUpdateUiViaAsyncAwaitCommand);
			this.UpdateAsyncTplCommand = new DelegateCommand(OnUpdateUiViaTplCommand);
			this.UpdateAsyncDispCommand = new DelegateCommand(OnUpdateUiViaThreadCommand);
			this.UpdateAsyncThreadCommand = new DelegateCommand(OnUpdateUiViaWorkThreadCommand);
			this.UpdateAsyncThreadCommand2 = new DelegateCommand(OnUpdateUiViaThreadCommand2);
		}

		private void OnControlClosing()
		{
			if (this.clientDisposable != null)
				this.clientDisposable.Dispose();
		}

		private void OnControlLoaded()
		{
			var client = new TcpQbservableClient<string>(new IPEndPoint(IPAddress.Loopback, 3205));

			var query = from value in client.Query() select value;

			this.clientDisposable = query.Subscribe(
								  value => OnParseMessageAsync(value),
								  ex => this.Message = ex.Message,
								  () => this.Message = "Completed");
		}

		private void OnParseMessageAsync(string value)
		{
			var task = Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Person>(value));

			task.ContinueWith((t) =>
			{
				var person = t.Result;
				var item = this.Items.FirstOrDefault(p => p.Id == person.Id);
				if (item != null)
				{
					item.Name = person.Name;
					item.DateOfBirth = person.DateOfBirth;
				}
				else
				{
					this.Items.Add(person);
				}
			}, this.taskScheduler);
		}

        /// ///////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////

        private async void OnUpdateUiViaAsyncAwaitCommand()
		{
			this.Message = await UpdateTextAsyncAwait();
		}

		private async Task<string> UpdateTextAsyncAwait()
		{
			var textAsync = "TextAsync Await";
			await Task.Delay(200);
			return textAsync;
		}

		private void OnUpdateUiViaTplCommand()
		{
			var task = Task.Factory.StartNew(() =>
			{
				Thread.Sleep(200);
				return "TextAsync TPL";
			});
			task.ContinueWith((t) =>
			{
				this.Message = task.Result;
			}, this.taskScheduler);
		}

		private void OnUpdateUiViaThreadCommand()
		{
			var thread = new Thread(DoWork);
			thread.Start();
		}

		private void DoWork(object state)
		{
			Thread.Sleep(200);
			Application.Current.Dispatcher.BeginInvoke(new Action(() => this.Message = "From Thread (via Application Dispatcher)"), DispatcherPriority.Normal);
		}

		private void OnUpdateUiViaWorkThreadCommand()
		{
			var worker = new BackgroundWorker();
			worker.DoWork += (s, e) =>
			{
				Thread.Sleep(200);
				e.Result = "Text from BackgroundWorker";
			};
			worker.RunWorkerCompleted += (s, e) =>
			{
				if (e.Error != null && !e.Cancelled)
					this.Message = e.Result as string;
			};
			worker.RunWorkerAsync();
		}

		private void OnUpdateUiViaThreadCommand2()
		{
			var thread = new Thread(DoWork2);
			thread.Start();
		}

		private void DoWork2(object state)
		{
			Thread.Sleep(200);
			this.dispatcher.BeginInvoke(new Action(() => this.Message = "From Thread (via View Dispatcher)"), DispatcherPriority.Normal);
		}
	}
}
