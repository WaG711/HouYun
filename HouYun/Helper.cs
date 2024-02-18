namespace HouYun
{
    public static class Helper
    {
        public static string TimeAgo(DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime;

            if (timeSpan.TotalMinutes < 1)
            {
                return "менее минуты назад";
            }
            if (timeSpan.TotalMinutes < 60)
            {
                var minutes = (int)timeSpan.TotalMinutes;
                return $"{minutes} {(minutes == 1 ? "минуту" : minutes < 5 ? "минуты" : "минут")} назад";
            }
            if (timeSpan.TotalHours < 24)
            {
                var hours = (int)timeSpan.TotalHours;
                if (hours == 1)
                {
                    return "час назад";
                }
                else if (hours == 2 || hours == 3 || hours == 4)
                {
                    return $"{hours} часа назад";
                }
                else
                {
                    return $"{hours} часов назад";
                }
            }
            if (timeSpan.TotalDays < 30)
            {
                var days = (int)timeSpan.TotalDays;
                if (days == 1)
                {
                    return "день назад";
                }
                else if (days == 2 || days == 3 || days == 4)
                {
                    return $"{days} дня назад";
                }
                else
                {
                    return $"{days} дней назад";
                }
            }
            if (timeSpan.TotalDays < 365)
            {
                var months = (int)(timeSpan.TotalDays / 30);
                if (months == 1)
                {
                    return "месяц назад";
                }
                else if (months == 2 || months == 3 || months == 4)
                {
                    return $"{months} месяца назад";
                }
                else
                {
                    return $"{months} месяцев назад";
                }
            }
            var years = (int)(timeSpan.TotalDays / 365);
            if (years == 1)
            {
                return "год назад";
            }
            else if (years == 2 || years == 3 || years == 4)
            {
                return $"{years} года назад";
            }
            else
            {
                return $"{years} лет назад";
            }
        }

        public static string FormatViews(int views)
        {
            if (views < 10000)
            {
                return $"{views} {(views == 1 ? "просмотр" : views < 5 ? "просмотра" : "просмотров")}";
            }
            else if (views < 1000000)
            {
                double thousands = views / 1000.0;
                return $"{thousands:0.#}K просмотров";
            }
            else
            {
                double millions = views / 1000000.0;
                return $"{millions:0.#}M просмотров";
            }
        }

        public static string FormatSubscribers(int count)
        {
            if (count == 0)
            {
                return "0 подписчиков";
            }
            else if (count == 1)
            {
                return "1 подписчик";
            }
            else if (count < 1000)
            {
                return $"{count} подписчик{(count != 1 ? "ов" : "")}";
            }
            else if (count < 1000000)
            {
                return $"{Math.Round((double)count / 1000, 1)}K подписчиков";
            }
            else
            {
                return $"{Math.Round((double)count / 1000000, 1)}M подписчиков";
            }
        }
    }
}
