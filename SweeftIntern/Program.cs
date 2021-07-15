using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SweeftIntern
{
    class Program
    {
        /************************************დავალება 1************************************/
        // #1 ვარიანტი
        public static Boolean isPalindrome_1(String text)
        {
            var isEven = (text.Length % 2) == 0;
            string firstHalf = "";
            string secondHalf = "";
            bool ispal = true;
            for (var i = 0; i < text.Length / 2; i++)
            {
                firstHalf += text[i];
            }
            if (isEven)
            {
                for (var i = text.Length - 1; i >= text.Length / 2; i--)
                {
                    secondHalf += text[i];
                }
                ispal = firstHalf == secondHalf;
            }
            if (!isEven)
            {
                for (var i = text.Length - 1; i >= text.Length / 2 + 1; i--)
                {
                    secondHalf += text[i];
                }
                ispal = firstHalf == secondHalf;
            }
            return ispal;
        }
        // #2 ვარიანტი
        public static Boolean isPalindrome_2(string text)
        {
            string firstHalf = text.Substring(0, text.Length / 2);

            char[] arr = text.ToCharArray();
            Array.Reverse(arr);
            string temp = new string(arr);

            string secondHalf = temp.Substring(0, temp.Length / 2);

            return firstHalf.Equals(secondHalf);
        }
        /************************************დავალება 2************************************/
        public static int minSplit(int amount)
        {
            int[] monets = new int[] { 50, 20, 10, 5, 1 };
            int count = 0;

            for (int i = 0; i < monets.Length; i++)
            {
                if (amount / monets[i] > 0)
                {
                    count += amount / monets[i];
                    amount = amount % monets[i];
                }
            }
            return count;
        }
        /************************************დავალება 3************************************/
        public static int notContains(int[] array)
        {
            int[] positiveArray = Array.FindAll(array, x => x > 0);
            Array.Sort(positiveArray);

            int min = 1;

            if (positiveArray[0] != 1) return min;
            for (int i = 1; i < positiveArray.Length; i++)
            {
                if (positiveArray[i] - 1 == positiveArray[i - 1] || positiveArray[i] == positiveArray[i - 1])
                {
                    continue;
                }
                min = positiveArray[i - 1] + 1; break;
            }
            if (min == 1)
            {
                min = positiveArray[positiveArray.Length - 1] + 1;
            }

            return min;
        }
        /************************************დავალება 4************************************/
        public static Boolean isProperly(string sequence)
        {
            int left = 0, right = 0;
            char[] parts = sequence.ToCharArray();
            foreach (var part in parts)
            {
                if (part == '(')
                {
                    left++;
                }
                if (part == ')')
                {
                    right++;
                }
            }
            return left == right;
        }
        /************************************დავალება 5***********************************/
        public static int countVariants(int stearsCount)
        {
            if (stearsCount == 1 || stearsCount == 2) return stearsCount;
            return countVariants(stearsCount - 1) + countVariants(stearsCount - 2);
        }
        /************************************დავალება 6***********************************/
        public class MyDataStructure
        {
            List<int> list = new List<int>();
            Dictionary<int, int> dictonary = new Dictionary<int, int>();
            public MyDataStructure()
            {

            }
            public MyDataStructure(int[] mass = null)
            {
                foreach (var m in mass)
                {
                    Add(m);
                }
            }
            public void Add(int x)
            {
                if (dictonary.ContainsKey(x))
                    return;
                int index = list.Count;
                dictonary.Add(x, index);
                list.Add(x);
            }
            //O(1)
            public void Remove(int x)
            {
                if (!dictonary.ContainsKey(x))
                    return;
                int index = dictonary[x];
                dictonary.Remove(x);

                int lastIndex = list.Count - 1;
                int temp = list[index];
                list[index] = list[lastIndex];
                list[lastIndex] = temp;
                list.RemoveAt(list.Count - 1);
            }
            public void Show()
            {
                foreach (var l in list)
                {
                    Console.WriteLine(l);
                }
            }
        }
        /************************************დავალება 8************************************/
        public static double exchangeRate(String from, String to)
        {
            XDocument xmldoc = XDocument.Load("http://www.nbg.ge/rss.php");
            var html = xmldoc.Descendants("item").Descendants("description").First().Value;

            HtmlDocument htmldoc = new HtmlDocument();
            htmldoc.LoadHtml(html);

            var resultFrom = htmldoc.DocumentNode.Descendants("tr").Where(r => r.ChildNodes.Any(c => c.InnerText == from))
                 .Select(x => new
                 {
                     currency = Convert.ToDouble(x.ChildNodes[5].InnerText),
                     amount = Convert.ToDouble(x.ChildNodes[3].InnerText.Substring(0, x.ChildNodes[3].InnerText.IndexOf(" "))),
                 }).FirstOrDefault();

            var resultTo = htmldoc.DocumentNode.Descendants("tr").Where(r => r.ChildNodes.Any(c => c.InnerText == to))
             .Select(x => new
             {
                 currency = Convert.ToDouble(x.ChildNodes[5].InnerText),
                 amount = Convert.ToDouble(x.ChildNodes[3].InnerText.Substring(0, x.ChildNodes[3].InnerText.IndexOf(" "))),
             }).FirstOrDefault();

            if (from == "GEL")
            {
                return resultTo.amount / resultTo.currency;
            }

            if (to == "GEL")
                return resultFrom.currency / resultFrom.amount;

            return (resultFrom.currency * resultTo.amount) / (resultFrom.amount * resultTo.currency);
        }
        static void Main(string[] args)
        {
            // დავალება 1
            Console.WriteLine("davaleba 1");
            if (isPalindrome_1/*isPalindrome_2*/("lool"))
            {
                Console.WriteLine("palindromia");
            }
            else
            {
                Console.WriteLine("araa palindromi");
            }
            Console.WriteLine();

            // დავალება 2
            Console.WriteLine("davaleba 2");
            Console.WriteLine(minSplit(50));
            Console.WriteLine();

            //დავალება 3
            Console.WriteLine("davaleba 3");
            Console.WriteLine(notContains(new int[] { 1, 2, 3, -9, 4 }));
            Console.WriteLine();

            //დავალება 4
            Console.WriteLine("davaleba 4");
            if (isProperly("(()())"))
            {
                Console.WriteLine("mimdevroba sworia");
            }
            else
            {
                Console.WriteLine("mimdevroba arasworia");
            }
            Console.WriteLine();

            //დავალება 5
            Console.WriteLine("davaleba 5");
            Console.WriteLine(countVariants(8));
            Console.WriteLine();

            //დავალება 6
            Console.WriteLine("davaleba 6");
            MyDataStructure data = new MyDataStructure(new int[] { 1, 0, -5, 4 });
            data.Show();
            Console.WriteLine("waashlis shemdeg");
            data.Remove(-5);
            data.Show();
            Console.WriteLine();

            //დავალება 8
            Console.WriteLine(exchangeRate("KRW", "KWD"));
            Console.WriteLine(exchangeRate("GEL", "AED"));
            Console.WriteLine(exchangeRate("AED", "GEL"));
        }
    }
}