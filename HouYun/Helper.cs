namespace HouYun
{
    public static class Helper
    {
        public static string TimeAgo(DateTime dateTime)
        {
            var elapsedTime = DateTime.UtcNow - dateTime;

            if (elapsedTime.TotalMinutes < 1)
            {
                return "менее минуты назад";
            }

            switch (elapsedTime)
            {
                case var ts when ts.TotalMinutes < 60:
                    return FormatTime((int)ts.TotalMinutes, "минуту", "минуты", "минут", "назад");
                case var ts when ts.TotalHours < 24:
                    return FormatTime((int)ts.TotalHours, "час", "часа", "часов", "назад");
                case var ts when ts.TotalDays < 30:
                    return FormatTime((int)ts.TotalDays, "день", "дня", "дней", "назад");
                case var ts when ts.TotalDays < 365:
                    return FormatTime((int)(ts.TotalDays / 30), "месяц", "месяца", "месяцев", "назад");
                default:
                    return FormatTime((int)(elapsedTime.TotalDays / 365), "год", "года", "лет", "назад");
            }
        }

        public static string FormatViews(int views)
        {
            switch (views)
            {
                case 0:
                    return "0 просмотров";
                case < 1000:
                    return $"{views} {(views == 1 ? "просмотр" : "просмотра")}";
                case < 1000000:
                    return $"{views / 1000.0:#.#}K просмотров";
                default:
                    return $"{views / 1000000.0:#.#}M просмотров";
            }
        }

        public static string FormatSubscribers(int count)
        {
            switch (count)
            {
                case 0:
                    return "0 подписчиков";
                case 1:
                    return "1 подписчик";
                default:
                    if (count < 1000)
                        return $"{count} подписчик{(count != 1 ? "ов" : "")}";
                    else if (count < 1000000)
                        return $"{count / 1000.0:F1}K подписчиков";
                    else
                        return $"{count / 1000000.0:F1}M подписчиков";
            }
        }

        private static string FormatTime(int value, string singular, string few, string many, string suffix)
        {
            string result;

            if (value == 1)
            {
                result = singular;
            }
            else if (value >= 2 && value <= 4)
            {
                result = $"{value} {few}";
            }
            else
            {
                result = $"{value} {many}";
            }

            return $"{result} {suffix}";
        }
    }
}
