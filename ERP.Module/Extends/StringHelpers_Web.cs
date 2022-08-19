using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
//
namespace ERP.Module.Extends
{
    public static class StringHelpers_Web
    {
        
        private static string CreateTable(string result)
        {
            result = Regex.Replace(result, "\\w+([.]\\w+){2,10}", match =>
            {
                var count = (from c in match.Value.ToCharArray()
                             where c == '.'
                             select c).Count();

                if (count >= 2)
                {
                    int i = 0;
                    int length = match.Length;
                    int index = length;
                    while (index > 0)
                    {
                        index--;
                        if (match.Value[index] == '.')
                        {
                            i++;
                            if (i == 2)
                            {
                                return String.Format("{0}", match.Value.Substring(index + 1, length - index - 1));
                            }
                        }
                    }
                }
                return match.Value;
            });
            return result;
        }

        private static string EnumOperator(string result)
        {
            result = Regex.Replace(result, "##Enum#\\w+([.]\\w+)*,\\w+#", match =>
            {
                string temp = match.Value;
                Match match1 = Regex.Match(temp, "\\w+([.]\\w+)*,");
                if (match1.Success)
                {
                    Assembly asm = Assembly.GetExecutingAssembly();
                    string t = match1.Value.Replace(",", "");
                    Type type = asm.GetType(match1.Value.Replace(",", ""));
                    if (type != null)
                    {
                        match1 = Regex.Match(temp, ",\\w+");
                        if (match1.Success)
                        {
                            //
                            object obj = System.Enum.Parse(type, match1.Value.Replace(",", ""));
                            if (obj != null)
                                return ((byte)obj).ToString();
                        }
                    }
                }
                return match.Value;
            });
            return result;
        }

        private static string EndsWith(string result)
        {
            result = Regex.Replace(result, "EndsWith[(]\\w+([.]\\w+)*, '[%]*(,*[.-<>=+]*\\w*)*[%]*'[)]", match =>
            {
                string temp = match.Value;

                StringBuilder column = new StringBuilder();
                Match match1 = Regex.Match(temp, "[(]\\w+([.]\\w+)*,");
                if (match1.Success)
                {
                    var count = (from c in match1.Value.ToCharArray()
                                 where c == '.'
                                 select c).Count();

                    if (count >= 2)
                    {
                        int i = 0;
                        int length = match1.Length;
                        int index = length;
                        while (index > 0)
                        {
                            index--;
                            if (match1.Value[index] == '.')
                            {
                                i++;
                                if (i == 2)
                                {
                                    column.Append(String.Format("{0}", match1.Value.Substring(index + 1, length - index - 2)));
                                    break;
                                }
                            }
                        }
                    }
                    else
                        column.Append(match1.Value.Substring(1, match1.Length - 2));
                }

                match1 = Regex.Match(temp, "'[%]*\\w*[%]*'");
                if (match1.Success)
                {
                    column.Append(String.Format(" Like N'%{0}'", match1.Value.Replace("'", "").Replace("%", "")));
                }

                return column.ToString();
            });
            return result;
        }

        private static string StartsWith(string result)
        {
            result = Regex.Replace(result, "StartsWith[(]\\w+([.]\\w+)*, '[%]*\\w*[%]*'[)]", match =>
            {
                string temp = match.Value;

                StringBuilder column = new StringBuilder();
                Match match1 = Regex.Match(temp, "[(]\\w+([.]\\w+)*,");
                if (match1.Success)
                {
                    var count = (from c in match1.Value.ToCharArray()
                                 where c == '.'
                                 select c).Count();

                    if (count >= 2)
                    {
                        int i = 0;
                        int length = match1.Length;
                        int index = length;
                        while (index > 0)
                        {
                            index--;
                            if (match1.Value[index] == '.')
                            {
                                i++;
                                if (i == 2)
                                {
                                    column.Append(String.Format("{0}", match1.Value.Substring(index + 1, length - index - 2)));
                                    break;
                                }
                            }
                        }
                    }
                    else
                        column.Append(match1.Value.Substring(1, match1.Length - 2));
                }

                match1 = Regex.Match(temp, "'[%]*\\w*[%]*'");
                if (match1.Success)
                {
                    column.Append(String.Format(" Like N'{0}%'", match1.Value.Replace("'", "").Replace("%", "")));
                }

                return column.ToString();
            });

            return result;
        }

        private static string Like(string result)
        {
            result = Regex.Replace(result, "Like '", match =>
            {
                return match.Value.Replace("'", "N'");
            });
            return result;
        }

        private static string Between(string result)
        {
            result = Regex.Replace(result, "Between[(][']?\\w*\\s*[']?, [']?\\w*\\s*[']?[)]", match =>
            {
                string temp = match.Value.Replace("(", " ").Replace(")", "").Replace(",", " AND");

                return temp;
            });
            result = Regex.Replace(result, "Between[(]#\\d{4}(-\\d{2}){2}#, #\\d{4}(-\\d{2}){2}#[)]", match =>
            {
                string temp = match.Value.Replace("(", " ").Replace(")", "").Replace(",", " AND").Replace("#", "'");

                return temp;
            });
            return result;
        }

        private static string Contains(string result)
        {
            result = Regex.Replace(result, "Contains[(]\\w+([.]\\w+)*, '[%]*\\w*[%]*'[)]", match =>
            {
                string temp = match.Value;

                StringBuilder column = new StringBuilder();
                Match match1 = Regex.Match(temp, "[(]\\w+([.]\\w+)*,");
                if (match1.Success)
                {
                    var count = (from c in match1.Value.ToCharArray()
                                 where c == '.'
                                 select c).Count();

                    if (count >= 2)
                    {
                        int i = 0;
                        int length = match1.Length;
                        int index = length;
                        while (index > 0)
                        {
                            index--;
                            if (match1.Value[index] == '.')
                            {
                                i++;
                                if (i == 2)
                                {
                                    column.Append(String.Format("dbo.{0}", match1.Value.Substring(index + 1, length - index - 2)));
                                    break;
                                }
                            }
                        }
                    }
                    else
                        column.Append(match1.Value.Substring(1, match1.Length - 2));
                }

                match1 = Regex.Match(temp, "'[%]*\\w*[%]*'");
                if (match1.Success)
                {
                    column.Append(String.Format(" Like N'%{0}%'", match1.Value.Replace("'", "").Replace("%", "")));
                }

                return column.ToString();
            });

            return result;
        }

        private static string CalculatorOperator(string newCriteria)
        {
            return Regex.Replace(newCriteria, "\\w+([.]\\w+)* [<>=]+ ##XpoObject#\\w+([.]\\w+)*", match =>
            {
                string temp = match.Value;
                StringBuilder table = new StringBuilder();
                {
                    int begin = temp.LastIndexOf('.') + 1;
                    int end = temp.Length;
                    table.Append(String.Format("{0}.Oid", temp.Substring(begin, end - begin)));
                }
                Match match1 = Regex.Match(temp, " [<>=]+ ");
                if (match1.Success)
                    table.Append(match1.Value);

                return table.ToString();
            });
        }

        private static string InOperator(string result)
        {
            result = Regex.Replace(result, "\\w+([.]\\w+)* In [(]##XpoObject#", match =>
            {
                string temp = match.Value;
                int begin = temp.IndexOf('.');
                begin = begin < 0 ? 0 : begin;
                int end = temp.IndexOf(' ');
                StringBuilder table = new StringBuilder();
                table.Append(String.Format("{0}.Oid", temp.Substring(begin, end - begin).Replace(".", "")));

                Match match1 = Regex.Match(temp, " In [(]##XpoObject#");
                if (match1.Success)
                    table.Append(match1.Value);

                return table.ToString();
            });
            result = Regex.Replace(result, "##XpoObject#\\w+([.]\\w+)*", "");

            return result;
        }

        private static string Common(string result, params object[] args)
        {
            //ngày tháng
            result = Regex.Replace(result, "#\\d{4}(-\\d{2}){2}#", match =>
            {
                return match.Value.Replace("#", "'");
            });

            //kieu so
            result = Regex.Replace(result, "\\d+([.]?\\d+)?m", match =>
            {
                return match.ToString().Replace("m", "");
            });

            //chuoi
            result = Regex.Replace(result, "[^N]'[%]?(\\w+\\s*[/]*)*[%]?'", match =>
            {
                return match.Value.Insert(1, "N");
            });

            //Bien che
            result = Regex.Replace(result, "True", match =>
            {
                return "1";
            });

            //Ngoai Bien che
            result = Regex.Replace(result, "False", match =>
            {
                return "0";
            });

            return result;
        }
    }
}
