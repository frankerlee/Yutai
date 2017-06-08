using System;
using System.Collections.Generic;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class ExpressionCalculator
    {
        private bool bool_0 = false;
        private ExpressionTree expressionTree_0;
        private ExpressionTree expressionTree_1 = null;
        private List<string> list_0;
        private SortedList<string, object> sortedList_0 = new SortedList<string, object>();

        public ExpressionTree BulidTree(ExpressionTree expressionTree_2, string string_0)
        {
            int num2;
            int num3;
            ExpressionTree tree2;
            ExpressionTree tree5;
            string str2;
            int num = 0;
            if (!this.IsFuction(string_0, out num))
            {
                if (string_0[0] != '(')
                {
                    ExpressionTree tree7;
                    int startIndex = 0;
                    num3 = 0;
                    while (num3 < string_0.Length)
                    {
                        ExpressionTree tree6;
                        if (this.method_0(string_0[num3]))
                        {
                            tree6 = new ExpressionTree {
                                Tag = string.Format("{0}", string_0[num3])
                            };
                            if (num3 == 0)
                            {
                                str2 = null;
                            }
                            else
                            {
                                str2 = string_0.Substring(startIndex, num3 - startIndex);
                            }
                            tree7 = new ExpressionTree {
                                Tag = str2
                            };
                            foreach (string str3 in str2.Split(new char[] { ',' }))
                            {
                                if (this.method_3(str3) && !this.sortedList_0.ContainsKey(str3))
                                {
                                    this.sortedList_0.Add(str3, null);
                                }
                            }
                            if (expressionTree_2 == null)
                            {
                                tree6.LeftTree = tree7;
                                expressionTree_2 = tree6;
                            }
                            else if (this.method_2(tree6.Tag, expressionTree_2.Tag) == 1)
                            {
                                expressionTree_2.RightTree = tree6;
                                tree6.LeftTree = tree7;
                                expressionTree_2 = tree6;
                            }
                            else
                            {
                                expressionTree_2.RightTree = tree7;
                                if (expressionTree_2.Parent != null)
                                {
                                    expressionTree_2.Parent.RightTree = tree6;
                                }
                                tree6.LeftTree = expressionTree_2;
                                expressionTree_2 = tree6;
                            }
                            break;
                        }
                        if (this.method_1(string_0[num3]))
                        {
                            char ch = string_0[num3];
                            string str4 = ch.ToString();
                            if (this.method_1(string_0[num3 + 1]))
                            {
                                ch = string_0[num3 + 1];
                                str4 = str4 + ch.ToString();
                                num3++;
                            }
                            tree6 = new ExpressionTree {
                                Tag = str4
                            };
                            if (num3 == 0)
                            {
                                str2 = "0";
                            }
                            else
                            {
                                str2 = string_0.Substring(startIndex, num3 - startIndex);
                            }
                            tree7 = new ExpressionTree {
                                Tag = str2
                            };
                            foreach (string str3 in str2.Split(new char[] { ',' }))
                            {
                                if (this.method_3(str3) && !this.sortedList_0.ContainsKey(str3))
                                {
                                    this.sortedList_0.Add(str3, null);
                                }
                            }
                            if (expressionTree_2 == null)
                            {
                                tree6.LeftTree = tree7;
                                expressionTree_2 = tree6;
                            }
                            else if (this.method_2(tree6.Tag, expressionTree_2.Tag) == 1)
                            {
                                expressionTree_2.RightTree = tree6;
                                tree6.LeftTree = tree7;
                                expressionTree_2 = tree6;
                            }
                            else
                            {
                                expressionTree_2.RightTree = tree7;
                                if (expressionTree_2.Parent != null)
                                {
                                    expressionTree_2.Parent.RightTree = tree6;
                                }
                                tree6.LeftTree = expressionTree_2;
                                expressionTree_2 = tree6;
                            }
                            break;
                        }
                        num3++;
                    }
                    if (num3 == string_0.Length)
                    {
                        tree7 = new ExpressionTree {
                            Tag = string_0
                        };
                        if (expressionTree_2 == null)
                        {
                            expressionTree_2 = tree7;
                        }
                        else
                        {
                            expressionTree_2.RightTree = tree7;
                        }
                        foreach (string str3 in string_0.Split(new char[] { ',' }))
                        {
                            if (this.method_3(str3) && !this.sortedList_0.ContainsKey(str3))
                            {
                                this.sortedList_0.Add(str3, null);
                            }
                        }
                        return this.FindRootTree(expressionTree_2);
                    }
                    string str5 = string_0.Substring(num3 + 1);
                    this.BulidTree(expressionTree_2, str5);
                    goto Label_0695;
                }
                num2 = 1;
                num3 = 0;
                tree2 = null;
                for (num3 = 1; num3 < string_0.Length; num3++)
                {
                    if (string_0[num3] == ')')
                    {
                        num2--;
                        if (num2 != 0)
                        {
                            continue;
                        }
                        string str = string_0.Substring(1, (num3 - num) - 1);
                        tree2 = this.BulidTree(null, str);
                        break;
                    }
                    if (string_0[num3] == '(')
                    {
                        num2++;
                    }
                }
            }
            else
            {
                ExpressionTree tree = new ExpressionTree {
                    Tag = string_0.Substring(0, num)
                };
                num2 = 1;
                num3 = 0;
                num3 = num + 1;
                while (num3 < string_0.Length)
                {
                    if (string_0[num3] == ')')
                    {
                        num2--;
                        if (num2 != 0)
                        {
                            goto Label_0064;
                        }
                        string[] strArray = string_0.Substring(num + 1, (num3 - num) - 1).Split(new char[] { ',' });
                        tree2 = this.BulidTree(null, strArray[0]);
                        tree.RightTree = tree2;
                        if (strArray.Length == 2)
                        {
                            ExpressionTree tree3 = this.BulidTree(null, strArray[1]);
                            tree.SecondTree = tree3;
                        }
                        break;
                    }
                    if (string_0[num3] == '(')
                    {
                        num2++;
                    }
                Label_0064:
                    num3++;
                }
                if (num3 == (string_0.Length - 1))
                {
                    if (expressionTree_2 != null)
                    {
                        expressionTree_2.RightTree = tree;
                    }
                    else
                    {
                        expressionTree_2 = tree;
                    }
                    return this.FindRootTree(expressionTree_2);
                }
                tree5 = new ExpressionTree();
                tree5.Tag = string_0[num3 + 1].ToString();
                if (expressionTree_2 != null)
                {
                    if (this.method_2(tree5.Tag, expressionTree_2.Tag) == 1)
                    {
                        expressionTree_2.RightTree = tree5;
                        tree5.LeftTree = tree;
                        expressionTree_2 = tree5;
                    }
                    else
                    {
                        expressionTree_2.RightTree = tree;
                        if (expressionTree_2.Parent != null)
                        {
                            expressionTree_2.Parent.RightTree = tree5;
                        }
                        tree5.LeftTree = expressionTree_2;
                        expressionTree_2 = tree5;
                    }
                }
                else
                {
                    tree5.LeftTree = tree;
                    expressionTree_2 = tree5;
                }
                str2 = string_0.Substring(num3 + 2);
                this.BulidTree(expressionTree_2, str2);
                goto Label_0695;
            }
            if (num3 == (string_0.Length - 1))
            {
                if (expressionTree_2 != null)
                {
                    expressionTree_2.RightTree = tree2;
                }
                else
                {
                    expressionTree_2 = tree2;
                }
                return this.FindRootTree(expressionTree_2);
            }
            tree5 = new ExpressionTree();
            tree5.Tag = string_0[num3 + 1].ToString();
            if (expressionTree_2 != null)
            {
                if (this.method_2(tree5.Tag, expressionTree_2.Tag) == 1)
                {
                    expressionTree_2.RightTree = tree5;
                    tree5.LeftTree = tree2;
                    expressionTree_2 = tree5;
                }
                else
                {
                    expressionTree_2.RightTree = tree2;
                    if (expressionTree_2.Parent != null)
                    {
                        expressionTree_2.Parent.RightTree = tree5;
                    }
                    tree5.LeftTree = expressionTree_2;
                    expressionTree_2 = tree5;
                }
            }
            else
            {
                tree5.LeftTree = tree2;
                expressionTree_2 = tree5;
            }
            str2 = string_0.Substring(num3 + 2);
            this.BulidTree(expressionTree_2, str2);
        Label_0695:
            return this.FindRootTree(expressionTree_2);
        }

        public object Calculate()
        {
            try
            {
                return this.expressionTree_1.Calculate(this.sortedList_0);
            }
            catch (Exception)
            {
            }
            return 0;
        }

        public ExpressionTree FindRootTree(ExpressionTree expressionTree_2)
        {
            if (expressionTree_2.Parent != null)
            {
                do
                {
                    expressionTree_2 = expressionTree_2.Parent;
                }
                while (expressionTree_2.Parent != null);
            }
            return expressionTree_2;
        }

        public void Init(string string_0)
        {
            string_0 = string_0.Replace(" ", "");
            this.sortedList_0.Clear();
            this.expressionTree_1 = this.BulidTree(null, string_0);
        }

        public bool IsFuction(string string_0, out int int_0)
        {
            int_0 = 0;
            if (((((string_0.IndexOf("sin(", StringComparison.OrdinalIgnoreCase) == 0) || (string_0.IndexOf("cos(", StringComparison.OrdinalIgnoreCase) == 0)) || ((string_0.IndexOf("tan(", StringComparison.OrdinalIgnoreCase) == 0) || (string_0.IndexOf("exp(", StringComparison.OrdinalIgnoreCase) == 0))) || (((string_0.IndexOf("pow(", StringComparison.OrdinalIgnoreCase) == 0) || (string_0.IndexOf("log(", StringComparison.OrdinalIgnoreCase) == 0)) || ((string_0.IndexOf("abs(", StringComparison.OrdinalIgnoreCase) == 0) || (string_0.IndexOf("max(", StringComparison.OrdinalIgnoreCase) == 0)))) || (string_0.IndexOf("min(", StringComparison.OrdinalIgnoreCase) == 0))
            {
                int_0 = 3;
                return true;
            }
            if (string_0.IndexOf("ceiling(", StringComparison.OrdinalIgnoreCase) == 0)
            {
                int_0 = 7;
                return true;
            }
            if ((string_0.IndexOf("floor(", StringComparison.OrdinalIgnoreCase) == 0) || (string_0.IndexOf("atan2(", StringComparison.OrdinalIgnoreCase) == 0))
            {
                int_0 = 5;
                return true;
            }
            if (string_0.IndexOf("truncate(", StringComparison.OrdinalIgnoreCase) == 0)
            {
                int_0 = 8;
                return true;
            }
            if (((((string_0.IndexOf("log10(", StringComparison.OrdinalIgnoreCase) == 0) || (string_0.IndexOf("sqrt(", StringComparison.OrdinalIgnoreCase) == 0)) || ((string_0.IndexOf("acos(", StringComparison.OrdinalIgnoreCase) == 0) || (string_0.IndexOf("asin(", StringComparison.OrdinalIgnoreCase) == 0))) || ((string_0.IndexOf("cosh(", StringComparison.OrdinalIgnoreCase) == 0) || (string_0.IndexOf("sinh(", StringComparison.OrdinalIgnoreCase) == 0))) || (string_0.IndexOf("atan(", StringComparison.OrdinalIgnoreCase) == 0))
            {
                int_0 = 4;
                return true;
            }
            if (string_0.IndexOf("ieeeremainder(", StringComparison.OrdinalIgnoreCase) == 0)
            {
                int_0 = 13;
                return true;
            }
            return false;
        }

        private bool method_0(char char_0)
        {
            return (((((char_0 == '+') || (char_0 == '-')) || ((char_0 == '*') || (char_0 == '/'))) || (char_0 == '%')) || (char_0 == '?'));
        }

        private bool method_1(char char_0)
        {
            return (((((char_0 == '>') || (char_0 == '<')) || ((char_0 == '=') || (char_0 == '!'))) || (char_0 == '|')) || (char_0 == '&'));
        }

        private int method_2(string string_0, string string_1)
        {
            if (string_0 == string_1)
            {
                return 0;
            }
            string[] array = new string[] { "?", ":" };
            string[] strArray3 = new string[] { "&&", "||" };
            string[] strArray4 = new string[] { "<", ">", "<=", ">=", "!=", "==" };
            string[] strArray5 = new string[] { "+", "-" };
            string[] strArray6 = new string[] { "*", "/", "%" };
            int num2 = -1;
            int num3 = -1;
            num2 = (Array.IndexOf<string>(array, string_0) != -1) ? 0 : -1;
            num3 = (Array.IndexOf<string>(array, string_1) != -1) ? 0 : -1;
            if (num2 == -1)
            {
                num2 = (Array.IndexOf<string>(strArray3, string_0) != -1) ? 1 : -1;
            }
            if (num3 == -1)
            {
                num3 = (Array.IndexOf<string>(strArray3, string_1) != -1) ? 1 : -1;
            }
            if (num2 == -1)
            {
                num2 = (Array.IndexOf<string>(strArray4, string_0) != -1) ? 2 : -1;
            }
            if (num3 == -1)
            {
                num3 = (Array.IndexOf<string>(strArray4, string_1) != -1) ? 2 : -1;
            }
            if (num2 == -1)
            {
                num2 = (Array.IndexOf<string>(strArray5, string_0) != -1) ? 3 : -1;
            }
            if (num3 == -1)
            {
                num3 = (Array.IndexOf<string>(strArray5, string_1) != -1) ? 3 : -1;
            }
            if (num2 == -1)
            {
                num2 = (Array.IndexOf<string>(strArray6, string_0) != -1) ? 4 : -1;
            }
            if (num3 == -1)
            {
                num3 = (Array.IndexOf<string>(strArray6, string_1) != -1) ? 4 : -1;
            }
            if (num2 == num3)
            {
                return 0;
            }
            if (num2 < num3)
            {
                return -1;
            }
            return 1;
        }

        private bool method_3(string string_0)
        {
            if ((string_0[0] == '\'') && (string_0[string_0.Length - 1] == '\''))
            {
                return false;
            }
            try
            {
                double.Parse(string_0);
                return false;
            }
            catch
            {
            }
            return true;
        }

        public SortedList<string, object> ParamList
        {
            get
            {
                return this.sortedList_0;
            }
            set
            {
                this.sortedList_0 = value;
            }
        }
    }
}

