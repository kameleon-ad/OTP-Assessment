using System;
using System.IO;

namespace OTP_Assessment
{
    class Program
    {
        static void Main(string[] args)
        {
            Email_OPT_Module otp;
            if (args.Length < 2)
            {
                otp = new Email_OPT_Module("peter.jamesds@outlook.com", "Test!234");
            }
            else
            {
                otp = new Email_OPT_Module(args[0], args[1]);
            }
            otp.Start();

            Console.Write("Enter the email address to get OTP: ");
            string dstEmail = Console.ReadLine().Trim();
            dstEmail = dstEmail != "" ? dstEmail : "gribnekov.jeong@outlook.com";

            if (otp.GenerateOPTEmail(dstEmail) != Email_OPT_Module.EmailStatus.STATUS_EMAIL_OK)
            {
                Console.WriteLine("Fail");
                return;
            }
            Console.WriteLine("[INFO] Sent Email");
            Stream inputStream = Console.OpenStandardInput();
            otp.CheckOTP(inputStream).Wait();
        }
    }
}
