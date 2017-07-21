using System;
using System.Globalization;


namespace Ecommittees.Model
{
    public class MeetingItem
    {
        public string Title { get; set; }
        private DateTime MeetingTimeFrom { get; set; } = default(DateTime);
        private DateTime MeetingTimeTill { get; set; } = default(DateTime);
        private CultureInfo _CultureFormat { get; } = CultureInfo.CurrentCulture;

        public string MeetingTime
        {
            get
            {
                return $"{MeetingTimeFrom.ToString("MMMM dd, yyyy", _CultureFormat)}\n{MeetingTimeFrom.ToString("t", _CultureFormat)} - {MeetingTimeTill.ToString("t", _CultureFormat)}";
            }
        }

        public string MeetingDateString => $"{MeetingTimeFrom.ToString("MMMM dd, yyyy", _CultureFormat)}";
        public string MeetingTimeString => $"{MeetingTimeFrom.ToString("t", _CultureFormat)} - {MeetingTimeTill.ToString("t", _CultureFormat)}";

        public string Description { get; set; }

        public string Location { get; set; }

        public int RecentActivitiesCount { get; set; }
        public int RecentCommentsCount { get; set; }

        public MeetingItem()
        {

        }
    }
}
