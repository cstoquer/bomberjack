using UnityEngine;
using System.Collections;

public class checkSprites : MonoBehaviour {

	public SpriteRenderer indx;
	public SpriteRenderer whiteReference;
	public SpriteRenderer blackReference;
	public SpriteRenderer blueReference;
	public SpriteRenderer redReference;

	private string[] imgs = {
		"dead1",
		"dead2",
		"dead3",
		"dead4",
		"down1",
		"down2",
		"down3",
		"left1",
		"left2",
		"left3",
		"punchDown",
		"punchLeft",
		"punchRight",
		"punchUp",
		"right1",
		"right2",
		"right3",
		"up1",
		"up2",
		"up3"
	};

	private int i = 0;

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")) {
			i++;
			if (i >= imgs.Length) i = 0;

			ChangeSprite(indx,  "indexed", imgs[i]);
			ChangeSprite(whiteReference, "white",   imgs[i]);
			ChangeSprite(blackReference, "black",   imgs[i]);
			ChangeSprite(blueReference,  "blue",    imgs[i]);
			ChangeSprite(redReference,   "red",     imgs[i]);
		}
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	private void ChangeSprite(SpriteRenderer sr, string id, string variation) {
		string path = "Images/men/" + id + "/" + variation;
		Sprite sprite = Resources.Load<Sprite>(path);
		sr.sprite = sprite;
	}
}
