using System;
using System.Collections.Generic;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class ExpressionTree
    {
        public string _Tag = "";
        private ExpressionTree expressionTree_0 = null;
        private ExpressionTree expressionTree_1 = null;
        private ExpressionTree expressionTree_2 = null;
        private ExpressionTree expressionTree_3 = null;

        public object Calculate(SortedList<string, object> sortedList_0)
        {
            if ((this.LeftTree == null) && (this.RightTree == null))
            {
                if (sortedList_0.ContainsKey(this.Tag))
                {
                    return sortedList_0[this.Tag];
                }
                string str = this.Tag.ToString();
                if ((str[0] == '\'') && (str[str.Length - 1] == '\''))
                {
                    return str.Substring(1, str.Length - 2);
                }
                return this.Tag;
            }
            if ((this.LeftTree != null) || (this.RightTree == null))
            {
                if ((this.LeftTree != null) && (this.RightTree != null))
                {
                    object obj4 = this.LeftTree.Calculate(sortedList_0);
                    if (this.Tag == "?")
                    {
                        bool flag = false;
                        if (obj4 is int)
                        {
                            flag = Convert.ToInt32(obj4) == 1;
                        }
                        if (flag)
                        {
                            return this.RightTree.LeftTree.Calculate(sortedList_0);
                        }
                        return this.RightTree.RightTree.Calculate(sortedList_0);
                    }
                    object obj5 = this.RightTree.Calculate(sortedList_0);
                    switch (this.Tag)
                    {
                        case "+":
                            if (!(obj4 is double) || !(obj5 is double))
                            {
                                return string.Format("{0}{1}", obj4, obj5);
                            }
                            return (Convert.ToDouble(obj4) + Convert.ToDouble(obj5));

                        case "-":
                            if (!(obj4 is double) || !(obj5 is double))
                            {
                                return 0;
                            }
                            return (Convert.ToDouble(obj4) - Convert.ToDouble(obj5));

                        case "*":
                            if (!(obj4 is double) || !(obj5 is double))
                            {
                                return 0;
                            }
                            return (Convert.ToDouble(obj4)*Convert.ToDouble(obj5));

                        case "/":
                            if (!(obj4 is double) || !(obj5 is double))
                            {
                                return 0;
                            }
                            return (Convert.ToDouble(obj4)/Convert.ToDouble(obj5));

                        case "%":
                            if (!(obj4 is double) || !(obj5 is double))
                            {
                                return 0;
                            }
                            return (Convert.ToDouble(obj4)%Convert.ToDouble(obj5));

                        case "<":
                            if (!(obj4 is double) || !(obj5 is double))
                            {
                                return (string.Compare(obj4.ToString(), obj5.ToString()) < 0);
                            }
                            return ((Convert.ToDouble(obj4) < Convert.ToDouble(obj5)) ? 1 : 0);

                        case "<=":
                            if (!(obj4 is double) || !(obj5 is double))
                            {
                                return (string.Compare(obj4.ToString(), obj5.ToString()) <= 0);
                            }
                            return ((Convert.ToDouble(obj4) <= Convert.ToDouble(obj5)) ? 1 : 0);

                        case ">":
                            if (!(obj4 is double) || !(obj5 is double))
                            {
                                return (string.Compare(obj4.ToString(), obj5.ToString()) > 0);
                            }
                            return ((Convert.ToDouble(obj4) > Convert.ToDouble(obj5)) ? 1 : 0);

                        case ">=":
                            if (!(obj4 is double) || !(obj5 is double))
                            {
                                return (string.Compare(obj4.ToString(), obj5.ToString()) >= 0);
                            }
                            return ((Convert.ToDouble(obj4) >= Convert.ToDouble(obj5)) ? 1 : 0);

                        case "!=":
                            if (!(obj4 is double) || !(obj5 is double))
                            {
                                return (string.Compare(obj4.ToString(), obj5.ToString()) != 0);
                            }
                            return ((Convert.ToDouble(obj4) != Convert.ToDouble(obj5)) ? 1 : 0);

                        case "==":
                            if (!(obj4 is double) || !(obj5 is double))
                            {
                                return (string.Compare(obj4.ToString(), obj5.ToString()) == 0);
                            }
                            return ((Convert.ToDouble(obj4) == Convert.ToDouble(obj5)) ? 1 : 0);

                        case "&&":
                            try
                            {
                                if ((Convert.ToInt32(obj4) == 1) && (Convert.ToInt32(obj5) == 1))
                                {
                                    return 1;
                                }
                                return 0;
                            }
                            catch
                            {
                            }
                            return 0;

                        case "||":
                            try
                            {
                                if ((Convert.ToInt32(obj4) == 1) || (Convert.ToInt32(obj5) == 1))
                                {
                                    return 1;
                                }
                                return 0;
                            }
                            catch
                            {
                            }
                            return 0;
                    }
                    return 0;
                }
            }
            else
            {
                object obj3 = this.expressionTree_1.Calculate(sortedList_0);
                if (obj3 is double)
                {
                    double num3;
                    double a = Convert.ToDouble(obj3);
                    switch (this._Tag.ToLower())
                    {
                        case "sin":
                            return Math.Sin(a);

                        case "cos":
                            return Math.Cos(a);

                        case "log10":
                            return Math.Log10(a);

                        case "tan":
                            return Math.Tan(a);

                        case "sqrt":
                            return Math.Sqrt(a);

                        case "exp":
                            return Math.Exp(a);

                        case "floor":
                            return Math.Floor(a);

                        case "pow":
                            num3 = 2.0;
                            if (this.expressionTree_3 != null)
                            {
                                try
                                {
                                    num3 = Convert.ToDouble(this.expressionTree_3.Calculate(sortedList_0));
                                }
                                catch
                                {
                                }
                            }
                            return Math.Pow(a, num3);

                        case "log":
                            num3 = 10.0;
                            if (this.expressionTree_3 != null)
                            {
                                try
                                {
                                    num3 = Convert.ToDouble(this.expressionTree_3.Calculate(sortedList_0));
                                }
                                catch
                                {
                                }
                            }
                            return Math.Log(a, num3);

                        case "abs":
                            return Math.Abs(a);

                        case "acos":
                            return Math.Acos(a);

                        case "asin":
                            return Math.Asin(a);

                        case "atan":
                            return Math.Atan(a);

                        case "atan2":
                            num3 = 10.0;
                            if (this.expressionTree_3 != null)
                            {
                                try
                                {
                                    num3 = Convert.ToDouble(this.expressionTree_3.Calculate(sortedList_0));
                                }
                                catch
                                {
                                }
                            }
                            return Math.Atan2(a, num3);

                        case "ceiling":
                            return Math.Ceiling(a);

                        case "cosh":
                            return Math.Cosh(a);

                        case "ieeeremainder":
                            num3 = 10.0;
                            if (this.expressionTree_3 != null)
                            {
                                try
                                {
                                    num3 = Convert.ToDouble(this.expressionTree_3.Calculate(sortedList_0));
                                }
                                catch
                                {
                                }
                            }
                            return Math.IEEERemainder(a, num3);

                        case "max":
                            num3 = 10.0;
                            if (this.expressionTree_3 == null)
                            {
                                return a;
                            }
                            try
                            {
                                num3 = Convert.ToDouble(this.expressionTree_3.Calculate(sortedList_0));
                            }
                            catch
                            {
                            }
                            return Math.Max(a, num3);

                        case "min":
                            num3 = 10.0;
                            if (this.expressionTree_3 == null)
                            {
                                return a;
                            }
                            try
                            {
                                num3 = Convert.ToDouble(this.expressionTree_3.Calculate(sortedList_0));
                            }
                            catch
                            {
                            }
                            return Math.Min(a, num3);

                        case "sinh":
                            return Math.Sinh(a);

                        case "truncate":
                            return Math.Truncate(a);
                    }
                }
            }
            return 0;
        }

        private bool method_0(char char_0)
        {
            return ((((char_0 == '+') || (char_0 == '-')) || (char_0 == '*')) || (char_0 == '/'));
        }

        public ExpressionTree LeftTree
        {
            get { return this.expressionTree_0; }
            set
            {
                this.expressionTree_0 = value;
                this.expressionTree_0.Parent = this;
            }
        }

        public ExpressionTree Parent
        {
            get { return this.expressionTree_2; }
            set { this.expressionTree_2 = value; }
        }

        public ExpressionTree RightTree
        {
            get { return this.expressionTree_1; }
            set
            {
                this.expressionTree_1 = value;
                this.expressionTree_1.Parent = this;
            }
        }

        public ExpressionTree SecondTree
        {
            get { return this.expressionTree_3; }
            set { this.expressionTree_3 = value; }
        }

        public string Tag
        {
            get { return this._Tag; }
            set { this._Tag = value; }
        }
    }
}