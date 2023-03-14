using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentDistance
{
    class DocDistance
    {
        public static double FastSqrt(double num)
        {
            if (0 == num) {return 0;}
            double number = (num / 2) + 1;  double myval = (number + (num / number)) / 2;
            while (myval < number) { number = myval; myval = (number + (num / number)) / 2;}
            return number;
        }
        public static Dictionary<string, double> loopThroughNotificationCountQueries(string doc1filepath)
        {
            Dictionary<string, double> d1 = new Dictionary<string, double>();
            string one = System.IO.File.ReadAllText(doc1filepath);
            //Console.WriteLine("loop1");
            for (int i = 0; i < one.Length; i++)
            {
                string inner = "";
                int loop = i;
                while (loop < one.Length && (char.IsLetterOrDigit(one[loop]) && one[loop] != ' '))
                {
                    if (((int)one[loop] >= 65 && (int)one[loop] <= 90))
                        inner += Convert.ToChar(one[loop] + 32);
                    else
                        inner += one[loop];
                    loop++;
                }
                //Console.WriteLine(inner);
                i = loop;
                if (d1.ContainsKey(inner) && inner != "")
                {
                    d1[inner]++;
                }
                else
                {
                    if (inner != "")
                        d1.Add(inner, 1);
                }
            }
            return d1;
        }
        public static Dictionary<string, double> loopThroughNotificationCountQueries2(string doc2filepath)
        {
            Dictionary<string, double> d2 = new Dictionary<string, double>();
            string two = System.IO.File.ReadAllText(doc2filepath);
            //Console.WriteLine("loop244444");
            for (int i = 0; i < two.Length; i++)
            {
                string inner = "";
                int loop = i;
                while (loop < two.Length && (char.IsLetterOrDigit(two[loop]) && two[loop] != ' '))
                {
                    if (((int)two[loop] >= 65 && (int)two[loop] <= 90))
                        inner += Convert.ToChar(two[loop] + 32);
                    else
                        inner += two[loop];
                    loop++;
                    //Console.WriteLine(inner);
                }
                //Console.WriteLine(inner);
                i = loop;
                if (d2.ContainsKey(inner) && inner != "")
                {
                    d2[inner]++;
                }
                else
                {
                    if (inner != "")
                        d2.Add(inner, 1);
                }
            }
            return d2;
        }
        // *****************************************
        // DON'T CHANGE CLASS OR FUNCTION NAME
        // YOU CAN ADD FUNCTIONS IF YOU NEED TO
        // *****************************************
        /// <summary>
        /// Write an efficient algorithm to calculate the distance between two documents
        /// </summary>
        /// <param name="doc1FilePath">File path of 1st document</param>
        /// <param name="doc2FilePath">File path of 2nd document</param>
        /// <returns>The angle (in degree) between the 2 documents</returns>
        
        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {
            // TODO comment the following line THEN fill your code here
            //throw new NotImplementedException();
            string x = doc1FilePath;
            string y = doc2FilePath;
            Dictionary<string , double> d1 = new Dictionary<string , double>();
            //d1 =DocDistance.loopThroughNotificationCountQueries(doc1FilePath);
            Dictionary<string, double> d2 = new Dictionary<string, double>();
            //d2 = DocDistance.loopThroughNotificationCountQueries2(doc2FilePath);
            Thread ta = new Thread(() => d1=loopThroughNotificationCountQueries(x));
            Thread tb = new Thread(() => d2=loopThroughNotificationCountQueries2(y));
            ta.Start();
            tb.Start();
            ta.Join();
            tb.Join();
            HashSet<String> unique = new HashSet<string>();
            foreach (KeyValuePair<string, double> entry in d1)
            {
                if (!unique.Contains(entry.Key))
                    unique.Add(entry.Key);
            }
            foreach (KeyValuePair<string, double> entry in d2)
            {
                if (!unique.Contains(entry.Key))
                    unique.Add(entry.Key);
            }
            double product1 = 0;
            double product2 = 0;
            foreach (KeyValuePair<string, double> entry in d1)
            {
                //product1 += Math.Pow(entry.Value, 2);
                //product1 += DocDistance.ipow(entry.Value, 2);
                product1 += (entry.Value * entry.Value);
            }
            foreach (KeyValuePair<string, double> entry in d2)
            {
                //product2 += Math.Pow(entry.Value, 2);
                //product2 += DocDistance.ipow(entry.Value, 2);
                product2 += (entry.Value * entry.Value);
            }
            double assad = 0;
            foreach (string inner in unique)
            {
                if (d1.ContainsKey(inner) && d2.ContainsKey(inner))
                {
                    assad += (d1[inner] * d2[inner]);
                }
            }
            double ans = 0;
            ans = assad / (DocDistance.FastSqrt(product1*product2));
            return (Math.Acos(ans)*180)/3.141592653589793238462643383279502884197;
        }
    }
}
