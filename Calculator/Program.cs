using System.Reflection.Metadata.Ecma335;
using System.Text;

ConsoleKeyInfo key;
int currentValue;
int calcIndex;
string calcText = "";
string calcActive = "";
float numOne;
float numTwo;

Console.OutputEncoding = Encoding.UTF8;

calcIndex = 0;

while (true)
{
    Console.SetCursorPosition(0, 0);
    string calcBase = @$"
╭─────────────────────╮  
│ ╭─────────────────╮ │  Controls:
│ │{calcText.PadLeft(17)}│ │  0-9 | Number Keys
│ ╰─────────────────╯ │   +  | Add
│ ╭───┬───┬───╮ ╭───╮ │   -  | Subtract
│ │ 7 │ 8 │ 9 │ │ {(calcActive == "add" ? "\x1b[0;33m" : "")}+[0m │ │   x  | Times
│ ├───┼───┼───┤ ├───┤ │   /  | divide
│ │ 4 │ 5 │ 6 │ │ {(calcActive == "minus" ? "\x1b[0;33m" : "")}-[0m │ │   C  | Cancel/CancelAll
│ ├───┼───┼───┤ ├───┤ │
│ │ 1 │ 2 │ 3 │ │ {(calcActive == "times" ? "\x1b[0;33m" : "")}x[0m │ │
│ ├───┼───┼───┤ ├───┤ │
│ │ {(calcText.ToLower().Contains('.') ? "\x1b[0;33m" : "")}.[0m │ 0 │ = │ │ {(calcActive == "divide" ? "\x1b[0;33m" : "")}/[0m │ │
│ ╰───┴───┴───╯ ╰───╯ │
╰─────────────────────╯
";
    Console.WriteLine(calcBase);

    key = Console.ReadKey(true);
    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter || (key.Key == ConsoleKey.OemPlus && key.Modifiers != ConsoleModifiers.Shift))
    {
        if (calcIndex < 17) {

            if ((key.Modifiers & ConsoleModifiers.Alt) != 0 || (key.Modifiers & ConsoleModifiers.Control) != 0)
            {
                continue;
            }

            if (key.Key == ConsoleKey.OemPeriod)
            {
                if (calcText.ToLower().Contains('.'))
                {
                    continue;
                }
                else if (calcIndex == 0)
                {
                    continue;
                }
            }

            int i;
            if (int.TryParse(key.KeyChar.ToString(), out i))
            {
                calcText += key.KeyChar;
                calcIndex += 1;
            }
            else if (key.Key == ConsoleKey.OemPeriod)
            {
                calcText += key.KeyChar;
                calcIndex += 1;
            }
            else if (key.Key == ConsoleKey.Add || ((key.Modifiers & ConsoleModifiers.Shift) != 0 && key.Key == ConsoleKey.OemPlus))
            {
                calcActive = "add";

            }
            else if (key.Key == ConsoleKey.Subtract || key.Key == ConsoleKey.OemMinus)
            {
                calcActive = "minus";
            }
            else if (key.Key == ConsoleKey.Multiply || ((key.Modifiers & ConsoleModifiers.Shift) != 0 && key.Key == ConsoleKey.D8) || key.Key == ConsoleKey.X)
            {
                calcActive = "times";
            }
            else if (key.Key == ConsoleKey.Divide || key.Key == ConsoleKey.Oem2)
            {
                calcActive = "divide";
            }
        }
    }
    else
    {
        Console.WriteLine("test");
        if (key.Key == ConsoleKey.Backspace)
        {
            if (calcIndex > 0)
            {
                calcText = calcText.Remove(calcText.Length - 1);
                calcIndex -= 1;
            }
        }
        else
        {
            calcActive = "";
        }
    }

}
