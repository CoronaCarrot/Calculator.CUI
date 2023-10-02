using System.Reflection.Metadata.Ecma335;
using System.Text;

bool debugMode = false;

ConsoleKeyInfo key;
int calcIndex;
string calcText = "";
string calcActive = "";
string calcPrep = "";
string numTemp = "none";

double numOne = 0;
double numTwo = 0;
double numAns = 0;

bool equalizer = false;

Console.OutputEncoding = Encoding.UTF8;

calcIndex = 0;

while (true)
{
    Console.SetCursorPosition(0, 0);

    string debugMenu = debugMode ? $@"
╭┤Debug Screen
│
│int calcIndex = {calcIndex}                    
│string calcText = ""{calcText}""                                                        
│string calcActive = ""{calcActive}""           
│string calcPrep = ""{calcPrep}""               
│string numTemp = ""{numTemp}""     
│
│double numOne = {numOne}                                                                       
│double numTwo = {numTwo}                                                               
│double numAns = {numAns}                                                                       
│
│bool equalizer = {equalizer}   
│
╰┤" : "";

    string calcBase = @$"
╭─────────────────────╮  
│ ╭─────────────────╮ │  Controls:
│ │{calcText.PadLeft(17)}│ │  0-9 | Number Keys
│ ╰─────────────────╯ │   +  | Add
│ ╭───┬───┬───╮ ╭───╮ │   -  | Subtract
│ │ 7 │ 8 │ 9 │ │ {(calcActive == "+" ? "\x1b[0;33m" : "")}+[0m │ │   x  | Times
│ ├───┼───┼───┤ ├───┤ │   /  | divide
│ │ 4 │ 5 │ 6 │ │ {(calcActive == "-" ? "\x1b[0;33m" : "")}-[0m │ │   C  | Cancel/CancelAll
│ ├───┼───┼───┤ ├───┤ │
│ │ 1 │ 2 │ 3 │ │ {(calcActive == "x" ? "\x1b[0;33m" : "")}x[0m │ │
│ ├───┼───┼───┤ ├───┤ │
│ │ {(calcText.ToLower().Contains('.') ? "\x1b[0;33m" : "")}.[0m │ 0 │ = │ │ {(calcActive == "/" ? "\x1b[0;33m" : "")}/[0m │ │
│ ╰───┴───┴───╯ ╰───╯ │
╰─────────────────────╯
{debugMenu}
";
    Console.WriteLine(calcBase);

    key = Console.ReadKey(true);
    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter && !(key.Key == ConsoleKey.OemPlus && key.Modifiers == 0))
    {
        if (!(calcIndex >= 17 && calcActive == ""))
        {
            
            if ((key.Modifiers & ConsoleModifiers.Alt) != 0 || (key.Modifiers & ConsoleModifiers.Control) != 0)
            {
                continue;
            }
            else if ((key.Key == ConsoleKey.OemPeriod || key.Key == ConsoleKey.Decimal) && (calcText.ToLower().Contains('.') || calcIndex == 0))
            {
                continue;
            }

            int i;
            if (int.TryParse(key.KeyChar.ToString(), out i))
            {
                if (equalizer)
                {
                    calcText = "";
                    calcIndex = 0;
                    equalizer = false;
                }
                if (calcActive != "")
                {
                    numTemp = numTemp == "none" ? calcText : numTemp;

                    calcIndex = 0;
                    calcText = "";
                    calcPrep = calcActive;
                    calcActive = "";
                }
                calcText += key.KeyChar;
                calcIndex += 1;
            }
            else if (key.Key == ConsoleKey.OemPeriod || key.Key == ConsoleKey.Decimal)
            {
                calcActive = calcActive != "" ? "" : calcActive;

                calcText += key.KeyChar;
                calcIndex += 1;
            }
        }
        if (key.Key == ConsoleKey.Add || ((key.Modifiers & ConsoleModifiers.Shift) != 0 && key.Key == ConsoleKey.OemPlus))
        {
            calcActive = calcIndex > 0 ? "+" : calcActive;
        }
        else if (key.Key == ConsoleKey.Subtract || key.Key == ConsoleKey.OemMinus)
        {
            calcActive = calcIndex > 0 ? "-" : calcActive;
        }
        else if (key.Key == ConsoleKey.Multiply || ((key.Modifiers & ConsoleModifiers.Shift) != 0 && key.Key == ConsoleKey.D8) || key.Key == ConsoleKey.X)
        {
            calcActive = calcIndex > 0 ? "x" : calcActive;
        }
        else if (key.Key == ConsoleKey.Divide || key.Key == ConsoleKey.Oem2)
        {
            calcActive = calcIndex > 0 ? "/" : calcActive;
        }
        equalizer = false;
    }
    else
    {
        if (key.Key == ConsoleKey.Backspace)
        {
            if (calcIndex > 0)
            {
                calcActive = "";
                calcText = calcText.Remove(calcText.Length - 1);
                calcIndex -= 1;
            }
        }
        else
        {
            equalizer = true;
            calcActive = "equals";
        }
    }

    if (numTemp != "none" && calcPrep != "" & calcActive != "")
    {
        numOne = Convert.ToDouble(numTemp);
        numTwo = Convert.ToDouble(calcText);

        switch (calcPrep)
        {
            case "/":
                numAns = numOne / numTwo;
                break;

            case "x":
                numAns = numOne * numTwo;
                break;

            case "+":
                numAns = numOne + numTwo;
                break;

            case "-":
                numAns = numOne - numTwo;
                break;

        }
        calcPrep = "";
        numOne = 0;
        numTwo = 0;
        numTemp = Convert.ToString(numAns);
        calcText = numTemp;
        calcIndex = Convert.ToString(numTemp).Length;
    }
    if (equalizer)
    {
        calcActive = "";
        calcPrep = "";
        numTemp = "none";

        numOne = 0;
        numTwo = 0;
        numAns = 0;
    }

}
