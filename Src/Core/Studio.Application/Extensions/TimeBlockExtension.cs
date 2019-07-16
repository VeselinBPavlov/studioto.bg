namespace Studio.Application.Extensions
{
    using System;
    using System.Text;
    using Itenso.TimePeriod;

    public class TimeBlockExtension : TimeBlock
    {
        public TimeBlockExtension(DateTime x, TimeSpan y)
            : base(x, y)
        {
        }

        // Overriding toString Method
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Start.ToShortTimeString());
            sb.Append(" to ");
            sb.Append(this.End.ToShortTimeString());
            return sb.ToString();
        }
    }    
}