using System;

namespace MoM.Module.Dtos
{
    public class HolidayWeekendWorkdayDto
    {
        public DateTime date { get; set; }
        public bool isHoliday { get; set; }
        public bool isWeekend { get; set; }
        public bool isWorkDay { get; set; }
        public string title { get; set; }
        public string shortWeekDayName { get; set; }

        public HolidayWeekendWorkdayDto() { }
        public HolidayWeekendWorkdayDto(
            DateTime Date,
            bool IsHoliday,
            bool IsWeekend,
            bool IsWorkDay,
            string Title,
            string ShortWeekDayName
            )
        {
            date = Date;
            isHoliday = IsHoliday;
            isWeekend = IsWeekend;
            isWorkDay = IsWorkDay;
            title = Title;
            shortWeekDayName = ShortWeekDayName;
        }
    }
}
