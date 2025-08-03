// See https://aka.ms/new-console-template for more information


using CL_REUFT;
using System.Text;
using static CL_REUFT.GeneralUtilityFunctions;
using static CL_REUFT.Text_UI;
using System.Diagnostics;
Console.OutputEncoding = Encoding.UTF8;


RSA RSA_object = new RSA();
static void FlashUsb()
{
    Console.WriteLine("THIS PROGRAM REQUIRES DD, PLEASE PROVIDE LOCATION OF DD");
    string DD_Location = Console.ReadLine();
   
        //Not tested, might work, might completely screw up drive
        Console.WriteLine("Please tell the location of the ISO file");
    string IsoLocation = @Console.ReadLine();
    Console.WriteLine("Select a drive, First Input The number then the name of the usb");
    Console.WriteLine();

   

   System.Diagnostics.Process.Start("powershell", "get-disk");

    string driveNumber = Console.ReadLine();
    string driveName = Console.ReadLine();

    string script = "Clear-Disk " + driveNumber + " -RemoveData";
    ProcessStartInfo processStartInfo = new ProcessStartInfo
    {
        FileName = "Powershell.exe",
        Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command {script}",
        Verb = "runas",
        UseShellExecute = true
    };
    using (Process process = Process.Start(processStartInfo))
    {
        process.WaitForExit();
    }
    script = "New-Partition -DiskNumber " + driveNumber + " -UseMaximumSize -DriveLetter Z";

    processStartInfo = new ProcessStartInfo
    {
        FileName = "Powershell.exe",
        Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command {script}",
        Verb = "runas",
        UseShellExecute = true
    };
    using (Process process = Process.Start(processStartInfo))
    {
        process.WaitForExit();
    }


    script =  DD_Location+@"if=\\.\z: of="+IsoLocation +"bs=1M --progress";
    processStartInfo = new ProcessStartInfo
    {
        FileName = "Powershell.exe",
        Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command {script}",
        Verb = "runas",
        UseShellExecute = true
    };
    using (Process process = Process.Start(processStartInfo))
    {
        process.WaitForExit();
    }
   
    // System.Diagnostics.Process.Start("powershell", "Clear-Disk " + driveNumber + " -RemoveData" + " -verb RunAs");
    //System.Diagnostics.Process.Start("powershell", @".\\dd.exe if=" + IsoLocation + @"of=\\\\.\+" + @"\" + driveName + @"bs=4M --progress\r\n");
}

 void RSA_Encryption() 
{
    Console.CursorVisible = true;
Console.Clear();


List<int> primes = new List<int>();
    Console.WriteLine();
    Console.WriteLine("Give the two primes");
for(int i  = 0; i < 2; i++) { primes.Add(Convert.ToInt32(Console.ReadLine())); }
    Console.WriteLine("Give the message to be encrypted");
    var info = RSA_object.encrypt_text(Console.ReadLine(), primes);
    var encrypted_message = CL_REUFT.GeneralUtilityFunctions.EscapeIt(info.Item1);
    Console.Write(encrypted_message);
    Console.Write(" ");
    Console.Write(info.Item2);
    Console.Write(" ");
    Console.Write(info.Item3);
    Console.WriteLine();
    Console.WriteLine("Encrypted message,Private key, product of primes");
    Console.WriteLine("Press Any key to go back");
    Console.ReadLine();

    Console.CursorVisible=false;


}
void RSA_Decryptor() 
{

    

    Console.CursorVisible=true;
    Console.Clear();
    Console.WriteLine("Give the encrypted text");
    string encrypted_text = CL_REUFT.GeneralUtilityFunctions.EscapeIt(Console.ReadLine());
    Console.WriteLine("Give the private key");
    var private_key = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Give product of primes");
    var product_of_primes = Convert.ToInt32(Console.ReadLine());

    var decrypted_message = RSA_object.decrypt_text(encrypted_text, private_key, product_of_primes);
    Console.WriteLine(decrypted_message);
    Console.ReadLine();
}

Canvas canvas = new Canvas();

CL_REUFT.Text_UI.Create_Text(canvas, new CL_REUFT.Text_UI.Text("Welcome to CL-Reuft"));

CL_REUFT.Text_UI.Create_Button(canvas, new CL_REUFT.Text_UI.Text("RSA Encryption"), RSA_Encryption);
CL_REUFT.Text_UI.Create_Button(canvas, new CL_REUFT.Text_UI.Text("RSA Decryption"), RSA_Decryptor);
CL_REUFT.Text_UI.Create_Button(canvas, new CL_REUFT.Text_UI.Text("Flash Usb"), FlashUsb);


Console.ReadKey();

Console.CursorVisible = false;
while (true)
{

    Console.Clear();
    canvas.UpdateLoop();

}


