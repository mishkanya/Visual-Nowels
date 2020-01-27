using System.Collections;
using UnityEngine;
using System.Xml;
using TMPro;

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
    public void ReadNewText()
    {
        if(_personID == _dialogReader.Persons.Count - 1 && _replicID == _dialogReader.Persons[_personID].PersonReplics.Count - 1 )
        {
            print("no new bitch");
            return;
        }
        if(_replicID == _dialogReader.Persons[_personID].PersonReplics.Count - 1){
            _replicID = 0;
            _personID++;
        }
        else{
            _replicID++;
        }
        Write();
    }
    public void ReadOldText()
    {
        
        if(_personID == 0 && _replicID == 0)
        {
            print("no old bitch");
            return;
        }
        if(_replicID == 0){
            _personID--;
            _replicID = _dialogReader.Persons[_personID].PersonReplics.Count - 1;
        }
        else{
            _replicID--;
        }
        Write();
    }

    private void Write()
    {
        StopAllCoroutines();
        SetName(_dialogReader.Persons[_personID].Name,_dialogReader.Persons[_personID].HEXColor);
        StartCoroutine(WriteText(_dialogReader.Persons[_personID].PersonReplics[_replicID]));
    }
}



