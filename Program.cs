using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Data;

namespace CmdCalc
{


    public readonly struct dg
    {

        private readonly double num1;
        private readonly string str1;

        public dg(double num)
        {
            num1 = num;
            str1 = string.Empty;
        }

        public dg(string str)
        {
            num1 = 0;
            str1 = str;
        }
        
        public static dg operator +(dg a) => a;
        public static dg operator +(dg a, dg b)
        => new dg(a.num1 + b.num1);

        public static dg operator -(dg a, dg b)
        => new dg(a.num1 - b.num1);

        public static dg operator /(dg a, dg b)
        => new dg(a.num1 / b.num1);

        public static dg operator *(dg a, dg b)
        => new dg(a.num1 * b.num1);

        public static dg operator ^(dg a, dg b)
        => new dg(Math.Pow(a.num1, b.num1));

        public override string ToString() => $"{num1}";

        public float Calculate()
        {

            var dt = new DataTable();
            dt.Columns.Add("ArithmaticStr", typeof(float), str1);
            dt.Rows.Add();

            return (float)dt.Rows[0][0];

        }

    } // public readonly struct dg

    class Program
    {

        static DateTime startTime, endTime;
        static string strCalc;
        static void Main(string[] args)
        {

            try
            {

                startTime = DateTime.Now;
                DisplayAppDetails();

                if (args.Length == 0)
                {
                    throw new Exception("\n\t- No parameteres passed");
                }
                else // if (args.Length == 0)
                {

                    Console.WriteLine();
                    Console.WriteLine("\t - Process started on {0} at {1}", startTime.ToString("ddd dd MMM yyyy"), startTime.ToString("HH:mm:ss.fff"));

                    // Print args list
                    Console.Write("\t - Procesisng ");
                    foreach (string par in args)
                        strCalc += par + ' ';
                    Console.Write("\"" + strCalc.Trim() + "\" ");
                    dg c = new dg(strCalc);
                    Console.WriteLine("= {0:N3}", c.Calculate());

                    // Testing -->>>The below has successfully trigger the Math.Pow
                    //dg a = new dg(2);
                    //dg b = new dg(5);
                    //Console.WriteLine(a ^ b);

                } // if (args.Length == 0)

            } // try

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            } // catch

            finally
            {

                endTime = DateTime.Now;
                Console.WriteLine("\t - Process ended   on {0} at {1}", endTime.ToString("ddd dd MMM yyyy"), endTime.ToString("HH:mm:ss.fff"));
                Console.WriteLine("\t - Process took {0}", endTime - startTime);

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();

            } // finally

        }

        /// <summary>
        /// Displays the application details suchs as app name, version, build date, developer and application description
        /// </summary>
        static private void DisplayAppDetails()
        {

            // I used to obtain build date using the linker, but this for some reason is getting the date wrong
            string AppString = string.Format("Running application \"{0}\" (Version {1} - Build Date {2})",
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version,
                new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime);

            Console.WriteLine(AppString);
            Console.WriteLine("".PadLeft(AppString.Length, '-'));

            var attribute2 = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)
                    .Cast<AssemblyCompanyAttribute>().FirstOrDefault();
            if (attribute2 != null)
            {
                Console.WriteLine("Developer   : {0}", attribute2.Company);
            }

            var attribute1 = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)
                    .Cast<AssemblyDescriptionAttribute>().FirstOrDefault();
            if (attribute1 != null)
            {
                Console.WriteLine("Description : {0}", attribute1.Description);
            }

        }

        /// <summary>
        /// Calculates the the string provided
        /// </summary>
        /// <param name="strCalc">Calculation string</param>
        /// <returns>The calculation result</returns>
        static private float Calculate(string strCalc)
        {

            // This is unable to handle new arithmatic operator such as ^
            var dt = new DataTable();
            dt.Columns.Add("r", typeof(float), strCalc);
            dt.Rows.Add();

            return (float)dt.Rows[0][0];

        } // static private float Calculate(string strCalc)

    } // class Program

} // namespace CmdCalc

