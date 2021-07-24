using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    #region Utils


    void Start()
    {
        InitTest1();
        InitTest2();
        InitTest3();
    }
    [System.Serializable]
    public class UIWrapper
    {
        public TMP_InputField inputField;
        public Button btn;
        public TMP_Text resultText;
        [HideInInspector]
        public string defaultText;

        public void Init(string defaultText) 
        {
            this.defaultText = defaultText;

            inputField.onValueChanged.AddListener((x) => { resultText.text = this.defaultText; });
                        
        }

    }

    #endregion

    #region Test 1 - Reverse Text

    [Header("Reverse Text")]

    public UIWrapper reverseTextUIWrapper;

    private void InitTest1()
    {
        reverseTextUIWrapper.Init("Test 1 Result");
        reverseTextUIWrapper.btn.onClick.AddListener(() =>
        {
            var result = ReverseString(reverseTextUIWrapper.inputField.text);
            reverseTextUIWrapper.resultText.text = result;
        });
    }

    private string ReverseString(string txt)
    {
        StringBuilder sb = new StringBuilder();

        for(int i = txt.Length - 1; i>=0 ; i--)
        {
            sb.Append(txt[i]);
        }

        return sb.ToString();
    }

    #endregion

    #region Test 2 - Generate Prime Numbers

    [Header("Prime Number")]

    public UIWrapper primeNumberUIWrapper;

    private List<int> savedPrime;

    private void InitTest2()
    {
        primeNumberUIWrapper.Init("Test 2 Result");
        savedPrime = new List<int>();
        savedPrime.Add(2);

        primeNumberUIWrapper.btn.onClick.AddListener(() =>
        {
            int input = 0;
            int.TryParse(primeNumberUIWrapper.inputField.text, out input);

            var result = GeneratePrimeNumbers(input);
            primeNumberUIWrapper.resultText.text = result;
        });

        primeNumberUIWrapper.inputField.onValueChanged.AddListener((x) => {

            int i = 0;
            if (int.TryParse(x, out i))
            {
                if (i > 15)
                    primeNumberUIWrapper.inputField.text = "15";
            }

        });

    }

    private string GeneratePrimeNumbers(int n)
    {
        if (n < 1)
            return "invalid";

        var sb = new StringBuilder();

        //fill savedPrime list
        if (n > savedPrime.Count)
        {
            var currentNum = savedPrime[savedPrime.Count - 1];

            while (n > savedPrime.Count)
            {
                currentNum++;

                for (int i = 0; i < savedPrime.Count; i++)
                {
                    if (currentNum % savedPrime[i] == 0)
                        break;
                    else if (i == savedPrime.Count - 1)
                    {                        
                        savedPrime.Add(currentNum);
                        break;
                    }
                }

            }
        }

        for(int i = 0; i < n; i++)
        {
            if (i == 0)
                sb.Append(savedPrime[i]);
            else
                sb.AppendFormat(", {0}", savedPrime[i]);
        }



        return sb.ToString();
    }




    #endregion

    #region Test 3 - Find Number Index

    [Header("Find Number Index")]
    public UIWrapper findNumUIWrapper;

    public Button fillArrayBtn;
    public TMP_Text arrayText;


    private int[] numArray;

    private void InitTest3()
    {
        findNumUIWrapper.Init("Test 3 Result");
        fillArrayBtn.onClick.AddListener(FillArray);
        numArray = new int[10];
        FillArray();

        findNumUIWrapper.btn.onClick.AddListener(() =>
        {
            string result = "invalid";

            int num;

            if(int.TryParse(findNumUIWrapper.inputField.text,out num))
            {
                result = GetIndexOfNumber(numArray, num);
            }

            findNumUIWrapper.resultText.text = result;

        });

        

    }

    private void FillArray()
    {
        findNumUIWrapper.resultText.text = findNumUIWrapper.defaultText;

        for(int i = 0; i < numArray.Length; i++)
        {
            numArray[i] = UnityEngine.Random.Range(0, 9);
        }

        StringBuilder sb = new StringBuilder();

        for(int i = 0; i < numArray.Length; i++)
        {
            if (i == 0)
                sb.Append(numArray[i]);
            else
                sb.AppendFormat(" {0}", numArray[i]);
        }

        arrayText.text = sb.ToString();

    }

    private string GetIndexOfNumber(int[] arr, int targetNumber)
    {

        var lastIndex = -1;
        var secondIndex = -1;

        int firstIndex = -1;
        
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == targetNumber)
            {
                if (firstIndex == -1)
                    firstIndex = i;
                else
                {
                    secondIndex = i;
                    break;
                }
            }

        }

        if (firstIndex != -1)
        {
            if (secondIndex == -1)
                lastIndex = firstIndex;
            else
            {
                for (int i = arr.Length - 1; i >= 0; i--)
                {
                    if (arr[i] == targetNumber)
                    {
                        lastIndex = i;
                        break;
                    }

                }
            }
        }
            

        


        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("Second sighting : index {0},\n", secondIndex);
        sb.AppendFormat("Last sighting : index {0}.", lastIndex);
        return sb.ToString();
    }



    #endregion
}
