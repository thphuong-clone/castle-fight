using System;
using System.Collections.Generic;
using System.Xml.Linq;

public class Dialogue
{
    private List<Speech> listSpeech;

    public List<Speech> ListSpeech
    {
        get { return listSpeech;}
    }

    public Dialogue(List<Speech> listSpeech)
    {
        this.listSpeech = listSpeech;
    }

    public Dialogue(string dialogue)
    {
        string xmlPath;
        GameUtil.GameConstant.dialoguesIndex.TryGetValue(dialogue, out xmlPath);

        if (xmlPath != null)
        {
            XDocument xmlDoc = new XDocument();
            xmlDoc = XDocument.Load(xmlPath);

            UnityEngine.Debug.Log(xmlPath);

            List<Speech> speechsInDialogue = new List<Speech>();
            XElement root = (XElement) xmlDoc.FirstNode;

            UnityEngine.Debug.Log(root == null);

            foreach (XElement node in root.Elements("speech"))
            {
                speechsInDialogue.Add(GetSpeech(node));
            }

            this.listSpeech = speechsInDialogue;
        }
    }

    public Speech GetSpeech(XElement node)
    {
        Speech speech = new Speech();

        speech.character = node.Attribute("character").Value;
        speech.dialogueText = node.Attribute("dialogueText").Value;

        return speech;
    }

    public class Speech
    {
        public string character;
        public string dialogueText;
    }
}
