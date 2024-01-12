using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserIO.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string barcode = "";

            barcode = "010080174113605417101031107112832\u001d913303";

            Core.Functions client = new Core.Functions();
            DAO.InformationSet result;
            result = client.GetFullInformationSet(barcode);
        }
    }
}
