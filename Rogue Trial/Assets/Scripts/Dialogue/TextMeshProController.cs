using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using NaughtyAttributes;

namespace GCODialogueEngine
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class TextMeshProController : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        TextMeshProUGUI textMeshProUGUI = null;
        private void OnValidate()
        {
            if (Application.isEditor)
            {
                if (textMeshProUGUI == null)
                {
                    textMeshProUGUI = GetComponent<TextMeshProUGUI>();
#if UNITY_EDITOR
                    UnityEditor.EditorUtility.SetDirty(this);
#endif
                }
            }
        }

        public enum Mode { scrollText, instant, }
        public Mode mode;

        Coroutine coroutine = null;

        public void PrintParagraph(Paragraph line)
        {
            if (printing)
            {
                StopCoroutine(coroutine);
            }
            printing = true;
            coroutine = StartCoroutine(ChangeText(line, mode));
        }

        bool printing = false;
        public bool IsPrinting { get => printing; private set => printing = value; }
        IEnumerator ChangeText(Paragraph paragraph, Mode mode)
        {
            float Overflow = 0;
            StringBuilder dialogueText = new StringBuilder("");

            while (paragraph.chars.Count > 0)
            {
                int charachtersToPrint = getCharachtersToPrint(ref Overflow);
                for (int i = 0; i < charachtersToPrint && paragraph.chars.Count > 0; i++)
                {
                    if (paragraph.chars[0].charachter != null)
                    {
                        textMeshProUGUI.text += paragraph.chars[0].charachter;
                        paragraph.chars.RemoveAt(0);
                    }
                    else
                    {
                        paragraph.chars[0].action.Invoke();
                        paragraph.chars.RemoveAt(0);
                    }
                }
                yield return null;
            }
            printing = false;
        }


        [MinValue(0)]
        public float characterPerSecond = 4;
        private int getCharachtersToPrint(ref float Overflow)
        {
            int charachtersToPrint = (int)((Time.deltaTime * characterPerSecond) + Overflow);
            Overflow += (Time.deltaTime * characterPerSecond) - charachtersToPrint;
            return charachtersToPrint;
        }
    }
}