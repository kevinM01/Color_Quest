// using UnityEngine;

// [RequireComponent(typeof(SpriteRenderer))]
// public class PlayerSpriteRenderer : MonoBehaviour
// {
//     private PlayerJump movement;
//     public SpriteRenderer spriteRenderer { get; private set; }
//     public Sprite idle;
//     public Sprite jump;
//     public Sprite run;
//     // public AnimatedSprite run;

//     private void Awake()
//     {
//         movement = GetComponentInParent<PlayerJump>();
//         spriteRenderer = GetComponent<SpriteRenderer>();
//     }

//     private void LateUpdate()
//     {
//         run.enabled = movement.running;

//         if (movement.jumping) {
//             spriteRenderer.sprite = jump;
//         } else if (!movement.running) {
//             spriteRenderer.sprite = idle;
//         }
//     }

//     private void OnEnable()
//     {
//         spriteRenderer.enabled = true;
//     }

//     private void OnDisable()
//     {
//         spriteRenderer.enabled = false;
//         run.enabled = false;
//     }

// }