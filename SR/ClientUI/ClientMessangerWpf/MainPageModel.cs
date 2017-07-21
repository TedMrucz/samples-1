using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNet.SignalR.Client;


namespace ClientMessangerWpf
{
	public class MainPageModel : BindableBase
	{
		private IList<Message> messages = new ObservableCollection<Message>();
		public IList<Message> Messages
		{
			get { return messages; }
			set { SetProperty(ref messages, value); }
		}

		private int clientId = DateTime.Now.Minute * 100 + DateTime.Now.Second;
		public int ClientId
		{
			get { return clientId; }
			set { SetProperty(ref clientId, value); }
		}

		private string message = String.Empty;
		public string Message
		{
			get { return message; }
			set { SetProperty(ref message, value); }
		}

		public DelegateCommand SubscribeCommand { get; private set; }
		public DelegateCommand UnsubscribeCommand { get; private set; }
		public DelegateCommand SendCommand { get; private set; }
		public DelegateCommand LoadControlCommand { get; private set; }
		public DelegateCommand UnloadedControlCommand { get; private set; }

		public MainPageModel()
		{
			SubscribeCommand = new DelegateCommand(OnSubscribeCommand, CanSubscribe);
			UnsubscribeCommand = new DelegateCommand(OnUnsubscribeCommand, CanUnsubscribe);
			SendCommand = new DelegateCommand(OnSendCommand, CanSend);
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

			canSend = false;
			SendCommand.RaiseCanExecuteChanged();
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

			canSend = true;
			SendCommand.RaiseCanExecuteChanged();
		}

		private bool canSubscribe = true;
		private bool CanSubscribe()
		{
			return canSubscribe;
		}

		private async void OnSendCommand()
		{
			await hub.Invoke<Message>("PostMessage", new Message(ClientId, this.Message));
		}

		private bool canSend = true;
		private bool CanSend()
		{
			return canSend;
		}

		private IHubProxy hub;
		private string url = @"http://localhost:8080/";
		private HubConnection connection;

		private async void Initialize()
		{
			try
			{
				connection = new HubConnection(url);
				hub = connection.CreateHubProxy("MessangerHub");
				await connection.Start();
			}
			catch (Exception ex)
			{
				Message = ex.Message;
			}

			hub.On<Message>("OnReceiveMessage", x => OnReceiveMessage(x));
		}
		public void OnReceiveMessage(Message post)
		{
			App.Current.Dispatcher.BeginInvoke((Action)(() => { Messages.Add(post); }));
		}
	}
}
