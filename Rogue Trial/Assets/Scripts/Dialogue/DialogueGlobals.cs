using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GCODialogueEngine
{
    public delegate void DialogueEngineActionCharachterEvent();
    public struct ActionCharachter
    {
        public char? charachter;
        public DialogueEngineActionCharachterEvent action;
    }
    public struct Paragraph
    {
        public List<ActionCharachter> chars;
        public void Load(string str)
        {
            for(int i = 0; i<str.Length;i++)
            {

            }
        }
    }
}
