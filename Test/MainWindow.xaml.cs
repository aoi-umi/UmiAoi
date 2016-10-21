using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string format = "yyMMddmmHHss";
            var x = DateTime.ParseExact("160831151801", format, CultureInfo.InvariantCulture);
            var y = x.ToString(format+"ddd%c");
        }

        UmiAoi.Adorners.ReflectorAdorner ra1;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            AdornerLayer al1 = AdornerLayer.GetAdornerLayer(b1);
            ra1 = new UmiAoi.Adorners.ReflectorAdorner(b1, Dock.Top);
            al1.Add(ra1);
            AdornerLayer al2 = AdornerLayer.GetAdornerLayer(b2);
            al2.Add(new UmiAoi.Adorners.ReflectorAdorner(b2, Dock.Bottom));
            AdornerLayer al3 = AdornerLayer.GetAdornerLayer(b3);
            al3.Add(new UmiAoi.Adorners.ReflectorAdorner(b3, Dock.Left));
            AdornerLayer al4 = AdornerLayer.GetAdornerLayer(b4);
            al4.Add(new UmiAoi.Adorners.ReflectorAdorner(b4, Dock.Right));
        }

        private void mtb_PreviewDrop(object sender, DragEventArgs e)
        {
            UmiAoi.Controls.MyTextBox m = mtb;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {        
            UmiAoi.Controls.MyTextBox m = mtb;
            string filename = mtb.Text;
            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("文件名不能为空");
                return;
            }
            if (!File.Exists(filename))
            {
                MessageBox.Show($"文件{filename}不存在");
                return;
            }
            try
            {                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }

        private void compressFile(string inFile, string outFile)
        {

            FileStream outFileStream = new FileStream(outFile, FileMode.Create);
            zlib.ZOutputStream outZStream = new zlib.ZOutputStream(outFileStream, zlib.zlibConst.Z_DEFAULT_COMPRESSION);
            FileStream inFileStream = new FileStream(inFile, FileMode.Open);
            try
            {
                CopyStream(inFileStream, outZStream);
            }
            finally
            {
                outZStream.Close();
                outFileStream.Close();
                inFileStream.Close();
            }
        }
        private void decompressFile(string inFile, string outFile)
        {
            FileStream outFileStream = new FileStream(outFile, FileMode.Create);
            zlib.ZOutputStream outZStream = new zlib.ZOutputStream(outFileStream);
            FileStream inFileStream = new FileStream(inFile, FileMode.Open);
            try
            {
                CopyStream(inFileStream, outZStream);
            }
            finally
            {
                outZStream.Close();
                outFileStream.Close();
                inFileStream.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ra1.ShowOrHideReflector();
        }
    }
}
