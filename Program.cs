using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestEx3b
{

    class Valute
    {
        public Valute(int Nominal, string Name, double Value)
        {
            this.Nominal = Nominal;
            this.Name = Name;
            this.Value = Value;
        }

        public int Nominal { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1251");

            String URLString = "http://www.cbr.ru/scripts/XML_daily.asp";

            XElement root = XElement.Load(URLString);

            IEnumerable<XElement> tests =
                from el in root.Elements("Valute")
                where (string)el.Element("Name") == "Венгерских форинтов" || (string)el.Element("Name") == "Норвежская крона"
                select el;

            Dictionary<string, Valute> valuteDic = new Dictionary<string, Valute>();
            foreach(XElement el in tests)
            {
                valuteDic.Add((string)el.Element("CharCode"), new Valute(Convert.ToInt32((string)el.Element("Nominal")),
                                                                         (string)el.Element("Name"), 
                                                                         Convert.ToDouble((string)el.Element("Value"))
                                                                        ));
            }

            Console.WriteLine($"{valuteDic["NOK"].Nominal} {valuteDic["NOK"].Name} равна {ConvertValute(valuteDic["NOK"], valuteDic["HUF"]):0.0000} {valuteDic["HUF"].Name}");
        }

        static public double ConvertValute(Valute CharCodeFrom, Valute CharCodeIn)
        {
            return CharCodeFrom.Value / CharCodeIn.Value * CharCodeIn.Nominal;
        }
    }
}