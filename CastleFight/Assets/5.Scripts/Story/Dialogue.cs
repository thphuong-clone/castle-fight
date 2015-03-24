using System;
using System.Collections.Generic;
using System.Xml;

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
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            List<Speech> speechsInDialogue = new List<Speech>();
            XmlNode root = xmlDoc.SelectSingleNode("dialogue");

            foreach (XmlNode node in root.SelectNodes("speech"))
            {
                speechsInDialogue.Add(GetSpeech(node));
            }

            this.listSpeech = speechsInDialogue;
        }
    }

    public Speech GetSpeech(XmlNode node)
    {
        Speech speech = new Speech();

        speech.character = node.Attributes.GetNamedItem("character").Value;
        speech.dialogueText = node.Attributes.GetNamedItem("dialogueText").Value;

        return speech;
    }

    public class Speech
    {
        public string character;
        public string dialogueText;
    }
}
