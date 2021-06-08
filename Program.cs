using System;

namespace MortgageCalculatorProgram
{
    class Program
    {   
        static readonly byte MONTHS_IN_YEAR = 12;
        static readonly byte PERCENT = 100;
        public static void Main(string[] args)
        {
            int principal = (int) readNumber("Principal: ", 1000, 1_000_000);
            float annualInterest = (float) readNumber("Annual Interest Rate: ", 1, 30);
            byte years = (byte) readNumber("Period (Years): ", 1, 30);

            printMortgage(principal, annualInterest, years);
            printPaymentSchedule(principal, annualInterest, years);
        }

        public static void printMortgage(int principal, float annualInterest, byte years) 
        {
            double mortgage = calculateMorgage(principal, annualInterest, years);
            string mortgageFormatted = String.Format("{0:0,0.00}", mortgage);
            Console.WriteLine();
            Console.WriteLine("MORTGAGE");
            Console.WriteLine("--------");
            Console.WriteLine("Monthly Payments: " + mortgageFormatted);
        }

        public static void printPaymentSchedule(int principal, float annualInterest, byte years) 
        {
            Console.WriteLine();
            Console.WriteLine("PAYMENT SCHEDULE");
            Console.WriteLine("----------------");
            for (short month = 1; month <= years * MONTHS_IN_YEAR; month++) {
                double balance = calculateBalance(principal, annualInterest, years, month);
                Console.WriteLine(String.Format("{0:0,0.00}", balance));
            }
        }
        

        public static double readNumber(string prompt, double min, double max) 
        {
            double value;
            string userInput;
            while (true) {
                Console.Write(prompt);
                userInput = Console.ReadLine();
                value = Convert.ToDouble(userInput);
                if (value >= min && value <= max)
                    break;
                Console.WriteLine("Enter a value between {0} and {1}", min, max);
            }
            return value;
        }
        public static double calculateBalance(
            int principal,
            float annualInterest,
            byte years,
            short numberOfPaymentsMade) 
        {
            float monthlyInterest = annualInterest / PERCENT / MONTHS_IN_YEAR;
            float numberOfPayments = years * MONTHS_IN_YEAR;

            double balance = principal
                        * (Math.Pow(1 + monthlyInterest, numberOfPayments) - Math.Pow(1 + monthlyInterest, numberOfPaymentsMade))
                        / (Math.Pow(1 + monthlyInterest, numberOfPayments) - 1);
            
            return balance;
        }

        public static double calculateMorgage(
            int principal,
            float annualInterest,
            byte years)
        {
            float monthlyInterest = annualInterest / PERCENT / MONTHS_IN_YEAR;
            float numberOfPayments = years * MONTHS_IN_YEAR;

            double mortgage = principal
                * (monthlyInterest * Math.Pow(1 + monthlyInterest, numberOfPayments))
                / (Math.Pow(1 + monthlyInterest, numberOfPayments) - 1);

            return mortgage;
        }
    }
}
