using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TestEx3b
{
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
                where (string)el.Element("Name") == "Венгерских форинтов" || (string)el.Element("Name") == "Норвежских крон"
                select el;

            Dictionary<string, Valute> valuteDic = new Dictionary<string, Valute>();
            foreach(XElement el in tests)
            {
                valuteDic.Add((string)el.Element("CharCode"), new Valute(Convert.ToInt32((string)el.Element("Nominal")),
                              (string)el.Element("Name"),
                              Convert.ToDouble((string)el.Element("Value"))));
            }
           
           Console.WriteLine("1 Норвежская крона равна " + ConvertValute(valuteDic["NOK"], valuteDic["HUF"]).ToString("00.0000") + " " + valuteDic["HUF"].Name);
        }

        static public double ConvertValute(Valute CharCodeFrom, Valute CharCodeIn)
        {
            return CharCodeFrom.Value / CharCodeIn.Value * CharCodeFrom.Nominal;
        }
    }
}

//List<Valute> valutes = new List<Valute>();
//foreach (XElement el in tests)
//{
//    valutes.Add(
//        new Valute(
//            (string)el.Element("CharCode"), 
//            Convert.ToInt32((string)el.Element("Nominal")), 
//            (string)el.Element("Name"), 
//            Convert.ToDouble((string)el.Element("Value"))
//        )
//    );

//Console.WriteLine((string)el.Element("CharCode"));
//Console.WriteLine((string)el.Element("Nominal"));
//Console.WriteLine((string)el.Element("Name"));
//Console.WriteLine((string)el.Element("Value"));
//Console.WriteLine("--------------");
//}

//foreach (var v in valutes)
//{
//    Console.WriteLine(v.CharCode);
//    Console.WriteLine(v.Nominal);
//    Console.WriteLine(v.Name);
//    Console.WriteLine(v.Value);
//    Console.WriteLine("--------------");
//}

//Console.WriteLine(valutes.ElementAt(1).Value / valutes.ElementAt(0).Value * valutes.ElementAt(1).Nominal);