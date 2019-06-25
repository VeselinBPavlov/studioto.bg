namespace Studio.Domain.Entities
{
    public class WorkTime
    {
        public string DayName { get; set; }

        public string OpenTime { get; set; }

        public string CloseTime { get; set; }

        public string BreakFrom { get; set; }

        public string BreakTo { get; set; }

        public bool IsWorkingDay { get; set; }
    }
}