using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Win32;
using PortableDevices;
using MediaDevices;
using OpenNETCF.Desktop.Communication;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin;

namespace ORGA_PDA
{
    public partial class Form1 : Form
    {
        

        private PortableDevice device1;
        private PortableDeviceObject portableDeviceObject;
        private PortableDeviceCollection collection;
        private bool bFindDevice;

        public object OpenFileDialog1 { get; private set; }

        public Form1()
        {
            
            InitializeComponent();
            



        }

        private void button1_Click(object sender, EventArgs e)
        {

            string Path = "C:/ORGAPDA/ORDER";

            DirectoryInfo dirs = new DirectoryInfo(Path);

            if(dirs.Exists== false)
            {
                dirs.Create();
            }

            var devices = MediaDevice.GetDevices();


            try {
                using (var device = devices.First(d => d.Manufacturer == "Honeywell"))
                {
                    device.Connect();

                    var dir = device.GetDirectoryInfo(@"\내부 공유 저장용량\Android\data\com.example.orga_pda\files");
                    var files = dir.EnumerateFiles("ORDER.txt", SearchOption.AllDirectories);
                    
                    Console.WriteLine(files.Count());
                    if (files.Count()==0)
                    {
                        MessageBox.Show("PDA에 파일이 없습니다.", "ORGAPDA");
                        return;
                    }
                    foreach (var file in files)
                    { 
                        MemoryStream memoryStream = new System.IO.MemoryStream();
                        device.DownloadFile(file.FullName, memoryStream);
                        memoryStream.Position = 0;
                        WriteSreamToDisk($@"C:\ORGAPDA\ORDER\{file.Name}", memoryStream);
                    }
                    device.Disconnect();
                }
                MessageBox.Show("데이터 수신 완료", "ORGAPDA");
            }
            catch(Exception ex)
            {
                MessageBox.Show("PDA를 연결해주세요", "ORGAPDA");
            }
            
            

            




            
        }
        static void WriteSreamToDisk(string filePath, MemoryStream memoryStream)
        {
            using(FileStream file= new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bytes = new byte[memoryStream.Length];
                memoryStream.Read(bytes, 0, (int)memoryStream.Length);
                file.Write(bytes, 0, bytes.Length);
                memoryStream.Close();
            }
        }

     

        

        private void button2_Click(object sender, EventArgs e)


        {
            


            string Path = "C:/ORGAPDA/PRODUCT";

            DirectoryInfo dirs = new DirectoryInfo(Path);

            if (dirs.Exists == false)
            {
                dirs.Create();
            }

            var devices = MediaDevice.GetDevices();
            // Console.WriteLine(devices.FriendlyName);

            try {
                using (var device = devices.First(d => d.Manufacturer == "Honeywell"))
                {
                    device.Connect();

                    var dir = device.GetDirectoryInfo(@"\내부 공유 저장용량\Android\data\com.example.orga_pda\files");


                    var dir2 = device.GetDirectoryInfo(@"\내부 공유 저장용량\Android\data");
                    var files = dir.EnumerateFiles("PRODUCT.txt", SearchOption.AllDirectories);
                   

                    try
                    {
                        using (MemoryStream memoryStream = new System.IO.MemoryStream())
                        using (FileStream file = new FileStream(@"C:\ORGAPDA\PRODUCT\PRODUCT.txt", FileMode.Open, FileAccess.Read))
                        {
                            
                            byte[] bytes = new byte[file.Length];
                            file.Read(bytes, 0, (int)file.Length);
                            memoryStream.Write(bytes, 0, (int)file.Length);
                            memoryStream.Position = 0;
                            try {
                                device.UploadFile(memoryStream, dir.FullName + "\\PRODUCT.txt");
                                MessageBox.Show("상품 데이터 생성 완료", "ORGAPDA");
                                memoryStream.Close();
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show("PDA에 이미 파일이 존재합니다", "ORGAPDA");
                                return;
                            }
                            
                        }
                    }
                    catch(IOException ex)
                    {
                        
                        MessageBox.Show("상품 마스터 파일이 존재하지 않습니다.", "ORGAPDA");
                    }
                    

                    device.Disconnect();
                }
            }
            catch(Exception ex) {
                MessageBox.Show("PDA를 연결해주세요", "ORGAPDA");
            }
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Path = "C:/ORGAPDA/DISPLAY";

            DirectoryInfo dirs = new DirectoryInfo(Path);

            if (dirs.Exists == false)
            {
                dirs.Create();
            }

            var devices = MediaDevice.GetDevices();


            try
            {
                using (var device = devices.First(d => d.Manufacturer == "Honeywell"))
                {
                    device.Connect();

                    var dir = device.GetDirectoryInfo(@"\내부 공유 저장용량\Android\data\com.example.orga_pda\files");
                    var files = dir.EnumerateFiles("DISPLAY.txt", SearchOption.AllDirectories);

                    if (files.Count() == 0)
                    {
                        MessageBox.Show("PDA에 파일이 없습니다.", "ORGAPDA");
                        return;
                    }
                    foreach (var file in files)
                    {
                        MemoryStream memoryStream = new System.IO.MemoryStream();
                        device.DownloadFile(file.FullName, memoryStream);
                        memoryStream.Position = 0;
                        WriteSreamToDisk($@"C:\ORGAPDA\DISPLAY\{file.Name}", memoryStream);
                    }
                    device.Disconnect();
                }
                MessageBox.Show("데이터 수신 완료", "ORGAPDA");
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDA를 연결해주세요", "ORGAPDA");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Path = "C:/ORGAPDA/INVENTORY";

            DirectoryInfo dirs = new DirectoryInfo(Path);

            if (dirs.Exists == false)
            {
                dirs.Create();
            }

            var devices = MediaDevice.GetDevices();


            try
            {
                using (var device = devices.First(d => d.Manufacturer == "Honeywell"))
                {
                    device.Connect();

                    var dir = device.GetDirectoryInfo(@"\내부 공유 저장용량\Android\data\com.example.orga_pda\files");
                    var files = dir.EnumerateFiles("INVENTORY.txt", SearchOption.AllDirectories);

                    if (files.Count() == 0)
                    {
                        MessageBox.Show("PDA에 파일이 없습니다.", "ORGAPDA");
                        return;
                    }

                    foreach (var file in files)
                    {
                        MemoryStream memoryStream = new System.IO.MemoryStream();
                        device.DownloadFile(file.FullName, memoryStream);
                        memoryStream.Position = 0;
                        WriteSreamToDisk($@"C:\ORGAPDA\INVENTORY\{file.Name}", memoryStream);
                    }
                    device.Disconnect();
                }
                MessageBox.Show("데이터 수신 완료", "ORGAPDA");
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDA를 연결해주세요", "ORGAPDA");
            }
        }
    }
}
