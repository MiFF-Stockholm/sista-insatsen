using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class CreditsScript : MonoBehaviour 
{
	public GUIText creditTextItem;
	private List<GUIText> Credits = new List<GUIText>();
	private TextReader tr;
	public string path;
	private List<string> credits = new List<string>();

	public float creditsCooldown = 2.0f;
	public float creditSpeed = 1f;
	private float timer = 0f;
	private Vector3 startPos;
	private int iconCounter = 0;

	public List<GUITexture> creditIcons = new List<GUITexture>();
	
	// Use this for initialization
	void Start () 
	{
		// Set the path for the credits.txt file
		path = "Assets/Resources/Credits.txt";
		
		// Create reader & open file
		tr = new StreamReader(path, System.Text.Encoding.Default);
		
		string temp;
		while((temp = tr.ReadLine()) != null)
		{
			// Read a line of text
			credits.Add(temp);
		}
		
		// Close the stream
		tr.Close();

		startPos = GameObject.Find("CreditStart").transform.position;
		//CreateCredits();
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.LoadLevel("MainMenu");
        }

		if(timer <= 0){
			timer = creditsCooldown*Time.deltaTime;
			if(credits.Count != 0){
				CreateNewCredit();
			}else{
				if(creditIcons.Count > iconCounter){
					MoveIcon();
				}
			}
		}

		timer -= 1*Time.deltaTime;


		float credSpeed = creditSpeed *Time.deltaTime;

		GameObject[] creds = GameObject.FindGameObjectsWithTag("Credit");
		foreach (GameObject cred in creds){
			cred.transform.Translate(new Vector3(0f, credSpeed, 0f));
		}
		/*GameObject[] icons = GameObject.FindObjectsOfType(typeof(GUITexture)) as GameObject[];
		foreach(GameObject icon in icons){
			icon.transform.Translate(new Vector3(0f, credSpeed, 0f));
		}*/
	}

	void CreateNewCredit(){
		string c = credits[0];
		Instantiate(creditTextItem);
		creditTextItem.transform.position = startPos;
		creditTextItem.text = c;
		//Debug.Log(c);
		Credits.Add(creditTextItem);

		credits.RemoveAt(0);
	}

	void MoveIcon(){
		creditIcons[iconCounter].transform.position = startPos;
		iconCounter++;
	}
}