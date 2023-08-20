using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Terminal : MonoBehaviour
{
    [System.Serializable]
    public struct Commands
    {
        public string commandString;
        [TextArea(5, 15)]
        public string outputString;
    }

    public static string hash;
    [SerializeField] private GameObject terminalWindow;
    [SerializeField] private TMP_InputField inputText;
    [SerializeField] private TextMeshProUGUI outputText;
    [SerializeField] private WebPage realDarkWeb;
    [SerializeField] private List<Commands> output = new List<Commands>();
    private bool isOn = false;

    private void Start()
    {
        realDarkWeb.url = LinkLists.CreateRandomURL();
        hash = CreateMD5(realDarkWeb.url);

        output.Add(new Commands
        {
            commandString = "hash " + hash,
            outputString = realDarkWeb.url
        });

        output.Add(new Commands
        {
            commandString = "hash " + hash + " -h",
            outputString = realDarkWeb.url
        });
    }

    private void Update()
    {
        if (!isOn) return;
        if(Input.GetKeyDown(KeyCode.Return))
        {
            outputText.text = getOutput(isValid());
        }
    }

    public void ToggleDOS(bool val)
    {
        isOn = val;
        terminalWindow.SetActive(isOn);
        inputText.ActivateInputField();
        if (val == false)
        {
            inputText.text = "";
            outputText.text = "";
            inputText.DeactivateInputField();
        }
    }

    private bool isValid()
    {
        foreach (Commands cmd in output)
        {
            if (inputText.text == cmd.commandString)
            {
                return true;
            }
        }
        return false;
    }

    private string getOutput(bool validity)
    {
        string outputString = "";
        if (validity == true)
        {
            foreach (Commands cmd in output)
            {
                if (inputText.text == cmd.commandString)
                {
                    outputString = cmd.outputString;
                    if (cmd.outputString == "shutdown")
                    {
                        ToggleDOS(false);
                        outputString = "";
                    }
                }
            }
        }
        else
        {
            outputString = "Invalid Command";
        }
        return outputString;
    }

    public static string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
