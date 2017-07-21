using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CalculatorWpf
{
	public class MainPageModel : BindableBase
	{
		private int clientId = DateTime.Now.Minute * 100 + DateTime.Now.Second;
		public int ClientId
		{
			get { return clientId; }
			set { SetProperty(ref clientId, value); }
		}

		private IList<decimal> items = new ObservableCollection<decimal>();
		public IList<decimal> Items
		{
			get { return items; }
			set { SetProperty(ref items, value); }
		}

		private string data = String.Empty;
		public string Data
		{
			get { return data; }
			set { SetProperty(ref data, value); }
		}

		public DelegateCommand SubscribeCommand { get; private set; }
		public DelegateCommand UnsubscribeCommand { get; private set; }
		public DelegateCommand AddCommand { get; private set; }
		public DelegateCommand SendCommand { get; private set; }
		public DelegateCommand LoadControlCommand { get; private set; }
		public DelegateCommand UnloadedControlCommand { get; private set; }

		public MainPageModel()
		{
			SubscribeCommand = new DelegateCommand(OnSubscribeCommand, CanSubscribe);
			UnsubscribeCommand = new DelegateCommand(OnUnsubscribeCommand, CanUnsubscribe);
			SendCommand = new DelegateCommand(OnSendCommand, CanSend);
			AddCommand = new DelegateCommand(OnAddCommand);
			LoadControlCommand = new DelegateCommand(OnLoadControlCommand);
			UnloadedControlCommand = new DelegateCommand(OnUnloadedControlCommandd);
		}
		private void OnAddCommand()
		{
			if (!String.IsNullOrEmpty(data))
			{
				decimal item = 0M;
				if (Decimal.TryParse(data, out item))
					this.Items.Add(item);
			}
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
				Debug.WriteLine(ex.Message);
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
			Debug.WriteLine("OnSendCommand");
			await hub.Invoke<IList<decimal>>("Calculate", Items);
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
				hub = connection.CreateHubProxy("CalculatorHub");
				await connection.Start();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			hub.On<decimal>("OnResult", x => OnResult(x));
		}
		public void OnResult(decimal post)
		{
			App.Current.Dispatcher.BeginInvoke((Action)(() => { Data = post.ToString(); }));
		}
	}
}
