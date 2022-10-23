namespace IsakUtils
{
    public class TimeString
    {
        private int hours;
        private int minutes;
        private int seconds;
        private int milliseconds;
        public TimeString(float timeFloat)
        {
            milliseconds = ((int)(timeFloat * 1000)) % 1000;
            seconds = ((int)timeFloat) % 60;
            minutes = ((int)(timeFloat / 60)) % 60;
            hours = (int)((timeFloat / 60) / 60);
        }

        public string Generate()
        {
            if (hours > 0)
            {
                return HoursToString() + ":" + MinutesToString(true) + ":" + SecondsToString(true) + ":" + MillisecondsToString();
            }
            else if (minutes > 0)
            {
                return MinutesToString(false) + ":" + SecondsToString(true) + ":" + MillisecondsToString();
            }
            else
            {
                return SecondsToString(false) + ":" + MillisecondsToString();
            }
        }

        private string HoursToString()
        {
            if (hours > 9)
            {
                return hours.ToString("00");
            }
            else
            {
                return hours.ToString("00");
            }
        }

        private string MinutesToString(bool forceDoubleDigit)
        {
            if (forceDoubleDigit || minutes > 9)
            {
                return minutes.ToString("00");
            }
            else
            {
                return minutes.ToString("00");
            }
        }

        private string SecondsToString(bool forceDoubleDigit)
        {
            if (forceDoubleDigit || minutes > 9)
            {
                return seconds.ToString("00");
            }
            else
            {
                return seconds.ToString("00");
            }
        }

        private string MillisecondsToString()
        {
            return milliseconds.ToString("000");
        }
    }

}