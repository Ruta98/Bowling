using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling.Classes
{
    class InputOutputData : BaseViewModel
    {
        string inputText;
        public string InputText
        {
            get => inputText;
            set
            {
                inputText = value;
                OnPropertyChanged(nameof(InputText));
            }
        }
        string outputText;
        public string OutputText
        {
            get => outputText;
            set
            {
                outputText = value;
                OnPropertyChanged(nameof(OutputText));
            }
        }
    }
}
