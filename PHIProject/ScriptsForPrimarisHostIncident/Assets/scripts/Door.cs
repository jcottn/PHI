using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum openTypeENUM { rotToOpen, moveToOpen }
    public openTypeENUM openType;
    public enum doorAxisENUM { X, Y, Z }
    public doorAxisENUM doorAxis;

    public bool onlyOpen;
    public bool canBeOpenedNow;
    private bool _isOpen;

    public float openSpeed = 150f;
    public float openDistOrAngle = 140f;

     
    public AudioSource moveOrRotSound;
    public AudioSource openSound;
    public AudioSource closeSound;
    public AudioSource notOpeningSound; 
    


    public GameObject doorHandle;
    public enum handleAxisENUM { X, Y, Z };
    public handleAxisENUM handleAxis;
    public float handleRotAngle = -45f;
    private Quaternion handleStartRot;
    private float startDistOrAngle;
    private bool openCloseOn;
    public GameObject interactionInage;


    private void Start()
    {
        if (openType == openTypeENUM.moveToOpen)
        {
            if (doorAxis == doorAxisENUM.X)
            {
                startDistOrAngle = transform.localPosition.x;
            }
            else if (doorAxis == doorAxisENUM.Y)
            {
                startDistOrAngle = transform.localPosition.y;
            }
            else if (doorAxis == doorAxisENUM.Z)
            {
                startDistOrAngle = transform.localPosition.z;
            }
        }
        else
        {
            if (doorAxis == doorAxisENUM.X)
            {
                startDistOrAngle = transform.localEulerAngles.x;
            }
            else if (doorAxis == doorAxisENUM.Y)
            {
                startDistOrAngle = transform.localEulerAngles.y;
            }
            else if (doorAxis == doorAxisENUM.Z)
            {
                startDistOrAngle = transform.localEulerAngles.z;
            }
        }
        if (doorHandle) handleStartRot = doorHandle.transform.localRotation;
    }

    private void OnMouseEnter()
    {
        if (gameObject.tag == "door")
        {
            interactionInage.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (gameObject.tag == "door")
        {
            interactionInage.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (gameObject.tag == "door")
        {
            if (handleAxis == handleAxisENUM.X)
            {
                doorHandle.transform.localRotation = Quaternion.Euler(handleRotAngle, 0f, 0f);
            }
            else if (handleAxis == handleAxisENUM.Y)
            {
                doorHandle.transform.localRotation = Quaternion.Euler(0f, handleRotAngle, 0f);
            }
            else if (handleAxis == handleAxisENUM.Z)
            {
                doorHandle.transform.localRotation = Quaternion.Euler(0f, 0f, handleRotAngle);
            }
            OpenClose();
        }
    }

    private void OnMouseUp()
    {
        if (doorHandle)
        {
            doorHandle.transform.localRotation = handleStartRot;
        }
    }

    private void Update()
    {
        if (openCloseOn)
        {
            if (_isOpen) // open door
            {
                if (openType == openTypeENUM.moveToOpen)
                {
                    if (doorAxis == doorAxisENUM.X)
                    {
                        float posX = Mathf.MoveTowards(transform.localPosition.x, startDistOrAngle + openDistOrAngle, openSpeed * Time.deltaTime);
                        transform.localPosition = new Vector3(posX, transform.localPosition.y, transform.localPosition.z);
                        if (transform.localRotation.x == startDistOrAngle + openDistOrAngle)
                        {
                            StopOpenClose();
                        }
                    }
                    else if (doorAxis == doorAxisENUM.Y)
                    {
                        float posY = Mathf.MoveTowards(transform.localPosition.y, startDistOrAngle + openDistOrAngle, openSpeed * Time.deltaTime);
                        transform.localPosition = new Vector3(transform.localPosition.x, posY, transform.localPosition.z);
                        if (transform.localRotation.y == startDistOrAngle + openDistOrAngle)
                        {
                            StopOpenClose();
                        }
                    }
                    else if (doorAxis == doorAxisENUM.Z)
                    {
                        float posZ = Mathf.MoveTowards(transform.localPosition.z, startDistOrAngle + openDistOrAngle, openSpeed * Time.deltaTime);
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, posZ);
                        if (transform.localRotation.z == startDistOrAngle + openDistOrAngle)
                        {
                            StopOpenClose();
                        }
                    }
                    else // rotation door
                    {
                        if (doorAxis == doorAxisENUM.X)
                        {
                            float angleX = Mathf.MoveTowardsAngle(transform.localEulerAngles.x, startDistOrAngle + openDistOrAngle, openSpeed * Time.deltaTime);
                            transform.localEulerAngles = new Vector3(angleX, 0, 0);
                            if (transform.localEulerAngles.x == startDistOrAngle + openDistOrAngle)
                            {
                                StopOpenClose();
                            }
                        }
                        else if (doorAxis == doorAxisENUM.Y)
                        {
                            float angleY = Mathf.MoveTowardsAngle(transform.localEulerAngles.y, startDistOrAngle + openDistOrAngle, openSpeed * Time.deltaTime);
                            transform.localEulerAngles = new Vector3(angleY, 0, 0);
                            if (transform.localEulerAngles.y == startDistOrAngle + openDistOrAngle)
                            {
                                StopOpenClose();
                            }
                        }
                        else if (doorAxis == doorAxisENUM.Z)
                        {
                            float angleZ = Mathf.MoveTowardsAngle(transform.localEulerAngles.z, startDistOrAngle + openDistOrAngle, openSpeed * Time.deltaTime);
                            transform.localEulerAngles = new Vector3(angleZ, 0, 0);
                            if (transform.localEulerAngles.z == startDistOrAngle + openDistOrAngle)
                            {
                                StopOpenClose();
                            }
                        }
                    }
                }
            }
            else // close door
            {
                if (openType == openTypeENUM.moveToOpen)
                {
                    if (doorAxis == doorAxisENUM.X)
                    {
                        float posX = Mathf.MoveTowards(transform.localPosition.x, startDistOrAngle, openSpeed * Time.deltaTime);
                        transform.localPosition = new Vector3(posX, transform.localPosition.y, transform.localPosition.z);
                        if (transform.localRotation.x == startDistOrAngle + openDistOrAngle)
                        {
                            StopOpenClose();
                        }
                    }
                    else if (doorAxis == doorAxisENUM.Y)
                    {
                        float posY = Mathf.MoveTowards(transform.localPosition.y, startDistOrAngle, openSpeed * Time.deltaTime);
                        transform.localPosition = new Vector3(transform.localPosition.x, posY, transform.localPosition.z);
                        if (transform.localRotation.y == startDistOrAngle + openDistOrAngle)
                        {
                            StopOpenClose();
                        }
                    }
                    else if (doorAxis == doorAxisENUM.Z)
                    {
                        float posZ = Mathf.MoveTowards(transform.localPosition.z, startDistOrAngle, openSpeed * Time.deltaTime);
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, posZ);
                        if (transform.localRotation.z == startDistOrAngle + openDistOrAngle)
                        {
                            StopOpenClose();
                        }
                    }
                    else // rotation door
                    {
                        if (doorAxis == doorAxisENUM.X)
                        {
                            float angleX = Mathf.MoveTowardsAngle(transform.localEulerAngles.x, startDistOrAngle, openSpeed * Time.deltaTime);
                            transform.localEulerAngles = new Vector3(angleX, 0, 0);
                            if (transform.localEulerAngles.x == startDistOrAngle + openDistOrAngle)
                            {
                                StopOpenClose();
                            }
                        }
                        else if (doorAxis == doorAxisENUM.Y)
                        {
                            float angleY = Mathf.MoveTowardsAngle(transform.localEulerAngles.y, startDistOrAngle, openSpeed * Time.deltaTime);
                            transform.localEulerAngles = new Vector3(angleY, 0, 0);
                            if (transform.localEulerAngles.y == startDistOrAngle + openDistOrAngle)
                            {
                                StopOpenClose();
                            }
                        }
                        else if (doorAxis == doorAxisENUM.Z)
                        {
                            float angleZ = Mathf.MoveTowardsAngle(transform.localEulerAngles.z, startDistOrAngle, openSpeed * Time.deltaTime);
                            transform.localEulerAngles = new Vector3(angleZ, 0, 0);
                            if (transform.localEulerAngles.z == startDistOrAngle + openDistOrAngle)
                            {
                                StopOpenClose();
                            }
                        }
                    }
                }
            }
        }
    }
    
    public void OpenClose()
    {
        if (canBeOpenedNow)
        {
            if (moveOrRotSound)
            {
                moveOrRotSound.Play();
            }
            openCloseOn = true;
            if (_isOpen)
            {
                _isOpen = false;
            }
            else
            {
                _isOpen = true;
                if(openSound)
                {
                    openSound.Play();
                }
                if(onlyOpen)
                {
                    gameObject.tag = "Untagged";
                    interactionInage.SetActive(false);
                }
            }
        }
       else
        {
            if(notOpeningSound)
            {
                notOpeningSound.Play();
                print("Close");
            }
        }
    }

    void StopOpenClose()
    {
        openCloseOn = false;
        if (moveOrRotSound)
        {
            moveOrRotSound.Stop();
        }
        if (closeSound && !_isOpen)
        {
            closeSound.Play();
        }
    }
}

                    