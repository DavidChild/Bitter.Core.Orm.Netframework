using System;
using Bitter.Core;
namespace bitter.test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SysPbinstanceInfo pbinstanceInfo = db.FindQuery<SysPbinstanceInfo>().Where(p => p.id=="B0729BF6-64D6-448E-B145-605133CD73EE").Find().FirstOrDefault();
            pbinstanceInfo.id = "B0729BF6-64D6-448E-B145-605133CD73KKK";
            var id=pbinstanceInfo.Insert().Submit();
            var bb=db.FindQuery<SysPbinstanceInfo>().QueryById("B0729BF6-64D6-448E-B145-605133CD73KKK");
            Console.ReadKey();

        }
    }
}
