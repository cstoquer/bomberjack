using UnityEngine;
using System;
using System.Collections.Generic;


public class SpriteAnimator {

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	[Serializable]
	public class AnimationData {
		public string id;
		public int speed;
		public bool loop;
		public string[] sprites;
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	[Serializable]
	public class AnimationsData {
		public AnimationData[] animations;

		public static AnimationsData Load(string id) {
			TextAsset textAsset = Resources.Load("Data/animations/" + id) as TextAsset;
			AnimationsData data = JsonUtility.FromJson<AnimationsData>(textAsset.text);
			return data;
		}
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public class SpriteAnimation {
		public int speed;
		public bool loop;
		public Sprite[] sprites;

		public SpriteAnimation(Sprite[] sprites, AnimationData data) {
			this.sprites = sprites;
			this.loop = data.loop;
			this.speed = data.speed;
		}
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	private static Dictionary<string, AnimationsData> animationsDataList = new Dictionary<string, AnimationsData>();

	private static AnimationsData GetAnimationsData(string animation) {
		if (animationsDataList.ContainsKey(animation)) return animationsDataList[animation];
		AnimationsData animationData = AnimationsData.Load(animation);
		animationsDataList.Add(animation, animationData);
		return animationData;
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	private Dictionary<string, SpriteAnimation> spriteAnimations;
	private SpriteRenderer spriteRenderer;
	private SpriteAnimation currentAnimation;
	public string currentAnimationId;
	private bool playing = false;
	private int counter;
	private int frame;

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public SpriteAnimator(string id, string variation, SpriteRenderer spriteRenderer) {
		this.spriteRenderer = spriteRenderer;
		AnimationData[] animationsData = GetAnimationsData(id).animations;
		spriteAnimations = new Dictionary<string, SpriteAnimation>();
		for (int i = 0; i < animationsData.Length; i++) {
			AnimationData animationData = animationsData[i];
			
			// load sprites
			Sprite[] sprites = new Sprite[animationData.sprites.Length];
			string path = "Images/" + id + "/" + variation + "/";
			for (int j = 0; j < animationData.sprites.Length; j++) {
				string spritename = animationData.sprites[j];
				if (spritename != "") sprites[j] = Resources.Load<Sprite>(path + spritename);
			}

			SpriteAnimation spriteAnimation = new SpriteAnimation(sprites, animationData);
			spriteAnimations.Add(animationData.id, spriteAnimation);
		}
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public void Start(string id, bool forceRestart = false) {
		if (!spriteAnimations.ContainsKey(id)) return;
		if (!forceRestart && currentAnimationId == id) return;
		currentAnimationId = id;
		currentAnimation = spriteAnimations[id];
		spriteRenderer.sprite = currentAnimation.sprites[0];
		playing = true;
		counter = 0;
		frame = 0;
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public int GetDuration() {
		if (currentAnimation == null) return 0;
		return currentAnimation.sprites.Length * currentAnimation.speed;
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public void Play() {
		if (!playing) return;
		if (++counter > currentAnimation.speed) {
			counter = 0;
			if (++frame >= currentAnimation.sprites.Length) {
				if (currentAnimation.loop) {
					frame = 0;
				} else {
					playing = false;
					return;
				}
			}
			spriteRenderer.sprite = currentAnimation.sprites[frame];
		}
	}
}
