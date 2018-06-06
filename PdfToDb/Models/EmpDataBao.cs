using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PdfToDb.Models
{
    public class EmpDataBao
    {
        
        public static EmpDataViewModel GetsEmpData(string[] words,string[] items)
        {
            var obj = new EmpDataViewModel();
            for (int j = 0, len = words.Length; j < len; j++)
            {

                var line = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(words[j]));
                
                foreach (var a in items)
                {
                    if (line.Contains(a))
                    {

                        int index = line.IndexOf(a) + a.Length;
                        if (a != "CODE")
                        {
                            var re = line.Substring(index).Trim(new char[] { ':', ' ' });
                            switch (a)
                            {
                                case "NAME":
                                    obj.Name = re;
                                    break;
                                case "WORK HOURS":
                                    obj.WorkHours = re;
                                    break;
                                case "OVER TIME":
                                    obj.OverTime = re;
                                    break;
                            }
                        }
                        else
                        {
                            int endIndex = line.IndexOf(items[1]);
                            string re = line.Substring(index, endIndex - 4).Trim(new char[] { ':', ' ' });
                            obj.Code = int.Parse(re);
                        }

                    }
                }
            }
            return obj;
        }
    }
}