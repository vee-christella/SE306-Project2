//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class HighlightHoverController : MonoBehaviour
//{
//    public GameObject selectedObject;
//    public int red;
//    public int green;
//    public int blue;
//    public bool hoveringOverObject = false;
//    public bool flashingIn = true;
//    public bool startedFlashing = false;

//    // Update is called once per frame
//    void Update()
//    {
//        if (GameController.Instance.Game.HasStarted && hoveringOverObject && selectedObject.name.Contains("Building"))
//        {
//            Debug.Log("HOVER UPDATE");
//            selectedObject.GetComponent<Renderer>().material.color = new Color32((byte)red, (byte)green, (byte)blue, 255);
//        }
//    }

//    void OnMouseOver()
//    {
//        selectedObject = GameObject.Find(MouseHoverController.selectedObject);

//        // Only highlight buildings
//        if (selectedObject.name.Contains("Building"))
//        {
//            Debug.Log("MOUSE EXIT");
//            hoveringOverObject = true;

//            if (!startedFlashing)
//            {
//                startedFlashing = true;
//                StartCoroutine(FlashObject());
//            }
//        }

//    }

//    void OnMouseExit()
//    {
//        // Only highlight buildings
//        if (selectedObject.name.Contains("Building"))
//        {
//            Debug.Log("MOUSE EXIT");
//            hoveringOverObject = false;
//            startedFlashing = false;
//            StopCoroutine(FlashObject());
//            selectedObject.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
//        }
//    }

//    IEnumerator FlashObject()
//    {
//        while (hoveringOverObject)
//        {
//            yield return new WaitForSeconds(0.05f);

//            if (flashingIn)
//            {
//                if (blue <= 30)
//                {
//                    flashingIn = false;
//                }
//                else
//                {
//                    blue -= 25;
//                    green -= 1;
//                }
//            }

//            if (!flashingIn)
//            {
//                if (blue >= 250)
//                {
//                    flashingIn = true;
//                }
//                else
//                {
//                    blue += 25;
//                    green += 1;
//                }
//            }
//        }
//    }

//}
