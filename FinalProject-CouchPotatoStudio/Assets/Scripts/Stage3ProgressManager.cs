using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3ProgressManager : MonoBehaviour
{
    public GameObject winScreen, gameUI;
    private float timeOffset;
    public Text objectiveText;
    public AudioSource music;
    public Transform winCamera;
    private Transform mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform.parent;
        timeOffset = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        objectiveText.text = (Mathf.RoundToInt(60 + timeOffset - Time.time).ToString());
        if ((60 + timeOffset - Time.time) < 0)
        {
            GetComponent<HealthManager>().currentHealth = 0;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Attack"))
        {
            return;
        }
        if (collider.CompareTag("KeyItem"))
        {
            if (collider.gameObject.GetComponent<KeyItem>().itemName == "ofuda")
            {
                GetComponent<HealthManager>().death = true;
                GetComponent<Animator>().SetTrigger("Win");
                gameObject.GetComponent<PlayerMovement>().controlsLocked = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                gameUI.SetActive(false);
                StartCoroutine(Win());
                StartCoroutine(MoveCamera());
            }
            Destroy(collider.gameObject);
        }
    }

    IEnumerator Win()
    {
        StartCoroutine(FadeMusic());
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().cameraLocked = true;
        yield return new WaitForSeconds(2);
        winScreen.SetActive(true);
        Image loseBG = winScreen.GetComponent<Image>();
        float a = loseBG.color.a;
        for (int i = 0; i < 181; i++)
        {
            loseBG.color = new Color(loseBG.color.r, loseBG.color.g, loseBG.color.b, i / 120f * a);
            for (int j = 0; j < winScreen.transform.childCount; j++)
            {
                Transform child = winScreen.transform.GetChild(j);
                Text text;
                if (child.gameObject.TryGetComponent(out text))
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, i / 180f);
                }
                else if (child.childCount != 0)
                {
                    text = child.GetChild(0).gameObject.GetComponent<Text>();
                    text.color = new Color(text.color.r, text.color.g, text.color.b, i / 180f);
                }
            }
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    IEnumerator FadeMusic()
    {
        for (int i = 100; i > -1; i--)
        {
            music.volume = i / 100f;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    IEnumerator MoveCamera()
    {
        for (int i = 0; i < 300; i++)
        {
            mainCamera.position = Vector3.Lerp(mainCamera.position, winCamera.position, 1f * Time.deltaTime);
            mainCamera.LookAt(transform, Vector3.up);
            yield return new WaitForEndOfFrame();
        }
    }
}
