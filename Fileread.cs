using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORGA_PDA
{
    class Fileread
    {
       // public static String Path = @"\mnt\sdcard\Android\data\com.example.orga_pda\files\PRODUCT.TXT";
   

        
        public static void read(string Path)
        {
            String textValue = System.IO.File.ReadAllText(Path);
            Console.WriteLine(textValue);
        }

    }
}
