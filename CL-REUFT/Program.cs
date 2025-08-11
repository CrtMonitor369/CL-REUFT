// See https://aka.ms/new-console-template for more information


using CL_REUFT;
using static CL_REUFT.Tetris;
using System.Text;
using static CL_REUFT.Text_UI;

using System.Diagnostics;
using System.Numerics;
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
    List<BigInteger> Encrypted_Numbers= new List<BigInteger>();
    Console.WriteLine("Give all the letters of the thing you want to encrypt, type * once you're done");
    while (true) 
    {
        var tmp = Console.ReadLine()[0];
        if (tmp == '*') { break; }
        long tmp2 = Convert.ToInt64(tmp);
        var tmp3 = tmp2.ToString();
        Encrypted_Numbers.Add(BigInteger.Parse(tmp3));   
    }

    var Keys = RSA_object.GenerateKeys();
    //var Encrypted_Num = RSA_object.Encrypt_text(m, Keys[0], Keys[2]);
    foreach(var enc_num in Encrypted_Numbers) 
    {
        var Encrypted_Num = RSA_object.Encrypt_text(enc_num, Keys[0], Keys[2]);
        Console.WriteLine(Encrypted_Num);
    }
   
    Console.ReadKey();
    
}
void RSA_Decryptor() 
{
    Console.CursorVisible = true;
    Console.Clear();
    Console.WriteLine("Give private key values (d, n)");
    var Keys = RSA_object.GenerateKeys();
    var d = Keys[1];
    var n = Keys[2];
    List<BigInteger> Encrypted_Numbers = new List<BigInteger>();
    Console.WriteLine("Write the number series one at a time, type Q once you're done");
    while (true) 
    {
       
        var tmp = Console.ReadLine();
        if (tmp.ToUpper() == "Q") { break; }
        var tmp2= Convert.ToInt64(tmp);
        tmp = Convert.ToString(tmp);
        Encrypted_Numbers.Add(BigInteger.Parse(tmp));
    }
    foreach (var num in Encrypted_Numbers) 
    {
        Console.Write((char)RSA_object.decrypt_text(num, d, n));
    }
    Console.ReadKey();
    //Console.WriteLine(RSA_object.decrypt_text(Encrypted_Num, d, n));

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


