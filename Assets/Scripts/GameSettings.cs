using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml;
using System.IO;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Visual Nowels/GameSettings", order = 0)]
public class GameSettings : ScriptableObject {
    
    [Range(0.001f,0.5f)]
    public float SpeedOfTextWriter = 0.05f;
    public TextAsset XMLFile;
    public string DefaultHEXColor = "#042466";
}
