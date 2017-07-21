using System.Linq;
using System.Collections.Generic;
using Ecommittees.Model;
using Ecommittees.DataContext;

namespace WebHost.WebAPI
{
	public class MembersController
    {
		private readonly IDataContext dataContext;

		public MembersController(DataContext dataContext)
		{
			this.dataContext = dataContext;
		}

		public string Hello(string name) => $"Hello, {name}";

		public IList<Committee> GetCommittees()
		{
			return this.dataContext.Committees.ToList();
		}

		public IList<Meeting> GetMeetings()
		{
			return this.dataContext.Meetings.ToList();
		}

		public IList<Conversation> GetConversations()
		{
			return this.dataContext.Conversations.ToList();
		}

		public IList<Conversation> GetConversations(int documentId)
		{
			return this.dataContext.Conversations.Where(p => p.DocumentId == documentId).ToList();
		}
	}
}
