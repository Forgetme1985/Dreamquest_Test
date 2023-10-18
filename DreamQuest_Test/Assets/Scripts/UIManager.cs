using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : GenericSingletonClass<UIManager>
{
    public const int CUBEQUANTITY = 10;
    private int redCubeCounter;
    private int greenCubeCounter;
    private int blueCubeCounter;
    public TextMeshProUGUI labelRedCube;
    public TextMeshProUGUI lableGreenCube;
    public TextMeshProUGUI lableBlueCube;

    public void Start()
    {
        redCubeCounter = greenCubeCounter = blueCubeCounter = CUBEQUANTITY;
        labelRedCube.text = redCubeCounter + "";
        lableGreenCube.text = greenCubeCounter + "";
        lableBlueCube.text = blueCubeCounter + "";

    }
    public void increaseCounter(int colorCode)
    {
        if (colorCode == 0)
        {
            redCubeCounter++;
            labelRedCube.text = redCubeCounter + "";
        }
        else if (colorCode == 1)
        {
            greenCubeCounter++;
            lableGreenCube.text = greenCubeCounter + "";
        }
        else if (colorCode == 2)
        {
            blueCubeCounter++;
            lableBlueCube.text = blueCubeCounter + "";
        }
    }
    public void decreaseCounter(int colorCode)
    {
        if (colorCode == 0)
        {
            redCubeCounter--;
            labelRedCube.text = redCubeCounter + "";
        }
        else if (colorCode == 1)
        {
            greenCubeCounter--;
            lableGreenCube.text = greenCubeCounter + "";
        }
        else if (colorCode == 2)
        {
            blueCubeCounter--;
            lableBlueCube.text = blueCubeCounter + "";
        }
    }
}
