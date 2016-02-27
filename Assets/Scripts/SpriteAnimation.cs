using UnityEngine;
using System.Collections;

public class SpriteAnimation {

	public Sprite[] sprites;
	public int frameDuration;
	public bool loop;
	public SpriteRenderer spriteRenderer;

	private int counter;
	private int frame;
	private bool playing;

	public void Start() {
		playing = true;
		counter = 0;
		frame = 0;
		spriteRenderer.sprite = sprites[0];
	}

	public void Play() {
		if (!playing) return;
		if (++counter > frameDuration) {
			counter = 0;
			if (++frame >= sprites.Length) {
				if (loop) {
					frame = 0;
				} else {
					playing = false;
					return;
				}
			}
			spriteRenderer.sprite = sprites[frame];
		}
	}
}
