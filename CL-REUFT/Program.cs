// See https://aka.ms/new-console-template for more information


using CL_REUFT;
using static CL_REUFT.Tetris;
using System.Text;
using static CL_REUFT.Text_UI;

using System.Diagnostics;
Console.OutputEncoding = Encoding.UTF8;


RSA RSA_object = new RSA();
static void FlashUsb()
{
    Console.CursorVisible = true;
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
   

List<long> primes = new List<long>();
    Console.WriteLine();
 
    primes.Add(0);
    primes.Add(0);
    do
    {
        Console.WriteLine("Give the two primes");
        primes[0] = (Convert.ToInt32(Console.ReadLine()));
        primes[1] = (Convert.ToInt32(Console.ReadLine()));
    }
    while ((primes[0] == 0 || primes[1] == 0) && (primes[0].GetType() != typeof(int) || primes[1].GetType() != typeof(int)));

    
    
    


        Console.WriteLine("Give the message to be encrypted (It will be converted into an integer form)");
        var info = RSA_object.Encrypt_text(Console.ReadLine(), primes);
    var encrypted_message = info.Item1;
    Console.Write("[");
        foreach(var item in encrypted_message) {  Console.Write(item); Console.Write(" "); }
    Console.Write("]");
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

    List<long> encrypted_message = new List<long>();
    Console.WriteLine("Enter every number of the encrypted message, type S to stop");
    var tmp = Console.ReadLine();
    do
    {
        tmp = Console.ReadLine();
        if (tmp.ToUpper() != "S")
        {
            encrypted_message.Add(Convert.ToInt32(tmp));
        }
    }
    while (tmp.ToUpper() != "S");

    Console.WriteLine("Give private key");
    long private_key = Convert.ToInt64(Console.ReadLine());
    Console.WriteLine("Give Product of primes");
    long product_of_primes = Convert.ToInt64(Console.ReadLine());
    List<long> tmp2 = RSA_object.decrypt_text(encrypted_message, private_key, product_of_primes);

    foreach (long num in tmp2) { Console.Write(num); }
    
    
    Console.WriteLine("Press Any key to go back");
    Console.ReadLine();
}



void Submenu_amusements() 
{
Canvas Amusements_Canvas = new Canvas();
Console.Clear();
CL_REUFT.Text_UI.Create_Button(Amusements_Canvas, new CL_REUFT.Text_UI.Text("Tetris"), new Tetris().init);
    CL_REUFT.Text_UI.Create_Button(Amusements_Canvas, new CL_REUFT.Text_UI.Text("Back"), main_menu);

    while (true) 
    {
     Console.Clear();
     Amusements_Canvas.UpdateLoop();
    
    }
}

void main_menu()
{
    Canvas Main_Menu_Canvas = new Canvas();
    CL_REUFT.Text_UI.Create_Text(Main_Menu_Canvas, new CL_REUFT.Text_UI.Text("Welcome to CL-Reuft"));

    CL_REUFT.Text_UI.Create_Button(Main_Menu_Canvas, new CL_REUFT.Text_UI.Text("RSA Encryption"), RSA_Encryption);
    CL_REUFT.Text_UI.Create_Button(Main_Menu_Canvas, new CL_REUFT.Text_UI.Text("RSA Decryption"), RSA_Decryptor);
    CL_REUFT.Text_UI.Create_Button(Main_Menu_Canvas, new CL_REUFT.Text_UI.Text("Flash Usb ||| Needs DD and Admin"), FlashUsb);

    CL_REUFT.Text_UI.Create_Button(Main_Menu_Canvas, new CL_REUFT.Text_UI.Text("Amusements"), Submenu_amusements);





    Console.Clear();
    Console.CursorVisible = false;
    while (true)
    {

        Console.Clear();
        Main_Menu_Canvas.UpdateLoop();

    }
    


}

main_menu();


