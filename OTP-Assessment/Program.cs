using System;
using System.IO;

namespace OTP_Assessment
{
    class Program
    {
        static void Main(string[] args)
        {
            var otp = new Email_OPT_Module(args[0], args[1]);
            otp.Start();

            if (otp.GenerateOPTEmail("eujinpotter@outlook.com") != Email_OPT_Module.EmailStatus.STATUS_EMAIL_OK)
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
