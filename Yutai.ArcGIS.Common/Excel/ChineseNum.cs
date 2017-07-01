namespace Yutai.ArcGIS.Common.Excel
{
    public class ChineseNum
    {
        public static string GetChineseNum(string string_0)
        {
            ChineseNum num = new ChineseNum();
            return num.method_4(string_0);
        }

        public static string GetUpperMoney(double double_0)
        {
            ChineseNum num = new ChineseNum();
            return num.method_5(double_0);
        }

        private char method_0(char char_0)
        {
            string str = "零一二三四五六七八九";
            string str2 = "0123456789";
            return str[str2.IndexOf(char_0)];
        }

        private string method_1(string string_0)
        {
            string[] strArray2 = new string[] {"", "十", "百", "千"};
            string str = "";
            int startIndex = string_0.Length - 1;
            while (startIndex >= 0)
            {
                if (string_0[startIndex] == '0')
                {
                    str = this.method_0(string_0[startIndex]) + str;
                }
                else
                {
                    str = this.method_0(string_0[startIndex]) + strArray2[(string_0.Length - 1) - startIndex] + str;
                }
                startIndex--;
            }
            while ((startIndex = str.IndexOf("零零")) != -1)
            {
                str = str.Remove(startIndex, 1);
            }
            if ((str[str.Length - 1] == 38646) && (str.Length > 1))
            {
                str = str.Remove(str.Length - 1, 1);
            }
            if ((str.Length >= 2) && (str.Substring(0, 2) == "一十"))
            {
                str = str.Remove(0, 1);
            }
            return str;
        }

        private string method_2(string string_0)
        {
            string str;
            int length = string_0.Length;
            if (length <= 4)
            {
                str = this.method_1(string_0);
            }
            else
            {
                string str2;
                if (length <= 8)
                {
                    str = this.method_1(string_0.Substring(0, length - 4)) + "万";
                    str2 = this.method_1(string_0.Substring(length - 4, 4));
                    if ((str2.IndexOf("千") == -1) && (str2 != ""))
                    {
                        str = str + "零" + str2;
                    }
                    else
                    {
                        str = str + str2;
                    }
                }
                else
                {
                    str = this.method_1(string_0.Substring(0, length - 8)) + "亿";
                    str2 = this.method_1(string_0.Substring(length - 8, 4));
                    if ((str2.IndexOf("千") == -1) && (str2 != ""))
                    {
                        str = str + "零" + str2;
                    }
                    else
                    {
                        str = str + str2;
                    }
                    str = str + "万";
                    str2 = this.method_1(string_0.Substring(length - 4, 4));
                    if ((str2.IndexOf("千") == -1) && (str2 != ""))
                    {
                        str = str + "零" + str2;
                    }
                    else
                    {
                        str = str + str2;
                    }
                }
            }
            int index = str.IndexOf("零万");
            if (index != -1)
            {
                str = str.Remove(index + 1, 1);
            }
            while ((index = str.IndexOf("零零")) != -1)
            {
                str = str.Remove(index, 1);
            }
            if ((str[str.Length - 1] == 38646) && (str.Length > 1))
            {
                str = str.Remove(str.Length - 1, 1);
            }
            return str;
        }

        private string method_3(string string_0)
        {
            string str = "";
            for (int i = 0; i < string_0.Length; i++)
            {
                str = str + this.method_0(string_0[i]);
            }
            return str;
        }

        private string method_4(string string_0)
        {
            if (string_0.Length == 0)
            {
                return "";
            }
            string str2 = "";
            if (string_0[0] == '-')
            {
                str2 = "负";
                string_0 = string_0.Remove(0, 1);
            }
            char ch = string_0[0];
            if (ch.ToString() == ".")
            {
                string_0 = "0" + string_0;
            }
            ch = string_0[string_0.Length - 1];
            if (ch.ToString() == ".")
            {
                string_0 = string_0.Remove(string_0.Length - 1, 1);
            }
            if (string_0.IndexOf(".") > -1)
            {
                str2 = str2 + this.method_2(string_0.Substring(0, string_0.IndexOf("."))) + "点" +
                       this.method_3(string_0.Substring(string_0.IndexOf(".") + 1));
            }
            else
            {
                str2 = str2 + this.method_2(string_0);
            }
            return str2;
        }

        private string method_5(double double_0)
        {
            if (double_0 == 0.0)
            {
                return "";
            }
            string str2 = double_0.ToString("#0.00");
            if (str2.IndexOf(".") > 0)
            {
                str2 = str2.Replace(".", "");
            }
            if (str2.Substring(0, 1) == "0")
            {
                str2 = str2.Remove(0, 1);
            }
            str2 = this.method_6(str2);
            if (str2.Length == 0)
            {
                return "";
            }
            if (double_0 < 0.0)
            {
                str2 = "负" + str2;
            }
            str2 =
                str2.Replace("0", "零")
                    .Replace("1", "壹")
                    .Replace("2", "贰")
                    .Replace("3", "叁")
                    .Replace("4", "肆")
                    .Replace("5", "伍")
                    .Replace("6", "陆")
                    .Replace("7", "柒")
                    .Replace("8", "捌")
                    .Replace("9", "玖")
                    .Replace("M", "亿")
                    .Replace("W", "万")
                    .Replace("S", "仟")
                    .Replace("H", "佰")
                    .Replace("T", "拾")
                    .Replace("Y", "圆")
                    .Replace("J", "角")
                    .Replace("F", "分");
            if (str2.Substring(str2.Length - 1, 1) != "分")
            {
                str2 = str2 + "整";
            }
            return str2;
        }

        private string method_6(string string_0)
        {
            string[] strArray = new string[4];
            string str = "";
            bool flag = false;
            strArray[0] = "";
            strArray[1] = "T";
            strArray[2] = "H";
            strArray[3] = "S";
            for (int i = 1; i <= string_0.Length; i++)
            {
                int num2 = string_0.Length - i;
                string str2 = string_0.Substring(i - 1, 1);
                if ((str2 != "0") && (num2 > 1))
                {
                    str = str + str2 + strArray[(num2 - 2)%4];
                }
                if (!(!(str2 == "0") || flag))
                {
                    str = str + "0";
                    flag = true;
                }
                switch (num2)
                {
                    case 14:
                        if (str.Substring(str.Length - 1) == "0")
                        {
                            str = str.Substring(0, str.Length - 1) + "W0";
                        }
                        else
                        {
                            str = str + "W";
                        }
                        break;

                    case 2:
                        if (str.Substring(str.Length - 1, 1) == "0")
                        {
                            str = str.Substring(0, str.Length - 1) + "Y0";
                        }
                        else
                        {
                            str = str + "Y";
                        }
                        break;
                }
                if (num2 == 6)
                {
                    if (str.Length > 2)
                    {
                        if (str.Substring(str.Length - 2) != "M0")
                        {
                            if (str.Substring(str.Length - 1) == "0")
                            {
                                str = str.Substring(0, str.Length - 1) + "W0";
                            }
                            else
                            {
                                str = str + "W";
                            }
                        }
                    }
                    else if (str.Substring(str.Length - 1) == "0")
                    {
                        str = str.Substring(0, str.Length - 1) + "W0";
                    }
                    else
                    {
                        str = str + "W";
                    }
                }
                if (num2 == 10)
                {
                    if (str.Substring(str.Length - 1) == "0")
                    {
                        str = str.Substring(0, str.Length - 1) + "M0";
                    }
                    else
                    {
                        str = str + "M";
                    }
                }
                if ((num2 == 0) && (str2 != "0"))
                {
                    str = str + str2 + "F";
                }
                if ((num2 == 1) && (str2 != "0"))
                {
                    str = str + str2 + "J";
                }
                if (str2 != "0")
                {
                    flag = false;
                }
            }
            if ((str.Substring(0, 1) == "1") && (str.Substring(1, 1) == strArray[1]))
            {
                str = str.Substring(1);
            }
            if (str.Substring(str.Length - 1, 1) == "0")
            {
                str = str.Substring(0, str.Length - 1);
            }
            if (str.Substring(0, 1) == "0")
            {
                str = str.Substring(1);
            }
            if ((((str.Substring(str.Length - 1, 1) == "M") || (str.Substring(str.Length - 1, 1) == "W")) ||
                 ((str.Substring(str.Length - 1, 1) == "S") || (str.Substring(str.Length - 1, 1) == "H"))) ||
                (str.Substring(str.Length - 1, 1) == "T"))
            {
                str = str + "Y";
            }
            return str;
        }
    }
}