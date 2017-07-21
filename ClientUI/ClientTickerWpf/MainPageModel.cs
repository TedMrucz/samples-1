using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;


namespace ClientTickerWpf
{
	public class MainPageModel : BindableBase
	{
		private string message = String.Empty;
		public string Message
		{
			get { return message; }
			set { SetProperty(ref message, value); }
		}

		private string tick = "33";
		public string Tick
		{
			get { return tick; }
			set { SetProperty(ref tick, value); }
		}

		private int clientId = DateTime.Now.Minute * 100 + DateTime.Now.Second;
		public int ClientId
		{
			get { return clientId; }
			set { SetProperty(ref clientId, value); }
		}

		public DelegateCommand SubscribeCommand { get; private set; }
		public DelegateCommand UnsubscribeCommand { get; private set; }
		public DelegateCommand LoadControlCommand { get; private set; }
		public DelegateCommand UnloadedControlCommand { get; private set; }

		public MainPageModel()
		{
			SubscribeCommand = new DelegateCommand(OnSubscribeCommand, CanSubscribe);
			UnsubscribeCommand = new DelegateCommand(OnUnsubscribeCommand, CanUnsubscribe);
			LoadControlCommand = new DelegateCommand(OnLoadControlCommand);
			UnloadedControlCommand = new DelegateCommand(OnUnloadedControlCommandd);
		}
		private void OnLoadControlCommand()
		{
			Initialize();
		}
		private void OnUnloadedControlCommandd()
		{
			OnUnsubscribeCommand();
		}

		private async void OnUnsubscribeCommand()
		{
			canSubscribe = true;
			SubscribeCommand.RaiseCanExecuteChanged();
			canUnsubscribe = false;
			UnsubscribeCommand.RaiseCanExecuteChanged();

			await hub.Invoke<int>("Unsubscribe", new object[] { ClientId });

			if (connection.State == ConnectionState.Connected)
				connection.Stop();
		}

		private bool canUnsubscribe = false;
		private bool CanUnsubscribe()
		{
			return canUnsubscribe;
		}

		private async void OnSubscribeCommand()
		{
			canSubscribe = false;
			SubscribeCommand.RaiseCanExecuteChanged();
			canUnsubscribe = true;
			UnsubscribeCommand.RaiseCanExecuteChanged();

			if (connection.State != ConnectionState.Connected)
				await connection.Start();

			try
			{
				await hub.Invoke<int>("Subscribe", new object[] { ClientId });
			}
			catch (Exception ex)
			{
				Message = ex.Message;
			}
		}

		private bool canSubscribe = true;
		private bool CanSubscribe()
		{
			return canSubscribe;
		}

		private IHubProxy hub;
		private string url = @"http://localhost:8080/";
		private HubConnection connection;

		private void Initialize()
		{
			try
			{
				connection = new HubConnection(url);
				hub = connection.CreateHubProxy("TickerHub");
				//await connection.Start();

				hub?.On("OnNextTick", x => OnNextTick(x));
			}
			catch (Exception ex)
			{
				Message = ex.Message;
			}
		}

		public void OnNextTick(string tick)
		{
			App.Current.Dispatcher.BeginInvoke((Action)(() => { Tick = tick; }));
		}
	}
}
