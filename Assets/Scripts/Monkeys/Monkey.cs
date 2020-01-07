using TMPro;
using UnityEngine;

namespace Monkeys
{
    public class Monkey : MonoBehaviour
    {
        public GameObject dialogTextObject;
        [HideInInspector] public TextMeshPro dialogText;
        
        public GameObject dialogNextTextObject;
        [HideInInspector] public TextMeshPro dialogNextText;
        
        public GameObject dialogBackTextObject;
        [HideInInspector] public TextMeshPro dialogBackText;

        public MinigamesPlayer player;

        protected virtual void Start()
        {
            dialogText = dialogTextObject.GetComponent<TextMeshPro>();
            dialogNextText = dialogNextTextObject.GetComponent<TextMeshPro>();
        }
    }
}