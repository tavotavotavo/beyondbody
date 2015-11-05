using Domain;
using System.Collections.Generic;
using WindowsInput;

namespace KeyboardSimulation.Simulators
{
    public class KeyboardSimulator
    {
        public void PressKeys(IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                //Agregamos un espacio al final para que pueda seguir hablando y no le junte las palabras
                InputSimulatorExtension.SimulateTextEntry(string.Concat(word.Value));
                InputSimulator.SimulateKeyDown(VirtualKeyCode.SPACE);
            }
        }

        public static class InputSimulatorExtension
        {
            public static void SimulateTextEntry(string text)
            {
                string accents = "áéíóúÁÉÍÚÓ";
                string noaccents = "aeiouAEIOU";

                foreach (char character in text)
                {
                    bool shouldAccent = false;
                    uint charValue;
                    char newChar = character;

                    if (accents.Contains(character.ToString()))
                    {
                        shouldAccent = true;
                        charValue = (uint)noaccents[accents.IndexOf(character)];
                    }
                    else
                    {
                        charValue = (uint)character;
                    }


                    if (char.IsLower(character))
                    {
                        if (shouldAccent)
                        {
                            charValue = ((uint)noaccents[accents.IndexOf(character)]) - 32;
                        }
                        else
                        {
                            charValue = ((uint)character) - 32;
                        }
                    }

                    if (shouldAccent)
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.OEM_1);

                    if (char.IsUpper(character)) InputSimulator.SimulateKeyDown(VirtualKeyCode.SHIFT);

                    InputSimulator.SimulateKeyPress((VirtualKeyCode)charValue);

                    if (char.IsUpper(character)) InputSimulator.SimulateKeyUp(VirtualKeyCode.SHIFT);
                }
            }
        }

        public void PressKey(Word word)
        {
            this.PressKeys(new List<Word> { word });
        }

        public void PressAddSymbol()
        {
            InputSimulator.SimulateKeyPress(VirtualKeyCode.ADD);
            InputSimulator.SimulateKeyPress(VirtualKeyCode.BACK);
        }
    }
}