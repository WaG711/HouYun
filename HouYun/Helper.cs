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
                    return $"{views} {GetProperForm(views, "просмотр", "просмотра", "просмотров")}";
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
                    {
                        return $"{count} {GetProperForm(count, "подписчик", "подписчика", "подписчиков")}";
                    }
                    else if (count < 1000000)
                    {
                        return $"{count / 1000.0:F1}K {GetProperForm(count / 1000, "тысячный", "тысячных", "тысяч")}";
                    }
                    else
                    {
                        return $"{count / 1000000.0:F1}M {GetProperForm(count / 1000000, "миллионный", "миллионных", "миллионов")}";
                    }
            }
        }

        private static string FormatTime(int value, string singular, string few, string many, string suffix)
        {
            string result;

            if (value >= 11 && value <= 19)
            {
                result = $"{value} {many}";
            }
            else
            {
                int lastDigit = value % 10;
                switch (lastDigit)
                {
                    case 1:
                        result = $"{value} {singular}";
                        break;
                    case 2:
                    case 3:
                    case 4:
                        result = $"{value} {few}";
                        break;
                    default:
                        result = $"{value} {many}";
                        break;
                }
            }

            return $"{result} {suffix}";
        }

        private static string GetProperForm(int value, string form1, string form2, string form3)
        {
            if (value % 10 == 1 && value % 100 != 11)
            {
                return form1;
            }
            else if (value % 10 >= 2 && value % 10 <= 4 && (value % 100 < 10 || value % 100 >= 20))
            {
                return form2;
            }
            else
            {
                return form3;
            }
        }
    }
}
