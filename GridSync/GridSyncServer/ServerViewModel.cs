using System;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Newtonsoft.Json;
using Qactive;
using GridSyncEntity;

namespace GridSyncServer
{
	public class ServerViewModel : BindableBase
	{
		public DelegateCommand ControlLoaded { get; set; }
		public DelegateCommand ControlClosing { get; set; }

		private ObservableCollection<Person> items;
		public ObservableCollection<Person> Items
		{ 
			get { return this.items; }
			set { SetProperty(ref items, value); }
		}

		private string message;
		public string Message
		{
			get { return message; }
			set { SetProperty(ref message, value); }
		}

		private string text;
		public string Text
		{
			get { return text; }
			set { SetProperty(ref text, value); }
		}

		public ServerViewModel()
		{
			this.ControlLoaded = new DelegateCommand(OnControlLoaded);
			this.ControlClosing = new DelegateCommand(OnControlClosing);
		}

		private Subject<string> server = new Subject<string>();
		private IDisposable serverDisposable;

		private void OnControlClosing()
		{
			if (serverDisposable != null)
				serverDisposable.Dispose();
		}

		private void OnControlLoaded()
		{
			Person person;
			ObservableCollection<Person> list = new ObservableCollection<Person>();

			person = new Person(1, "Joe", new DateTime(2017, 1, 23));
			person.PropertyChanged += OnPropertyChanged;
			list.Add(person);

			person = new Person(2, "Ted", new DateTime(1952, 3, 1));
			person.PropertyChanged += OnPropertyChanged;
			list.Add(person);

			person = new Person(3, "Mike", new DateTime(1981, 9, 27));
			person.PropertyChanged += OnPropertyChanged;
			list.Add(person);

			Items = list;

			this.CreateServer();
		}

		private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			Person person = sender as Person;
			var content = JsonConvert.SerializeObject(person);
			server.OnNext(content);
		}

		private void CreateServer()
		{
			var service = server.ServeQbservableTcp(new IPEndPoint(IPAddress.Loopback, 3205),
																 new QbservableServiceOptions()
																 {
																	 SendServerErrorsToClients = true,
																	 EnableDuplex = true,
																	 AllowExpressionsUnrestricted = true
																 });

		   serverDisposable = service.Subscribe(
							  client => this.Message = "Client shutdown",
							  ex => this.Message = ex.Message,
							  () => this.Message = "This will never be printed because a service host never completes");
		}
	}
}
