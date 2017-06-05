using System;
using System.ComponentModel.DataAnnotations;
using Prism.Mvvm;
 
namespace GridSyncEntity
{
	public class Person : BindableBase
	{
		private int id; 
		public int Id
		{
			get { return id; }
			set { SetProperty(ref id, value); }
		}

		private string name = "";
		public string Name
		{
			get { return name; }
			set { SetProperty(ref name, value); }
		}

		private DateTime dateOfBirth;
		[DataType(DataType.Date)]
		public DateTime DateOfBirth
		{
			get { return dateOfBirth; }
			set { SetProperty(ref dateOfBirth, value); }
		}

		public Person() { }
		public Person(int n, string name, DateTime dateOfBirth)
		{
			this.Id = n;
			this.Name = name;
			this.DateOfBirth = dateOfBirth;
		}

	}
}
