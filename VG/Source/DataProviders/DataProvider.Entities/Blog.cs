using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataProvider.Entities
{
	public class Blog : NotificationEntity
	{
		private int _id;
		private string _title;

		public int Id
		{
			get { return _id; }
			set { SetWithNotify(value, ref _id); }
		}

		public string Title
		{
			get { return _title; }
			set { SetWithNotify(value, ref _title); }
		}

		//public ICollection<Post> Posts { get; } = new ObservableHashSet<Post>();
	}

	public class NotificationEntity : INotifyPropertyChanged, INotifyPropertyChanging
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public event PropertyChangingEventHandler PropertyChanging;

		protected void SetWithNotify<T>(T value, ref T field, [CallerMemberName] string propertyName = "")
		{
			if (!Equals(field, value))
			{
				PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
				field = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
//modelBuilder.Entity<Blog>()
//   .HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotifications);

//modelBuilder.Entity<Blog>()
//    .HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);