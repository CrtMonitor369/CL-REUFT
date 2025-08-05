using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    static internal class Text_UI
    {

        public class Text(string text) {public string Text_text = text; }
        public class Button (Action behaviour, Text text, int id)
        {
            public Text ButtonText = text;
            public Action ButtonBehaviour = behaviour;
            public int ID =id;
        }

        public class Canvas 
        {

         int Selected_Button_ID = 0;
         public List<Text> TextList = new List<Text>();
         public List<Button> ButtonList = new List<Button>();
         private void RenderCanvasElements() 
            {
               
                foreach (Text text in TextList) 
                {
         
                    Console.WriteLine(text.Text_text);
                }
                foreach (Button button in ButtonList) 
                {
                  
                    if (button.ID == Selected_Button_ID) { Console.Write("*"); }
                    Console.WriteLine(button.ButtonText.Text_text);
                }
            }
        private void CanvasLogic() 
            {
                if (Selected_Button_ID < 0) { Selected_Button_ID = ButtonList.Count - 1; }
                else
                {
                    Selected_Button_ID = Selected_Button_ID % (ButtonList.Count);
                }
                if (Console.KeyAvailable) 
                {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter) { ButtonList[Selected_Button_ID].ButtonBehaviour(); }
                if (key.Key == ConsoleKey.UpArrow) { Selected_Button_ID -= 1; }
                if (key.Key == ConsoleKey.DownArrow) { Selected_Button_ID += 1; }
                }
            }
        public void UpdateLoop() 
            {
            RenderCanvasElements();
            CanvasLogic();
            }

        }

     

        static public void Create_Button(Canvas canvas, Text ButtonText, Action ButtonBehaviour) {
            canvas.ButtonList.Add(new Button(ButtonBehaviour, ButtonText, canvas.ButtonList.Count));    
        
        }
        static public void Create_Text(Canvas canvas, Text text) 
        {
        canvas.TextList.Add(text);
        }

    }
}
