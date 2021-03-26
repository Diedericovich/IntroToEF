using IntroToEF.Business;
using System;

namespace IntroToEF
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var business = new Business.Business();
            business.RunApp();
        }
    }
}