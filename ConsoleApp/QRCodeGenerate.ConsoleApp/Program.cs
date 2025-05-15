﻿using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace QRCodeGenerate.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the data to encode in the QR code:");
            string? data = Console.ReadLine();

            // Generate the QR code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            //// Save the QR code as a PNG image file
            //string fileName = data +"_"+ "QRCode.png";
            //qrCodeImage.Save(fileName, ImageFormat.Png);

            // Specify the folder path to save the QR code image
            string folderPath = @"D:\QRCODE";

            // Create the folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Save the QR code as a PNG image file inside the specified folder
            string fileName = Path.Combine(folderPath, data + "_" + "QRCode.png");
            qrCodeImage.Save(fileName, ImageFormat.Png);

            // Display the QR code image using an image viewer application
            DisplayQRCodeImage(fileName);

            Console.ReadLine();
        }

        static void DisplayQRCodeImage(string imagePath)
        {
            try
            {
                // Check if the file exists
                if (System.IO.File.Exists(imagePath))
                {
                    // Use the default image viewer to open and display the QR code image
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = imagePath,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
                else
                {
                    Console.WriteLine("QR code image not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}