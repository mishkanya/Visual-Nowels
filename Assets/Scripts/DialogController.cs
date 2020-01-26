using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using TMPro;
using System.Xml.Serialization;
using System.IO;

public class DialogController : MonoBehaviour
{
    public GameSettings Settings;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DialogBox;

    private int _personID = 0, 
                _replicID = 0; 
    private DialogReader _dialogReader;
    private void Start() {
        XmlDocument XMLDoc = new XmlDocument();
        XMLDoc.LoadXml(Settings.XMLFile.text);
        _dialogReader = new DialogReader(XMLDoc);

        Write();
    }

    public void SetName(string name, string HEXColor){
        NameText.text = name;
        if(HEXColor != "" && HEXColor != null){
            ColorUtility.TryParseHtmlString(HEXColor, out Color newColor);
            NameText.color = newColor;
        }
        else
        {
            ColorUtility.TryParseHtmlString(Settings.DefaultHEXColor, out Color newColor);
            NameText.color = newColor;
        }
    }
    IEnumerator WriteText(string replic){
        DialogBox.text = "";
        for(int i = 0; i < replic.Length; i++){
            if(replic[i] != '<')
            {
                DialogBox.text += replic[i];
            }
            else
            {
                while(true){
                    DialogBox.text += replic[i];
                    i++;

                    if(replic[i] == '>'){
                        DialogBox.text += replic[i];
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(Settings.SpeedOfTextWriter);
        }
    }
    public void Write(){
        SetName(_dialogReader.Persons[_personID].Name,_dialogReader.Persons[_personID].HEXColor);
        StartCoroutine(WriteText(_dialogReader.Persons[_personID].PersonReplics[_replicID]));
        if(_replicID == _dialogReader.Persons[_personID].PersonReplics.Count - 1){
            _replicID = 0;
            _personID++;
        }
        else{
            _replicID++;
        }
    }
    public void Write(bool writeNext){
        
    }
}



