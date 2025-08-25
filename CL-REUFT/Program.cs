// See https://aka.ms/new-console-template for more information


using CL_REUFT;
using static CL_REUFT.Tetris;
using System.Text;
using static CL_REUFT.Text_UI;
using static CL_REUFT.CaesarCipher;
using static CL_REUFT.GetToTheTop;
using static CL_REUFT.MaxSort;
using System.Diagnostics;
using System.Numerics;
Console.OutputEncoding = Encoding.UTF8;


RSA RSA_object = new RSA(); // Create The RSA object (contains RSA methods), this probably could've been a static class but oh well
static void FlashUsb()
{
    Console.CursorVisible = true; 
    Console.WriteLine("THIS PROGRAM REQUIRES DD, PLEASE PROVIDE LOCATION OF DD"); 
    string DD_Location = Console.ReadLine(); //Sets where the DD program is located
   
        
        Console.WriteLine("Please tell the location of the ISO file"); 
    string IsoLocation = @Console.ReadLine();
    Console.WriteLine("Select a drive, First Input The number then the name of the usb");
    Console.WriteLine();

   

   System.Diagnostics.Process.Start("powershell", "get-disk");

    string driveNumber = Console.ReadLine();
    string driveName = Console.ReadLine();

    string script = "Clear-Disk " + driveNumber + " -RemoveData";
    ProcessStartInfo processStartInfo = new ProcessStartInfo //Create a process instance of powershell with admin
    {
        FileName = "Powershell.exe",
        Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command {script}",
        Verb = "runas",
        UseShellExecute = true
    };
    using (Process process = Process.Start(processStartInfo)) //Start the process
    {
        process.WaitForExit();
    }
    script = "New-Partition -DiskNumber " + driveNumber + " -UseMaximumSize -DriveLetter Z";

    processStartInfo = new ProcessStartInfo //Same thing with different command
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
    while (true) //Get the numbers, bigint has no normal way of getting ints from input hence the weird conversions
    {
        var tmp = Console.ReadLine()[0];
        if (tmp == '*') { break; }
        long tmp2 = Convert.ToInt64(tmp);
        var tmp3 = tmp2.ToString();
        Encrypted_Numbers.Add(BigInteger.Parse(tmp3));   
    }

    var Keys = RSA_object.GenerateKeys(); //Compute the keys, prime numbers are predefined
    //var Encrypted_Num = RSA_object.Encrypt_text(m, Keys[0], Keys[2]);
    foreach(var enc_num in Encrypted_Numbers) 
    {
        var Encrypted_Num = RSA_object.Encrypt_text(enc_num, Keys[0], Keys[2]); //encrypt the values using the keys
        Console.WriteLine(Encrypted_Num);
    }
   
    Console.ReadKey();
    
}
void RSA_Decryptor() 
{
    Console.CursorVisible = true;
    Console.Clear();
    Console.WriteLine("Give private key values (d, n)");
    var Keys = RSA_object.GenerateKeys(); //Generate keys
    var d = Keys[1];
    var n = Keys[2];
    List<BigInteger> Encrypted_Numbers = new List<BigInteger>(); 
    Console.WriteLine("Write the number series one at a time, type Q once you're done");
    while (true) //Same finnicky way of getting the numbers
    {
       
        var tmp = Console.ReadLine();
        if (tmp.ToUpper() == "Q") { break; }
        var tmp2= Convert.ToInt64(tmp);
        tmp = Convert.ToString(tmp);
        Encrypted_Numbers.Add(BigInteger.Parse(tmp));
    }
    foreach (var num in Encrypted_Numbers) 
    {
        Console.Write((char)RSA_object.decrypt_text(num, d, n)); //Convert decrypted nums into char values and print
    }
    Console.ReadKey();
    //Console.WriteLine(RSA_object.decrypt_text(Encrypted_Num, d, n));

}

void Caesar_Cipher() 
{
    Console.Clear();
    int ShiftAmount = 0;
    string EncryptedMsg = "";
    Console.WriteLine("Give the message");
    EncryptedMsg = Console.ReadLine();
    Console.WriteLine("Give the shift amount");
    ShiftAmount = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(CL_REUFT.CaesarCipher.ShiftString(EncryptedMsg, ShiftAmount));
    Console.WriteLine("Press any key to go back");
    Console.Read();
}

void Submenu_amusements()  //Just a submenu for the amusements section
{
Canvas Amusements_Canvas = new Canvas();
Console.Clear();
CL_REUFT.Text_UI.Create_Button(Amusements_Canvas, new CL_REUFT.Text_UI.Text("Tetris"), new Tetris().init);
    CL_REUFT.Text_UI.Create_Button(Amusements_Canvas, new CL_REUFT.Text_UI.Text("Climb to the top : Tile scrolling test"), new CL_REUFT.GetToTheTop().Init);
    CL_REUFT.Text_UI.Create_Button(Amusements_Canvas, new CL_REUFT.Text_UI.Text("Back"), main_menu);

    while (true) 
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Amusements_Canvas.UpdateLoop();

        Thread.Sleep(50);
        Console.SetCursorPosition(20, 29);
        Console.Write(" ");

    }
}

void bucketSort() 
{
    Console.Clear();
    Console.WriteLine("Give the list of values, write * once you're done");
    List<int> listofvalues = new List<int>();
    while (true)
    {
        var tmp = Console.ReadLine();
        if (tmp == "*") { break; }
        listofvalues.Add(int.Parse(tmp));
    }

    foreach (var i in CL_REUFT.BucketSort.bucketsort(listofvalues)) { Console.WriteLine(i); }
    Console.ReadKey();
}

void maxSort() 
{

    Console.Clear();
    Console.WriteLine("Give the list of values, write * once you're done");
    List<int> listofvalues = new List<int>();
    while(true)
    {
        var tmp = Console.ReadLine();
        if(tmp == "*") { break; }
        listofvalues.Add(int.Parse(tmp));
    }

    foreach (var i in CL_REUFT.MaxSort.maxSort(listofvalues)) { Console.WriteLine(i); }
    Console.ReadKey();
}
void Submenu_sorters() 
{
    Canvas Sorters_Canvas = new Canvas();
    Console.Clear();
    CL_REUFT.Text_UI.Create_Button(Sorters_Canvas, new CL_REUFT.Text_UI.Text("Max Sort"), maxSort);
    CL_REUFT.Text_UI.Create_Button(Sorters_Canvas, new CL_REUFT.Text_UI.Text("Bucket Sort"), bucketSort);
    CL_REUFT.Text_UI.Create_Button(Sorters_Canvas, new CL_REUFT.Text_UI.Text("Back"), main_menu);
    while (true)
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Sorters_Canvas.UpdateLoop();

        Thread.Sleep(50);
        Console.SetCursorPosition(20, 29);
        Console.Write(" ");

    }
}
void main_menu()
{
    //Canvas is where all the elements are, essentially does nothing right now, mostly there in case the UI code is expanded upon in the future
    Canvas Main_Menu_Canvas = new Canvas();
    CL_REUFT.Text_UI.Create_Text(Main_Menu_Canvas, new CL_REUFT.Text_UI.Text("Welcome to CL-Reuft"));
    //Buttons are pressable, text is display only
    CL_REUFT.Text_UI.Create_Button(Main_Menu_Canvas, new CL_REUFT.Text_UI.Text("RSA Encryption"), RSA_Encryption);
    CL_REUFT.Text_UI.Create_Button(Main_Menu_Canvas, new CL_REUFT.Text_UI.Text("RSA Decryption"), RSA_Decryptor);
    CL_REUFT.Text_UI.Create_Button(Main_Menu_Canvas, new CL_REUFT.Text_UI.Text("Caesar Cipher"), Caesar_Cipher);
    CL_REUFT.Text_UI.Create_Button(Main_Menu_Canvas, new CL_REUFT.Text_UI.Text("Flash Usb ||| Needs DD and Admin"), FlashUsb);

    CL_REUFT.Text_UI.Create_Button(Main_Menu_Canvas, new CL_REUFT.Text_UI.Text("Sorters"), Submenu_sorters);
    CL_REUFT.Text_UI.Create_Button(Main_Menu_Canvas, new CL_REUFT.Text_UI.Text("Amusements"), Submenu_amusements);
    





    Console.Clear();
    Console.CursorVisible = false;
    while (true) //Simple forever loop for the main menu, ideally this would be done inside the Canvas but I made poor design choices
    {


        Console.Clear();
        
        Console.SetCursorPosition(0, 0);
        Main_Menu_Canvas.UpdateLoop();
        Thread.Sleep(50);

        //Hack to stop (more so lessen) the flickering
        Console.SetCursorPosition(20, 29);
        Console.Write(" ");

    }
    


}

main_menu(); //start of program


