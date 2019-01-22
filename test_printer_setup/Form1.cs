using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
//using System.Drawing.Printing;
//using System.Drawing.Printing.PrintDocument;

//using System.Drawing.Printing.PrinterSettings.PaperSizeCollection;

//using System.Drawing.Printing.PaperSize;

//using System.Drawing.Printing.PageSettings;

namespace test_printer_setup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private const int DM_PROMPT = 4;
        private const int DM_OUT_BUFFER = 2;
        private const int DM_IN_BUFFER = 8;
        [System.Runtime.InteropServices.DllImportAttribute("winspool.drv", SetLastError = true)]
        public extern static int DocumentProperties(
             IntPtr hWnd,              // handle to parent window 
             IntPtr hPrinter,           // handle to printer object
             string pDeviceName,   // device name
             ref IntPtr pDevModeOutput, // modified device mode
             ref IntPtr pDevModeInput,   // original device mode
             int fMode);                 // mode options 


        [System.Runtime.InteropServices.DllImportAttribute("winspool.drv")]
        public static extern int PrinterProperties(
            IntPtr hwnd,  // handle to parent window
            IntPtr hPrinter); // handle to printer object


        [System.Runtime.InteropServices.DllImportAttribute("winspool.drv", SetLastError = true)]
        public extern static int OpenPrinter(
            string pPrinterName,   // printer name
            ref IntPtr hPrinter,      // handle to printer object
            ref IntPtr pDefault);    // handle to default printer object. 


        [System.Runtime.InteropServices.DllImportAttribute("winspool.drv", SetLastError = true)]
        public static extern int ClosePrinter(
            IntPtr phPrinter); // handle to printer object
        PrintDocument doc = new PrintDocument();
        private void button1_Click(object sender, EventArgs e)
        {
            string printerName = doc.PrinterSettings.PrinterName;
            if (printerName != null && printerName.Length > 0)
            {
                IntPtr pPrinter = IntPtr.Zero;
                IntPtr pDevModeOutput = IntPtr.Zero;
                IntPtr pDevModeInput = IntPtr.Zero;
                IntPtr nullPointer = IntPtr.Zero;
                OpenPrinter(printerName, ref pPrinter, ref nullPointer);
                int iNeeded = DocumentProperties(this.Handle, pPrinter, printerName, ref pDevModeOutput, ref pDevModeInput, 0);
                pDevModeOutput = System.Runtime.InteropServices.Marshal.AllocHGlobal(iNeeded);
                pDevModeInput = System.Runtime.InteropServices.Marshal.AllocHGlobal(iNeeded);
                //DM_OUT_BUFFER
                DocumentProperties(this.Handle, pPrinter, printerName, ref pDevModeOutput, ref pDevModeInput, DM_OUT_BUFFER);//DM_PROMPT);
                
                //if (pDevModeOutput.dmFields & DM_ORIENTATION)
                //{
                //    /* If the printer supports paper orientation, set it.*/
                //    pDevModeOutput->dmOrientation = DMORIENT_LANDSCAPE;
                //}

                ClosePrinter(pPrinter);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string printerName = doc.PrinterSettings.PrinterName;


            if (printerName != null && printerName.Length > 0)
            {
                IntPtr pPrinter = IntPtr.Zero;
                IntPtr pDevModeOutput = IntPtr.Zero;
                IntPtr pDevModeInput = IntPtr.Zero;
                IntPtr nullPointer = IntPtr.Zero;


                OpenPrinter(printerName, ref pPrinter, ref nullPointer);


                int iNeeded = PrinterProperties(this.Handle, pPrinter);
                ClosePrinter(pPrinter);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void test()
        {
            /////////需要以上引用

            PrintDocument pd = new PrintDocument();
            pd.DocumentName = "PDF-XChange Lite";
            pd.PrinterSettings.PrinterName = "PDF-XChange Lite";
            PaperSize p = null;
            pd.DefaultPageSettings.Landscape = false;
            foreach (PaperSize ps in pd.PrinterSettings.PaperSizes)
            {
                if (ps.PaperName.Equals("QRCode"))
                    p = ps;
            }
            pd.DefaultPageSettings.PaperSize = p;
            pd.Print();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            test();
        }
        


    }
}
